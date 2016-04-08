﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpAvi.Codecs;
using SharpAvi;
using NAudio.Wave;
using SharpAvi.Output;
using System.Diagnostics;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using UX_Affectiva_Research_Tool;
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
        private int mScreenWidth;
        private int mScreenHeight;
        IAviVideoStream mVideoStream;
        private int mQuality = 50;
        private string mFilePath;
        private string mFullPath;
        private Mpeg4VideoEncoderVcm mEncoder;

        private readonly ManualResetEvent mStopThread = new ManualResetEvent(false);
        private readonly AutoResetEvent mVideoFrameWritten = new AutoResetEvent(false);
        private readonly AutoResetEvent mAudioBlockWritten = new AutoResetEvent(false);


        public string GetFullName()
        {
            return mFullPath;
        }

        public Recorder(AudioDevice _myDevice, CodecInfo _myCodec = null)
        {
            if (_myCodec == null)
            {
                _myCodec = new CodecInfo(Mpeg4VideoEncoderVcm.GetAvailableCodecs()[0].Codec, "v_encoder");
            }
            mAudioDevice = _myDevice;
            mCodecInfo = _myCodec;

            InitializeRecorder();
        }

        public void InitializeRecorder()
        {
            System.Windows.Media.Matrix toDevice;
            using (var source = new HwndSource(new HwndSourceParameters()))
            {
                toDevice = source.CompositionTarget.TransformToDevice;
            }
            mScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            mScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            string currentDirectory = Environment.CurrentDirectory;
            string newDir = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));

            mFullPath = newDir + @"\SaveFolder\screenRecord.avi";



            mWriter = new AviWriter(mFullPath)
            {
                FramesPerSecond = 10,
                EmitIndex1 = true,
            };

            mVideoStream = CreateVideoStream(mCodecInfo.Codec, mQuality);

            mVideoStream.Name = "Screencast";



            suppForm = SupportedWaveFormat.WAVE_FORMAT_44S16;
            mWaveFormat = ToWaveFormat(suppForm);
          

            mAudioStream = CreateAudioStream(mWaveFormat, false, 160);
            mAudioStream.Name = "Voice";
            mAudioSource = new WaveInEvent
            {
                DeviceNumber = mAudioDevice.Value,
                WaveFormat = mWaveFormat,
                BufferMilliseconds = (int)Math.Ceiling(1000 / mWriter.FramesPerSecond),
                NumberOfBuffers = 3,
            };
            mAudioSource.DataAvailable += audioSource_DataAvailable;

            mEncoder = new Mpeg4VideoEncoderVcm(
                    mScreenWidth,
                    mScreenHeight,
                    (int)mWriter.FramesPerSecond,
                    0,
                    70,
                    mCodecInfo.Codec
            );
        }
        public override bool stopRecording()
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

        public override bool startRecording()
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
                // LAME DLL path is set in App.OnStartup()
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

        private void GetScreenshot(byte[] _buffer)
        {
            using (Bitmap bitmap = new Bitmap(mScreenWidth, mScreenHeight))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenWidth, mScreenHeight));
                var bits = bitmap.LockBits(new Rectangle(0, 0, mScreenWidth, mScreenHeight), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                Marshal.Copy(bits.Scan0, _buffer, 0, _buffer.Length);
                bitmap.UnlockBits(bits);

                byte[] compressed = encodeBuffer(_buffer);
                Console.WriteLine("CompressedSize: " + compressed.Length);

                // Should also capture the mouse cursor here, but skipping for simplicity
                // For those who are interested, look at http://www.codeproject.com/Articles/12850/Capturing-the-Desktop-Screen-with-the-Mouse-Cursor
            }
        }

        private byte[] encodeBuffer(byte[] _buffer)
        {
            byte[] dest = new byte[_buffer.Length];
            bool isKeyFrame;
            int imgSize = mEncoder.EncodeFrame(_buffer, 0, dest, 0, out isKeyFrame);
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
