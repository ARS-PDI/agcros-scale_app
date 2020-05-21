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
    public partial class RepetitionRemeasure : Form
    {
        private ScaleAppViewModel vm;
        private ScaleReadingValue tempReading;
        private ScaleReadingValue measurement;
        public RepetitionRemeasure(ScaleAppViewModel vm, ScaleReadingValue measurement)
        {
            InitializeComponent();
            this.vm = vm;
            this.measurement = measurement;
            this.tempReading = new ScaleReadingValue()
            {
                ID = measurement.ID,
                TempID = 0,
                Samples = new List<TimestampedSample>()
            };
            ConfigureGrid();
            loadingBoxControl1.Visible = false;
            txtScanField.Focus();
            UpdateSpreadsheet();
        }

        private void ConfigureGrid()
        {
            var numRepeats = this.vm.RepeatMeasures.NumMeasurements;

            this.reoGridControl1.SetSettings(WorkbookSettings.View_ShowHorScroll, true);
            var ws = this.reoGridControl1.CurrentWorksheet;
            ws.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            ws.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            ws.SetSettings(WorksheetSettings.Edit_Readonly, true);
            ws.SetRows(1);
            ws.SelectionForwardDirection = SelectionForwardDirection.Down;
            ws.SetCols(1 + numRepeats.Value);
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
                txtScanField.Focus();
                loadingBoxControl1.Visible = false;
            }
        }

        private void PerformReading()
        {
            var id = this.txtScanField.Text;
            if (this.measurement.ID.Equals(id))
            {
                TakeReading(id);
                UpdateSpreadsheet();
            }
            else
            {
                // todo: figure out if we should ignore or prompt message?
            }

        }

        private void UpdateSpreadsheet()
        {
            var ws = this.reoGridControl1.CurrentWorksheet;
            ws[tempReading.TempID, 0] = tempReading.ID;
            for (int idx = 0; idx < tempReading.Samples.Count; idx++)
            {
                ws[tempReading.TempID, 1 + idx] = tempReading.Samples[idx].Value.ToString("N3");
            }
            this.reoGridControl1.Refresh();
        }

        private void TakeReading(string id)
        {
            try
            {
                var time = DateTime.Now;
                var readValue = this.vm.ReadWeight();
                if (tempReading.Samples.Count < vm.RepeatMeasures.NumMeasurements)
                {
                    tempReading.Samples.Add(new TimestampedSample()
                    {
                        Timestamp = time,
                        Value = readValue.ReadValue,
                        Units = readValue.ReadValueUnits
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error taking reading...{ex.Message}");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var samples = tempReading.Samples;
            if (samples.Count == vm.RepeatMeasures.NumMeasurements)
            {
                // Override
                var val = samples.Select(o => o.Value);
                var mean = val.Average();
                this.measurement.Mean = mean;
                var sum = val.Sum(o => (o - mean) * (o - mean));
                this.measurement.StdDev = (decimal)Math.Sqrt((double) sum / val.Count());
                this.measurement.Samples = samples;
            }
            else
            {
                var cont = MessageBox.Show("Unable to save, not enough readings taken.\nDo you want to complete the remeasurement?", "Error Saving Record", MessageBoxButtons.YesNo);
                if (cont == DialogResult.Yes)
                {
                    return; // Don't save unless remeasure is a complete measurement
                }
                //otherwise: close dialog without saving.
            }
            this.vm.SaveReading(this.measurement);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
