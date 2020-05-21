using AgCROSScaleApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace AgCROSScaleApp.Dialogs
{
    public partial class RepetitionSample : Form
    {
        private ScaleAppViewModel vm;
        public List<ScaleReadingValue> readings;
        public RepetitionSample(ScaleAppViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            ConfigureGrid();
            loadingBoxControl1.Visible = false;
            txtScanField.Focus();
            SetupPartials();
        }

        private void SetupPartials()
        {
            readings = new List<ScaleReadingValue>();
            var partials = vm.Readings.Where(o => o.Samples.Count < vm.RepeatMeasures.NumMeasurements);
            foreach (var partial in partials)
            {
                partial.TempID = readings.Count();
                readings.Add(partial);
                UpdateSpreadsheet(partial);
            }
            this.reoGridControl1.Refresh();
        }

        private void ConfigureGrid()
        {
            var numRepeats = this.vm.RepeatMeasures.NumMeasurements.Value;

            this.reoGridControl1.SetSettings(WorkbookSettings.View_ShowHorScroll, true);
            var ws = this.reoGridControl1.CurrentWorksheet;
            ws.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SetSettings(WorksheetSettings.Edit_Readonly, true);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;
            ws.SetCols(1 + numRepeats);
            ws.ColumnHeaders[0].Text = "Sample_ID";
            var varName = this.vm.FileSave.VariableName;
            varName = string.IsNullOrEmpty(varName) ? "SampleValue" : varName;
            for (int idx = 0; idx < numRepeats; idx++)
            {
                ws.ColumnHeaders[1 + idx].Text = $"{varName}[{idx + 1}]";
            }
            this.reoGridControl1.Refresh();
            ws.FocusPos = new CellPosition(ws.MaxContentRow == 0 ? 0 : ws.MaxContentRow + 1, 0);
        }

        private void RepetitionSample_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadingBoxControl1.Visible = true;
                // take reading
                PerformReading();
                this.txtScanField.Clear();
                loadingBoxControl1.Visible = false;
                this.reoGridControl1.Refresh();
                txtScanField.Focus();
            }
        }

        private void PerformReading()
        {
            var id = this.txtScanField.Text;
            var reading = TakeReading(id);
            UpdateSpreadsheet(reading);
        }

        private void UpdateSpreadsheet(ScaleReadingValue reading)
        {
            var ws = this.reoGridControl1.CurrentWorksheet;
            ws[reading.TempID, 0] = reading.ID;
            for (int idx = 0; idx < reading.Samples.Count; idx++)
            {
                ws[reading.TempID, 1 + idx] = reading.Samples[idx].Value.ToString();
            }
            this.reoGridControl1.Refresh();
        }

        private ScaleReadingValue TakeReading(string id)
        {
            try
            {
                var time = DateTime.Now;
                var readValue = this.vm.ReadWeight();
                var reading = readings.Find(o => o.ID.Contains(id));
                if (reading == null)
                {
                    reading = new ScaleReadingValue()
                    {
                        ID = id,
                        RowID = -1,
                        TempID = readings.Count,
                        Samples = new List<TimestampedSample>()
                    };
                    readings.Add(reading);
                }
                if (reading.Samples.Count < vm.RepeatMeasures.NumMeasurements)
                {
                    reading.Samples.Add(new TimestampedSample()
                    {
                        Timestamp = time,
                        Value = readValue.ReadValue,
                        Units = readValue.ReadValueUnits
                    });
                }
                return reading;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error taking reading...");
            }
            // take a reading.
            return null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            foreach (var rec in readings)
            {
                if (rec.Samples.Count == vm.RepeatMeasures.NumMeasurements)
                {
                    var val = rec.Samples.Select(o => o.Value);
                    var mean = val.Average();
                    rec.Mean = mean;

                    var sum = val.Sum(o => (o - mean) * (o - mean));
                    rec.StdDev = (decimal)Math.Sqrt((double)sum / val.Count());
                }
                rec.TempID = -1;
                vm.SaveReading(rec);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
