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
    public enum BaudRates: int
    {
        BR9600 = 9600,
        BR12800 = 12800,
        BR38400 = 38400,
        BR57600 = 57600
    }

    public enum DataBits: int
    {
        DB7 = 7,
        DB8 = 8
    }

    public partial class ConnectionSettingsDialog : Form
    {

        private ConnectionModel model;
        public ConnectionSettingsDialog(ConnectionModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void ConnectDialog_Load(object sender, EventArgs e)
        {
            this.textConnectionTimeout.Text = model.ConnectTimeout.ToString();
            this.textRetries.Text = model.NumRetries.ToString();
            this.comboBaud.SelectedIndex = this.comboBaud.Items.IndexOf((BaudRates)model.BaudRate);
            this.comboDatabits.SelectedIndex = comboDatabits.Items.IndexOf((DataBits)model.DataBits);
            this.comboFlow.SelectedIndex = comboFlow.Items.IndexOf(model.Handshake);
            this.comboParity.SelectedIndex = comboParity.Items.IndexOf(model.Parity);
            this.comboStopBits.SelectedIndex = comboStopBits.Items.IndexOf(model.StopBits);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            model.ConnectTimeout = Int32.Parse(textConnectionTimeout.Text);
            model.NumRetries = Int32.Parse(textRetries.Text);
            model.BaudRate = (int)((BaudRates)this.comboBaud.SelectedItem);
            model.DataBits = (int)((BaudRates)this.comboDatabits.SelectedItem);
            model.Handshake = (System.IO.Ports.Handshake)this.comboFlow.SelectedItem;
            model.Parity = (System.IO.Ports.Parity)this.comboParity.SelectedItem;
            model.StopBits = (System.IO.Ports.StopBits)this.comboStopBits.SelectedItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
