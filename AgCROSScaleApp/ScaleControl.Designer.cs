namespace AgCROSScaleApp
{
    partial class ScaleControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadingBoxCtrl1 = new AgCROSScaleApp.LoadingBoxCtrl();
            this.label1 = new System.Windows.Forms.Label();
            this.reoGridSheet = new unvell.ReoGrid.ReoGridControl();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.cbxSerialPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBoxCtrl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.loadingBoxCtrl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.reoGridSheet);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.btnSelectFile);
            this.panel1.Controls.Add(this.cbxSerialPort);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 608);
            this.panel1.TabIndex = 0;
            // 
            // loadingBoxCtrl1
            // 
            this.loadingBoxCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingBoxCtrl1.BackColor = System.Drawing.Color.Transparent;
            this.loadingBoxCtrl1.Image = global::AgCROSScaleApp.Properties.Resources.SpinningGIF;
            this.loadingBoxCtrl1.Location = new System.Drawing.Point(306, 298);
            this.loadingBoxCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadingBoxCtrl1.Name = "loadingBoxCtrl1";
            this.loadingBoxCtrl1.Size = new System.Drawing.Size(152, 154);
            this.loadingBoxCtrl1.TabIndex = 5;
            this.loadingBoxCtrl1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 208);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(551, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Note: data is saved after every reading and on close to the indicated save file.";
            // 
            // reoGridSheet
            // 
            this.reoGridSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reoGridSheet.BackColor = System.Drawing.Color.White;
            this.reoGridSheet.ColumnHeaderContextMenuStrip = null;
            this.reoGridSheet.LeadHeaderContextMenuStrip = null;
            this.reoGridSheet.Location = new System.Drawing.Point(30, 242);
            this.reoGridSheet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reoGridSheet.Name = "reoGridSheet";
            this.reoGridSheet.RowHeaderContextMenuStrip = null;
            this.reoGridSheet.Script = null;
            this.reoGridSheet.SheetTabContextMenuStrip = null;
            this.reoGridSheet.SheetTabNewButtonVisible = false;
            this.reoGridSheet.SheetTabVisible = false;
            this.reoGridSheet.SheetTabWidth = 90;
            this.reoGridSheet.ShowScrollEndSpacing = true;
            this.reoGridSheet.Size = new System.Drawing.Size(699, 340);
            this.reoGridSheet.TabIndex = 4;
            this.reoGridSheet.Text = "reoGridControl1";
            this.reoGridSheet.Resize += new System.EventHandler(this.reoGridSheet_Resize);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(152, 55);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(576, 26);
            this.txtFileName.TabIndex = 0;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(30, 52);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(112, 35);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // cbxSerialPort
            // 
            this.cbxSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSerialPort.FormattingEnabled = true;
            this.cbxSerialPort.Location = new System.Drawing.Point(152, 152);
            this.cbxSerialPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxSerialPort.Name = "cbxSerialPort";
            this.cbxSerialPort.Size = new System.Drawing.Size(576, 28);
            this.cbxSerialPort.TabIndex = 2;
            this.cbxSerialPort.Click += new System.EventHandler(this.cbxSerialPort_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(30, 149);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 35);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // ScaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ScaleControl";
            this.Size = new System.Drawing.Size(750, 608);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBoxCtrl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxSerialPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnSelectFile;
        private unvell.ReoGrid.ReoGridControl reoGridSheet;
        private LoadingBoxCtrl loadingBoxCtrl1;
        private System.Windows.Forms.Label label1;
    }
}
