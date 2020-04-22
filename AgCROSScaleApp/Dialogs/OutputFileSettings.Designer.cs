namespace AgCROSScaleApp.Dialogs
{
    partial class OutputFileSettings
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
            this.textMetadata = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textVariableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.btnCreateNewFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.buttonOpenExistingFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textMetadata
            // 
            this.textMetadata.Location = new System.Drawing.Point(202, 142);
            this.textMetadata.Multiline = true;
            this.textMetadata.Name = "textMetadata";
            this.textMetadata.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textMetadata.Size = new System.Drawing.Size(343, 144);
            this.textMetadata.TabIndex = 5;
            this.textMetadata.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Metadata (Optional):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 91);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter metadata in pairs per line- \r\n<title>:<description>\r\n<title>:<description>\r" +
    "\n\r\nExample - \r\nLocation: Someplace, SomeState\r\nDescription: Initial Soil Weight";
            // 
            // textVariableName
            // 
            this.textVariableName.Location = new System.Drawing.Point(202, 93);
            this.textVariableName.Name = "textVariableName";
            this.textVariableName.Size = new System.Drawing.Size(343, 20);
            this.textVariableName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Variable Name (for CSV header, Optional)";
            // 
            // textFileName
            // 
            this.textFileName.Location = new System.Drawing.Point(202, 47);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(343, 20);
            this.textFileName.TabIndex = 3;
            // 
            // btnCreateNewFile
            // 
            this.btnCreateNewFile.Location = new System.Drawing.Point(15, 26);
            this.btnCreateNewFile.Name = "btnCreateNewFile";
            this.btnCreateNewFile.Size = new System.Drawing.Size(75, 23);
            this.btnCreateNewFile.TabIndex = 1;
            this.btnCreateNewFile.Text = "New File";
            this.btnCreateNewFile.UseVisualStyleBackColor = true;
            this.btnCreateNewFile.Click += new System.EventHandler(this.btnCreateNewFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Output File Name:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(227, 323);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(400, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // buttonOpenExistingFile
            // 
            this.buttonOpenExistingFile.Location = new System.Drawing.Point(15, 67);
            this.buttonOpenExistingFile.Name = "buttonOpenExistingFile";
            this.buttonOpenExistingFile.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenExistingFile.TabIndex = 2;
            this.buttonOpenExistingFile.Text = "Open File";
            this.buttonOpenExistingFile.UseVisualStyleBackColor = true;
            this.buttonOpenExistingFile.Click += new System.EventHandler(this.buttonOpenExistingFile_Click);
            // 
            // OutputFileSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 374);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOpenExistingFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCreateNewFile);
            this.Controls.Add(this.textFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textVariableName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textMetadata);
            this.Name = "OutputFileSettings";
            this.Text = "OutputFileSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textMetadata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textVariableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.Button btnCreateNewFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button buttonOpenExistingFile;
    }
}