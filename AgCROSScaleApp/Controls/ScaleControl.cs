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
using AgCROSScaleApp.Models;
using AgCROSScaleApp.Dialogs;

namespace AgCROSScaleApp.Controls
{

    public partial class ScaleControl : UserControl
    {
        private ScaleAppViewModel vm;


        public ScaleControl()
        {
            InitializeComponent();
            InitialGridSetup();
        }

        private void InitialGridSetup()
        {
            this.reoGridControl.SetSettings(WorkbookSettings.View_ShowHorScroll, false);
            var ws = this.reoGridControl.CurrentWorksheet;
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;
            takingReadingLoadingBox.Visible = false;
        }

        private void SetupSpreadtool()
        {
            ResizeGrid();
            var ws = this.reoGridControl.CurrentWorksheet;
            ws.CellDataChanged += Ws_CellDataChanged;
            ws.SetCols(4);
            ws.ColumnHeaders[0].Text = "Sample_ID";
            ws.ColumnHeaders[1].Text = "Sample_time";
            ws.ColumnHeaders[2].Text = "Sample_measurement";
            ws.ColumnHeaders[3].Text = "Remeasure?";
            ws.ColumnHeaders[3].DefaultCellBody = typeof(CheckBoxCell);
            ws.ColumnHeaders[3].Style.HorizontalAlign = ReoGridHorAlign.Center;
            ws.ColumnHeaders[3].Style.Padding = new PaddingValue(2);
            this.reoGridControl.Refresh();
            ws.FocusPos = new CellPosition(ws.MaxContentRow == 0 ? 0 : ws.MaxContentRow + 1, 0);
        }

        private void reoGridControl_Resize(object sender, EventArgs e)
        {
            ResizeGrid();
        }

        private void ResizeGrid()
        {
            var ws = this.reoGridControl.CurrentWorksheet;
            ws.SetColumnsWidth(0, 3, (ushort)(reoGridControl.Size.Width * 0.2725));
            ws.SetColumnsWidth(3, 1, (ushort)(reoGridControl.Size.Width * 0.1502));
        }

        internal void GridCleanup()
        {
            // remove action handler
            var ws = reoGridControl.CurrentWorksheet;
            ws.CellDataChanged -= Ws_CellDataChanged;
            ws.ClearRangeContent(ws.UsedRange, CellElementFlag.All);
        }

        private async void Ws_CellDataChanged(object sender, CellEventArgs e)
        {
            if (e.Cell.Column != 3 && e.Cell.Column != 0)
            {
                return;
            }
            var ws = this.reoGridControl.CurrentWorksheet;
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
            this.reoGridControl.Focus();
        }

        private async Task TakeReading(Worksheet ws, int row)
        {
            // Figuring something was scanned:
            // trigger read event
            var uiDisp = Dispatcher.CurrentDispatcher;

            this.reoGridControl.Enabled = false;
            this.takingReadingLoadingBox.Visible = true;
            await Task.Run(() =>
            {
                try
                {
                    var reading = vm.TakeReading(row, ws[row, 0].ToString());
                    uiDisp.BeginInvoke((Action)(() =>
                    {
                        while (vm.PromptOnZero && Math.Abs(reading.ReadingValue) < ScaleAppViewModel.ZeroEpsilon)
                        {
                            var res = MessageBox.Show($"Scale read ~0.0...{Environment.NewLine}Do you want to retry the reading?", 
                                "Reading Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (res == DialogResult.No)
                            {
                                break;
                            }
                            reading = vm.TakeReading(row, ws[row, 0].ToString());
                        }
                        // ok they got a good reading OR don't want to retry: save results.
                        this.vm.SaveReading(reading);
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
            this.reoGridControl.Enabled = true;
            this.takingReadingLoadingBox.Visible = false;
        }

        public void SetVM(ScaleAppViewModel viewModel)
        {
            this.vm = viewModel;
            this.reoGridControl.Focus();
            LoadInitialGridData();
            SetupSpreadtool();
            this.reoGridControl.Refresh();
        }


        private void LoadInitialGridData()
        {
            var ws = this.reoGridControl.CurrentWorksheet;
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


    }
}
