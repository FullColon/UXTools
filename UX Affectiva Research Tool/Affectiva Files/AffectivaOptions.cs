using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    public partial class AffectivaOptions : Form
    {
        AffectOptions Information;

        public AffectOptions InformationSetup
        {
            get
            {
                return Information;
            }

            set
            {
                Information = value;
            }
        }

        public AffectivaOptions()
        {
            InitializeComponent();
            Information = new AffectOptions();
        }
        private void SetUpInformation()
        {
            Information.FaceMode = Affdex.FaceDetectorMode.SMALL_FACES;
            Information.DectectionValence = 5;
            Information.Post = true;
            Information.ProcessPerSec = 20;
        }
        private void checkBoxDuring_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPost.Checked = !checkBoxDuring.Checked;
        }

        private void checkBoxPost_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxDuring.Checked  = !checkBoxPost.Checked;
        }

        private void checkBoxCloseFace_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCloseFace.Checked)
                Information.FaceMode = Affdex.FaceDetectorMode.SMALL_FACES;
            else
                Information.FaceMode = Affdex.FaceDetectorMode.LARGE_FACES;
        }

        

        private void numericUpDownValence_ValueChanged(object sender, EventArgs e)
        {
            Information.DectectionValence = (double) numericUpDownValence.Value;
        }

        private void numericUpDownProcessPerSceond_ValueChanged(object sender, EventArgs e)
        {
            Information.DectectionValence = (double)numericUpDownProcessPerSceond.Value;
        }
    }


    public class AffectOptions
    {
      public  bool Post=false;
      public  Affdex.FaceDetectorMode FaceMode = Affdex.FaceDetectorMode.SMALL_FACES;
      public  int ProcessPerSec = 10;
      public  double DectectionValence = .1d;
    }
}
