using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affdex;
using System.IO;
using SharpAvi.Output;
using SharpAvi.Codecs;
using SharpAvi;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    /// <summary>
    /// This Class is Soley Designed to Stream BitMaps into a File Streamer and Save it;
    /// </summary>
   public class VideoStreamFileSaver
    {
        IAviVideoStream mVideoStream;
        AviWriter mWriter;
        //   private AForge.Video.FFMPEG.VideoFileWriter mFileWriter = new AForge.Video.FFMPEG.VideoFileWriter();
        private System.Windows.Forms.SaveFileDialog mSaveAvi;
        bool mOpened = false;
        /// <summary>
        /// Byte Arrays for bitmas need allocated memory, this will speed up the process
        /// </summary>
        IntPtr mAllocatedPointer;
      
        
        string mFilePath;
        int mScreenHeight;
         int mScreenWidth;
        byte[] mCopyToStreammBuffer;

        public string getFilePath()
        {
            return mFilePath;
        }
        /// <summary>
        /// Set up Codec and stream for videowriter
        /// </summary>
        /// <param name="codec"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        private IAviVideoStream CreateVideoStream(FourCC codec, int quality)
        {
            // Select encoder type based on FOURCC of codec
            if (codec == SharpAvi.KnownFourCCs.Codecs.Uncompressed)
            {
                return mWriter.AddUncompressedVideoStream(mScreenWidth, mScreenHeight);
            }
            else if (codec == KnownFourCCs.Codecs.MotionJpeg)
            {
                return mWriter.AddMotionJpegVideoStream(mScreenWidth, mScreenHeight, quality);
            }
            else
            {
                return mWriter.AddMpeg4VideoStream(mScreenWidth, mScreenHeight, (double)mWriter.FramesPerSecond,
                    // It seems that all tested MPEG-4 VfW codecs ignore the quality affecting parameters passed through VfW API
                    // They only respect the settings from their own configuration dialogs, and Mpeg4VideoEncoder currently has no support for this
                    quality: quality,
                    codec: codec,
                    // Most of VfW codecs expect single-threaded use, so we wrap this encoder to special wrapper
                    // Thus all calls to the encoder (including its instantiation) will be invoked on a single thread although encoding (and writing) is performed asynchronously
                    forceSingleThreadedAccess: true);
            }
            
        }
        /// <summary>
        ///  Set Up SaveFileDialog to AVI
        /// </summary>
        public VideoStreamFileSaver()
        {

            mSaveAvi = new SaveFileDialog();
            mSaveAvi.Filter = "Avi Files (*.avi)|*.avi";
            string currentDirectory = Environment.CurrentDirectory;
            string newDir = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
            mFilePath = newDir + @"\SaveFolder\TestSave.avi";
        }
        /// <summary>
        /// 
        /// Pass in Affectiva Camera Tool to receive frames from it;
        /// </summary>
        /// <param name="_camdect"></param>
        public VideoStreamFileSaver(AffectivaCameraRecordingTool _camdect)
        {
            mSaveAvi = new SaveFileDialog();
            mSaveAvi.Filter = "Avi Files (*.avi)|*.avi";
          

            string currentDirectory = Environment.CurrentDirectory;
            string newDir = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
            mFilePath = newDir + @"\SaveFolder\TestSave.avi";
        }
        /// <summary>
        /// This needs a frame to start for height and width, will change when setting up camera resoultion section
        /// </summary>
        /// <param name="_currenFrame"></param>
        public void OpenStream(Frame _currenFrame)
        {
           
            System.Drawing.Bitmap currentImage=FrameToBitmap(_currenFrame);
             mScreenHeight = currentImage.Height;
             mScreenWidth = currentImage.Width;
           
            mWriter = new AviWriter(mFilePath);
           
            mWriter.FramesPerSecond = 30;
            mVideoStream = CreateVideoStream(KnownFourCCs.Codecs.MotionJpeg, 20);
           
            mVideoStream.Height = _currenFrame.getHeight();
            mVideoStream.Width = _currenFrame.getWidth();
            mAllocatedPointer = System.Runtime.InteropServices.Marshal.AllocHGlobal(_currenFrame.getBGRByteArray().Length);
            mOpened = true;
            mCopyToStreammBuffer = new byte[mScreenWidth * mScreenHeight * 4];
            AddBitmap(_currenFrame.getWidth(), _currenFrame.getHeight(), _currenFrame.getBGRByteArray(), _currenFrame.getColorFormat(), _currenFrame.getTimestamp());
        }
        /// <summary>
        /// TUrning frames to bitmaps
        /// </summary>
        /// <param name="_currenFrame"></param>
        /// <returns></returns>
        public System.Drawing.Bitmap FrameToBitmap(Frame _currenFrame)
        {
            return new System.Drawing.Bitmap(_currenFrame.getWidth(), _currenFrame.getHeight(), (_currenFrame.getWidth() * System.Windows.Media.PixelFormats.Bgr24.BitsPerPixel + 7) / 8,
                         System.Drawing.Imaging.PixelFormat.Format24bppRgb, mAllocatedPointer);
        }
        /// <summary>
        /// Adds a Bit map to a stream
        /// by turning frame into a bitmap
        /// then convert bitmap into proper pixel format for streaming
        /// then passing it to stream
        /// </summary>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="pixels"></param>
        /// <param name="frameColorFormat"></param>
        /// <param name="timestamp"></param>
        public void AddBitmap(int frameWidth, int frameHeight, byte[] pixels, Frame.COLOR_FORMAT frameColorFormat, float timestamp)
        {


            // Frame _currentImage = new Frame(frameWidth, frameHeight,(byte[]) pixels.Clone(), frameColorFormat, timestamp);
             ///setup bitmap from frame
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, mAllocatedPointer, pixels.Length);
           System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(frameWidth, frameHeight, (frameWidth * System.Windows.Media.PixelFormats.Bgr24.BitsPerPixel + 7) / 8,
                        System.Drawing.Imaging.PixelFormat.Format24bppRgb, mAllocatedPointer);
         
            /// convert bitmap into proper format for stream
            var bmp = new Bitmap(_bitmap.Width, _bitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(_bitmap, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height));

           // place into buffer
            var bits = bmp.LockBits(new Rectangle(0, 0, frameWidth, frameHeight), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Marshal.Copy(bits.Scan0, mCopyToStreammBuffer, 0, mCopyToStreammBuffer.Length);
            bmp.UnlockBits(bits);
            try
                {
                
              
                /// pass in buffer
               mVideoStream.WriteFrame(true, mCopyToStreammBuffer, 0, mCopyToStreammBuffer.Length);
               
                
            }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                
          
            
           
             
        }
        /// <summary>
        /// shuts mwriter off
        /// </summary>
        public void CloseFileSaver()
        {

            mOpened = false;
         
          
            mWriter.Close();
            System.Runtime.InteropServices.Marshal.FreeHGlobal(mAllocatedPointer);

        }
        /// <summary>
        /// passes back variable for checking if streamer is open
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            // return mFileWriter.IsOpen;
            return mOpened;
        }
    }
}
