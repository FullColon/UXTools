namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    partial class ManuelTagWindow
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
            this.TagButton = new System.Windows.Forms.Button();
            this.NameOfTagTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NameOfTagLabel = new System.Windows.Forms.Label();
            this.DescriptonLabel = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.EmotionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TagButton
            // 
            this.TagButton.Location = new System.Drawing.Point(10, 163);
            this.TagButton.Name = "TagButton";
            this.TagButton.Size = new System.Drawing.Size(75, 23);
            this.TagButton.TabIndex = 0;
            this.TagButton.Text = "AddTag";
            this.TagButton.UseVisualStyleBackColor = true;
            this.TagButton.Click += new System.EventHandler(this.TagButton_Click);
            // 
            // NameOfTagTextBox
            // 
            this.NameOfTagTextBox.Location = new System.Drawing.Point(6, 42);
            this.NameOfTagTextBox.Name = "NameOfTagTextBox";
            this.NameOfTagTextBox.Size = new System.Drawing.Size(100, 20);
            this.NameOfTagTextBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EmotionComboBox);
            this.groupBox1.Controls.Add(this.DescriptionTextBox);
            this.groupBox1.Controls.Add(this.DescriptonLabel);
            this.groupBox1.Controls.Add(this.NameOfTagLabel);
            this.groupBox1.Controls.Add(this.NameOfTagTextBox);
            this.groupBox1.Controls.Add(this.TagButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 192);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // NameOfTagLabel
            // 
            this.NameOfTagLabel.AutoSize = true;
            this.NameOfTagLabel.Location = new System.Drawing.Point(7, 23);
            this.NameOfTagLabel.Name = "NameOfTagLabel";
            this.NameOfTagLabel.Size = new System.Drawing.Size(71, 13);
            this.NameOfTagLabel.TabIndex = 2;
            this.NameOfTagLabel.Text = "Name Of Tag";
            // 
            // DescriptonLabel
            // 
            this.DescriptonLabel.AutoSize = true;
            this.DescriptonLabel.Location = new System.Drawing.Point(10, 69);
            this.DescriptonLabel.Name = "DescriptonLabel";
            this.DescriptonLabel.Size = new System.Drawing.Size(60, 13);
            this.DescriptonLabel.TabIndex = 3;
            this.DescriptonLabel.Text = "Description";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(6, 85);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(100, 20);
            this.DescriptionTextBox.TabIndex = 4;
            // 
            // EmotionComboBox
            // 
            this.EmotionComboBox.FormattingEnabled = true;
            this.EmotionComboBox.Items.AddRange(new object[] {
            "Sadness",
            "Anger",
            "Disgust",
            "Fear",
            "Joy",
            "Surprise",
            "Contempt",
            "Engagment",
            "Valence"});
            this.EmotionComboBox.Location = new System.Drawing.Point(6, 111);
            this.EmotionComboBox.Name = "EmotionComboBox";
            this.EmotionComboBox.Size = new System.Drawing.Size(121, 21);
            this.EmotionComboBox.TabIndex = 5;
            // 
            // ManuelTagWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 213);
            this.Controls.Add(this.groupBox1);
            this.Name = "ManuelTagWindow";
            this.Text = "ManuelTagWindow";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TagButton;
        private System.Windows.Forms.TextBox NameOfTagTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label DescriptonLabel;
        private System.Windows.Forms.Label NameOfTagLabel;
        private System.Windows.Forms.ComboBox EmotionComboBox;
    }
}