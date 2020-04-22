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
    public partial class DebugSettingsDialog : Form
    {
        private ScaleAppViewModel vm;
        public DebugSettingsDialog(ScaleAppViewModel vm)
        {
            this.vm = vm;
            InitializeComponent();
            this.chxTestDevice.Checked = vm.AllowTestDevice;
            this.chxDebug.Checked = vm.Debug;
            this.chxPromptZero.Checked = vm.PromptOnZero;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            vm.AllowTestDevice = this.chxTestDevice.Checked;
            vm.Debug = this.chxDebug.Checked;
            vm.PromptOnZero = this.chxPromptZero.Checked;
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
