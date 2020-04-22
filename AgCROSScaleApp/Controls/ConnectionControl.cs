using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgCROSScaleApp.Models;
using MeasurementEquipment.Types;

namespace AgCROSScaleApp.Controls
{
    public partial class ConnectionControl : UserControl
    {
        private ScaleAppViewModel model;
        public ConnectionControl()
        {
            InitializeComponent();
            this.connectionLoadingBox.Visible = false;
            this.cbxSerialPort.DisplayMember = "DisplayName";
            this.cbxSerialPort.ValueMember = "PortName";
        }

        internal void SetViewModel(ScaleAppViewModel model)
        {
            this.model = model;
            Cursor = Cursors.WaitCursor;
            this.model.FindSerialPorts();
            this.cbxSerialPort.DataSource = new BindingSource(new BindingList<SerialPortValue>(model.SerialPorts), null);
            if (this.model.SelectedSerialPort != null)
            {
                if (this.cbxSerialPort.Items.Contains(this.model.SelectedSerialPort))
                {
                    this.cbxSerialPort.SelectedItem = this.cbxSerialPort.Items[this.cbxSerialPort.Items.IndexOf(this.model.SelectedSerialPort)];
                }
            }
            Cursor = Cursors.Default;
        }

        public event EventHandler ConnectButtonClicked;
        protected virtual void OnConnectButtonClicked(EventArgs e)
        {
            ConnectButtonClicked?.Invoke(this, e);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.model.DeviceIsConnected())
                {
                    this.connectionLoadingBox.Visible = true;
                    if (this.model.ConnectToDevice((SerialPortValue)this.cbxSerialPort.SelectedItem))
                    {
                        this.btnConnect.Text = "Disconnect";
                        // turn off input boxes and file selection
                        this.cbxSerialPort.Enabled = false;

                        // have the form tell everyone else to lock down or show up based on state of model.
                        OnConnectButtonClicked(e);
                    }
                    this.connectionLoadingBox.Visible = false;
                }
                else
                {
                    // disconnect
                    this.model.Disconnect();
                    if (!this.model.DeviceIsConnected())
                    {
                        this.btnConnect.Text = "Connect";
                        OnConnectButtonClicked(e);
                        // turn back on checkbox, filename box, and filename button.
                        this.cbxSerialPort.Enabled = true;
                    }
                }
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show($"{ioe.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Encounted issue: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.connectionLoadingBox.Visible = false;
            }
        }
    }
}
