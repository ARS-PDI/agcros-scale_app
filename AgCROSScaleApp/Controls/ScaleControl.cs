using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MeasurementEquipment.Types;
using unvell.ReoGrid.Events;
using unvell.ReoGrid;
using System.Windows.Threading;
using System.IO;
using unvell.ReoGrid.CellTypes;

namespace AgCROSScaleApp
{

    public partial class ScaleControl : UserControl
    {
        private ScaleAppViewModel vm;
        private bool hasLoaded = false;
        public ScaleControl()
        {
            InitializeComponent();
            this.cbxSerialPort.DisplayMember = "DisplayName";
            this.cbxSerialPort.ValueMember = "PortName";
            this.loadingBox1.Visible = false;
            this.loadingBox2.Visible = false;
            InitialGridSetup();
        }

        private void InitialGridSetup()
        {
            this.reoGridSheet.Visible = false;
            this.reoGridSheet.Enabled = false;
            this.reoGridSheet.SetSettings(WorkbookSettings.View_ShowHorScroll, false);
            var ws = this.reoGridSheet.CurrentWorksheet;
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;

        }

        private void SetupSpreadtool()
        {
            ResizeGrid();
            var ws = this.reoGridSheet.CurrentWorksheet;
            ws.CellDataChanged += Ws_CellDataChanged;
            ws.SetCols(4);
            ws.ColumnHeaders[0].Text = "Sample_ID";
            ws.ColumnHeaders[1].Text = "Sample_time";
            ws.ColumnHeaders[2].Text = "Sample_measurement";
            ws.ColumnHeaders[3].Text = "Remeasure?";
            ws.ColumnHeaders[3].DefaultCellBody = typeof(CheckBoxCell);
            ws.ColumnHeaders[3].Style.HorizontalAlign = ReoGridHorAlign.Center;
            ws.ColumnHeaders[3].Style.Padding = new PaddingValue(2);
            this.reoGridSheet.Refresh();
            ws.FocusPos = new CellPosition(ws.MaxContentRow == 0 ? 0 : ws.MaxContentRow + 1, 0);
        }

        private void reoGridSheet_Resize(object sender, EventArgs e)
        {
            ResizeGrid();
        }

        private void ResizeGrid()
        {
            var ws = this.reoGridSheet.CurrentWorksheet;
            ws.SetColumnsWidth(0, 3, (ushort)(reoGridSheet.Size.Width * 0.2725));
            ws.SetColumnsWidth(3, 1, (ushort)(reoGridSheet.Size.Width * 0.1502));
        }

        private async void Ws_CellDataChanged(object sender, CellEventArgs e)
        {
            if (e.Cell.Column != 3 && e.Cell.Column != 0)
            {
                return;
            }
            var ws = this.reoGridSheet.CurrentWorksheet;
            if (e.Cell.Column == 0)
            {
                await TakeReading(ws, e.Cell.Row);
            }
            else if (e.Cell.Column == 3)
            {
                if ((ws[e.Cell.Row, 3] as bool?) ?? false)
                {
                    await TakeReading(ws, e.Cell.Row);
                }
            }
            this.reoGridSheet.Focus();
        }

        private async Task TakeReading(Worksheet ws, int row)
        {
            // Figuring something was scanned:
            // trigger read event
            var uiDisp = Dispatcher.CurrentDispatcher;

            this.reoGridSheet.Enabled = false;
            this.loadingBox2.Visible = true;
            await Task.Run(() =>
            {
                try
                {
                    var reading = vm.TakeReading(row, ws[row, 0].ToString());
                    uiDisp.BeginInvoke((Action)(() =>
                    {
                        ws[row, 1] = reading.ReadingTimeStamp.ToString("O");
                        ws[row, 2] = reading.ReadingValue;
                        ws[row, 3] = false;
                        this.vm.SaveFile();
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Issue taking reading: {ex.Message}", "Reading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            });
            this.reoGridSheet.Enabled = true;
            this.loadingBox2.Visible = false;
        }

        public void SaveGridToFile()
        {
            if (!hasLoaded)
                return;// don't write file if data was never loaded.
            var usedRanged = this.reoGridSheet.CurrentWorksheet.UsedRange;
            var exportRange = new RangePosition(0, 0, usedRanged.Rows, usedRanged.Cols - 1);
            this.reoGridSheet.CurrentWorksheet.ExportAsCSV(this.vm.SaveFileName, exportRange);
        }

        public void SetVM(ScaleAppViewModel viewModel)
        {
            this.vm = viewModel;
            UpdateConfiguredOptions();
        }

        internal void UpdateConfiguredOptions()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.vm.DetectSerialPorts();
                this.cbxSerialPort.DataSource = new BindingSource(new BindingList<SerialPortValue>(vm.SerialPorts), null);
                this.txtFileName.Text = vm.SaveFileName;
                if (this.vm.SelectedSerialPort != null)
                {
                    if (this.cbxSerialPort.Items.Contains(this.vm.SelectedSerialPort))
                    {
                        this.cbxSerialPort.SelectedItem = this.cbxSerialPort.Items[this.cbxSerialPort.Items.IndexOf(this.vm.SelectedSerialPort)];
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "Error Setting Up", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.vm.Device?.IsConnected ?? true)
                {
                    this.loadingBox1.Visible = true;
                    await Task.Run(() =>
                    {
                        this.vm.ConnectToDevice((SerialPortValue)this.cbxSerialPort.SelectedItem);

                    });
                    if (this.vm.Device.IsConnected)
                    {
                        this.btnConnect.Text = "Disconnect";
                        // turn off input boxes and file selection
                        this.cbxSerialPort.Enabled = false;
                        this.btnSelectFile.Enabled = false;
                        this.txtFileName.Enabled = false;
                        // turn on grid.
                        this.reoGridSheet.Visible = true;
                        this.reoGridSheet.Enabled = true;
                        // read in file info if it exists.
                        this.reoGridSheet.Focus();
                        ReadGridDataFromFile();
                        SetupSpreadtool();
                        this.reoGridSheet.Refresh();
                    }

                    this.loadingBox1.Visible = false;
                }
                else
                {
                    // disconnect
                    this.vm.Device.Disconnect();
                    if (!this.vm.Device.IsConnected)
                    {
                        this.btnConnect.Text = "Connect";
                        // turn back on checkbox, filename box, and filename button.
                        this.cbxSerialPort.Enabled = true;
                        this.txtFileName.Enabled = true;
                        this.btnSelectFile.Enabled = true;
                        // 
                        this.reoGridSheet.Visible = false;
                        this.reoGridSheet.Enabled = false;
                        // spread cleanup
                        GridCleanup();
                    }
                }
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show($"{ioe.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Encounted issue: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GridCleanup()
        {
            // remove action handler
            var ws = reoGridSheet.CurrentWorksheet;
            ws.CellDataChanged -= Ws_CellDataChanged;
            ws.ClearRangeContent(ws.UsedRange, CellElementFlag.All);
        }

        private void ReadGridDataFromFile()
        {
            hasLoaded = true;
            this.vm.ReadSavedFile();
            var ws = this.reoGridSheet.CurrentWorksheet;
            for (int idx = 0; idx < this.vm.Readings.Count; idx++)
            {
                var record = this.vm.Readings[idx];
                ws[idx, 0] = record.ID;
                ws[idx, 1] = record.ReadingTimeStamp.ToString("O");
                ws[idx, 2] = record.ReadingValue;
            }
            var usedRanged = ws.UsedRange;
            ws[new RangePosition(0, 3, usedRanged.Rows, 1)] = Enumerable.Range(0, usedRanged.Rows).Select(o => new CheckBoxCell()).Cast<object>().ToArray(); ;

        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = $"ScaleAppReadings_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv";
            saveFileDialog.Filter = "CSV (*.csv)|*.csv";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.AddExtension = true; // add default CSV
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.vm.UpdateFileSave(saveFileDialog.FileName);
                // reset the reading value;
                this.reoGridSheet.Reset();
                this.InitialGridSetup();


            }
        }

        private void cbxSerialPort_Click(object sender, EventArgs e)
        {
            UpdateConfiguredOptions();
            this.cbxSerialPort.DroppedDown = true;
        }
    }
}
