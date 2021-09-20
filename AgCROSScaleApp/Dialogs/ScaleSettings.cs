using AgCROSScaleApp.Models;
using MeasurementEquipment.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgCROSScaleApp.Dialogs
{
    public partial class ScaleSettings : Form
    {
        private ScaleAppViewModel vm;
        public ScaleSettings(Models.ScaleAppViewModel model)
        {
            InitializeComponent();
            this.vm = model;
            foreach (var item in ScaleInfoModel.InterfaceOptions)
            {
                comboInterface.Items.Add(item);
                comboInterface.DisplayMember = "ListName";
            }
            if (this.vm.AllowTestDevice)
            {
                comboInterface.Items.Add("Virtual Interface (test)");
            }
        }

        private void SetupFields(ScaleAppViewModel model)
        {
            var scaleInfo = model.ScaleInfo;
            if (scaleInfo != null)
            {
                this.txtModel.Text = scaleInfo.Model;
                this.txtReadability.Text = scaleInfo.Readabilitymg?.ToString();
                this.txtRepeatability.Text = scaleInfo.Repeatabilitymg?.ToString();
                this.comboUnits.SelectedIndex = this.comboUnits.Items.IndexOf((Constants.ScaleUnits)(scaleInfo.Unit ?? -1));
                comboInterface.SelectedIndex = comboInterface.Items.IndexOf(scaleInfo.ConnectionInterface);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            vm.ScaleInfo = new ScaleInfoModel();
            vm.ScaleInfo.ConnectionInterface = comboInterface.SelectedItem?.ToString() ?? "";
            vm.ScaleInfo.Model = txtModel.Text;
            if(this.comboUnits.SelectedItem != null)
            {
                vm.ScaleInfo.Unit = (int)this.comboUnits.SelectedItem;
            }
            if (decimal.TryParse(txtReadability.Text, out decimal parse))
            {
                vm.ScaleInfo.Readabilitymg = parse;
            }
            if (decimal.TryParse(txtRepeatability.Text, out parse))
            {
                vm.ScaleInfo.Repeatabilitymg = parse;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ScaleSettings_Load(object sender, EventArgs e)
        {
            SetupFields(this.vm);
        }

        private void txtReadability_TextChanged(object sender, EventArgs e)
        {
            if (!Double.TryParse(this.txtReadability.Text, out double res))
            {
                MessageBox.Show("Decimal number required");
            }
        }

        private void txtRepeatability_TextChanged(object sender, EventArgs e)
        {
            if (!Double.TryParse(this.txtRepeatability.Text, out double res))
            {
                MessageBox.Show("Decimal number required");
            }
        }
    }
}
