namespace AgCROSScaleApp.Controls
{
    partial class ScaleControlCalculation
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reoGridControl = new unvell.ReoGrid.ReoGridControl();
            this.takingReadingLoadingBox = new AgCROSScaleApp.Controls.LoadingBoxControl();
            this.minTol = new System.Windows.Forms.Label();
            this.maxTol = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.takingReadingLoadingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // reoGridControl
            // 
            this.reoGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reoGridControl.BackColor = System.Drawing.Color.White;
            this.reoGridControl.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl.LeadHeaderContextMenuStrip = null;
            this.reoGridControl.Location = new System.Drawing.Point(14, 36);
            this.reoGridControl.Name = "reoGridControl";
            this.reoGridControl.RowHeaderContextMenuStrip = null;
            this.reoGridControl.Script = null;
            this.reoGridControl.SheetTabContextMenuStrip = null;
            this.reoGridControl.SheetTabNewButtonVisible = false;
            this.reoGridControl.SheetTabVisible = true;
            this.reoGridControl.SheetTabWidth = 300;
            this.reoGridControl.ShowScrollEndSpacing = false;
            this.reoGridControl.Size = new System.Drawing.Size(498, 196);
            this.reoGridControl.TabIndex = 0;
            this.reoGridControl.Text = "reoGridControl1";
            this.reoGridControl.Resize += new System.EventHandler(this.reoGridControl_Resize);
            // 
            // takingReadingLoadingBox
            // 
            this.takingReadingLoadingBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.takingReadingLoadingBox.BackColor = System.Drawing.Color.Transparent;
            this.takingReadingLoadingBox.Image = global::AgCROSScaleApp.Properties.Resources.SpinningGIF;
            this.takingReadingLoadingBox.Location = new System.Drawing.Point(220, 62);
            this.takingReadingLoadingBox.Name = "takingReadingLoadingBox";
            this.takingReadingLoadingBox.Size = new System.Drawing.Size(100, 99);
            this.takingReadingLoadingBox.TabIndex = 11;
            this.takingReadingLoadingBox.TabStop = false;
            // 
            // minTol
            // 
            this.minTol.AutoSize = true;
            this.minTol.Location = new System.Drawing.Point(132, 4);
            this.minTol.Name = "minTol";
            this.minTol.Size = new System.Drawing.Size(69, 13);
            this.minTol.TabIndex = 12;
            this.minTol.Text = "Min Tol: 0.00";
            // 
            // maxTol
            // 
            this.maxTol.AutoSize = true;
            this.maxTol.Location = new System.Drawing.Point(268, 4);
            this.maxTol.Name = "maxTol";
            this.maxTol.Size = new System.Drawing.Size(84, 13);
            this.maxTol.TabIndex = 13;
            this.maxTol.Text = "Max Tol: 100.00";
            // 
            // ScaleControlCalculation
            // 
            this.Controls.Add(this.takingReadingLoadingBox);
            this.Controls.Add(this.maxTol);
            this.Controls.Add(this.minTol);
            this.Controls.Add(this.reoGridControl);
            this.Name = "ScaleControlCalculation";
            this.Size = new System.Drawing.Size(530, 251);
            ((System.ComponentModel.ISupportInitialize)(this.takingReadingLoadingBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private unvell.ReoGrid.ReoGridControl reoGridControl;
        private LoadingBoxControl takingReadingLoadingBox;
        private System.Windows.Forms.Label minTol;
        private System.Windows.Forms.Label maxTol;
    }
}
