using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using UX_Affectiva_Research_Tool.Affectiva_Files;
using WeifenLuo.WinFormsUI.Docking;
using RecordingTool;
using SharpAvi.Codecs;
using SharpAvi;
using NAudio.Wave;
using System.IO;
using System.Reflection;
using DirectShowLib;

namespace UX_Affectiva_Research_Tool
{
    public partial class RecordGui : Form
    {

        // Class variables

        private List<RecordingToolBase> mListOfRecorders;
        private Stopwatch mStopWatch = new Stopwatch();
        private MainWindow mDocablePanel;
        private AudioDevice mScreenAudioInput, mCameraAudioInput;
        private dynamic mScreenCodecInfo, mCameraCodecInfo;
        private AffectOptions SetupAffectiva;
        private int mScreenQuality, mCameraQuality;
        private int mScreenFramesPerSecond, mCameraFramesPerSecond;
        private Rectangle mScreenArea;
        private List<string> mListOfScreens;
        private List<string> mListOfAudioCodecs;
        private string mScreenAudioCodec, mCameraAudioCodec;
        private readonly SupportedWaveFormat[] audioFormats = new[] { SupportedWaveFormat.WAVE_FORMAT_44M16, SupportedWaveFormat.WAVE_FORMAT_44S16};

        //     string FilePath = "C:\\DFiles\\WorkFolderFinalProdject\\ux-affectiva.git.0\\UX Affectiva Research Tool\\UX Affectiva Research Tool\\SaveFolder\\test.avi";

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordGui" /> class.
        /// </summary>
        public RecordGui()
        {
            InitAudioDll();
            InitializeComponent();
//            SetUpAffectivaOptions();
            InitAvailableVideoCodecs();
            InitAvailableAudioSources();
            InitAvailableDisplays();
            InitAudioCodec();

            stopButton.Enabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordGui"/> class.
        /// </summary>
        /// <param name="_DocPanel">The _ document panel.</param>
        public RecordGui(MainWindow _DocPanel)
        {
            
            InitializeComponent();
 //           SetUpAffectivaOptions();
            InitAvailableVideoCodecs();
            InitAvailableAudioSources();
            InitAvailableDisplays();
            InitAudioCodec();
            mDocablePanel = _DocPanel;
        }

        /// <summary>
        /// Initializes the available displays.
        /// </summary>
        private void InitAvailableDisplays()
        {
            mListOfScreens = new List<string>();
            mListOfScreens.Add(@"Select ALL");
            foreach (var screen in Screen.AllScreens)
            {
                mListOfScreens.Add(screen.DeviceName);
            }

            this.cmbx_screenSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbx_screenSelect.DataSource = mListOfScreens;
        }

        /// <summary>
        /// Sets the screen area.
        /// </summary>
        private void SetScreenArea()
        {
            // get entire desktop area size
            string screenName = this.cmbx_screenSelect.SelectedValue.ToString();
            if (string.Compare(screenName, @"Select ALL", StringComparison.OrdinalIgnoreCase) == 0)
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    this.mScreenArea = Rectangle.Union(mScreenArea, screen.Bounds);
                    int x = 5;
                }
            }
            else
            {
                this.mScreenArea = Screen.AllScreens.First(scr => scr.DeviceName.Equals(screenName)).Bounds;
                int x = 5;
            }
        }

        /// <summary>
        /// Sets up affectiva options.
        /// </summary>
        public void SetUpAffectivaOptions()
        {
            SetupAffectiva = new AffectOptions();
        }


        /// <summary>
        /// Handles the Click event of the startButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void startButton_Click(object sender, EventArgs e)
        {
            SetAllSettings();
            SetUpRecordingTools();

            mStopWatch.Start();
            for (int count = 0; count < mListOfRecorders.Count; count++)
            {
                mListOfRecorders[count].startRecording();
         
            }
            SetStartStopButtonsVisibility(true);
        }

        /// <summary>
        /// Handles the Click event of the stopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void stopButton_Click(object sender, EventArgs e)
        {
            mStopWatch.Stop();
            for (int count = 0; count < mListOfRecorders.Count; count++)
            {
                mListOfRecorders[count].stopRecording();
              
            }
            SetStartStopButtonsVisibility(false);
//            OpenMainWindow();
        }

        /// <summary>
        /// Sets the start stop buttons visibility.
        /// </summary>
        /// <param name="_visibility">if set to <c>true</c> [_visibility].</param>
        public void SetStartStopButtonsVisibility(bool _visibility)
        {
            this.startButton.Enabled = !_visibility;
            this.stopButton.Enabled = _visibility;     
        }

        /// <summary>
        /// Sets up recording tools.
        /// </summary>
        public void SetUpRecordingTools()
        {
            mListOfRecorders = new List<RecordingToolBase>();
           // mListOfRecorders.Add(new ManuelTagRecordingTool(mStopWatch,20,true));
           //     mListOfRecorders.Add(new AffectivaCameraFaceRecordingAndVideoRecording());
              mListOfRecorders.Add(new Recorder(mScreenAudioInput, mScreenAudioCodec, mScreenCodecInfo, mScreenArea, mScreenFramesPerSecond, mScreenQuality));


            // mListOfRecorders.Add(new Audio());
        }

        /// <summary>
        /// Opens the main window.
        /// </summary>
        private void OpenMainWindow()
        {
            if(mDocablePanel == null)
            {
                mDocablePanel = new MainWindow();
                mDocablePanel.Show();
            }
            CreateDockPanelsForMediaPlayBack();

        }

        /// <summary>
        /// Creates the dock panels for media play back.
        /// </summary>
        private void CreateDockPanelsForMediaPlayBack()
        {
            for (int count = 0; count < mListOfRecorders.Count; count++)
            {
     //           mDocablePanel.MakeFormFromRecordingToolBase(mListOfRecorders[count]);

            }
        }

        private void InitCameras()
        {
            DsDevice[] mCaptureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            List<string> cameraNames = new List<string>();
            for (int i = 0; i < mCaptureDevices.Length; i++)
            {
                cameraNames.Add((mCaptureDevices[i]).Name);
            }
            this.cmbx_cameras.DataSource = cameraNames;
            this.cmbx_cameras.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void InitAvailableVideoCodecs()
        {
            List<CodecInfo> codecs = new List<CodecInfo>();

            codecs.Add(new CodecInfo(KnownFourCCs.Codecs.Uncompressed, "(none)"));
            codecs.Add(new CodecInfo(KnownFourCCs.Codecs.MotionJpeg, "Motion JPEG"));
            codecs.AddRange(Mpeg4VideoEncoderVcm.GetAvailableCodecs());

            this.cmbx_screenVideoCodecs.DataSource = codecs;
            this.cmbx_screenVideoCodecs.DisplayMember = "Name";
            this.cmbx_screenVideoCodecs.ValueMember = "Codec";
            this.cmbx_screenVideoCodecs.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Initializes the available audio sources.
        /// </summary>
        private void InitAvailableAudioSources()
        {
            List<AudioDevice> deviceList = new List<AudioDevice>();
            deviceList.Add(new AudioDevice("(no sound)", -1));

            for (var i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                var caps = WaveInEvent.GetCapabilities(i);
                if (audioFormats.All(caps.SupportsWaveFormat))
                {
                    deviceList.Add(new AudioDevice(caps.ProductName, i));
                }
            }
            this.cmbx_screenAudioDevice.DataSource = deviceList;
            this.cmbx_screenAudioDevice.DisplayMember = "Name";
            this.cmbx_screenAudioDevice.ValueMember = "Value";
            this.cmbx_screenAudioDevice.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Handles the Click event of the moreOptionsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void moreOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AffectivaOptions Aff = new AffectivaOptions();
         
            if (DialogResult.OK == Aff.ShowDialog())
            {

                this.SetupAffectiva = Aff.InformationSetup;
              
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonFilePath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonFilePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderSelect = new FolderBrowserDialog();
             if (FolderSelect.ShowDialog() == DialogResult.OK)
             {
      
                labelPath.Text = FolderSelect.SelectedPath;
                
             }
        }

        /// <summary>
        /// Handles the Scroll event of the trackBar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lbl_screenQualityValue.Text = trkbr_screenQuality.Value.ToString();
        }

        /// <summary>
        /// Initializes the audio DLL.
        /// </summary>
        private void InitAudioDll()
        {
            var asmDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var dllName = string.Format("libmp3lame.{0}.dll", Environment.Is64BitProcess ? "64" : "32");
            Mp3AudioEncoderLame.SetLameDllLocation(Path.Combine(asmDir, dllName));
        }

        /// <summary>
        /// Initializes the audio codec.
        /// </summary>
        ///
        private void InitAudioCodec()
        {

            mListOfAudioCodecs = new List<string>();
            mListOfAudioCodecs.Add("Wav");
            mListOfAudioCodecs.Add("MP3");
            cmbx_screenAudioCodecs.DataSource = mListOfAudioCodecs;
            cmbx_screenAudioCodecs.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Sets all settings.
        /// </summary>
        private void SetAllSettings()
        {
            SetScreenArea();
            mScreenFramesPerSecond = (int)this.nud_screenFPS.Value;
            mScreenQuality = trkbr_screenQuality.Value;
            mScreenAudioInput = (AudioDevice)cmbx_screenAudioDevice.SelectedItem;
            mScreenCodecInfo = (CodecInfo)cmbx_screenVideoCodecs.SelectedItem;
            mScreenAudioCodec = cmbx_screenAudioCodecs.SelectedItem.ToString();
/*
            mCameraFramesPerSecond = (int)this.nud_cameraFPS.Value;
            mCameraQuality = trkbr_cameraQuality.Value;
            mCameraAudioInput = (AudioDevice)cmbx_cameraAudioInput.SelectedItem;
            mCameraCodecInfo = (CodecInfo)cmbx_cameraVideoCodecs.SelectedItem;
            mCameraAudioCodec = cmbx_cameraAudioCodecs.SelectedItem.ToString();
  */        
        }

        /// <summary>
        /// Handles the Scroll event of the trkbr_cameraQuality control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trkbr_cameraQuality_Scroll(object sender, EventArgs e)
        {
            lbl_cameraQualityValue.Text = trkbr_cameraQuality.Value.ToString();
        }
    }
}
