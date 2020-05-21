namespace AgCROSScaleApp.Controls
{
    partial class ScaleControlRepetitions
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
            this.btnTakeMeasurements = new System.Windows.Forms.Button();
            this.takingReadingLoadingBox = new AgCROSScaleApp.Controls.LoadingBoxControl();
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
            this.reoGridControl.Location = new System.Drawing.Point(14, 54);
            this.reoGridControl.Name = "reoGridControl";
            this.reoGridControl.RowHeaderContextMenuStrip = null;
            this.reoGridControl.Script = null;
            this.reoGridControl.SheetTabContextMenuStrip = null;
            this.reoGridControl.SheetTabNewButtonVisible = false;
            this.reoGridControl.SheetTabVisible = false;
            this.reoGridControl.SheetTabWidth = 60;
            this.reoGridControl.ShowScrollEndSpacing = true;
            this.reoGridControl.Size = new System.Drawing.Size(498, 217);
            this.reoGridControl.TabIndex = 0;
            this.reoGridControl.Text = "reoGridControl1";
            this.reoGridControl.Resize += new System.EventHandler(this.reoGridControl_Resize);
            // 
            // btnTakeMeasurements
            // 
            this.btnTakeMeasurements.Location = new System.Drawing.Point(14, 13);
            this.btnTakeMeasurements.Name = "btnTakeMeasurements";
            this.btnTakeMeasurements.Size = new System.Drawing.Size(119, 23);
            this.btnTakeMeasurements.TabIndex = 12;
            this.btnTakeMeasurements.Text = "Take Measurements";
            this.btnTakeMeasurements.UseVisualStyleBackColor = true;
            this.btnTakeMeasurements.Click += new System.EventHandler(this.btnTakeMeasurements_Click);
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
            this.takingReadingLoadingBox.Size = new System.Drawing.Size(100, 106);
            this.takingReadingLoadingBox.TabIndex = 11;
            this.takingReadingLoadingBox.TabStop = false;
            // 
            // ScaleControlRepetitions
            // 
            this.Controls.Add(this.btnTakeMeasurements);
            this.Controls.Add(this.takingReadingLoadingBox);
            this.Controls.Add(this.reoGridControl);
            this.Name = "ScaleControlRepetitions";
            this.Size = new System.Drawing.Size(530, 290);
            ((System.ComponentModel.ISupportInitialize)(this.takingReadingLoadingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private unvell.ReoGrid.ReoGridControl reoGridControl;
        private LoadingBoxControl takingReadingLoadingBox;
        private System.Windows.Forms.Button btnTakeMeasurements;
    }
}
