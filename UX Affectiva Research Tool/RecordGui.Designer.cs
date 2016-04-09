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
            this.lbl_screenAudioCodecs = new System.Windows.Forms.Label();
            this.cmbx_screenAudioCodecs = new System.Windows.Forms.ComboBox();
            this.lbl_screenQualityValue = new System.Windows.Forms.Label();
            this.lbl_screenQuality = new System.Windows.Forms.Label();
            this.trkbr_screenQuality = new System.Windows.Forms.TrackBar();
            this.lbl_screenSelect = new System.Windows.Forms.Label();
            this.cmbx_screenSelect = new System.Windows.Forms.ComboBox();
            this.nud_screenFPS = new System.Windows.Forms.NumericUpDown();
            this.lbl_screenFPS = new System.Windows.Forms.Label();
            this.label_screenAudioDevice = new System.Windows.Forms.Label();
            this.cmbx_screenAudioDevice = new System.Windows.Forms.ComboBox();
            this.label_screenVideoCodecs = new System.Windows.Forms.Label();
            this.cmbx_screenVideoCodecs = new System.Windows.Forms.ComboBox();
            this.label_RecordOptions = new System.Windows.Forms.Label();
            this.lbl_cameraAudioCodec = new System.Windows.Forms.Label();
            this.cmbx_cameraAudioCodecs = new System.Windows.Forms.ComboBox();
            this.lbl_cameraQualityValue = new System.Windows.Forms.Label();
            this.lbl_cameraQaulity = new System.Windows.Forms.Label();
            this.trkbr_cameraQuality = new System.Windows.Forms.TrackBar();
            this.nud_cameraFPS = new System.Windows.Forms.NumericUpDown();
            this.lbl_cameraFramesPerSecond = new System.Windows.Forms.Label();
            this.lbl_cameraAudioInput = new System.Windows.Forms.Label();
            this.cmbx_cameraAudioInput = new System.Windows.Forms.ComboBox();
            this.lbl_camaraVideoCodecs = new System.Windows.Forms.Label();
            this.cmbx_cameraVideoCodecs = new System.Windows.Forms.ComboBox();
            this.lbl_cameras = new System.Windows.Forms.Label();
            this.cmbx_cameras = new System.Windows.Forms.ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_screenQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_screenFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_cameraQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cameraFPS)).BeginInit();
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
            this.startButton.Size = new System.Drawing.Size(286, 54);
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
            this.stopButton.Size = new System.Drawing.Size(303, 54);
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
            this.splitContainer1.Size = new System.Drawing.Size(593, 476);
            this.splitContainer1.SplitterDistance = 418;
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
            this.splitContainer3.Size = new System.Drawing.Size(593, 418);
            this.splitContainer3.SplitterDistance = 36;
            this.splitContainer3.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(593, 24);
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
            this.affectivaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.affectivaToolStripMenuItem.Text = "Affectiva";
            // 
            // moreOptionsToolStripMenuItem
            // 
            this.moreOptionsToolStripMenuItem.Name = "moreOptionsToolStripMenuItem";
            this.moreOptionsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
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
            this.splitContainer4.Panel1.Controls.Add(this.lbl_screenAudioCodecs);
            this.splitContainer4.Panel1.Controls.Add(this.cmbx_screenAudioCodecs);
            this.splitContainer4.Panel1.Controls.Add(this.lbl_screenQualityValue);
            this.splitContainer4.Panel1.Controls.Add(this.lbl_screenQuality);
            this.splitContainer4.Panel1.Controls.Add(this.trkbr_screenQuality);
            this.splitContainer4.Panel1.Controls.Add(this.lbl_screenSelect);
            this.splitContainer4.Panel1.Controls.Add(this.cmbx_screenSelect);
            this.splitContainer4.Panel1.Controls.Add(this.nud_screenFPS);
            this.splitContainer4.Panel1.Controls.Add(this.lbl_screenFPS);
            this.splitContainer4.Panel1.Controls.Add(this.label_screenAudioDevice);
            this.splitContainer4.Panel1.Controls.Add(this.cmbx_screenAudioDevice);
            this.splitContainer4.Panel1.Controls.Add(this.label_screenVideoCodecs);
            this.splitContainer4.Panel1.Controls.Add(this.cmbx_screenVideoCodecs);
            this.splitContainer4.Panel1.Controls.Add(this.label_RecordOptions);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lbl_cameraAudioCodec);
            this.splitContainer4.Panel2.Controls.Add(this.cmbx_cameraAudioCodecs);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_cameraQualityValue);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_cameraQaulity);
            this.splitContainer4.Panel2.Controls.Add(this.trkbr_cameraQuality);
            this.splitContainer4.Panel2.Controls.Add(this.nud_cameraFPS);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_cameraFramesPerSecond);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_cameraAudioInput);
            this.splitContainer4.Panel2.Controls.Add(this.cmbx_cameraAudioInput);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_camaraVideoCodecs);
            this.splitContainer4.Panel2.Controls.Add(this.cmbx_cameraVideoCodecs);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_cameras);
            this.splitContainer4.Panel2.Controls.Add(this.cmbx_cameras);
            this.splitContainer4.Panel2.Controls.Add(this.label1);
            this.splitContainer4.Panel2.Controls.Add(this.labelPath);
            this.splitContainer4.Panel2.Controls.Add(this.buttonFilePath);
            this.splitContainer4.Panel2.Controls.Add(this.textBoxName);
            this.splitContainer4.Size = new System.Drawing.Size(593, 378);
            this.splitContainer4.SplitterDistance = 285;
            this.splitContainer4.TabIndex = 0;
            // 
            // lbl_screenAudioCodecs
            // 
            this.lbl_screenAudioCodecs.AutoSize = true;
            this.lbl_screenAudioCodecs.Location = new System.Drawing.Point(15, 98);
            this.lbl_screenAudioCodecs.Name = "lbl_screenAudioCodecs";
            this.lbl_screenAudioCodecs.Size = new System.Drawing.Size(73, 13);
            this.lbl_screenAudioCodecs.TabIndex = 13;
            this.lbl_screenAudioCodecs.Text = "Audio Codecs";
            // 
            // cmbx_screenAudioCodecs
            // 
            this.cmbx_screenAudioCodecs.FormattingEnabled = true;
            this.cmbx_screenAudioCodecs.Location = new System.Drawing.Point(88, 95);
            this.cmbx_screenAudioCodecs.Name = "cmbx_screenAudioCodecs";
            this.cmbx_screenAudioCodecs.Size = new System.Drawing.Size(121, 21);
            this.cmbx_screenAudioCodecs.TabIndex = 12;
            // 
            // lbl_screenQualityValue
            // 
            this.lbl_screenQualityValue.AutoSize = true;
            this.lbl_screenQualityValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_screenQualityValue.Location = new System.Drawing.Point(51, 189);
            this.lbl_screenQualityValue.Name = "lbl_screenQualityValue";
            this.lbl_screenQualityValue.Size = new System.Drawing.Size(29, 20);
            this.lbl_screenQualityValue.TabIndex = 11;
            this.lbl_screenQualityValue.Text = "50";
            // 
            // lbl_screenQuality
            // 
            this.lbl_screenQuality.AutoSize = true;
            this.lbl_screenQuality.Location = new System.Drawing.Point(9, 189);
            this.lbl_screenQuality.Name = "lbl_screenQuality";
            this.lbl_screenQuality.Size = new System.Drawing.Size(42, 13);
            this.lbl_screenQuality.TabIndex = 10;
            this.lbl_screenQuality.Text = "Quality:";
            // 
            // trkbr_screenQuality
            // 
            this.trkbr_screenQuality.LargeChange = 10;
            this.trkbr_screenQuality.Location = new System.Drawing.Point(88, 189);
            this.trkbr_screenQuality.Maximum = 100;
            this.trkbr_screenQuality.Name = "trkbr_screenQuality";
            this.trkbr_screenQuality.Size = new System.Drawing.Size(121, 45);
            this.trkbr_screenQuality.SmallChange = 5;
            this.trkbr_screenQuality.TabIndex = 9;
            this.trkbr_screenQuality.TickFrequency = 10;
            this.trkbr_screenQuality.Value = 50;
            this.trkbr_screenQuality.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lbl_screenSelect
            // 
            this.lbl_screenSelect.AutoSize = true;
            this.lbl_screenSelect.Location = new System.Drawing.Point(9, 126);
            this.lbl_screenSelect.Name = "lbl_screenSelect";
            this.lbl_screenSelect.Size = new System.Drawing.Size(74, 13);
            this.lbl_screenSelect.TabIndex = 8;
            this.lbl_screenSelect.Text = "Screen Select";
            // 
            // cmbx_screenSelect
            // 
            this.cmbx_screenSelect.FormattingEnabled = true;
            this.cmbx_screenSelect.Location = new System.Drawing.Point(89, 123);
            this.cmbx_screenSelect.Name = "cmbx_screenSelect";
            this.cmbx_screenSelect.Size = new System.Drawing.Size(121, 21);
            this.cmbx_screenSelect.TabIndex = 7;
            // 
            // nud_screenFPS
            // 
            this.nud_screenFPS.Location = new System.Drawing.Point(88, 150);
            this.nud_screenFPS.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nud_screenFPS.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_screenFPS.Name = "nud_screenFPS";
            this.nud_screenFPS.Size = new System.Drawing.Size(120, 20);
            this.nud_screenFPS.TabIndex = 6;
            this.nud_screenFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_screenFPS.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lbl_screenFPS
            // 
            this.lbl_screenFPS.AutoSize = true;
            this.lbl_screenFPS.Location = new System.Drawing.Point(56, 155);
            this.lbl_screenFPS.Name = "lbl_screenFPS";
            this.lbl_screenFPS.Size = new System.Drawing.Size(27, 13);
            this.lbl_screenFPS.TabIndex = 5;
            this.lbl_screenFPS.Text = "FPS";
            // 
            // label_screenAudioDevice
            // 
            this.label_screenAudioDevice.AutoSize = true;
            this.label_screenAudioDevice.Location = new System.Drawing.Point(19, 68);
            this.label_screenAudioDevice.Name = "label_screenAudioDevice";
            this.label_screenAudioDevice.Size = new System.Drawing.Size(61, 13);
            this.label_screenAudioDevice.TabIndex = 4;
            this.label_screenAudioDevice.Text = "Audio Input";
            // 
            // cmbx_screenAudioDevice
            // 
            this.cmbx_screenAudioDevice.FormattingEnabled = true;
            this.cmbx_screenAudioDevice.Location = new System.Drawing.Point(89, 65);
            this.cmbx_screenAudioDevice.Name = "cmbx_screenAudioDevice";
            this.cmbx_screenAudioDevice.Size = new System.Drawing.Size(121, 21);
            this.cmbx_screenAudioDevice.TabIndex = 3;
            // 
            // label_screenVideoCodecs
            // 
            this.label_screenVideoCodecs.AutoSize = true;
            this.label_screenVideoCodecs.Location = new System.Drawing.Point(12, 44);
            this.label_screenVideoCodecs.Name = "label_screenVideoCodecs";
            this.label_screenVideoCodecs.Size = new System.Drawing.Size(73, 13);
            this.label_screenVideoCodecs.TabIndex = 2;
            this.label_screenVideoCodecs.Text = "Video Codecs";
            // 
            // cmbx_screenVideoCodecs
            // 
            this.cmbx_screenVideoCodecs.FormattingEnabled = true;
            this.cmbx_screenVideoCodecs.Location = new System.Drawing.Point(89, 37);
            this.cmbx_screenVideoCodecs.Name = "cmbx_screenVideoCodecs";
            this.cmbx_screenVideoCodecs.Size = new System.Drawing.Size(121, 21);
            this.cmbx_screenVideoCodecs.TabIndex = 1;
            // 
            // label_RecordOptions
            // 
            this.label_RecordOptions.AutoSize = true;
            this.label_RecordOptions.Location = new System.Drawing.Point(52, 10);
            this.label_RecordOptions.Name = "label_RecordOptions";
            this.label_RecordOptions.Size = new System.Drawing.Size(132, 13);
            this.label_RecordOptions.TabIndex = 0;
            this.label_RecordOptions.Text = "Screen Recording Options";
            // 
            // lbl_cameraAudioCodec
            // 
            this.lbl_cameraAudioCodec.AutoSize = true;
            this.lbl_cameraAudioCodec.Location = new System.Drawing.Point(11, 219);
            this.lbl_cameraAudioCodec.Name = "lbl_cameraAudioCodec";
            this.lbl_cameraAudioCodec.Size = new System.Drawing.Size(73, 13);
            this.lbl_cameraAudioCodec.TabIndex = 24;
            this.lbl_cameraAudioCodec.Text = "Audio Codecs";
            // 
            // cmbx_cameraAudioCodecs
            // 
            this.cmbx_cameraAudioCodecs.FormattingEnabled = true;
            this.cmbx_cameraAudioCodecs.Location = new System.Drawing.Point(84, 216);
            this.cmbx_cameraAudioCodecs.Name = "cmbx_cameraAudioCodecs";
            this.cmbx_cameraAudioCodecs.Size = new System.Drawing.Size(121, 21);
            this.cmbx_cameraAudioCodecs.TabIndex = 23;
            // 
            // lbl_cameraQualityValue
            // 
            this.lbl_cameraQualityValue.AutoSize = true;
            this.lbl_cameraQualityValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cameraQualityValue.Location = new System.Drawing.Point(47, 310);
            this.lbl_cameraQualityValue.Name = "lbl_cameraQualityValue";
            this.lbl_cameraQualityValue.Size = new System.Drawing.Size(29, 20);
            this.lbl_cameraQualityValue.TabIndex = 22;
            this.lbl_cameraQualityValue.Text = "50";
            // 
            // lbl_cameraQaulity
            // 
            this.lbl_cameraQaulity.AutoSize = true;
            this.lbl_cameraQaulity.Location = new System.Drawing.Point(5, 310);
            this.lbl_cameraQaulity.Name = "lbl_cameraQaulity";
            this.lbl_cameraQaulity.Size = new System.Drawing.Size(42, 13);
            this.lbl_cameraQaulity.TabIndex = 21;
            this.lbl_cameraQaulity.Text = "Quality:";
            // 
            // trkbr_cameraQuality
            // 
            this.trkbr_cameraQuality.LargeChange = 10;
            this.trkbr_cameraQuality.Location = new System.Drawing.Point(84, 310);
            this.trkbr_cameraQuality.Maximum = 100;
            this.trkbr_cameraQuality.Name = "trkbr_cameraQuality";
            this.trkbr_cameraQuality.Size = new System.Drawing.Size(121, 45);
            this.trkbr_cameraQuality.SmallChange = 5;
            this.trkbr_cameraQuality.TabIndex = 20;
            this.trkbr_cameraQuality.TickFrequency = 10;
            this.trkbr_cameraQuality.Value = 50;
            this.trkbr_cameraQuality.Scroll += new System.EventHandler(this.trkbr_cameraQuality_Scroll);
            // 
            // nud_cameraFPS
            // 
            this.nud_cameraFPS.Location = new System.Drawing.Point(84, 271);
            this.nud_cameraFPS.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nud_cameraFPS.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_cameraFPS.Name = "nud_cameraFPS";
            this.nud_cameraFPS.Size = new System.Drawing.Size(120, 20);
            this.nud_cameraFPS.TabIndex = 19;
            this.nud_cameraFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_cameraFPS.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lbl_cameraFramesPerSecond
            // 
            this.lbl_cameraFramesPerSecond.AutoSize = true;
            this.lbl_cameraFramesPerSecond.Location = new System.Drawing.Point(52, 276);
            this.lbl_cameraFramesPerSecond.Name = "lbl_cameraFramesPerSecond";
            this.lbl_cameraFramesPerSecond.Size = new System.Drawing.Size(27, 13);
            this.lbl_cameraFramesPerSecond.TabIndex = 18;
            this.lbl_cameraFramesPerSecond.Text = "FPS";
            // 
            // lbl_cameraAudioInput
            // 
            this.lbl_cameraAudioInput.AutoSize = true;
            this.lbl_cameraAudioInput.Location = new System.Drawing.Point(15, 189);
            this.lbl_cameraAudioInput.Name = "lbl_cameraAudioInput";
            this.lbl_cameraAudioInput.Size = new System.Drawing.Size(61, 13);
            this.lbl_cameraAudioInput.TabIndex = 17;
            this.lbl_cameraAudioInput.Text = "Audio Input";
            // 
            // cmbx_cameraAudioInput
            // 
            this.cmbx_cameraAudioInput.FormattingEnabled = true;
            this.cmbx_cameraAudioInput.Location = new System.Drawing.Point(85, 186);
            this.cmbx_cameraAudioInput.Name = "cmbx_cameraAudioInput";
            this.cmbx_cameraAudioInput.Size = new System.Drawing.Size(121, 21);
            this.cmbx_cameraAudioInput.TabIndex = 16;
            // 
            // lbl_camaraVideoCodecs
            // 
            this.lbl_camaraVideoCodecs.AutoSize = true;
            this.lbl_camaraVideoCodecs.Location = new System.Drawing.Point(8, 165);
            this.lbl_camaraVideoCodecs.Name = "lbl_camaraVideoCodecs";
            this.lbl_camaraVideoCodecs.Size = new System.Drawing.Size(73, 13);
            this.lbl_camaraVideoCodecs.TabIndex = 15;
            this.lbl_camaraVideoCodecs.Text = "Video Codecs";
            // 
            // cmbx_cameraVideoCodecs
            // 
            this.cmbx_cameraVideoCodecs.FormattingEnabled = true;
            this.cmbx_cameraVideoCodecs.Location = new System.Drawing.Point(85, 158);
            this.cmbx_cameraVideoCodecs.Name = "cmbx_cameraVideoCodecs";
            this.cmbx_cameraVideoCodecs.Size = new System.Drawing.Size(121, 21);
            this.cmbx_cameraVideoCodecs.TabIndex = 14;
            // 
            // lbl_cameras
            // 
            this.lbl_cameras.AutoSize = true;
            this.lbl_cameras.Location = new System.Drawing.Point(20, 130);
            this.lbl_cameras.Name = "lbl_cameras";
            this.lbl_cameras.Size = new System.Drawing.Size(48, 13);
            this.lbl_cameras.TabIndex = 12;
            this.lbl_cameras.Text = "Cameras";
            // 
            // cmbx_cameras
            // 
            this.cmbx_cameras.FormattingEnabled = true;
            this.cmbx_cameras.Location = new System.Drawing.Point(85, 127);
            this.cmbx_cameras.Name = "cmbx_cameras";
            this.cmbx_cameras.Size = new System.Drawing.Size(121, 21);
            this.cmbx_cameras.TabIndex = 11;
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
            this.splitContainer2.Size = new System.Drawing.Size(593, 54);
            this.splitContainer2.SplitterDistance = 286;
            this.splitContainer2.TabIndex = 0;
            // 
            // RecordGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 476);
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
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_screenQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_screenFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_cameraQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cameraFPS)).EndInit();
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
        private System.Windows.Forms.ComboBox cmbx_screenAudioDevice;
        private System.Windows.Forms.Label label_screenVideoCodecs;
        private System.Windows.Forms.ComboBox cmbx_screenVideoCodecs;
        private System.Windows.Forms.Label label_RecordOptions;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem affectivaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreOptionsToolStripMenuItem;
        private System.Windows.Forms.Label lbl_screenFPS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonFilePath;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label lbl_screenQualityValue;
        private System.Windows.Forms.Label lbl_screenQuality;
        private System.Windows.Forms.TrackBar trkbr_screenQuality;
        private System.Windows.Forms.Label lbl_screenSelect;
        private System.Windows.Forms.ComboBox cmbx_screenSelect;
        private System.Windows.Forms.NumericUpDown nud_screenFPS;
        private System.Windows.Forms.ComboBox cmbx_screenAudioCodecs;
        private System.Windows.Forms.Label lbl_screenAudioCodecs;
        private System.Windows.Forms.Label lbl_cameraAudioCodec;
        private System.Windows.Forms.ComboBox cmbx_cameraAudioCodecs;
        private System.Windows.Forms.Label lbl_cameraQualityValue;
        private System.Windows.Forms.Label lbl_cameraQaulity;
        private System.Windows.Forms.TrackBar trkbr_cameraQuality;
        private System.Windows.Forms.NumericUpDown nud_cameraFPS;
        private System.Windows.Forms.Label lbl_cameraFramesPerSecond;
        private System.Windows.Forms.Label lbl_cameraAudioInput;
        private System.Windows.Forms.ComboBox cmbx_cameraAudioInput;
        private System.Windows.Forms.Label lbl_camaraVideoCodecs;
        private System.Windows.Forms.ComboBox cmbx_cameraVideoCodecs;
        private System.Windows.Forms.Label lbl_cameras;
        private System.Windows.Forms.ComboBox cmbx_cameras;
    }


}