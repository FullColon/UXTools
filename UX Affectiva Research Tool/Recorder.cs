using System;
using System.Threading;
using System.Threading.Tasks;
using SharpAvi.Codecs;
using SharpAvi;
using NAudio.Wave;
using SharpAvi.Output;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using UX_Affectiva_Research_Tool;
using System.Reflection;
using System.IO;

namespace RecordingTool
{
    class Recorder : RecordingToolBase
    {
        private AudioDevice mAudioDevice;
        private CodecInfo mCodecInfo;
        AviWriter mWriter;
        IAviAudioStream mAudioStream;
        WaveFormat mWaveFormat;
        SupportedWaveFormat suppForm;
        WaveInEvent mAudioSource;
        private Thread mScreenThread;
        private int mScreenWidth;
        private int mScreenHeight;
        IAviVideoStream mVideoStream;
        private int mQuality = 100;
        private FourCC mSelectedCodec;
        private int mFramesPerSecond;
        dynamic mEncoder;
        private bool mIsEncoded;
        private string mFullPath;
        private string mFilePath;

        public string GetFullName()
        {
            return mFullPath;
        }


        private readonly ManualResetEvent mStopThread = new ManualResetEvent(false);
        private readonly AutoResetEvent mVideoFrameWritten = new AutoResetEvent(false);
        private readonly AutoResetEvent mAudioBlockWritten = new AutoResetEvent(false);



        public Recorder(AudioDevice _myDevice, CodecInfo _myCodec, int _framesPerSecond, int _quality)
        {



            mAudioDevice = _myDevice;
            mCodecInfo = _myCodec;
            mFramesPerSecond = _framesPerSecond;
            mQuality = _quality;

            switch (_myCodec.Name)
            {
                case "x264vfw - H.264 / MPEG - 4 AVC codec":
                    {
                        mSelectedCodec = mCodecInfo.Codec;
                    }
                    break;
                default:
                    {
                        mIsEncoded = false;
                    }
                    break;
            }

            mScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            mScreenHeight = Screen.PrimaryScreen.Bounds.Height;


            mEncoder = mSelectedCodec;



            InitializeRecorder();
        }

        public void InitializeRecorder()
        {
            System.Windows.Media.Matrix toDevice;
            using (var source = new HwndSource(new HwndSourceParameters()))
            {
                toDevice = source.CompositionTarget.TransformToDevice;
            }


            string currentDirectory = Environment.CurrentDirectory;
            string saveDirectory = @"\SaveFolder";
            mFilePath = currentDirectory + saveDirectory;
            mFullPath = mFilePath + @"\screenRecord.avi";


            mWriter = new AviWriter("test.avi")
            {
                FramesPerSecond = 10,
                EmitIndex1 = true,
            };


            if (mIsEncoded)
            {
                mVideoStream = mWriter.AddEncodingVideoStream(mEncoder, width: mScreenWidth, height: mScreenHeight);
            }
            else
            {
                mVideoStream = CreateVideoStream(mCodecInfo.Codec, mQuality);
            }

            mVideoStream.Name = "Screencast";





            suppForm = SupportedWaveFormat.WAVE_FORMAT_44S16;
            mWaveFormat = ToWaveFormat(suppForm);


            mAudioStream = CreateAudioStream(mWaveFormat, true, 192);
            mAudioStream.Name = "Voice";
            mAudioSource = new WaveInEvent
            {
                DeviceNumber = mAudioDevice.Value,
                WaveFormat = mWaveFormat,
                BufferMilliseconds = (int)Math.Ceiling(1000 / mWriter.FramesPerSecond),
                NumberOfBuffers = 3,
            };
            mAudioSource.DataAvailable += audioSource_DataAvailable;




        }
       
        override public bool stopRecording()
        {

            Dispose();

            return true;

        }
        private void Dispose()
        {
            mStopThread.Set();
            mScreenThread.Join();
            if (mAudioSource != null)
            {
                mAudioSource.StopRecording();
                mAudioSource.DataAvailable -= audioSource_DataAvailable;
            }

            // Close writer: the remaining data is written to a file and file is closed
            mWriter.Close();

            mStopThread.Close();
        }

        override public bool startRecording()
        {

            mScreenThread = new Thread(RecordScreen)
            {
                Name = typeof(Recorder).Name + ".RecordScreen",
                IsBackground = true
            };
            if (mAudioSource != null)
            {
                mVideoFrameWritten.Set();
                mAudioBlockWritten.Reset();
                mAudioSource.StartRecording();
            }
            mScreenThread.Start();

            return true;
        }
        private void RecordScreen()
        {
            Stopwatch stopwatch = new Stopwatch();

            byte[] buffer = new byte[mScreenWidth * mScreenHeight * 4];

            Task videoWriteTask = null;
            bool isFirstFrame = true;
            int shotsTaken = 0;
            TimeSpan timeTillNextFrame = TimeSpan.Zero;



            stopwatch.Start();





            while (!mStopThread.WaitOne(timeTillNextFrame))
            {
                GetScreenshot(buffer);
                shotsTaken++;

                if (!isFirstFrame)
                {
                    videoWriteTask.Wait();



                    mVideoFrameWritten.Set();
                }
                if (mAudioSource != null)
                {
                    var signalled = WaitHandle.WaitAny(new WaitHandle[] { mAudioBlockWritten, mStopThread });
                    if (signalled == 1)
                        break;
                }

                videoWriteTask = mVideoStream.WriteFrameAsync(true, buffer, 0, buffer.Length);

                timeTillNextFrame = TimeSpan.FromSeconds(shotsTaken / (double)mWriter.FramesPerSecond - stopwatch.Elapsed.TotalSeconds);
                if (timeTillNextFrame < TimeSpan.Zero)
                    timeTillNextFrame = TimeSpan.Zero;

                isFirstFrame = false;


            }
            stopwatch.Stop();
            // Wait for the last frame is written
            if (!isFirstFrame)
            {

                videoWriteTask.Wait();

            }

        }
        private static WaveFormat ToWaveFormat(SupportedWaveFormat waveFormat)
        {
            switch (waveFormat)
            {
                case SupportedWaveFormat.WAVE_FORMAT_44M16:
                    return new WaveFormat(44100, 16, 1);
                case SupportedWaveFormat.WAVE_FORMAT_44S16:
                    return new WaveFormat(44100, 16, 2);
                default:
                    throw new NotSupportedException("Wave formats other than '16-bit 44.1kHz' are not currently supported.");
            }
        }

        private IAviAudioStream CreateAudioStream(WaveFormat waveFormat, bool encode, int bitRate)
        {
            // Create encoding or simple stream based on settings
            if (encode)
            {

                return mWriter.AddMp3AudioStream(waveFormat.Channels, waveFormat.SampleRate, bitRate);

            }
            else
            {
                return mWriter.AddAudioStream(
                    channelCount: waveFormat.Channels,
                    samplesPerSecond: waveFormat.SampleRate,
                    bitsPerSample: waveFormat.BitsPerSample);
            }
        }

        private void audioSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            var signalled = WaitHandle.WaitAny(new WaitHandle[] { mVideoFrameWritten, mStopThread });
            if (signalled == 0)
            {
                mAudioStream.WriteBlock(e.Buffer, 0, e.BytesRecorded);
                mAudioBlockWritten.Set();
            }
        }


        private IAviVideoStream CreateVideoStream(FourCC codec, int quality)
        {
            // Select encoder type based on FOURCC of codec
            if (codec == KnownFourCCs.Codecs.Uncompressed)
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

        private void GetScreenshot(byte[] buffer)
        {
            using (Bitmap bitmap = new Bitmap(mScreenWidth, mScreenHeight))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenWidth, mScreenHeight));
                var bits = bitmap.LockBits(new Rectangle(0, 0, mScreenWidth, mScreenHeight), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                Marshal.Copy(bits.Scan0, buffer, 0, buffer.Length);
                bitmap.UnlockBits(bits);

                // Should also capture the mouse cursor here, but skipping for simplicity
                // For those who are interested, look at http://www.codeproject.com/Articles/12850/Capturing-the-Desktop-Screen-with-the-Mouse-Cursor
            }
        }


    }


    public class AudioDevice
    {

        private string name;
        private int value;

        public AudioDevice(string _name, int _value)
        {
            this.name = _name;
            this.value = _value;
        }
        public string Name
        {
            get { return name; }
        }

        public int Value
        {
            get { return value; }
        }
    }
}
