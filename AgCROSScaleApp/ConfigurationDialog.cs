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
            this.txtConnectionTimeout.Text = vm.ConnectTimeout.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            vm.AllowTestDevice = this.chxTestDevice.Checked;
            vm.ConnectTimeout = int.Parse(this.txtConnectionTimeout.Text);
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
