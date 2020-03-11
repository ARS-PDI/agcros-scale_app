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
            this.loadingBox1 = new AgCROSScaleApp.LoadingBoxControl();
            this.label1 = new System.Windows.Forms.Label();
            this.reoGridSheet = new unvell.ReoGrid.ReoGridControl();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.cbxSerialPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.loadingBox2 = new AgCROSScaleApp.LoadingBoxControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.loadingBox2);
            this.panel1.Controls.Add(this.loadingBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.reoGridSheet);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.btnSelectFile);
            this.panel1.Controls.Add(this.cbxSerialPort);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 395);
            this.panel1.TabIndex = 0;
            // 
            // loadingBox1
            // 
            this.loadingBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingBox1.BackColor = System.Drawing.Color.Transparent;
            this.loadingBox1.Image = global::AgCROSScaleApp.Properties.Resources.SpinningGIF;
            this.loadingBox1.Location = new System.Drawing.Point(216, 51);
            this.loadingBox1.Name = "loadingBox1";
            this.loadingBox1.Size = new System.Drawing.Size(101, 100);
            this.loadingBox1.TabIndex = 5;
            this.loadingBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 13);
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
            this.reoGridSheet.Location = new System.Drawing.Point(20, 157);
            this.reoGridSheet.Name = "reoGridSheet";
            this.reoGridSheet.RowHeaderContextMenuStrip = null;
            this.reoGridSheet.Script = null;
            this.reoGridSheet.SheetTabContextMenuStrip = null;
            this.reoGridSheet.SheetTabNewButtonVisible = false;
            this.reoGridSheet.SheetTabVisible = false;
            this.reoGridSheet.SheetTabWidth = 60;
            this.reoGridSheet.ShowScrollEndSpacing = true;
            this.reoGridSheet.Size = new System.Drawing.Size(466, 221);
            this.reoGridSheet.TabIndex = 4;
            this.reoGridSheet.Text = "reoGridControl1";
            this.reoGridSheet.Resize += new System.EventHandler(this.reoGridSheet_Resize);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(101, 36);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(385, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(20, 34);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
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
            this.cbxSerialPort.Location = new System.Drawing.Point(101, 99);
            this.cbxSerialPort.Name = "cbxSerialPort";
            this.cbxSerialPort.Size = new System.Drawing.Size(385, 21);
            this.cbxSerialPort.TabIndex = 2;
            this.cbxSerialPort.Click += new System.EventHandler(this.cbxSerialPort_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(20, 97);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // loadingBox2
            // 
            this.loadingBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingBox2.BackColor = System.Drawing.Color.Transparent;
            this.loadingBox2.Image = global::AgCROSScaleApp.Properties.Resources.SpinningGIF;
            this.loadingBox2.Location = new System.Drawing.Point(216, 209);
            this.loadingBox2.Name = "loadingBox2";
            this.loadingBox2.Size = new System.Drawing.Size(101, 100);
            this.loadingBox2.TabIndex = 7;
            this.loadingBox2.TabStop = false;
            // 
            // ScaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ScaleControl";
            this.Size = new System.Drawing.Size(500, 395);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxSerialPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnSelectFile;
        private unvell.ReoGrid.ReoGridControl reoGridSheet;
        private LoadingBoxControl loadingBox1;
        private System.Windows.Forms.Label label1;
        private LoadingBoxControl loadingBox2;
    }
}
