using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{/// <summary>
/// This Class Is meant to combine Affectiva and combine Camera Recording Since Aforge.ffmpeg causes and issue of compiling and Losing track
/// of form based thread
/// So This will handel both
/// </summary>
    public class AffectivaCameraFaceRecordingAndVideoRecording: RecordingToolBase
    {
        AffectivaCameraRecordingTool mCameraAffectivaRecorder;
        Affectiva_Files.VideoStreamFileSaver mFilerWriterForVideo;
      //  ManuelTagWindow ManulTag;
        public Affectiva_Files.VideoStreamFileSaver getFileWriterVideo()
        {
            return mFilerWriterForVideo;
        }
        public AffectivaCameraRecordingTool getCameraAffectivaRecorder()
        {
            return mCameraAffectivaRecorder;
        }
        /// <summary>
        /// This is for turning off the mFileStreamThread so it can collect the data as a whole and not leave any frames behind
        /// </summary>
        bool moff;
        /// <summary>
        /// This Thread IS for setting up Filer Writer to Receive and Write Video From Affectiva to FileWriter
        /// </summary>
        System.Threading.Thread mFileStreamThread;
        /// <summary>
        /// Set up Cam for affectiva and give the mFileStream Thread ready for receiving bitmaps
        /// </summary>
        public AffectivaCameraFaceRecordingAndVideoRecording()
        {
          //  mCameraAffectivaRecorder = new AffectivaCameraRecordingTool();

         //   mFilerWriterForVideo = new Affectiva_Files.VideoStreamFileSaver(mCameraAffectivaRecorder);
           // mFileStreamThread = new System.Threading.Thread(FilerStreamerThread);
        }
        public AffectivaCameraFaceRecordingAndVideoRecording(String _FilePath, float _valenceEmotion, float _timeStep, int _cameraId, int _camperaCaptureRate, double _processRate, bool _FaceDetection)
        {
            mCameraAffectivaRecorder = new AffectivaCameraRecordingTool( _valenceEmotion,  _timeStep,  _cameraId, (double) _camperaCaptureRate,  _processRate, _FaceDetection);

            mFilerWriterForVideo = new Affectiva_Files.VideoStreamFileSaver(mCameraAffectivaRecorder,_FilePath,(int)_camperaCaptureRate);
            mFileStreamThread = new System.Threading.Thread(FilerStreamerThread);
        }
        /// <summary>
        /// mCameraAffectRecoder starts and loads camera
        /// then wait while its running and wait for the first frame
        /// then open FileWriterStreamer and start the Thread for readin frame
        /// </summary>
        /// <returns></returns>
        public override bool startRecording()
        {
           // ManulTag = new ManuelTagWindow(this);
            mCameraAffectivaRecorder.startRecording();
            while (mCameraAffectivaRecorder.isRunningRecording() && mCameraAffectivaRecorder.lastframe.Count < 1)
            {

            }
            if (mCameraAffectivaRecorder.isRunningRecording())
            {
                mFilerWriterForVideo.OpenStream(mCameraAffectivaRecorder.lastframe.Dequeue());
                mFileStreamThread.Start();
        //        ManulTag.Show();
            }
            return true;
        }
        /// <summary>
        /// Stop affectiva from running and turn the off varible
        /// </summary>
        /// <returns></returns>
        public override bool stopRecording()
        {
            mCameraAffectivaRecorder.stopRecording();
            moff = true;
            //ManulTag.Close();
            return true;
        }
        /// <summary>
        /// This Is meant to be apply to a thread so it can constantly run can check for frames and not stop the program
        /// first make sure off is set to false
        /// then while not off check for frames and pass to filewriterforvideo
        /// when is off
        /// exit current while loop
        /// go to loop to finish out frames to process
        /// </summary>
        void FilerStreamerThread()
        {
            moff = false;

            while (!moff)
            {
                if (mCameraAffectivaRecorder.lastframe.Count > 0)
                {
                    Affdex.Frame frame = mCameraAffectivaRecorder.lastframe.Dequeue();
                    mFilerWriterForVideo.AddBitmap(frame.getWidth(), frame.getHeight(), frame.getBGRByteArray(), frame.getColorFormat(), frame.getTimestamp());
                    System.Threading.Thread.Sleep(1);
                }
            }

            while (mCameraAffectivaRecorder.lastframe.Count > 0)
            {
                Affdex.Frame frame = mCameraAffectivaRecorder.lastframe.Dequeue();
                mFilerWriterForVideo.AddBitmap(frame.getWidth(), frame.getHeight(), frame.getBGRByteArray(), frame.getColorFormat(), frame.getTimestamp());
                System.Threading.Thread.Sleep(1);
            }
            mFilerWriterForVideo.CloseFileSaver();
        }
        /// <summary>
        /// this is meant to pass out Affectiva data back to a graph to process information
        /// </summary>
        /// <returns></returns>
        public AffectivaDataRecordingEmotionsandExpressions GetAffectiveData()
        {
            return mCameraAffectivaRecorder.GetAffectiveData();
        }
    }
}
