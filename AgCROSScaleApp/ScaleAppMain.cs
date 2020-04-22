using AgCROSScaleApp.Dialogs;
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

namespace AgCROSScaleApp
{
    public partial class ScaleAppMain : Form
    {

        private ScaleAppViewModel model;
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

        private void ConnectButtonClicked(object sender, EventArgs e)
        {
            if (model.DeviceIsConnected())
            {
                this.selectFileControl.Enabled = false;
                // turn on grid.
                this.scaleControl1.Visible = true;
                this.scaleControl1.Enabled = true;
                this.model.ReadSavedFile();
                this.scaleControl1.SetVM(model);
                // read in file info if it exists.

            } else
            {
                this.selectFileControl.Enabled = true;
                //
                this.scaleControl1.GridCleanup();
                this.scaleControl1.Visible = false;
                this.scaleControl1.Enabled = false;
                this.model.SaveFile();
                // spread cleanup
            }
        }

        private void ConfigureControls()
        {
            this.connectionControl.Enabled = !string.IsNullOrEmpty(model.FileSave.FileName);
            this.scaleControl1.Visible = false;
            this.scaleControl1.Enabled = false;
        }

        private void SelectFileButtonClicked(object sender, EventArgs e)
        {
            OutputFileSettings dialog = new OutputFileSettings(model.FileSave);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.model.UpdateModel();
                selectFileControl.SetToolTipForFileName(model);
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
                // TODO: update this to connect to the "connection" control
                //this.scaleControl1.UpdateConfiguredOptions();
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
    }
}
