using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace UX_Affectiva_Research_Tool
{
    public partial class ScreenCapturePlaybackWindow : DockContent
    {
        
       
        private string vidoeFile;
     
        public ScreenCapturePlaybackWindow()
        {
            InitializeComponent();
  /*          OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                vidoeFile = openFileDialog.FileName;
            }
  */          
            axWindowsMediaPlayer1.URL = vidoeFile;
           // axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        public ScreenCapturePlaybackWindow(string _FilePath)
        {
            InitializeComponent();
      //    openFileDialog = new OpenFileDialog();
            vidoeFile = _FilePath;

            axWindowsMediaPlayer1.URL = vidoeFile;
            //axWindowsMediaPlayer1.Ctlcontrols.play();
        }
       

        private void ScreenCapturePlaybackWindow_DockStateChanged(object sender, EventArgs e)
        {

            if (this.DockState == DockState.Unknown)
                Console.WriteLine("Unkown");


           
            // axWindowsMediaPlayer1.URL = vidoeFile;



        }

        private void axWindowsMediaPlayer1_Disconnect(object sender, AxWMPLib._WMPOCXEvents_DisconnectEvent e)
        {

        }

        private void axWindowsMediaPlayer1_DeviceSyncError(object sender, AxWMPLib._WMPOCXEvents_DeviceSyncErrorEvent e)
        {

        }

        private void axWindowsMediaPlayer1_DeviceDisconnect(object sender, AxWMPLib._WMPOCXEvents_DeviceDisconnectEvent e)
        {

        }

        private void axWindowsMediaPlayer1_PlayerDockedStateChange(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_LibraryDisconnect(object sender, AxWMPLib._WMPOCXEvents_LibraryDisconnectEvent e)
        {

        }

       
    }
}
