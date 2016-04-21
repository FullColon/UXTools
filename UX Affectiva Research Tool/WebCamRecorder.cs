using DirectX.Capture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UX_Affectiva_Research_Tool
{
   
    class WebCamRecorder:RecordingToolBase
    {
        Filters fnl;
        Capture cp;
       // Timer v;
       
        public WebCamRecorder(ref Panel _panel)
        {
           // v = new Timer();

           
            fnl = new Filters();

            for (var i = 0; i < fnl.VideoCompressors.Count; i++)
            {
                Console.WriteLine(fnl.VideoCompressors[i].Name);
               
            }
            cp = new Capture(fnl.VideoInputDevices[0], fnl.AudioInputDevices[0]);
            cp.Filename = "C:\\DFiles\\Downloads" + "captured-video.avi";
        
            cp.VideoCompressor = fnl.VideoCompressors[6];
            cp.PreviewWindow = _panel;
           
        }
        public override bool startRecording()
        {
            try
            {

                //cp.Cue();
                cp.Start();
                //v.Start();


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error Message: \n\n" + ex.Message);
            }
            return true;
        }
        public override bool stopRecording()
        {
            cp.Stop();
            return true;
        }
    }
}
