using System;

namespace AgCROSScaleApp.Dialogs
{
    partial class ConnectionSettingsDialog
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
            this.label3 = new System.Windows.Forms.Label();
            this.textRetries = new System.Windows.Forms.TextBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.textConnectionTimeout = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboStopBits = new System.Windows.Forms.ComboBox();
            this.comboParity = new System.Windows.Forms.ComboBox();
            this.comboFlow = new System.Windows.Forms.ComboBox();
            this.comboDatabits = new System.Windows.Forms.ComboBox();
            this.comboBaud = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Number of Retries";
            // 
            // textRetries
            // 
            this.textRetries.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textRetries.Location = new System.Drawing.Point(187, 83);
            this.textRetries.MaxLength = 10;
            this.textRetries.Name = "textRetries";
            this.textRetries.Size = new System.Drawing.Size(124, 20);
            this.textRetries.TabIndex = 12;
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeout.Location = new System.Drawing.Point(53, 41);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(128, 13);
            this.lblTimeout.TabIndex = 11;
            this.lblTimeout.Text = "Connection Timeout (sec)";
            // 
            // textConnectionTimeout
            // 
            this.textConnectionTimeout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textConnectionTimeout.Location = new System.Drawing.Point(187, 38);
            this.textConnectionTimeout.MaxLength = 10;
            this.textConnectionTimeout.Name = "textConnectionTimeout";
            this.textConnectionTimeout.Size = new System.Drawing.Size(124, 20);
            this.textConnectionTimeout.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(36, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 121);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Communication Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboStopBits);
            this.groupBox2.Controls.Add(this.comboParity);
            this.groupBox2.Controls.Add(this.comboFlow);
            this.groupBox2.Controls.Add(this.comboDatabits);
            this.groupBox2.Controls.Add(this.comboBaud);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(36, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 213);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CommPort Settings";
            // 
            // comboStopBits
            // 
            this.comboStopBits.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));
            this.comboStopBits.FormattingEnabled = true;
            this.comboStopBits.Location = new System.Drawing.Point(148, 171);
            this.comboStopBits.Name = "comboStopBits";
            this.comboStopBits.Size = new System.Drawing.Size(124, 21);
            this.comboStopBits.TabIndex = 30;
            // 
            // comboParity
            // 
            this.comboParity.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            this.comboParity.FormattingEnabled = true;
            this.comboParity.Location = new System.Drawing.Point(148, 142);
            this.comboParity.Name = "comboParity";
            this.comboParity.Size = new System.Drawing.Size(124, 21);
            this.comboParity.TabIndex = 29;
            // 
            // comboFlow
            // 
            this.comboFlow.DataSource = Enum.GetValues(typeof(System.IO.Ports.Handshake));
            this.comboFlow.FormattingEnabled = true;
            this.comboFlow.Location = new System.Drawing.Point(148, 113);
            this.comboFlow.Name = "comboFlow";
            this.comboFlow.Size = new System.Drawing.Size(124, 21);
            this.comboFlow.TabIndex = 28;
            // 
            // comboDatabits
            // 
            this.comboDatabits.DataSource = Enum.GetValues(typeof(DataBits));
            this.comboDatabits.FormattingEnabled = true;
            this.comboDatabits.Location = new System.Drawing.Point(148, 84);
            this.comboDatabits.Name = "comboDatabits";
            this.comboDatabits.Size = new System.Drawing.Size(124, 21);
            this.comboDatabits.TabIndex = 27;
            // 
            // comboBaud
            // 
            this.comboBaud.DataSource = Enum.GetValues(typeof(BaudRates));
            this.comboBaud.FormattingEnabled = true;
            this.comboBaud.Location = new System.Drawing.Point(148, 54);
            this.comboBaud.Name = "comboBaud";
            this.comboBaud.Size = new System.Drawing.Size(124, 21);
            this.comboBaud.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(59, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Stop Bits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Parity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Flow Control";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Data Bits";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Baud Rate";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(89, 362);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ConnectionSettingsDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(376, 404);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textRetries);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.textConnectionTimeout);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConnectionSettingsDialog";
            this.Text = "Connection Settings";
            this.Load += new System.EventHandler(this.ConnectDialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textRetries;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.TextBox textConnectionTimeout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBaud;
        private System.Windows.Forms.ComboBox comboDatabits;
        private System.Windows.Forms.ComboBox comboStopBits;
        private System.Windows.Forms.ComboBox comboParity;
        private System.Windows.Forms.ComboBox comboFlow;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}