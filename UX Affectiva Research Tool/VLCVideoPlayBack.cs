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
    public partial class VLCVideoPlayBack : DockContent
    {
        Vlc.DotNet.Forms.VlcControl player;
        string[] p;
        public VLCVideoPlayBack(string _FilePath)
        {
            InitializeComponent();
            player = new Vlc.DotNet.Forms.VlcControl();
            this.Controls.Add(player);
            player.Dock = DockStyle.Fill;
            Uri filepath = new Uri(_FilePath);
            p = new string[2] { "avi", "wav" };
            player.SetMedia(filepath,p);
            player.Show();
            
        }

       
    }
}
