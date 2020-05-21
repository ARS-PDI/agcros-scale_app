namespace AgCROSScaleApp.Controls
{
    partial class ConnectionControl
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxSerialPort = new System.Windows.Forms.ComboBox();
            this.connectionLoadingBox = new AgCROSScaleApp.Controls.LoadingBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.connectionLoadingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(28, 38);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbxSerialPort
            // 
            this.cbxSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSerialPort.FormattingEnabled = true;
            this.cbxSerialPort.Location = new System.Drawing.Point(127, 38);
            this.cbxSerialPort.Name = "cbxSerialPort";
            this.cbxSerialPort.Size = new System.Drawing.Size(309, 21);
            this.cbxSerialPort.TabIndex = 8;
            // 
            // connectionLoadingBox
            // 
            this.connectionLoadingBox.BackColor = System.Drawing.Color.Transparent;
            this.connectionLoadingBox.Image = global::AgCROSScaleApp.Properties.Resources.SpinningGIF;
            this.connectionLoadingBox.Location = new System.Drawing.Point(185, 0);
            this.connectionLoadingBox.Name = "connectionLoadingBox";
            this.connectionLoadingBox.Size = new System.Drawing.Size(96, 100);
            this.connectionLoadingBox.TabIndex = 10;
            this.connectionLoadingBox.TabStop = false;
            // 
            // ConnectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.connectionLoadingBox);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbxSerialPort);
            this.Name = "ConnectionControl";
            this.Size = new System.Drawing.Size(461, 101);
            ((System.ComponentModel.ISupportInitialize)(this.connectionLoadingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxSerialPort;
        private LoadingBoxControl connectionLoadingBox;
    }
}
