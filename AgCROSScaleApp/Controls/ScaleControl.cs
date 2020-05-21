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
using AgCROSScaleApp.Utilities;

namespace AgCROSScaleApp.Controls
{

    public partial class ScaleControl : UserControl
    {
        private ScaleAppViewModel vm;

        private static int IDCol = 0;
        private static int TimestampCol = 1;
        private static int ReadValCol = 2;
        private static int UnitCol = 3;
        private static int RemeasureCol = 4;

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
            ws.SetCols(5);
            ws.ColumnHeaders[IDCol].Text = "SampleID";
            ws.ColumnHeaders[TimestampCol].Text = "Timestamp";
            ws.ColumnHeaders[ReadValCol].Text = this.vm.FileSave.VariableName;
            ws.ColumnHeaders[UnitCol].Text = "Units";
            ws.ColumnHeaders[RemeasureCol].Text = "Remeasure?";
            ws.ColumnHeaders[RemeasureCol].DefaultCellBody = typeof(CheckBoxCell);
            ws.ColumnHeaders[RemeasureCol].Style.HorizontalAlign = ReoGridHorAlign.Center;
            ws.ColumnHeaders[RemeasureCol].Style.Padding = new PaddingValue(2);
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
            ws.SetColumnsWidth(0, RemeasureCol-1, (ushort)(reoGridControl.Size.Width * 0.2043));
            ws.SetColumnsWidth(RemeasureCol, 1, (ushort)(reoGridControl.Size.Width * 0.1502));
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
            if (e.Cell.Column != RemeasureCol && e.Cell.Column != IDCol)
            {
                return;
            }
            var ws = this.reoGridControl.CurrentWorksheet;
            if (e.Cell.Column == IDCol)
            {
                await TakeReading(ws, e.Cell.Row);
            }
            else if (e.Cell.Column == RemeasureCol)
            {
                if ((ws[e.Cell.Row, RemeasureCol] as bool?) ?? false)
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
                        // always 1
                        while (vm.PromptOnZero && Math.Abs(reading.Samples[0].Value) < AgCROSConstants.ZeroEpsilon)
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
                        ws[row, TimestampCol] = reading.Samples[0].Timestamp.ToString("O");
                        ws[row, ReadValCol] = reading.Samples[0].Value.ToString();
                        ws[row, UnitCol] = reading.Samples[0].Units;
                        ws[row, RemeasureCol] = false;
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
                ws[idx, IDCol] = record.ID;
                ws[idx, TimestampCol] = record.Samples[0].Timestamp.ToString("O");
                ws[idx, ReadValCol] = record.Samples[0].Value.ToString();
                ws[idx, UnitCol] = record.Samples[0].Units;

            }
            var usedRanged = ws.UsedRange;
            ws[new RangePosition(0, RemeasureCol, usedRanged.Rows, 1)] = Enumerable.Range(0, usedRanged.Rows).Select(o => new CheckBoxCell()).Cast<object>().ToArray(); ;
        }


    }
}
