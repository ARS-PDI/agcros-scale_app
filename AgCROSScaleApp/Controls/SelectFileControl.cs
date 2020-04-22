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
using AgCROSScaleApp.Dialogs;
using System.IO;

namespace AgCROSScaleApp.Controls
{
    public partial class SelectFileControl : UserControl
    {

        private ScaleAppViewModel model;
        public SelectFileControl()
        {
            InitializeComponent();
        }

        public void SetViewModel(ScaleAppViewModel model)
        {
            this.model = model;
            SetToolTipForFileName(model);
        }

        public void SetToolTipForFileName(ScaleAppViewModel model)
        {
            txtFileName.Text = Path.GetFileName(model.FileSave.FileName);
            toolTip1.SetToolTip(txtFileName, model.FileSave.FileName);
        }
                
        public event EventHandler SelectFileButtonClicked;
        protected virtual void OnSelectFileButtonClicked(EventArgs e)
        {
            SelectFileButtonClicked?.Invoke(this, e);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OnSelectFileButtonClicked(e);
        }
    }
}
