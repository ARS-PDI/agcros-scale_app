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
    public partial class RepeatedMeasurementOptions : Form
    {
        private RepeatedMeasurementModel model;
        public RepeatedMeasurementOptions(RepeatedMeasurementModel model)
        {
            this.model = model;
            InitializeComponent();
            this.boxNumReps.Value = model.NumMeasurements ?? 1;
            TriggerAllowStats(this.boxNumReps.Value > 1);
            this.txtStdDevLimit.Text = model.StdDevLimit.ToString() ?? 0.0m.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (boxNumReps.Value > 1 && string.IsNullOrWhiteSpace(this.txtStdDevLimit.Text))
            {
                MessageBox.Show("Please Provide a StdDev Limit.", "No StdDev Limit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.model.NumMeasurements = (int) boxNumReps.Value;
            if  (boxNumReps.Value > 1)
            {
                this.model.StdDevLimit = decimal.Parse(this.txtStdDevLimit.Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtStdDevLimit_TextChanged(object sender, EventArgs e)
        {
            if (!Double.TryParse(this.txtStdDevLimit.Text, out double res))
            {
                MessageBox.Show("Decimal number required");
            }
        }

        private void boxNumReps_ValueChanged(object sender, EventArgs e)
        {
            TriggerAllowStats(this.boxNumReps.Value > 1);
        }

        public void TriggerAllowStats(bool setting)
        {
            this.txtStdDevLimit.Enabled = setting;
        }
    }
}
