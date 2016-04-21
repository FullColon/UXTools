using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affdex;
using System.IO;
namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    /// <summary>
    /// This Class is Soley Designed to Stream BitMaps into a File Streamer and Save it;
    /// </summary>
   public class VideoStreamFileSaver
    {
        private AForge.Video.FFMPEG.VideoFileWriter mFileWriter = new AForge.Video.FFMPEG.VideoFileWriter();
        private System.Windows.Forms.SaveFileDialog mSaveAvi;
        bool mOpened = false;
        /// <summary>
        /// Byte Arrays for bitmas need allocated memory, this will speed up the process
        /// </summary>
        IntPtr mAllocatedPointer;
        int countofFramesToadd = 0;
        AffectivaCameraRecordingTool mCamDect;
        string mFilePath;
        public string getFilePath()
        {
            return mFilePath;
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
        public VideoStreamFileSaver(AffectivaCameraRecordingTool _camdect, String _BaseFilePath)
        {
            mSaveAvi = new SaveFileDialog();
            mSaveAvi.Filter = "Avi Files (*.avi)|*.avi";
            mCamDect = _camdect;

            //string currentDirectory = Environment.CurrentDirectory;
           // string newDir = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
           // mFilePath = newDir + @"\SaveFolder\TestSave.avi";
            mFilePath = _BaseFilePath + "_TestSave.avi";
        }
        /// <summary>
        /// This needs a frame to start for height and width, will change when setting up camera resoultion section
        /// </summary>
        /// <param name="_currenFrame"></param>
        public void OpenStream(Frame _currenFrame)
        {
            System.Drawing.Bitmap currentImage=FrameToBitmap(_currenFrame);
            int h = currentImage.Height;
            int w = currentImage.Width;
           // if (mSaveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
             
            mFileWriter.Open(mFilePath, w, h, 30, AForge.Video.FFMPEG.VideoCodec.Default, 5000000);
           
            mAllocatedPointer = System.Runtime.InteropServices.Marshal.AllocHGlobal(_currenFrame.getBGRByteArray().Length);
            mOpened = true;
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
        /// </summary>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="pixels"></param>
        /// <param name="frameColorFormat"></param>
        /// <param name="timestamp"></param>
        public void AddBitmap(int frameWidth, int frameHeight, byte[] pixels, Frame.COLOR_FORMAT frameColorFormat, float timestamp)
        {

           
                Frame _currentImage = new Frame(frameWidth, frameHeight,(byte[]) pixels.Clone(), frameColorFormat, timestamp);

                System.Runtime.InteropServices.Marshal.Copy(_currentImage.getBGRByteArray(), 0, mAllocatedPointer, _currentImage.getBGRByteArray().Length);

                System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(_currentImage.getWidth(), _currentImage.getHeight(), (_currentImage.getWidth() * System.Windows.Media.PixelFormats.Bgr24.BitsPerPixel + 7) / 8,
                             System.Drawing.Imaging.PixelFormat.Format24bppRgb, mAllocatedPointer);
                try
                {
                    mFileWriter.WriteVideoFrame(_bitmap);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                if (mOpened)
                    countofFramesToadd--;
                else
                    mFileWriter.Close();
            
           
           //     System.Runtime.InteropServices.Marshal.FreeHGlobal(pt);
        }
        public void CloseFileSaver()
        {

            mOpened = false;
            mFileWriter.Close();

        }
        public bool IsOpen()
        {
            return mFileWriter.IsOpen;
        }
    }
}
