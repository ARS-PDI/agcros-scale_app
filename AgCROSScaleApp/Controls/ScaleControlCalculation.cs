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
using AgCROSScaleApp.Models.Types;
using unvell.ReoGrid.Actions;

namespace AgCROSScaleApp.Controls
{

    public partial class ScaleControlCalculation : UserControl
    {
        private ScaleAppViewModel vm;

        private static string tareSheetName = "Tare Weights (Optional)";
        private static string preprocessSheetName = "Pre-process Weights";
        private static string postprocessSheetName = "Post-process Weights";

        private static int IDCol = 0;
        private static int TimestampCol = 1;
        private static int ReadValCol = 2;
        private static int UnitCol = 3;
        private static int RemeasureCol = 4;

        public ScaleControlCalculation()
        {
            InitializeComponent();
            takingReadingLoadingBox.Visible = false;

        }

        private void SetupSpreadtool()
        {
            this.reoGridControl.SetSettings(WorkbookSettings.View_ShowHorScroll, false);
            this.reoGridControl.Worksheets[0].Name = tareSheetName;
            reoGridControl.AddWorksheet(this.reoGridControl.CreateWorksheet(preprocessSheetName));
            reoGridControl.AddWorksheet(this.reoGridControl.CreateWorksheet(postprocessSheetName));
            SetupSheet(this.reoGridControl.GetWorksheetByName(tareSheetName));
            SetupSheet(this.reoGridControl.GetWorksheetByName(preprocessSheetName));
            SetupCalcSheet(this.reoGridControl.GetWorksheetByName(postprocessSheetName));
        }

        private void SetupCalcSheet(Worksheet ws)
        {
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;
            ws.SetCols(6);
            ws.ColumnHeaders[IDCol].Text = "SampleID";
            ws.ColumnHeaders[TimestampCol].Text = "Timestamp";
            ws.ColumnHeaders[ReadValCol].Text = this.vm.FileSave.VariableName;
            ws.ColumnHeaders[UnitCol].Text = "Units";
            ws.ColumnHeaders[RemeasureCol].Text = "Calculated Value";
            ws.ColumnHeaders[RemeasureCol + 1].Text = "Remeasure?";
            ws.ColumnHeaders[RemeasureCol + 1].DefaultCellBody = typeof(CheckBoxCell);
            ws.ColumnHeaders[RemeasureCol + 1].Style.HorizontalAlign = ReoGridHorAlign.Center;
            ws.ColumnHeaders[RemeasureCol + 1].Style.Padding = new PaddingValue(2);
            this.reoGridControl.Refresh();
            ws.FocusPos = new CellPosition(ws.MaxContentRow == 0 ? 0 : ws.MaxContentRow + 1, 0);
            ResizeGrid();
        }

        private void SetupSheet(Worksheet ws)
        {
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;
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
            ResizeGrid();
        }

        private void reoGridControl_Resize(object sender, EventArgs e)
        {
            ResizeGrid();
        }

        private void ResizeGrid()
        {
            if (reoGridControl.GetWorksheetByName(tareSheetName) != null)
                ResizeWorksheet(reoGridControl.GetWorksheetByName(tareSheetName));
            if (reoGridControl.GetWorksheetByName(preprocessSheetName) != null)
                ResizeWorksheet(reoGridControl.GetWorksheetByName(preprocessSheetName));
            if (reoGridControl.GetWorksheetByName(postprocessSheetName) != null)
                ResizeWorksheet(reoGridControl.GetWorksheetByName(postprocessSheetName));
        }
        private void ResizeWorksheet(Worksheet ws)
        {
            ws.SetColumnsWidth(0, RemeasureCol - 1, (ushort)(reoGridControl.Size.Width * 0.2043));
            ws.SetColumnsWidth(RemeasureCol, 1, (ushort)(reoGridControl.Size.Width * 0.1502));
        }

        internal void GridCleanup()
        {
            RemoveHandlers(this.reoGridControl.GetWorksheetByName(tareSheetName));
            RemoveHandlers(this.reoGridControl.GetWorksheetByName(preprocessSheetName));
            RemoveHandlers(this.reoGridControl.GetWorksheetByName(postprocessSheetName));
            this.reoGridControl.Reset();
        }

        private void RemoveHandlers(Worksheet ws)
        {
            ws.CellDataChanged -= Ws_CellDataChanged;
            ws.ClearRangeContent(ws.UsedRange, CellElementFlag.All);
        }

        private async void Ws_CellDataChanged(object sender, CellEventArgs e)
        {
            if (e.Cell.Column != RemeasureCol && e.Cell.Column != IDCol && e.Cell.Column != (RemeasureCol + 1))
            {
                return;
            }

            var ws = e.Cell.Worksheet;

            if (e.Cell.Column == IDCol)
            {
                var used = !string.IsNullOrWhiteSpace(ws[ws.UsedRange.Row, 0]?.ToString());
                if (string.IsNullOrWhiteSpace(ws[e.Cell.Position]?.ToString()))
                    return;
                await TakeReading(ws, e.Cell.Row);
            }
            else if (e.Cell.Column == RemeasureCol)
            {
                if ((ws[e.Cell.Row, RemeasureCol] as bool?) ?? false)
                {
                    await TakeReading(ws, e.Cell.Row);
                }
            }
            else if (e.Cell.Column == (RemeasureCol + 1))
            {
                if ((ws[e.Cell.Row, RemeasureCol + 1] as bool?) ?? false)
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
            var type = GetSampleType(ws);
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
                        if (!vm.CheckSaveNewCalcReading(reading.ID, type))
                        {
                            var res = MessageBox.Show($"This value has already been measured for this type {type}.\nDo You want to replace the measurement?",
                                "Reading Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (res == DialogResult.Yes)
                            {

                                ProcessRecordUpdate(ws, row, type, reading);
                            }
                            else
                            {
                                ZeroOutCurrentRow(ws, row, type);
                            }
                        }
                        else
                        {
                            ProcessRecordUpdate(ws, row, type, reading);
                        }
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

        private void ProcessRecordUpdate(Worksheet ws, int row, SampleTypes type, ScaleReadingValue reading)
        {
            var calcRecord = vm.SaveCalcReading(reading.ID, reading.Samples[0], type);

            if (!vm.CheckCalcReadingId(reading.ID, reading.RowID, type))
            {
                // if ID != the same -> clear out this row, update the actual row data....
                ZeroOutCurrentRow(ws, row, type);
            }
            var actID = vm.FindCalcRowIdForType(reading.ID, type);
            ws[actID, TimestampCol] = reading.Samples[0].Timestamp.ToString("O");
            ws[actID, ReadValCol] = reading.Samples[0].Value.ToString();
            ws[actID, UnitCol] = reading.Samples[0].Units;
            if (type == SampleTypes.PostSample)
            {
                ws[actID, RemeasureCol] = calcRecord.CalcValue?.ToString("F" + DecimalUtils.GetNumPlacesAfterPoint(reading.Samples[0].Value));
                ws[actID, RemeasureCol + 1] = false;
            }
            else
            {
                ws[actID, RemeasureCol] = false;
            }
            CheckIfSWCBad(calcRecord);
            SetFocusPosition(ws);
            this.vm.SaveFile();
        }

        private static void ZeroOutCurrentRow(Worksheet ws, int row, SampleTypes type)
        {
            ws[row, IDCol] = null;
            ws[row, TimestampCol] = null;
            ws[row, ReadValCol] = null;
            ws[row, UnitCol] = null;
            ws[row, RemeasureCol] = null;
            if (type == SampleTypes.PostSample)
            {
                ws[row, RemeasureCol + 1] = null;

            }
        }

        private SampleTypes GetSampleType(Worksheet ws)
        {
            if (ws.Name.Equals(tareSheetName))
                return SampleTypes.TareSample;
            if (ws.Name.Equals(preprocessSheetName))
                return SampleTypes.PreSample;
            if (ws.Name.Equals(postprocessSheetName))
                return SampleTypes.PostSample;
            return SampleTypes.SingleSample;
        }

        public void SetVM(ScaleAppViewModel viewModel)
        {
            this.vm = viewModel;
            this.reoGridControl.Focus();
            SetupSpreadtool();
            LoadInitialGridData();
            SetupLabels();
            SetupListeners();
            this.reoGridControl.Refresh();
        }

        private void SetupLabels()
        {
            this.minTol.Text = $"Min Tol: {vm.CalcModel.MinTolerance}";
            this.maxTol.Text = $"Max Tol: {vm.CalcModel.MaxTolerance}";

        }

        private void SetupListeners()
        {
            reoGridControl.GetWorksheetByName(tareSheetName).CellDataChanged += Ws_CellDataChanged;
            reoGridControl.GetWorksheetByName(preprocessSheetName).CellDataChanged += Ws_CellDataChanged;
            reoGridControl.GetWorksheetByName(postprocessSheetName).CellDataChanged += Ws_CellDataChanged;
        }

        private void LoadInitialGridData()
        {
            var tareWS = this.reoGridControl.GetWorksheetByName(tareSheetName);
            var preWS = this.reoGridControl.GetWorksheetByName(preprocessSheetName);
            var postWS = this.reoGridControl.GetWorksheetByName(postprocessSheetName);

            foreach (var calcRecord in vm.CalcModel.GridResults)
            {
                calcRecord.Value.TareWtRow = LoadValue(tareWS, calcRecord.Key, calcRecord.Value.TareWt);
                calcRecord.Value.PreWtRow = LoadValue(preWS, calcRecord.Key, calcRecord.Value.PreWt);
                calcRecord.Value.PostWtRow = LoadValue(postWS, calcRecord.Key, calcRecord.Value.PostWt);
                if (calcRecord.Value.PostWtRow != -1)
                {
                    postWS[calcRecord.Value.PostWtRow, RemeasureCol] =
                        calcRecord.Value.CalcValue?.ToString("F" + DecimalUtils.GetNumPlacesAfterPoint(calcRecord.Value.PostWt.Value));
                    CheckIfSWCBad(calcRecord.Value);
                }
            }
            tareWS[new RangePosition(0, RemeasureCol, tareWS.UsedRange.Rows, 1)] =
                Enumerable.Range(0, tareWS.UsedRange.Rows).Select(o => new CheckBoxCell()).Cast<object>().ToArray();
            preWS[new RangePosition(0, RemeasureCol, preWS.UsedRange.Rows, 1)] =
    Enumerable.Range(0, preWS.UsedRange.Rows).Select(o => new CheckBoxCell()).Cast<object>().ToArray();
            postWS[new RangePosition(0, RemeasureCol + 2, postWS.UsedRange.Rows, 1)] =
    Enumerable.Range(0, postWS.UsedRange.Rows).Select(o => new CheckBoxCell()).Cast<object>().ToArray();

            SetFocusPosition(tareWS);
            SetFocusPosition(preWS);
            SetFocusPosition(postWS);

        }

        private static void SetFocusPosition(Worksheet ws)
        {
            if (!string.IsNullOrWhiteSpace(ws[ws.UsedRange.Rows - 1, 0]?.ToString()))
            {
                ws.FocusPos = new CellPosition(ws.UsedRange.Rows, 0);
            }
            else
            {
                ws.FocusPos = new CellPosition(ws.UsedRange.Rows - 1, 0);
            }

        }

        private int LoadValue(Worksheet ws, string id, TimestampedSample sample)
        {
            if (sample == null)
                return -1;
            var row = ws.UsedRange.Rows - 1;
            var used = !string.IsNullOrWhiteSpace(ws[row, 0]?.ToString());
            if (used)
            {
                row++;
            }

            ws[row, IDCol] = id;
            ws[row, TimestampCol] = sample.Timestamp.ToString("O");
            ws[row, ReadValCol] = sample.Value.ToString();
            ws[row, UnitCol] = sample.Units;
            return row;
        }


        private void CheckIfSWCBad(GridCalcModel record)
        {
            var ws = reoGridControl.GetWorksheetByName(postprocessSheetName);
            unvell.ReoGrid.Graphics.SolidColor color = unvell.ReoGrid.Graphics.SolidColor.Transparent;
            if (record.PostWtRow == -1) return;
            if (
                record.CalcValue < (vm.CalcModel?.MinTolerance ?? 0.0m)
                ||
                record.CalcValue > (vm.CalcModel?.MaxTolerance ?? 100.0m)
                ||
                record.PreWtRow == -1)
            {
                color = unvell.ReoGrid.Graphics.SolidColor.Red;
            }
            ws.Cells[record.PostWtRow, RemeasureCol].Style.BackColor = color;
        }


    }
}
