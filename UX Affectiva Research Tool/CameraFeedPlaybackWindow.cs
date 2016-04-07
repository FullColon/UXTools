using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
namespace UX_Affectiva_Research_Tool
{
    public partial class CameraFeedPlaybackWindow : DockContent
    {
        private string cameraFile;
        public CameraFeedPlaybackWindow()
        {
            InitializeComponent();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cameraFile = openFileDialog.FileName;
            }

            axWindowsMediaPlayer1.URL = cameraFile;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
    }
}
