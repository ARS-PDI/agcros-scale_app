using AgCROSScaleApp.Models;
using AgCROSScaleApp.Models.Types;
using AgCROSScaleApp.Utilities;
using AgCROSScaleApp.Utilities.FileReaderWriters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AgCROSScaleApp.Dialogs
{
    public partial class OutputCalculator : Form
    {
        private string tareFile;
        private ScaleAppViewModel tareModel = null;
        private string preFile;
        private ScaleAppViewModel preMeasureModel = null;
        private string postFile;
        private ScaleAppViewModel postMeasureModel = null;
        private string fileOut;
        public OutputCalculator()
        {
            InitializeComponent();
            tareFile = "";
            preFile = "";
            postFile = "";
            this.tab2CalcOpts.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnNext.BackColor = Color.Yellow;
            this.btnClose.Visible = false;
            this.btnClose.Enabled = false;
            this.btnViewOutput.Visible = false;
            this.btnViewOutput.Enabled = false;
            this.btnCalc.Enabled = false;
            this.btnCalc.BackColor = Color.Red;
            this.cbCalcType.DataSource =
                Enum.GetValues(typeof(AppTypes))
                .Cast<AppTypes>()
                .Where(o => o != AppTypes.NoCalculation)
                .Select(o => new { Name = o.GetDescription(), Value = o }).ToList();
            this.cbCalcType.DisplayMember = "Name";
        }

        private void UnlockTabs()
        {
            if (preMeasureModel != null && postMeasureModel != null)
            {

                if (preMeasureModel.RepeatMeasures.NumMeasurements == postMeasureModel.RepeatMeasures.NumMeasurements)
                {
                    this.tab2CalcOpts.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnNext.BackColor = Color.Green;
                }
                else
                {
                    this.tab2CalcOpts.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnNext.BackColor = Color.Yellow;
                }
            }
        }

        private void OutputCalculator_Load(object sender, EventArgs e)
        {

        }

        private string OpenFile(string title)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = title;
            ofd.DefaultExt = "csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        private void textTareFile_DoubleClick(object sender, EventArgs e)
        {
            var res = OpenFile("Select the Tare File");
            if (!string.IsNullOrWhiteSpace(res))
            {
                textTareFile.Text = Path.GetFileName(res);
                tareModel = new ScaleAppViewModel();
                FileReadUtility.ReadSavedFile(tareModel, res);
                this.tareFile = res;
            }
        }

        private void textPreFile_DoubleClick(object sender, EventArgs e)
        {
            var res = OpenFile("test");
            if (!string.IsNullOrWhiteSpace(res))
            {
                textPreFile.Text = Path.GetFileName(res);
                preMeasureModel = new ScaleAppViewModel();
                FileReadUtility.ReadSavedFile(preMeasureModel, res);
                this.preFile = res;
                UnlockTabs();
            }
        }

        private void textPostFile_DoubleClick(object sender, EventArgs e)
        {
            var res = OpenFile("test");
            if (!string.IsNullOrWhiteSpace(res))
            {
                textPostFile.Text = Path.GetFileName(res);
                postMeasureModel = new ScaleAppViewModel();
                FileReadUtility.ReadSavedFile(postMeasureModel, res);
                this.postFile = res;
                UnlockTabs();
            }
        }

        private void textFileOut_DoubleClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Title = "Create/Overwrite a ScaleApp Output File...";
            sfd.FileName = $"ScaleApp_Calculation_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv";
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true; // add default CSV
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var fName = sfd.FileName;
                if (!string.IsNullOrWhiteSpace(fName))
                {
                    textFileOut.Text = Path.GetFileName(fName);
                    this.fileOut = fName;
                    CheckCanCalculate();
                }
            }

        }

        private void CheckCanCalculate()
        {
            if (!string.IsNullOrWhiteSpace(textFileOut.Text))
            {
                this.btnCalc.Enabled = true;
                this.btnCalc.BackColor = Color.Green;
                this.btnViewOutput.Enabled = false;
                this.btnViewOutput.Visible = false;
                this.btnClose.Enabled = false;
                this.btnClose.Visible = false;
            }
        }

        private void Calculate()
        {

            var results = new List<CalculationResultModel>();

            var prevaluesRaw = preMeasureModel.Readings;

            var preIds = preMeasureModel.Readings.Select(o => o.ID).Distinct();
            //if (tareModel != null)
            //{
            //    tareIds = tareModel.Readings.Select(o => o.ID).Where(o => preIds.Contains(o));
            //}

            foreach (var id in preIds)
            {
                var calcModel = new CalculationResultModel(id);
                if (tareModel != null)
                {
                    var val = tareModel.Readings.FirstOrDefault(o => o.ID.Equals(id))?.Samples[0] ?? null;
                    if (val != null)
                    {
                        calcModel.TareWt = new Tuple<decimal, string>(val.Value, val.Units);
                    }
                    else
                    {
                        calcModel.TareWt = null;
                    }

                }
                var preWts = preMeasureModel.Readings.FirstOrDefault(o => o.ID.Contains(id));
                var postWts = postMeasureModel.Readings.FirstOrDefault(o => o.ID.Contains(id));
                for (int idx = 0; idx < preWts.Samples.Count; idx++)
                {
                    try
                    {
                        var pre = preWts.Samples[idx].Value;
                        calcModel.PreWt.Add(new Tuple<decimal, string>(pre, preWts.Samples[idx].Units));
                        var post = postWts.Samples[idx]?.Value ?? null;
                        if (post.HasValue)
                        {
                            decimal val = 0.0m;
                            if (((dynamic)cbCalcType.SelectedItem).Value == AppTypes.WetBasisCalc)
                            {
                                val = (pre - ((calcModel.TareWt?.Item1 ?? 0.0m)) - post.Value) / (pre) * 100;
                            }
                            else if (((dynamic)cbCalcType.SelectedItem).Value == AppTypes.DryBasisCalc)
                            {
                                val = (pre - ((calcModel.TareWt?.Item1 ?? 0.0m)) - post.Value) / (post.Value) * 100;
                            }
                            var error = "";
                            if(val < numMinTol.Value)
                            {
                                error += "Calc Value < Min;";
                            }
                            if (val > numMaxTol.Value)
                            {
                                error += "Calc Value > Max;";
                            }
                            calcModel.PostWt.Add(new Tuple<decimal, string>(post.Value, postWts.Samples[idx].Units));
                            calcModel.CalcValue.Add(new Tuple<decimal, string>(val, error));
                        }

                    }
                    catch (Exception)
                    {
                        // if missing values for either, ignore.
                    }
                }
                results.Add(calcModel);
            }


            // todo:
            // get post values
            // todo:
            // for each post-value -> find the tared (or not) pre value.
            // calculate according to selected option.
            // write to new CSV file, include tare(optional), pre, post, and calculated values 
            // some MD pointing to original files?
            // done.
            WriteCalculatedResultsFile.WriteCalcFile(results,
                numMinTol.Value,
                numMaxTol.Value,
                fileOut,
                preFile,
                postFile,
                tareFile);
            OpenWithDefaultProgram(fileOut);
            this.btnClose.Visible = true;
            this.btnClose.Enabled = true;
            this.btnClose.BackColor = Color.Green;
            this.btnViewOutput.Visible = true;
            this.btnViewOutput.Enabled = true;
            this.btnViewOutput.BackColor = Color.Green;
            this.btnCalc.Enabled = false;
            this.btnCalc.Visible = false;
        }

        private static void OpenWithDefaultProgram(string path)
        {
            Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.tabCtrl.SelectedTab = this.tab2CalcOpts;
        }

        private void btnViewOutput_Click(object sender, EventArgs e)
        {
            OpenWithDefaultProgram(fileOut);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }
    }
}
