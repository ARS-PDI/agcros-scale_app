using AgCROSScaleApp.Controls;

namespace AgCROSScaleApp
{
    partial class ScaleAppMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleAppMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sampleRepToolOption = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRepeatedMeasures = new System.Windows.Forms.Button();
            this.btnScaleInfo = new System.Windows.Forms.Button();
            this.scaleControlCalculation1 = new AgCROSScaleApp.Controls.ScaleControlCalculation();
            this.scaleControlRepetitions1 = new AgCROSScaleApp.Controls.ScaleControlRepetitions();
            this.selectFileControl = new AgCROSScaleApp.Controls.SelectFileControl();
            this.scaleControl1 = new AgCROSScaleApp.Controls.ScaleControl();
            this.connectionControl = new AgCROSScaleApp.Controls.ConnectionControl();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem1,
            this.calculateToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuScaleApp";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(95, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(95, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationSettingsToolStripMenuItem,
            this.connectionSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.sampleRepToolOption,
            this.scaleInfoToolStripMenuItem});
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem1.Text = "Settings";
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.applicationSettingsToolStripMenuItem.Text = "App Settings";
            this.applicationSettingsToolStripMenuItem.Click += new System.EventHandler(this.debugOptionsToolStripMenuItem_Click);
            // 
            // connectionSettingsToolStripMenuItem
            // 
            this.connectionSettingsToolStripMenuItem.Name = "connectionSettingsToolStripMenuItem";
            this.connectionSettingsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.connectionSettingsToolStripMenuItem.Text = "Connection Settings";
            this.connectionSettingsToolStripMenuItem.Click += new System.EventHandler(this.connectionSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(241, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // sampleRepToolOption
            // 
            this.sampleRepToolOption.Enabled = false;
            this.sampleRepToolOption.Name = "sampleRepToolOption";
            this.sampleRepToolOption.Size = new System.Drawing.Size(244, 22);
            this.sampleRepToolOption.Text = "Repeated Measurement Settings";
            this.sampleRepToolOption.Visible = false;
            this.sampleRepToolOption.Click += new System.EventHandler(this.sampleRepToolOption_Click);
            // 
            // scaleInfoToolStripMenuItem
            // 
            this.scaleInfoToolStripMenuItem.Enabled = false;
            this.scaleInfoToolStripMenuItem.Name = "scaleInfoToolStripMenuItem";
            this.scaleInfoToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.scaleInfoToolStripMenuItem.Text = "Scale Info Settings";
            this.scaleInfoToolStripMenuItem.Visible = false;
            this.scaleInfoToolStripMenuItem.Click += new System.EventHandler(this.scaleInfoToolStripMenuItem_Click);
            // 
            // calculateToolStripMenuItem
            // 
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.calculateToolStripMenuItem.Text = "Calculate";
            this.calculateToolStripMenuItem.Click += new System.EventHandler(this.calculateToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            this.toolsToolStripMenuItem.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.scaleControlCalculation1);
            this.panel1.Controls.Add(this.btnRepeatedMeasures);
            this.panel1.Controls.Add(this.btnScaleInfo);
            this.panel1.Controls.Add(this.scaleControlRepetitions1);
            this.panel1.Controls.Add(this.selectFileControl);
            this.panel1.Controls.Add(this.scaleControl1);
            this.panel1.Controls.Add(this.connectionControl);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 574);
            this.panel1.TabIndex = 8;
            // 
            // btnRepeatedMeasures
            // 
            this.btnRepeatedMeasures.Location = new System.Drawing.Point(400, 126);
            this.btnRepeatedMeasures.Name = "btnRepeatedMeasures";
            this.btnRepeatedMeasures.Size = new System.Drawing.Size(189, 23);
            this.btnRepeatedMeasures.TabIndex = 13;
            this.btnRepeatedMeasures.Text = "Configure Repeated Measurements";
            this.btnRepeatedMeasures.UseVisualStyleBackColor = true;
            this.btnRepeatedMeasures.Click += new System.EventHandler(this.sampleRepToolOption_Click);
            // 
            // btnScaleInfo
            // 
            this.btnScaleInfo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnScaleInfo.Location = new System.Drawing.Point(145, 126);
            this.btnScaleInfo.Name = "btnScaleInfo";
            this.btnScaleInfo.Size = new System.Drawing.Size(189, 23);
            this.btnScaleInfo.TabIndex = 12;
            this.btnScaleInfo.Text = "Configure Scale Information";
            this.btnScaleInfo.UseVisualStyleBackColor = true;
            this.btnScaleInfo.Click += new System.EventHandler(this.scaleInfoToolStripMenuItem_Click);
            // 
            // scaleControlCalculation1
            // 
            this.scaleControlCalculation1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleControlCalculation1.Location = new System.Drawing.Point(69, 267);
            this.scaleControlCalculation1.Name = "scaleControlCalculation1";
            this.scaleControlCalculation1.Size = new System.Drawing.Size(580, 295);
            this.scaleControlCalculation1.TabIndex = 14;
            // 
            // scaleControlRepetitions1
            // 
            this.scaleControlRepetitions1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleControlRepetitions1.Location = new System.Drawing.Point(69, 267);
            this.scaleControlRepetitions1.Name = "scaleControlRepetitions1";
            this.scaleControlRepetitions1.Size = new System.Drawing.Size(580, 307);
            this.scaleControlRepetitions1.TabIndex = 11;
            // 
            // selectFileControl
            // 
            this.selectFileControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFileControl.Location = new System.Drawing.Point(69, 49);
            this.selectFileControl.Name = "selectFileControl";
            this.selectFileControl.Size = new System.Drawing.Size(560, 59);
            this.selectFileControl.TabIndex = 10;
            // 
            // scaleControl1
            // 
            this.scaleControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scaleControl1.Location = new System.Drawing.Point(69, 267);
            this.scaleControl1.Name = "scaleControl1";
            this.scaleControl1.Size = new System.Drawing.Size(580, 295);
            this.scaleControl1.TabIndex = 1;
            // 
            // connectionControl
            // 
            this.connectionControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionControl.BackColor = System.Drawing.Color.Transparent;
            this.connectionControl.Location = new System.Drawing.Point(69, 166);
            this.connectionControl.Name = "connectionControl";
            this.connectionControl.Size = new System.Drawing.Size(560, 120);
            this.connectionControl.TabIndex = 9;
            // 
            // ScaleAppMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 574);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScaleAppMain";
            this.Text = "ScaleApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScaleAppMain_FormClosing);
            this.Load += new System.EventHandler(this.ScaleAppMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connectionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private ScaleControl scaleControl1;
        private System.Windows.Forms.Panel panel1;
        private SelectFileControl selectFileControl;
        private ConnectionControl connectionControl;
        private System.Windows.Forms.ToolStripMenuItem sampleRepToolOption;
        private ScaleControlRepetitions scaleControlRepetitions1;
        private System.Windows.Forms.ToolStripMenuItem scaleInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnScaleInfo;
        private System.Windows.Forms.Button btnRepeatedMeasures;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private ScaleControlCalculation scaleControlCalculation1;
    }
}

