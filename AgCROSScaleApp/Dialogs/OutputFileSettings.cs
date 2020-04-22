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

namespace AgCROSScaleApp.Dialogs
{
    public partial class OutputFileSettings : Form
    {
        private FileSaveConfigurationModel model;
        public OutputFileSettings(FileSaveConfigurationModel model)
        {
            InitializeComponent();
            this.model = model;
            this.textFileName.Text = model.FileName;
            if (!model.VariableName.Equals("Reading"))
            {
                this.textVariableName.Text = model.VariableName;
            }
            this.textMetadata.Text = string.Join(Environment.NewLine, model.MetaData.Select(o => $"{o.Key}:{o.Value}"));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            model.FileName = textFileName.Text;
            if (string.IsNullOrEmpty(textVariableName.Text))
            {
                model.VariableName = "Reading";
            }
            else
            {
                model.VariableName = textVariableName.Text;
            }
            if (!string.IsNullOrEmpty(textMetadata.Text))
            {
                model.MetaData = textMetadata.Text.
                    Split(Environment.NewLine.ToCharArray()).Select(o =>
                    {
                        var split = o.Split(':');
                        return new Models.KeyValuePair<string, string>() { Key = split[0], Value = split[1] };
                    }).ToList();
            }
            else
            {
                model.MetaData = new List<Models.KeyValuePair<string, string>>();
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
                textFileName.Text = saveFileDialog.FileName;
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
                textFileName.Text = openFileDialog.FileName;
                // have the file dialog attempt to read in metadata:
                var metadata = this.model.OpenFileAndExtractMetadata(textFileName.Text);
                // update metadata

                if (!metadata.Item2.Equals("Reading"))
                {
                    this.textVariableName.Text = metadata.Item2;
                }
                else
                {
                    this.textVariableName.Text = "";
                }
                if (metadata.Item1.Count > 0)
                {
                    this.textMetadata.Text = string.Join(Environment.NewLine, metadata.Item1.Select(o => $"{o.Key}:{o.Value}"));
                }
                else
                {
                    this.textMetadata.Text = "";
                }
            }
        }
    }
}
