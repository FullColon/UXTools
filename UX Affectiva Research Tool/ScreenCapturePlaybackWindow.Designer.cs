namespace UX_Affectiva_Research_Tool
{
    partial class ScreenCapturePlaybackWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenCapturePlaybackWindow));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(384, 361);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            this.axWindowsMediaPlayer1.Disconnect += new AxWMPLib._WMPOCXEvents_DisconnectEventHandler(this.axWindowsMediaPlayer1_Disconnect);
            this.axWindowsMediaPlayer1.PlayerDockedStateChange += new System.EventHandler(this.axWindowsMediaPlayer1_PlayerDockedStateChange);
            this.axWindowsMediaPlayer1.DeviceDisconnect += new AxWMPLib._WMPOCXEvents_DeviceDisconnectEventHandler(this.axWindowsMediaPlayer1_DeviceDisconnect);
            this.axWindowsMediaPlayer1.DeviceSyncError += new AxWMPLib._WMPOCXEvents_DeviceSyncErrorEventHandler(this.axWindowsMediaPlayer1_DeviceSyncError);
            this.axWindowsMediaPlayer1.LibraryDisconnect += new AxWMPLib._WMPOCXEvents_LibraryDisconnectEventHandler(this.axWindowsMediaPlayer1_LibraryDisconnect);
            // 
            // ScreenCapturePlaybackWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ScreenCapturePlaybackWindow";
            this.Text = "ScreenCapturePlaybackWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.DockStateChanged += new System.EventHandler(this.ScreenCapturePlaybackWindow_DockStateChanged);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}