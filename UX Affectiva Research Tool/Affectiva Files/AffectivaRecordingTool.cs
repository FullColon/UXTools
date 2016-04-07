using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affdex;

namespace UX_Affectiva_Research_Tool
{
    /// <summary>
    /// this class is designed to detect the face and pull information with the cameraa dectector objec
    /// It uses the listeners to give when an event happens.
    /// </summary>
    class AffectivaWebCameraRecordingTool : RecordingToolBase, Affdex.FaceListener, Affdex.ProcessStatusListener, Affdex.ImageListener
    {
    
        /// <summary>
        /// Here is a default setup for
        /// </summary>
        public AffectivaWebCameraRecordingTool()
        {
         
        }
       
        public override bool startRecording()
        {
            return base.startRecording();
        }
        public void onFaceFound(float timestamp, int faceId)
        {
            throw new NotImplementedException();
        }

        public void onFaceLost(float timestamp, int faceId)
        {
            throw new NotImplementedException();
        }

        public void onProcessingException(AffdexException ex)
        {
            throw new NotImplementedException();
        }

        public void onProcessingFinished()
        {
            throw new NotImplementedException();
        }

        public void onImageResults(Dictionary<int, Face> faces, Frame frame)
        {
            throw new NotImplementedException();
        }

        public void onImageCapture(Frame frame)
        {
            throw new NotImplementedException();
        }
    }
}
