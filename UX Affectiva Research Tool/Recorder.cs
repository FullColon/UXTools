﻿using System;
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
    /// <summary>
    /// Event is fired when a frame buffer is encoded.
    /// </summary>
    /// <param name="_buffer">Frame buffer.</param>
    public delegate void FrameBufferEncoded(byte[] _buffer);


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
        IAviVideoStream mVideoStream;
        private int mQuality;
        private FourCC mSelectedCodec;
        private int mFramesPerSecond;
        private bool mIsEncoded;
        private string mFullPath;
        private IVideoEncoder mEncoder;
        Rectangle mScreenArea;
        private int mScreenTop;
        private int mScreenLeft;
        private string mAudioCodec;

        private readonly ManualResetEvent mStopThread = new ManualResetEvent(false);
        private readonly AutoResetEvent mVideoFrameWritten = new AutoResetEvent(false);
        private readonly AutoResetEvent mAudioBlockWritten = new AutoResetEvent(false);


        /// <summary>
        /// Gets the saved file path.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetSavedFilePath()
        {
            return mFullPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recorder"/> class.
        /// </summary>
        /// <param name="_myDevice">The _my device.</param>
        /// <param name="_myCodec">The _my codec.</param>
        /// <param name="_screenArea">The _screen area.</param>
        /// <param name="_framesPerSecond">The _frames per second.</param>
        /// <param name="_quality">The _quality.</param>
        public Recorder(AudioDevice _myDevice, string _audioCodec, CodecInfo _myCodec, Rectangle _screenArea, int  _framesPerSecond, int _quality)
        {



            //           if (_myCodec == null)
            //           {
            //              _myCodec = new CodecInfo(Mpeg4VideoEncoderVcm.GetAvailableCodecs()[0].Codec, "v_encoder");
            //          }
            mScreenArea = _screenArea;
            mScreenTop = _screenArea.Top;
            mScreenLeft = _screenArea.Left;
            mAudioCodec = _audioCodec;
            mAudioDevice = _myDevice;
            mCodecInfo = _myCodec;
            mFramesPerSecond = _framesPerSecond;
            mQuality = _quality;

            switch (_myCodec.Name)
            {
                case "x264vfw - H.264/MPEG-4 AVC codec":
                    {
                        mSelectedCodec = mCodecInfo.Codec;
                        mIsEncoded = true;
                    }
                    break;
                default:
                    {
                        mIsEncoded = false;
                    }
                    break;
            }

            if (mIsEncoded)
            {
                mEncoder = new Mpeg4VideoEncoderVcm(mScreenArea.Width, mScreenArea.Height, mFramesPerSecond, 0, mQuality, mSelectedCodec);
            }



            InitializeRecorder();
        }

        /// <summary>
        /// Initializes the recorder.
        /// </summary>
        public void InitializeRecorder()
        {
            System.Windows.Media.Matrix toDevice;
            using (var source = new HwndSource(new HwndSourceParameters()))
            {
                toDevice = source.CompositionTarget.TransformToDevice;
            }



            string currentDirectory = Environment.CurrentDirectory;
            string saveDirectory = @"\SaveFolder";
            string mFilePath = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
            mFullPath = mFilePath + saveDirectory + @"\screenRecord.avi";


            mWriter = new AviWriter(mFullPath)
            {
                FramesPerSecond = mFramesPerSecond,
                EmitIndex1 = true,
            };


            if (mIsEncoded)
            {
                mVideoStream = mWriter.AddEncodingVideoStream(mEncoder, width: mScreenArea.Width, height: mScreenArea.Height);
            }
            else
            {
                mVideoStream = CreateVideoStream(mCodecInfo.Codec, mQuality);
            }

            mVideoStream.Name = "Screencast";

            suppForm = SupportedWaveFormat.WAVE_FORMAT_44S16;
            mWaveFormat = ToWaveFormat(suppForm);


            mAudioStream = CreateAudioStream(mWaveFormat, mAudioCodec, 192);
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

        /// <summary>
        /// Stops the recording.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
        /// <summary>
        /// Records the screen.
        /// </summary>
        private void RecordScreen()
        {
            Stopwatch stopwatch = new Stopwatch();

            byte[] buffer = new byte[mScreenArea.Width * mScreenArea.Height * 4];

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
                    var signaled = WaitHandle.WaitAny(new WaitHandle[] { mAudioBlockWritten, mStopThread });
                    if (signaled == 1)
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
        /// <summary>
        /// To the wave format.
        /// </summary>
        /// <param name="_waveFormat">The wave format.</param>
        /// <returns>WaveFormat.</returns>
        /// <exception cref="System.NotSupportedException">Wave formats other than '16-bit 44.1kHz' are not currently supported.</exception>
        private static WaveFormat ToWaveFormat(SupportedWaveFormat _waveFormat)
        {
            switch (_waveFormat)
            {
                case SupportedWaveFormat.WAVE_FORMAT_44M16:
                    return new WaveFormat(44100, 16, 1);
                case SupportedWaveFormat.WAVE_FORMAT_44S16:
                    return new WaveFormat(44100, 16, 2);
                default:
                    throw new NotSupportedException("Wave formats other than '16-bit 44.1kHz' are not currently supported.");
            }
        }

  
        private IAviAudioStream CreateAudioStream(WaveFormat _waveFormat, string _encode, int _bitRate)
        {
            // Create encoding or simple stream based on settings
            if (_encode == "MP3")
            {

                return mWriter.AddMp3AudioStream(_waveFormat.Channels, _waveFormat.SampleRate, _bitRate);

            }
            else
            {
                return mWriter.AddAudioStream(
                    channelCount: _waveFormat.Channels,
                    samplesPerSecond: _waveFormat.SampleRate,
                    bitsPerSample: _waveFormat.BitsPerSample);
            }
        }

        /// <summary>
        /// Handles the DataAvailable event of the audioSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WaveInEventArgs"/> instance containing the event data.</param>
        private void audioSource_DataAvailable(object _sender, WaveInEventArgs _waveInEvenArgs)
        {
            var signalled = WaitHandle.WaitAny(new WaitHandle[] { mVideoFrameWritten, mStopThread });
            if (signalled == 0)
            {
                mAudioStream.WriteBlock(_waveInEvenArgs.Buffer, 0, _waveInEvenArgs.BytesRecorded);
                mAudioBlockWritten.Set();
            }
        }


        /// <summary>
        /// Creates the video stream.
        /// </summary>
        /// <param name="_codec">The codec.</param>
        /// <param name="_quality">The quality.</param>
        /// <returns>IAviVideoStream.</returns>
        private IAviVideoStream CreateVideoStream(FourCC _codec, int _quality)
        {
            // Select encoder type based on FOURCC of codec
            if (_codec == KnownFourCCs.Codecs.Uncompressed)
            {
                return mWriter.AddUncompressedVideoStream(mScreenArea.Width, mScreenArea.Height);
            }
            else if (_codec == KnownFourCCs.Codecs.MotionJpeg)
            {
                return mWriter.AddMotionJpegVideoStream(mScreenArea.Width, mScreenArea.Height, _quality);
            }
            else
            {
                return mWriter.AddMpeg4VideoStream(mScreenArea.Width, mScreenArea.Height, (double)mWriter.FramesPerSecond,
                    // It seems that all tested MPEG-4 VfW codecs ignore the quality affecting parameters passed through VfW API
                    // They only respect the settings from their own configuration dialogs, and Mpeg4VideoEncoder currently has no support for this
                    quality: _quality,
                    codec: _codec,
                    // Most of VfW codecs expect single-threaded use, so we wrap this encoder to special wrapper
                    // Thus all calls to the encoder (including its instantiation) will be invoked on a single thread although encoding (and writing) is performed asynchronously
                    forceSingleThreadedAccess: true);
            }
        }

        /// <summary>
        /// Gets the screenshot.
        /// </summary>
        /// <param name="_buffer">The _buffer.</param>
        private void GetScreenshot(byte[] _buffer)
        {
            using (Bitmap bitmap = new Bitmap(mScreenArea.Width, mScreenArea.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {

                graphics.CopyFromScreen(mScreenLeft, mScreenTop, 0, 0, new System.Drawing.Size(mScreenArea.Width, mScreenArea.Height));
                var bits = bitmap.LockBits(new Rectangle(0, 0, mScreenArea.Width, mScreenArea.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                Marshal.Copy(bits.Scan0, _buffer, 0, _buffer.Length);
                bitmap.UnlockBits(bits);

                int imgSize;
                byte[] compressed = encodeBuffer(_buffer, out imgSize);
                Console.WriteLine("CompressedSize: " + imgSize);

                // Should also capture the mouse cursor here, but skipping for simplicity
                // For those who are interested, look at http://www.codeproject.com/Articles/12850/Capturing-the-Desktop-Screen-with-the-Mouse-Cursor
            }
        }

        /// <summary>
        /// Encode a frame buffer with the requested codec.
        /// </summary>
        /// <param name="_buffer">Buffer to encode</param>
        /// <param name="_imgSize">Size of image after compression</param>
        /// <returns></returns>
        private byte[] encodeBuffer(byte[] _buffer, out int _imgSize)
        {
            byte[] dest = new byte[_buffer.Length];
            bool isKeyFrame;
            _imgSize = mEncoder.EncodeFrame(_buffer, 0, dest, 0, out isKeyFrame);
            return dest;
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
