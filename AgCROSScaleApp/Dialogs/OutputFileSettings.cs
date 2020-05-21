using AgCROSScaleApp.Models;
using AgCROSScaleApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgCROSScaleApp.Dialogs
{
    public partial class OutputFileSettings : Form
    {
        public ScaleAppViewModel model;
        public OutputFileSettings(ScaleAppViewModel model)
        {
            InitializeComponent();
            this.model = new ScaleAppViewModel()
            {
                FileSave = model.FileSave,
                RepeatMeasures = model.RepeatMeasures,
                Readings = model.Readings,
                ScaleInfo = model.ScaleInfo
            };
            SetFieldValues(model.FileSave?.FileName ?? "");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // updates this for saving later.
            if (!string.IsNullOrEmpty(textVariableName.Text))
            {
                model.FileSave.VariableName = textVariableName.Text;
            }
            if (!string.IsNullOrEmpty(textMetadata.Text))
            {
                model.FileSave.MetaData = textMetadata.Text.
                    Split(Environment.NewLine.ToCharArray()).Select(o =>
                    {
                        var split = o.Split(':');
                        return new Models.KeyValuePair<string, string>() { Key = split[0], Value = split[1] };
                    }).ToList();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCreateNewFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Create/Overwrite a ScaleApp Output File...";
            saveFileDialog.FileName = $"ScaleAppReadings_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv";
            saveFileDialog.Filter = "CSV (*.csv)|*.csv";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.AddExtension = true; // add default CSV
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                model.Readings = new List<ScaleReadingValue>();
                model.RepeatMeasures = new RepeatedMeasurementModel();
                model.ScaleInfo = new ScaleInfoModel();
                model.FileSave = new FileConfigurationModel()
                {
                    FileName = saveFileDialog.FileName
                };
                SetFieldValues(saveFileDialog.FileName);
            }
        }


        private void SetupApplication(string fileName)
        {
            FileReadUtility.ReadSavedFile(this.model, fileName);
            SetFieldValues(fileName);
        }

        private void SetFieldValues(string fileName)
        {
            this.textFileName.Text = Path.GetFileName(fileName);
            if (!AgCROSConstants.FileCfgConstants.DefaultVarName.Equals(model.FileSave?.VariableName ?? "") && model.FileSave != null)
            {
                this.textVariableName.Text = model.FileSave.VariableName;
            }
            else
            {
                this.textVariableName.Text = "";
            }

            if (model.FileSave?.MetaData?.Count > 0)
            {
                this.textMetadata.Text = string.Join(Environment.NewLine,
                    model.FileSave.MetaData.Select(o => $"{o.Key}:{o.Value}"));
            }
            else
            {
                this.textMetadata.Text = "";
            }
        }

        private void buttonOpenExistingFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open an existing ScaleApp Output File...";
            openFileDialog.Filter = "CSV (*.csv)|*.csv";
            openFileDialog.CheckFileExists = true;
            openFileDialog.DefaultExt = "csv";
            openFileDialog.AddExtension = true; // add default CSV
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetupApplication(openFileDialog.FileName);
                // update metadata
            }
        }
    }
}
