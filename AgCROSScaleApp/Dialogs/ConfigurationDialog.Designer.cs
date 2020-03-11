namespace AgCROSScaleApp
{
    partial class ConfigurationDialog
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chxTestDevice = new System.Windows.Forms.CheckBox();
            this.txtConnectionTimeout = new System.Windows.Forms.TextBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chxDebug = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textRetries = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(93, 245);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(198, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chxTestDevice
            // 
            this.chxTestDevice.AutoSize = true;
            this.chxTestDevice.Location = new System.Drawing.Point(161, 26);
            this.chxTestDevice.Name = "chxTestDevice";
            this.chxTestDevice.Size = new System.Drawing.Size(15, 14);
            this.chxTestDevice.TabIndex = 2;
            this.chxTestDevice.UseVisualStyleBackColor = true;
            // 
            // txtConnectionTimeout
            // 
            this.txtConnectionTimeout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtConnectionTimeout.Location = new System.Drawing.Point(161, 118);
            this.txtConnectionTimeout.MaxLength = 10;
            this.txtConnectionTimeout.Name = "txtConnectionTimeout";
            this.txtConnectionTimeout.Size = new System.Drawing.Size(124, 20);
            this.txtConnectionTimeout.TabIndex = 3;
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeout.Location = new System.Drawing.Point(27, 121);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(128, 13);
            this.lblTimeout.TabIndex = 4;
            this.lblTimeout.Text = "Connection Timeout (sec)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Allow Test Device";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Debug";
            // 
            // chxDebug
            // 
            this.chxDebug.AutoSize = true;
            this.chxDebug.Location = new System.Drawing.Point(161, 60);
            this.chxDebug.Name = "chxDebug";
            this.chxDebug.Size = new System.Drawing.Size(15, 14);
            this.chxDebug.TabIndex = 6;
            this.chxDebug.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Number of Retries";
            // 
            // textRetries
            // 
            this.textRetries.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textRetries.Location = new System.Drawing.Point(161, 163);
            this.textRetries.MaxLength = 10;
            this.textRetries.Name = "textRetries";
            this.textRetries.Size = new System.Drawing.Size(124, 20);
            this.textRetries.TabIndex = 8;
            // 
            // ConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 280);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textRetries);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chxDebug);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.txtConnectionTimeout);
            this.Controls.Add(this.chxTestDevice);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationDialog";
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chxTestDevice;
        private System.Windows.Forms.TextBox txtConnectionTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chxDebug;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textRetries;
    }
}