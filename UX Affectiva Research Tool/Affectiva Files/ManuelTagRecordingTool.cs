using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    class ManuelTagRecordingTool: RecordingToolBase
    {
        AffectivaDataRecordingEmotionsandExpressions AffCamFacRec;
        PostAffectivaEvaltionTool PostEvaulTool;
        ManuelTagWindow ManuelWindow;
        WebRecording GraphWindow;
       
        double fpsProcess;
        bool PostProcess;
      

        public ManuelTagRecordingTool(Stopwatch _stopWatch, double _fpsPost, bool _PostProcess)
        {
            
            PostProcess= _PostProcess;
            
            fpsProcess = _fpsPost;
        
            ManuelWindow = new ManuelTagWindow(_stopWatch);
        }
        public override bool startRecording()
        {
            ManuelWindow.Show();
            return true;
            
        }
        public override bool stopRecording()
        {
            AffCamFacRec = ManuelWindow.getAffData();
            ManuelWindow.Close();

            return true;
        }
        public WebRecording GetGraphWindow(string _path)
        {
            if (GraphWindow == null)
            {
                GraphWindow = new WebRecording(AffCamFacRec,true);
            }
            if (PostProcess)
            {
                PostEvaulTool = new PostAffectivaEvaltionTool(_path, fpsProcess, GraphWindow);
                GraphWindow.AffData = PostEvaulTool.GetAffectiveData();
                PostEvaulTool.startRecording();
            }
          
            return GraphWindow;
        }
       
    }
}
