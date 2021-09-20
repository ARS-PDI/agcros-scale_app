namespace AgCROSScaleApp.Dialogs
{
    partial class OutputCalculator
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
            this.tab2CalcOpts = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnViewOutput = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.numMaxTol = new System.Windows.Forms.NumericUpDown();
            this.numMinTol = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbCalcType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textFileOut = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tab1Inputs = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textPostFile = new System.Windows.Forms.TextBox();
            this.textTareFile = new System.Windows.Forms.TextBox();
            this.textPreFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tab2CalcOpts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinTol)).BeginInit();
            this.tab1Inputs.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab2CalcOpts
            // 
            this.tab2CalcOpts.Controls.Add(this.btnClose);
            this.tab2CalcOpts.Controls.Add(this.btnViewOutput);
            this.tab2CalcOpts.Controls.Add(this.btnCalc);
            this.tab2CalcOpts.Controls.Add(this.numMaxTol);
            this.tab2CalcOpts.Controls.Add(this.numMinTol);
            this.tab2CalcOpts.Controls.Add(this.label11);
            this.tab2CalcOpts.Controls.Add(this.label10);
            this.tab2CalcOpts.Controls.Add(this.label9);
            this.tab2CalcOpts.Controls.Add(this.cbCalcType);
            this.tab2CalcOpts.Controls.Add(this.label7);
            this.tab2CalcOpts.Controls.Add(this.textFileOut);
            this.tab2CalcOpts.Controls.Add(this.label8);
            this.tab2CalcOpts.Location = new System.Drawing.Point(4, 22);
            this.tab2CalcOpts.Name = "tab2CalcOpts";
            this.tab2CalcOpts.Padding = new System.Windows.Forms.Padding(3);
            this.tab2CalcOpts.Size = new System.Drawing.Size(459, 272);
            this.tab2CalcOpts.TabIndex = 1;
            this.tab2CalcOpts.Text = "Calculation Options";
            this.tab2CalcOpts.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(307, 233);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnViewOutput
            // 
            this.btnViewOutput.Location = new System.Drawing.Point(176, 233);
            this.btnViewOutput.Name = "btnViewOutput";
            this.btnViewOutput.Size = new System.Drawing.Size(75, 23);
            this.btnViewOutput.TabIndex = 9;
            this.btnViewOutput.Text = "View Output";
            this.btnViewOutput.UseVisualStyleBackColor = true;
            this.btnViewOutput.Click += new System.EventHandler(this.btnViewOutput_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(48, 233);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 8;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // numMaxTol
            // 
            this.numMaxTol.DecimalPlaces = 2;
            this.numMaxTol.Location = new System.Drawing.Point(28, 144);
            this.numMaxTol.Name = "numMaxTol";
            this.numMaxTol.Size = new System.Drawing.Size(115, 20);
            this.numMaxTol.TabIndex = 6;
            this.numMaxTol.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numMinTol
            // 
            this.numMinTol.DecimalPlaces = 2;
            this.numMinTol.Location = new System.Drawing.Point(28, 90);
            this.numMinTol.Name = "numMinTol";
            this.numMinTol.Size = new System.Drawing.Size(115, 20);
            this.numMinTol.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Max Tolerance";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Min Tolerance";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Calculation Type";
            // 
            // cbCalcType
            // 
            this.cbCalcType.FormattingEnabled = true;
            this.cbCalcType.Location = new System.Drawing.Point(28, 33);
            this.cbCalcType.Name = "cbCalcType";
            this.cbCalcType.Size = new System.Drawing.Size(223, 21);
            this.cbCalcType.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Output File";
            // 
            // textFileOut
            // 
            this.textFileOut.Location = new System.Drawing.Point(28, 194);
            this.textFileOut.Name = "textFileOut";
            this.textFileOut.Size = new System.Drawing.Size(223, 20);
            this.textFileOut.TabIndex = 7;
            this.textFileOut.DoubleClick += new System.EventHandler(this.textFileOut_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(257, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "click box to select...";
            // 
            // tab1Inputs
            // 
            this.tab1Inputs.Controls.Add(this.btnNext);
            this.tab1Inputs.Controls.Add(this.label5);
            this.tab1Inputs.Controls.Add(this.label1);
            this.tab1Inputs.Controls.Add(this.textPostFile);
            this.tab1Inputs.Controls.Add(this.textTareFile);
            this.tab1Inputs.Controls.Add(this.textPreFile);
            this.tab1Inputs.Controls.Add(this.label6);
            this.tab1Inputs.Controls.Add(this.label2);
            this.tab1Inputs.Controls.Add(this.label3);
            this.tab1Inputs.Controls.Add(this.label4);
            this.tab1Inputs.Location = new System.Drawing.Point(4, 22);
            this.tab1Inputs.Name = "tab1Inputs";
            this.tab1Inputs.Padding = new System.Windows.Forms.Padding(3);
            this.tab1Inputs.Size = new System.Drawing.Size(459, 272);
            this.tab1Inputs.TabIndex = 0;
            this.tab1Inputs.Text = "Input Files";
            this.tab1Inputs.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(31, 225);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "double click box to select...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tare Weight File (Optional):";
            // 
            // textPostFile
            // 
            this.textPostFile.Location = new System.Drawing.Point(30, 180);
            this.textPostFile.Name = "textPostFile";
            this.textPostFile.Size = new System.Drawing.Size(223, 20);
            this.textPostFile.TabIndex = 2;
            this.textPostFile.DoubleClick += new System.EventHandler(this.textPostFile_DoubleClick);
            // 
            // textTareFile
            // 
            this.textTareFile.Location = new System.Drawing.Point(30, 47);
            this.textTareFile.Name = "textTareFile";
            this.textTareFile.Size = new System.Drawing.Size(223, 20);
            this.textTareFile.TabIndex = 0;
            this.textTareFile.DoubleClick += new System.EventHandler(this.textTareFile_DoubleClick);
            // 
            // textPreFile
            // 
            this.textPreFile.Location = new System.Drawing.Point(31, 109);
            this.textPreFile.Name = "textPreFile";
            this.textPreFile.Size = new System.Drawing.Size(223, 20);
            this.textPreFile.TabIndex = 1;
            this.textPreFile.DoubleClick += new System.EventHandler(this.textPreFile_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Post-Process Weight File (Dry)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "double click box to select...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "double click box to select...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pre-Process Weight File (Wet)";
            // 
            // tabCtrl
            // 
            this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl.Controls.Add(this.tab1Inputs);
            this.tabCtrl.Controls.Add(this.tab2CalcOpts);
            this.tabCtrl.Location = new System.Drawing.Point(2, -1);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(467, 298);
            this.tabCtrl.TabIndex = 4;
            // 
            // OutputCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 296);
            this.Controls.Add(this.tabCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputCalculator";
            this.Text = "OutputCalculator";
            this.Load += new System.EventHandler(this.OutputCalculator_Load);
            this.tab2CalcOpts.ResumeLayout(false);
            this.tab2CalcOpts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinTol)).EndInit();
            this.tab1Inputs.ResumeLayout(false);
            this.tab1Inputs.PerformLayout();
            this.tabCtrl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tab2CalcOpts;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.NumericUpDown numMaxTol;
        private System.Windows.Forms.NumericUpDown numMinTol;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbCalcType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textFileOut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tab1Inputs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPostFile;
        private System.Windows.Forms.TextBox textTareFile;
        private System.Windows.Forms.TextBox textPreFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnViewOutput;
        private System.Windows.Forms.Button btnNext;
    }
}