namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    partial class AffectivaOptions
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.DectionBufferLabel = new System.Windows.Forms.Label();
            this.numericUpDownValence = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCloseFace = new System.Windows.Forms.CheckBox();
            this.colorDialogAuto = new System.Windows.Forms.ColorDialog();
            this.checkBoxDuring = new System.Windows.Forms.CheckBox();
            this.groupBoxTiming = new System.Windows.Forms.GroupBox();
            this.checkBoxPost = new System.Windows.Forms.CheckBox();
            this.numericUpDownProcessPerSceond = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValence)).BeginInit();
            this.groupBoxTiming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessPerSceond)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 212);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
          
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(93, 212);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // DectionBufferLabel
            // 
            this.DectionBufferLabel.AutoSize = true;
            this.DectionBufferLabel.Location = new System.Drawing.Point(9, 170);
            this.DectionBufferLabel.Name = "DectionBufferLabel";
            this.DectionBufferLabel.Size = new System.Drawing.Size(84, 13);
            this.DectionBufferLabel.TabIndex = 1;
            this.DectionBufferLabel.Text = "Detection Buffer";
            // 
            // numericUpDownValence
            // 
            this.numericUpDownValence.DecimalPlaces = 3;
            this.numericUpDownValence.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownValence.Location = new System.Drawing.Point(12, 186);
            this.numericUpDownValence.Name = "numericUpDownValence";
            this.numericUpDownValence.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownValence.TabIndex = 0;
            this.numericUpDownValence.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownValence.ValueChanged += new System.EventHandler(this.numericUpDownValence_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Process Per Sec";
            // 
            // checkBoxCloseFace
            // 
            this.checkBoxCloseFace.AutoSize = true;
            this.checkBoxCloseFace.Location = new System.Drawing.Point(12, 51);
            this.checkBoxCloseFace.Name = "checkBoxCloseFace";
            this.checkBoxCloseFace.Size = new System.Drawing.Size(118, 17);
            this.checkBoxCloseFace.TabIndex = 4;
            this.checkBoxCloseFace.Text = "Detect Small Faces";
            this.checkBoxCloseFace.UseVisualStyleBackColor = true;
            this.checkBoxCloseFace.CheckedChanged += new System.EventHandler(this.checkBoxCloseFace_CheckedChanged);
            // 
            // colorDialogAuto
            // 
            this.colorDialogAuto.AllowFullOpen = false;
            this.colorDialogAuto.SolidColorOnly = true;
            // 
            // checkBoxDuring
            // 
            this.checkBoxDuring.AutoSize = true;
            this.checkBoxDuring.Checked = true;
            this.checkBoxDuring.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDuring.Location = new System.Drawing.Point(6, 19);
            this.checkBoxDuring.Name = "checkBoxDuring";
            this.checkBoxDuring.Size = new System.Drawing.Size(57, 17);
            this.checkBoxDuring.TabIndex = 5;
            this.checkBoxDuring.Text = "During";
            this.checkBoxDuring.UseVisualStyleBackColor = true;
            this.checkBoxDuring.CheckedChanged += new System.EventHandler(this.checkBoxDuring_CheckedChanged);
            // 
            // groupBoxTiming
            // 
            this.groupBoxTiming.Controls.Add(this.checkBoxPost);
            this.groupBoxTiming.Controls.Add(this.checkBoxDuring);
            this.groupBoxTiming.Location = new System.Drawing.Point(12, 74);
            this.groupBoxTiming.Name = "groupBoxTiming";
            this.groupBoxTiming.Size = new System.Drawing.Size(89, 74);
            this.groupBoxTiming.TabIndex = 6;
            this.groupBoxTiming.TabStop = false;
            this.groupBoxTiming.Text = "Process Time";
            // 
            // checkBoxPost
            // 
            this.checkBoxPost.AutoSize = true;
            this.checkBoxPost.Location = new System.Drawing.Point(6, 43);
            this.checkBoxPost.Name = "checkBoxPost";
            this.checkBoxPost.Size = new System.Drawing.Size(47, 17);
            this.checkBoxPost.TabIndex = 6;
            this.checkBoxPost.Text = "Post";
            this.checkBoxPost.UseVisualStyleBackColor = true;
            this.checkBoxPost.CheckedChanged += new System.EventHandler(this.checkBoxPost_CheckedChanged);
            // 
            // numericUpDownProcessPerSceond
            // 
            this.numericUpDownProcessPerSceond.Location = new System.Drawing.Point(12, 25);
            this.numericUpDownProcessPerSceond.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownProcessPerSceond.Name = "numericUpDownProcessPerSceond";
            this.numericUpDownProcessPerSceond.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownProcessPerSceond.TabIndex = 7;
            this.numericUpDownProcessPerSceond.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownProcessPerSceond.ValueChanged += new System.EventHandler(this.numericUpDownProcessPerSceond_ValueChanged);
            // 
            // AffectivaOptions
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(277, 246);
            this.Controls.Add(this.DectionBufferLabel);
            this.Controls.Add(this.numericUpDownProcessPerSceond);
            this.Controls.Add(this.numericUpDownValence);
            this.Controls.Add(this.groupBoxTiming);
            this.Controls.Add(this.checkBoxCloseFace);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AffectivaOptions";
            this.Text = "AffectivaOptions";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValence)).EndInit();
            this.groupBoxTiming.ResumeLayout(false);
            this.groupBoxTiming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessPerSceond)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label DectionBufferLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownValence;
        private System.Windows.Forms.CheckBox checkBoxCloseFace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialogAuto;
        private System.Windows.Forms.CheckBox checkBoxDuring;
        private System.Windows.Forms.GroupBox groupBoxTiming;
        private System.Windows.Forms.CheckBox checkBoxPost;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessPerSceond;
    }
}