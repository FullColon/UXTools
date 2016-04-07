using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video.FFMPEG;
using AForge.Video;
using System.Diagnostics;
using System.IO;



namespace UX_Affectiva_Research_Tool
{


 public   class myRecorder : RecordingToolBase
    {
        // Delcare Variables
        private bool mIsRecording;
        private List<string> mScreenNames;
        private UInt32 mFrameCount;
        private VideoFileWriter mWriter;
        private int mWidth;
        private int mHeight;
        private ScreenCaptureStream mStreamVideo;
        private Stopwatch mStopWatch;
        private Rectangle mScreenArea;
        private int mFramesPerSecond;
        private VideoCodec mVideoCodec;
        private int mBitRate;
        private string mScreenTitle;
        private string mFilePath;
        private string mScreenName;
        private Label mFramesPerSecondLabel;
        private Label mStopWatchLabel;
        private string mFullName;
       

        public myRecorder(ref Stopwatch _stopwatch, Label _fpsLabel, Label _stopWatchLabel)
        {
            this.mStopWatch = _stopwatch;
            this.mVideoCodec = VideoCodec.MPEG4;

            mScreenName = "Select ALL";
            foreach (Screen screen in Screen.AllScreens)
            {
                this.mScreenArea = Rectangle.Union(mScreenArea, screen.Bounds);
            }
            this.mBitRate = (int) UX_Affectiva_Research_Tool.BitRate._500kbit;

            mFramesPerSecondLabel = _fpsLabel;
            mStopWatchLabel = _stopWatchLabel;
            screenRecordSetup();
        }
        // Setters
        public void SetIsRecording(bool _bool)
        {
            mIsRecording = _bool;
        }
        public void SetScreenNames(List<string> _list)
        {
            mScreenNames = _list;
        }
        public void SetFrameCount(UInt32 _uInt32)
        {
            mFrameCount = _uInt32;
        }
        public void SetWriter(VideoFileWriter _videoFileWriter)
        {
            mWriter = _videoFileWriter;
        }
        public void SetWidth(int _width)
        {
            mWidth = _width;
        }
        public void SetHeight(int _height)
        {
            mHeight = _height;
        }
        public void SetStreamVideo(ScreenCaptureStream _screenCaptureStream)
        {
            mStreamVideo = _screenCaptureStream;
        }
        public void SetStopWatch(Stopwatch _stopWatch)
        {
            mStopWatch = _stopWatch;
        }
        public void SetScreenArea(Rectangle _rectangle)
        {
            mScreenArea = _rectangle;
        }
        public void SetFramesPerSecond(int _framesPerSecond)
        {
            mFramesPerSecond = _framesPerSecond;
        }

        public void SetVideoCodec(VideoCodec _videoCodec)
        {
            mVideoCodec = _videoCodec;
        }
        public void SetBitRate(int _bitRate)
        {
            mBitRate = _bitRate;
        }
        public void SetScreenTitle(string _screenTitle)
        {
            mScreenTitle = _screenTitle;
        }
        public void SetFilePath(string _filePath)
        {
            mFilePath = _filePath;
        }
        // Getters
        public bool GetIsRecording()
        {
            return this.mIsRecording;
        }
        public List<string> GetScreenNames()
        {
            return this.mScreenNames;
        }
        public UInt32 GetFrameCount()
        {
            return this.mFrameCount;
        }
        public VideoFileWriter GetWriter()
        {
            return this.mWriter;
        }
        public int GetWidth()
        {
            return this.mWidth;
        }
        public int GetHeight()
        {
            return this.mHeight;
        }
        public ScreenCaptureStream GetStreamVideo()
        {
            return this.mStreamVideo;
        }
        public Stopwatch GetStopWatch()
        {
            return this.mStopWatch;
        }
        public Rectangle GetScreenArea()
        {
            return this.mScreenArea;
        }
        public int GetFramesPerSecond()
        {
            return this.mFramesPerSecond;
        }
        public VideoCodec GetVideoCodec()
        {
            return this.mVideoCodec;
        }
        public int GetBitRate()
        {
            return this.mBitRate;
        }
        public string GetScreenTitle()
        {
            return this.mScreenTitle;
        }

        public string GetFilePath()
        {
            return this.mFilePath;
        }
        public string GetFullName()
        {
            return this.mFullName;
        }






        public void screenRecordSetup()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string newDir = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));

            mFilePath = newDir + @"\SaveFolder";






            this.SetScreenArea();
            this.mFrameCount = 0;
            this.mWriter = new VideoFileWriter();
            this.mWidth = SystemInformation.VirtualScreen.Width;
            this.mHeight = SystemInformation.VirtualScreen.Height;
            this.mFramesPerSecond = 10;

            mFullName = string.Format(@"{0}\{1}_{2}.avi", mFilePath, Environment.UserName.ToUpper(), DateTime.Now.ToString("d_MMM_yyyy_HH_mm_ssff"));


        }

        public override bool startRecording()
        {
            // Save File option
            mWriter.Open(mFullName, this.mWidth, this.mHeight, this.mFramesPerSecond, this.mVideoCodec, this.mBitRate);
            mIsRecording = true;
            try
            {


                // create screen capture video source
                this.mStreamVideo = new ScreenCaptureStream(this.mScreenArea);

                // set NewFrame event handler
                this.mStreamVideo.NewFrame += new NewFrameEventHandler(this.video_NewFrame);

                // start the video source
                this.mStreamVideo.Start();

                // _stopWatch
                this.mStopWatch.Start();
               

                return true;
            }

            catch(Exception e)
            {
                MessageBox.Show("An error occurred: '{0}'", e.ToString());
                return false;
            }

          
        }

 
        private void SetScreenArea()
        {
            // get entire desktop area size
           
           if (string.Compare(this.mScreenName, @"Select ALL", StringComparison.OrdinalIgnoreCase) == 0)
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    this.mScreenArea = Rectangle.Union(mScreenArea, screen.Bounds);
                }
            }
            else
            {
                this.mScreenArea = Screen.AllScreens.First(scr => scr.DeviceName.Equals(this.mScreenName)).Bounds;
                this.mWidth = this.mScreenArea.Width;
                this.mHeight = this.mScreenArea.Height;
            } 
        }

       public void SetFramesPerSecondLabel()
        {
             mFramesPerSecondLabel.Invoke(new Action(() =>
            {
                mFramesPerSecondLabel.Text = string.Format(@"Frames: {0}", mFrameCount);
            }));
        }

        public void SetStopWatchLabel()
        {
            mStopWatchLabel.Invoke(new Action(() =>
            {
                mStopWatchLabel.Text = mStopWatch.Elapsed.ToString();
            }));
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (this.mIsRecording)
            {
                this.mFrameCount++;
                this.mWriter.WriteVideoFrame(eventArgs.Frame);

                SetFramesPerSecondLabel();

                SetStopWatchLabel();
               
             
            }
            else
            {
                mStopWatch.Reset();
                Thread.Sleep(500);
                mStreamVideo.SignalToStop();
                Thread.Sleep(500);
                mWriter.Close();
            }
        }

        private void displayFileSavedMesage()
        {
 //           this.SetVisible(false);
            MessageBox.Show(@"File saved!");
        }

        /*        private void SetVisible(bool visible)
              {
                   this.bt_start.Enabled = !visible;
                   this.bt_Save.Enabled = visible;
                   this.mIsRecording = visible;
               } 
       */

        public override bool stopRecording()
        {
            SetIsRecording(false);
            return true;
        }

 


    }

    public enum BitRate
    {
        _50kbit = 5000,
        _100kbit = 10000,
        _500kbit = 50000,
        _1000kbit = 1000000,
        _2000kbit = 2000000,
        _3000kbit = 3000000
    }
}
