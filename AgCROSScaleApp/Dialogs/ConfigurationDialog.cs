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
    public partial class ConfigurationDialog : Form
    {
        private ScaleAppViewModel vm;
        public ConfigurationDialog(ScaleAppViewModel vm)
        {
            this.vm = vm;
            InitializeComponent();
            this.chxTestDevice.Checked = vm.AllowTestDevice;
            this.txtConnectionTimeout.Text = Math.Max(5, vm.ConnectTimeout).ToString();
            this.chxDebug.Checked = vm.Debug;
            this.textRetries.Text = vm.NumRetries.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            vm.AllowTestDevice = this.chxTestDevice.Checked;
            vm.ConnectTimeout = Math.Max(5, int.Parse(this.txtConnectionTimeout.Text));
            vm.Debug = this.chxDebug.Checked;
            vm.NumRetries = int.Parse(this.textRetries.Text);
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
