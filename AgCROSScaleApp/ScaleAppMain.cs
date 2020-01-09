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
            this.scaleControl1.SetVM(model);
            this.Text = $"{this.Text}-v{Application.ProductVersion}";
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.model.Device?.IsConnected ?? false)
            {
                MessageBox.Show("Cannot change configuration while device is connected.", "Error Changing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConfigurationDialog dialog = new ConfigurationDialog(model);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.scaleControl1.UpdateConfiguredOptions();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var abtBox = new AboutBox();
                abtBox.Show();
        }
    }
}
