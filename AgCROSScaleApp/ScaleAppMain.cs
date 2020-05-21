using AgCROSScaleApp.Dialogs;
using AgCROSScaleApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid.IO.OpenXML.Schema;

namespace AgCROSScaleApp
{
    public partial class ScaleAppMain : Form
    {

        private ScaleAppViewModel model;

        private ManualResetEvent arEvent = new ManualResetEvent(false);
        //private Task createBackupTask;

        public ScaleAppMain()
        {
            model = new ScaleAppViewModel();
            InitializeComponent();
            this.selectFileControl.SetViewModel(model);
            this.connectionControl.SetViewModel(model);
            this.selectFileControl.SelectFileButtonClicked += SelectFileButtonClicked;
            this.connectionControl.ConnectButtonClicked += ConnectButtonClicked;
            //this.scaleControl1.SetVM(model);
            this.Text = $"{this.Text}-v{Application.ProductVersion}";
            ConfigureControls();
        }

        private void CreateBackupsTask()
        {
            while (!arEvent.WaitOne(TimeSpan.FromMinutes(30)))
            {
                this.model.CreateBackup();
            }
        }

        private void ConnectButtonClicked(object sender, EventArgs e)
        {
            var connected = model.DeviceIsConnected();
            this.selectFileControl.Enabled = !connected;
            this.btnRepeatedMeasures.Enabled = !connected;
            this.btnScaleInfo.Enabled = !connected;
            this.applicationSettingsToolStripMenuItem.Enabled = !connected;
            this.connectionSettingsToolStripMenuItem.Enabled = !connected;

            if (connected)
            {
                this.arEvent.Reset();
                this.model.CreateBackup();
                // will control this task via event
                Task.Run(() =>
                {
                    CreateBackupsTask();
                });
                // turn on grid.
                if (this.model.RepeatMeasures.NumMeasurements <= 1)
                {
                    this.scaleControl1.Visible = connected;
                    this.scaleControl1.Enabled = connected;
                    this.scaleControl1.SetVM(model);
                }
                else
                {
                    this.scaleControlRepetitions1.Visible = connected;
                    this.scaleControlRepetitions1.Enabled = connected;
                    this.scaleControlRepetitions1.SetVM(model);
                }
                // read in file info if it exists.
            }
            else
            {
                this.scaleControl1.Visible = connected;
                this.scaleControl1.Enabled = connected;
                this.scaleControlRepetitions1.Visible = connected;
                this.scaleControlRepetitions1.Enabled = connected;
                //
                arEvent.Set();
                if (this.model.RepeatMeasures.NumMeasurements <= 1)
                {
                    this.scaleControl1.GridCleanup();
                }
                else
                {
                    this.scaleControlRepetitions1.GridCleanup();
                }
                this.model.SaveFile();
                // spread cleanup
            }
        }

        private void ConfigureControls()
        {
            if ((this.model.RepeatMeasures?.ModelIsValid() ?? false)
                && (this.model.ScaleInfo?.ModelIsValid() ?? false)
                && !string.IsNullOrEmpty(model.FileSave?.FileName ?? ""))
            {
                this.connectionControl.Enabled = true;
            }
            else
            {
                this.connectionControl.Enabled = false;
            }
            if (this.model.RepeatMeasures?.ModelIsValid() ?? false)
            {
                this.btnRepeatedMeasures.BackColor = Color.Green;
            }
            else
            {
                this.btnRepeatedMeasures.BackColor = Color.Yellow;
            }
            if (this.model.ScaleInfo?.ModelIsValid() ?? false)
            {
                this.btnScaleInfo.BackColor = Color.Green;
            }
            else
            {
                this.btnScaleInfo.BackColor = Color.Yellow;
            }
            this.btnRepeatedMeasures.Enabled = !string.IsNullOrEmpty(model.FileSave?.FileName ?? "");
            this.btnScaleInfo.Enabled = !string.IsNullOrEmpty(model.FileSave?.FileName ?? "");
            this.scaleControl1.Visible = false;
            this.scaleControl1.Enabled = false;
            this.scaleControlRepetitions1.Visible = false;
            this.scaleControlRepetitions1.Visible = false;
        }

        private void SelectFileButtonClicked(object sender, EventArgs e)
        {
            OutputFileSettings dialog = new OutputFileSettings(this.model);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var tmpModel = dialog.model;
                this.model.RepeatMeasures = tmpModel.RepeatMeasures;
                this.model.ScaleInfo = tmpModel.ScaleInfo;
                this.model.FileSave = tmpModel.FileSave;
                this.model.Readings = tmpModel.Readings;
                this.model.FileHasRead = true;
                this.model.UpdateModel();
                if (!string.IsNullOrEmpty(model.FileSave?.FileName ?? ""))
                {
                    selectFileControl.SetToolTipForFileName(model, System.Drawing.Color.Green);
                }
                else
                {
                    selectFileControl.SetToolTipForFileName(model);
                }

                ConfigureControls();
            } // only update if a change to the configuration occurred 
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var abtBox = new AboutBox();
            abtBox.Show();
        }

        private void debugOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.model.DeviceIsConnected())
            {
                MessageBox.Show("Cannot change configuration while device is connected.", "Error Changing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DebugSettingsDialog dialog = new DebugSettingsDialog(model);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.model.UpdateModel();
                this.connectionControl.SetViewModel(model);
            } // only update if a change to the configuration occurred
        }

        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.model.DeviceIsConnected())
            {
                MessageBox.Show("Cannot change configuration while device is connected.", "Error Changing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConnectionSettingsDialog dialog = new ConnectionSettingsDialog(model.Connection);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.model.UpdateModel();
                this.connectionControl.SetViewModel(model);
            } // only update if a change to the configuration occurred 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ScaleAppMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveState();
        }

        private void SaveState()
        {
            this.model.SaveState();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveState();
        }

        private void sampleRepToolOption_Click(object sender, EventArgs e)
        {
            if (this.model.DeviceIsConnected())
            {
                MessageBox.Show("Cannot change configuration while device is connected.", "Error Changing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!this.model.FileHasRead)
            {
                // return, need scale configured.
                MessageBox.Show("Please configure the output file before updating these options...", "File Not Configured");
                return;
            }
            RepeatedMeasurementOptions dialog = new RepeatedMeasurementOptions(model.RepeatMeasures);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                this.model.UpdateModel();
                ConfigureControls();
            } // only update if a change to the configuration occurred
        }

        private void scaleInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.model.DeviceIsConnected())
            {
                MessageBox.Show("Cannot change configuration while device is connected.", "Error Changing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!this.model.FileHasRead)
            {
                // return, need scale configured.
                MessageBox.Show("Please configure the output file before updating these options...", "File Not Configured");
                return;
            }
            ScaleSettings dialog = new ScaleSettings(model);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                this.model.UpdateModel();
                ConfigureControls();
            }
        }

        private void ScaleAppMain_Load(object sender, EventArgs e)
        {
        }
    }
}
