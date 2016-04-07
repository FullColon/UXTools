namespace UX_Affectiva_Research_Tool
{
    partial class RecordGui
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
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.affectivaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.FPSTEXTBOX = new System.Windows.Forms.TextBox();
            this.FPSLabel = new System.Windows.Forms.Label();
            this.label_screenAudioDevice = new System.Windows.Forms.Label();
            this.comboBox_screenAudioDevice = new System.Windows.Forms.ComboBox();
            this.label_ScreenCodecs = new System.Windows.Forms.Label();
            this.comboBox_screenCodecs = new System.Windows.Forms.ComboBox();
            this.label_RecordOptions = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonFilePath = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startButton.Location = new System.Drawing.Point(0, 0);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(262, 35);
            this.startButton.TabIndex = 0;
            this.startButton.Text = " Start Recording";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopButton.Location = new System.Drawing.Point(0, 0);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(276, 35);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop Recording";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(542, 325);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(542, 286);
            this.splitContainer3.SplitterDistance = 25;
            this.splitContainer3.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(542, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.affectivaToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // affectivaToolStripMenuItem
            // 
            this.affectivaToolStripMenuItem.Checked = true;
            this.affectivaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.affectivaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moreOptionsToolStripMenuItem});
            this.affectivaToolStripMenuItem.Name = "affectivaToolStripMenuItem";
            this.affectivaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.affectivaToolStripMenuItem.Text = "Affectiva";
            // 
            // moreOptionsToolStripMenuItem
            // 
            this.moreOptionsToolStripMenuItem.Name = "moreOptionsToolStripMenuItem";
            this.moreOptionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.moreOptionsToolStripMenuItem.Text = "More Options";
            this.moreOptionsToolStripMenuItem.Click += new System.EventHandler(this.moreOptionsToolStripMenuItem_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.FPSTEXTBOX);
            this.splitContainer4.Panel1.Controls.Add(this.FPSLabel);
            this.splitContainer4.Panel1.Controls.Add(this.label_screenAudioDevice);
            this.splitContainer4.Panel1.Controls.Add(this.comboBox_screenAudioDevice);
            this.splitContainer4.Panel1.Controls.Add(this.label_ScreenCodecs);
            this.splitContainer4.Panel1.Controls.Add(this.comboBox_screenCodecs);
            this.splitContainer4.Panel1.Controls.Add(this.label_RecordOptions);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label1);
            this.splitContainer4.Panel2.Controls.Add(this.labelPath);
            this.splitContainer4.Panel2.Controls.Add(this.buttonFilePath);
            this.splitContainer4.Panel2.Controls.Add(this.textBoxName);
            this.splitContainer4.Size = new System.Drawing.Size(542, 257);
            this.splitContainer4.SplitterDistance = 261;
            this.splitContainer4.TabIndex = 0;
            // 
            // FPSTEXTBOX
            // 
            this.FPSTEXTBOX.Location = new System.Drawing.Point(89, 98);
            this.FPSTEXTBOX.Name = "FPSTEXTBOX";
            this.FPSTEXTBOX.Size = new System.Drawing.Size(100, 20);
            this.FPSTEXTBOX.TabIndex = 6;
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Location = new System.Drawing.Point(40, 101);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(27, 13);
            this.FPSLabel.TabIndex = 5;
            this.FPSLabel.Text = "FPS";
            // 
            // label_screenAudioDevice
            // 
            this.label_screenAudioDevice.AutoSize = true;
            this.label_screenAudioDevice.Location = new System.Drawing.Point(12, 68);
            this.label_screenAudioDevice.Name = "label_screenAudioDevice";
            this.label_screenAudioDevice.Size = new System.Drawing.Size(71, 13);
            this.label_screenAudioDevice.TabIndex = 4;
            this.label_screenAudioDevice.Text = "Audio Device";
            // 
            // comboBox_screenAudioDevice
            // 
            this.comboBox_screenAudioDevice.FormattingEnabled = true;
            this.comboBox_screenAudioDevice.Location = new System.Drawing.Point(89, 65);
            this.comboBox_screenAudioDevice.Name = "comboBox_screenAudioDevice";
            this.comboBox_screenAudioDevice.Size = new System.Drawing.Size(169, 21);
            this.comboBox_screenAudioDevice.TabIndex = 3;
            this.comboBox_screenAudioDevice.SelectedIndexChanged += new System.EventHandler(this.comboBox_screenAudioDevice_SelectedIndexChanged);
            // 
            // label_ScreenCodecs
            // 
            this.label_ScreenCodecs.AutoSize = true;
            this.label_ScreenCodecs.Location = new System.Drawing.Point(40, 40);
            this.label_ScreenCodecs.Name = "label_ScreenCodecs";
            this.label_ScreenCodecs.Size = new System.Drawing.Size(43, 13);
            this.label_ScreenCodecs.TabIndex = 2;
            this.label_ScreenCodecs.Text = "Codecs";
            // 
            // comboBox_screenCodecs
            // 
            this.comboBox_screenCodecs.FormattingEnabled = true;
            this.comboBox_screenCodecs.Location = new System.Drawing.Point(89, 37);
            this.comboBox_screenCodecs.Name = "comboBox_screenCodecs";
            this.comboBox_screenCodecs.Size = new System.Drawing.Size(169, 21);
            this.comboBox_screenCodecs.TabIndex = 1;
            this.comboBox_screenCodecs.SelectedIndexChanged += new System.EventHandler(this.comboBox_screenCodecs_SelectedIndexChanged);
            // 
            // label_RecordOptions
            // 
            this.label_RecordOptions.AutoSize = true;
            this.label_RecordOptions.Location = new System.Drawing.Point(52, 10);
            this.label_RecordOptions.Name = "label_RecordOptions";
            this.label_RecordOptions.Size = new System.Drawing.Size(95, 13);
            this.label_RecordOptions.TabIndex = 0;
            this.label_RecordOptions.Text = "Recording Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Test Name";
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(20, 98);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(51, 13);
            this.labelPath.TabIndex = 7;
            this.labelPath.Text = "File_Path";
            // 
            // buttonFilePath
            // 
            this.buttonFilePath.Location = new System.Drawing.Point(85, 68);
            this.buttonFilePath.Name = "buttonFilePath";
            this.buttonFilePath.Size = new System.Drawing.Size(101, 23);
            this.buttonFilePath.TabIndex = 8;
            this.buttonFilePath.Text = "Select File Path";
            this.buttonFilePath.UseVisualStyleBackColor = true;
            this.buttonFilePath.Click += new System.EventHandler(this.buttonFilePath_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(85, 37);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 9;
            this.textBoxName.Text = "Default";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.startButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.stopButton);
            this.splitContainer2.Size = new System.Drawing.Size(542, 35);
            this.splitContainer2.SplitterDistance = 262;
            this.splitContainer2.TabIndex = 0;
            // 
            // RecordGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 325);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RecordGui";
            this.Text = "RecordGui";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label_screenAudioDevice;
        private System.Windows.Forms.ComboBox comboBox_screenAudioDevice;
        private System.Windows.Forms.Label label_ScreenCodecs;
        private System.Windows.Forms.ComboBox comboBox_screenCodecs;
        private System.Windows.Forms.Label label_RecordOptions;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem affectivaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreOptionsToolStripMenuItem;
        private System.Windows.Forms.TextBox FPSTEXTBOX;
        private System.Windows.Forms.Label FPSLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonFilePath;
        private System.Windows.Forms.TextBox textBoxName;
    }


}