using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    public partial class ManuelTagWindow : Form
    {
        AffectivaDataRecordingEmotionsandExpressions mAffCamFacRec;
        public AffectivaDataRecordingEmotionsandExpressions getAffData()
        {
            return mAffCamFacRec;
        }
        Stopwatch stopWatch;
        public ManuelTagWindow( Stopwatch _stopWatch)
        {
            InitializeComponent();
            mAffCamFacRec =new AffectivaDataRecordingEmotionsandExpressions();
            stopWatch = _stopWatch;
            EmotionComboBox.SelectedIndex = 0;
        }
      
        private void TagButton_Click(object sender, EventArgs e)
        {
           
                    if( stopWatch.IsRunning)
                mAffCamFacRec.addManuelTag(stopWatch.Elapsed.Seconds,DescriptionTextBox.Text, (uint)EmotionComboBox.SelectedIndex, NameOfTagTextBox.Text);

            
        }
        
    }
}
