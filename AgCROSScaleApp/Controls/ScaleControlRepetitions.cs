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
using unvell.ReoGrid.Actions;
using System.Drawing;

namespace AgCROSScaleApp.Controls
{

    public partial class ScaleControlRepetitions : UserControl
    {
        private ScaleAppViewModel vm;
        private static int IDCol = 0;
        private static int MeanCol = 1;
        private static int StdDevCol = 2;
        private static int TimestampStartCol = 3;
        private static int ReadValStartCol = 4;
        private static int UnitStartCol = 5;

        private int remeasureCol;

        public ScaleControlRepetitions()
        {
            InitializeComponent();
            InitialGridSetup();
        }

        private void InitialGridSetup()
        {
            this.reoGridControl.SetSettings(WorkbookSettings.View_ShowHorScroll, true);
            var ws = this.reoGridControl.CurrentWorksheet;
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SetSettings(WorksheetSettings.Edit_Readonly, true);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;
            takingReadingLoadingBox.Visible = false;
        }

        private void SetupSpreadtool()
        {
            var ws = this.reoGridControl.CurrentWorksheet;
            ws.CellDataChanged += Ws_CellDataChanged;
            //can only remeasure using button.
            var numRepeats = this.vm.RepeatMeasures.NumMeasurements.Value;
            ws.SetCols(4 + (numRepeats* 3));
            ws.ColumnHeaders[IDCol].Text = "SampleID";
            ws.ColumnHeaders[MeanCol].Text = "Mean";
            ws.ColumnHeaders[StdDevCol].Text = "StdDev";
            var varName = this.vm.FileSave.VariableName;
            varName = string.IsNullOrEmpty(varName) ? "SampleValue" : varName;
            for (int idx = 0; idx < numRepeats; idx++)
            {
                ws.ColumnHeaders[TimestampStartCol + (idx * 3)].Text = $"SampleTime[{idx + 1}]";
                ws.ColumnHeaders[ReadValStartCol + (idx * 3)].Text = $"{varName}[{idx + 1}]";
                ws.ColumnHeaders[UnitStartCol + (idx * 3)].Text = $"unit[{idx + 1}]";

            }
            remeasureCol = 4 + (numRepeats * 3) - 1;
            ws.ColumnHeaders[remeasureCol].Text = "Remeasure?";
            ws.ColumnHeaders[remeasureCol].DefaultCellBody = typeof(CheckBoxCell);
            ws.ColumnHeaders[remeasureCol].Style.HorizontalAlign = ReoGridHorAlign.Center;
            ws.ColumnHeaders[remeasureCol].Style.Padding = new PaddingValue(2);
            this.reoGridControl.Refresh();
            ws.FocusPos = new CellPosition(ws.MaxContentRow == 0 ? 0 : ws.MaxContentRow + 1, 0);
        }

        private void reoGridControl_Resize(object sender, EventArgs e)
        {
        }

        internal void GridCleanup()
        {
            var ws = reoGridControl.CurrentWorksheet;
            ws.CellDataChanged -= Ws_CellDataChanged;
            ws.ClearRangeContent(ws.UsedRange, CellElementFlag.All);
        }

        private void Ws_CellDataChanged(object sender, CellEventArgs e)
        {
            if (e.Cell.Column != remeasureCol)
            {
                return;
            }
            var ws = this.reoGridControl.CurrentWorksheet;
            if (e.Cell.Column == remeasureCol)
            {
                if ((ws[e.Cell.Row, remeasureCol] as bool?) ?? false)
                {
                    // popup specialized "repeat reading for sample"
                    //await TakeReading(ws, e.Cell.Row);
                    ws[e.Cell.Row, remeasureCol] = false;
                    var msr = vm.Readings.SingleOrDefault(o => o.RowID == e.Cell.Row);
                    if (msr!= null)
                    {
                        var remeasureDlg = new RepetitionRemeasure(this.vm, msr);
                        if (remeasureDlg.ShowDialog() == DialogResult.OK)
                        {
                            // done, the measurement object was updated.
                            UpdateRecords();
                            this.vm.SaveFile();
                        }
                    }

                }
            }
            this.reoGridControl.Focus();
        }

        public void SetVM(ScaleAppViewModel viewModel)
        {
            this.vm = viewModel;
            this.reoGridControl.Focus();
            SetupSpreadtool();
            UpdateRecords();
            this.reoGridControl.Refresh();
        }

        private void btnTakeMeasurements_Click(object sender, EventArgs e)
        {
            // open dialog for specialized read process...

            RepetitionSample dlg = new RepetitionSample(this.vm);
            dlg.Owner = (this.Parent as Form);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // process data;
                UpdateRecords();
                this.vm.SaveFile();
            }
        }

        private void UpdateRecords()
        {
            var numRepeats = this.vm.RepeatMeasures.NumMeasurements;

            var ws = this.reoGridControl.CurrentWorksheet;
            for (int idx = 0; idx < this.vm.Readings.Count; idx++)
            {
                var record = this.vm.Readings[idx];
                ws[idx, IDCol] = record.ID;
                ws[idx, MeanCol] = record.Mean?.ToString("N3");
                ws[idx, StdDevCol] = record.StdDev?.ToString("N3");
                for (int recId = 0; recId < record.Samples.Count; recId++)
                {
                    ws[idx, TimestampStartCol + (recId * 3)] = record.Samples[recId].Timestamp.ToString("O");
                    ws[idx, ReadValStartCol + (recId * 3)] = record.Samples[recId].Value.ToString();
                    ws[idx, UnitStartCol + (recId * 3)] = record.Samples[recId].Units;

                }
                var style = new RangeBorderStyle();
                if (record.StdDev != null)
                {
                    if (record.StdDev> this.vm.RepeatMeasures.StdDevLimit)
                    {
                        style = new RangeBorderStyle()
                        {
                            Color = unvell.ReoGrid.Graphics.SolidColor.Red,
                            Style = BorderLineStyle.Solid
                        };
                    }
                }
                if (record.Samples.Count < this.vm.RepeatMeasures.NumMeasurements)
                {
                    style = new RangeBorderStyle()
                    {
                        Color = unvell.ReoGrid.Graphics.SolidColor.Yellow,
                        Style = BorderLineStyle.BoldSolid
                    };
                }
                reoGridControl.DoAction(ws,
                    new SetRangeBorderAction(new RangePosition(record.RowID, 0, 1, ws.UsedRange.Cols),
                    BorderPositions.All,
                    style
                    ));
            }
            var usedRanged = ws.UsedRange;
            

            ws[new RangePosition(0, remeasureCol, usedRanged.Rows, 1)] = Enumerable.Range(0, usedRanged.Rows).Select(o => new CheckBoxCell()).Cast<object>().ToArray(); ;
        }
    }
}


//private async Task TakeReading(Worksheet ws, int row)
//{
//    // Figuring something was scanned:
//    // trigger read event
//    var uiDisp = Dispatcher.CurrentDispatcher;

//    this.reoGridControl.Enabled = false;
//    this.takingReadingLoadingBox.Visible = true;
//    await Task.Run(() =>
//    {
//        try
//        {
//            var reading = vm.TakeReading(row, ws[row, 0].ToString());
//            uiDisp.BeginInvoke((Action)(() =>
//            {
//                // always 1
//                while (vm.PromptOnZero && Math.Abs(reading.Samples[0].Value) < ScaleAppViewModel.ZeroEpsilon)
//                {
//                    var res = MessageBox.Show($"Scale read ~0.0...{Environment.NewLine}Do you want to retry the reading?", 
//                        "Reading Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
//                    if (res == DialogResult.No)
//                    {
//                        break;
//                    }
//                    reading = vm.TakeReading(row, ws[row, 0].ToString());
//                }
//                // ok they got a good reading OR don't want to retry: save results.
//                this.vm.SaveReading(reading);
//                ws[row, 1] = reading.Samples[0].Timestamp.ToString("O");
//                ws[row, 2] = reading.Samples[0].Value;
//                ws[row, 3] = false;
//                this.vm.SaveFile();
//            }));
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Issue taking reading: {ex.Message}", "Reading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }


//    });
//    this.reoGridControl.Enabled = true;
//    this.takingReadingLoadingBox.Visible = false;
//}