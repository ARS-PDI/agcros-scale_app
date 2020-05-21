namespace AgCROSScaleApp.Dialogs
{
    partial class RepetitionSample
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.txtScanField = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.loadingBoxControl1 = new AgCROSScaleApp.Controls.LoadingBoxControl();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(22, 64);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = false;
            this.reoGridControl1.SheetTabVisible = false;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(467, 247);
            this.reoGridControl1.TabIndex = 0;
            this.reoGridControl1.TabStop = false;
            this.reoGridControl1.Text = "reoGridControl1";
            // 
            // txtScanField
            // 
            this.txtScanField.Location = new System.Drawing.Point(228, 28);
            this.txtScanField.Name = "txtScanField";
            this.txtScanField.Size = new System.Drawing.Size(193, 20);
            this.txtScanField.TabIndex = 0;
            this.txtScanField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RepetitionSample_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(228, 334);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(378, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // loadingBoxControl1
            // 
            this.loadingBoxControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingBoxControl1.BackColor = System.Drawing.Color.Transparent;
            this.loadingBoxControl1.Image = global::AgCROSScaleApp.Properties.Resources.SpinningGIF;
            this.loadingBoxControl1.Location = new System.Drawing.Point(203, 118);
            this.loadingBoxControl1.Name = "loadingBoxControl1";
            this.loadingBoxControl1.Size = new System.Drawing.Size(100, 99);
            this.loadingBoxControl1.TabIndex = 13;
            this.loadingBoxControl1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sample ID Scan Box";
            // 
            // RepetitionSample
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(515, 384);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadingBoxControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtScanField);
            this.Controls.Add(this.reoGridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepetitionSample";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Take Repeated Measurements";
            ((System.ComponentModel.ISupportInitialize)(this.loadingBoxControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.TextBox txtScanField;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Controls.LoadingBoxControl loadingBoxControl1;
        private System.Windows.Forms.Label label1;
    }
}