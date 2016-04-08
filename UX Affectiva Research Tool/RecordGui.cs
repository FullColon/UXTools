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


namespace UX_Affectiva_Research_Tool
{
    public partial class RecordGui : Form
    {

        List<RecordingToolBase> arrayOfRecordingTools;
        Stopwatch stopWatch = new Stopwatch();
        MainWindow mDocablePanel;
        AudioDevice mAudioDevice;
        CodecInfo mCodecInfo;
        AffectOptions SetupAffectiva;




   //     string FilePath = "C:\\DFiles\\WorkFolderFinalProdject\\ux-affectiva.git.0\\UX Affectiva Research Tool\\UX Affectiva Research Tool\\SaveFolder\\test.avi";




        public RecordGui()
        {
            InitializeComponent();
            SetUpOptions();

            InitAvailableCodecs();
            InitAvailableAudioSources();
            SetUpRecordingTools();
        }
        public void SetUpOptions()
        {
            SetupAffectiva = new AffectOptions();
        }
        public RecordGui(MainWindow _DocPanel)
        {
        
            InitializeComponent();
            mDocablePanel = _DocPanel;


            InitAvailableCodecs();
            InitAvailableAudioSources();
            SetUpRecordingTools();
        }

        private void startButton_Click(object sender, EventArgs e)
        {

            stopWatch.Start();
            for (int count = 0; count < arrayOfRecordingTools.Count; count++)
            {
                arrayOfRecordingTools[count].startRecording();
              
            }

          

            SetVisibility(true);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopWatch.Stop();
            for (int count = 0; count < arrayOfRecordingTools.Count; count++)
            {
                arrayOfRecordingTools[count].stopRecording();
              
            }



            SetVisibility(false);
            MakeNeedForms();
        }

        public void SetVisibility(bool _visibility)
        {
            this.startButton.Enabled = !_visibility;
            this.stopButton.Enabled = _visibility;
       
        }
        public void SetUpRecordingTools()
        {
            arrayOfRecordingTools = new List<RecordingToolBase>();
           // arrayOfRecordingTools.Add(new ManuelTagRecordingTool(stopWatch,20,true));
                arrayOfRecordingTools.Add(new AffectivaCameraFaceRecordingAndVideoRecording());
              arrayOfRecordingTools.Add(new RecordingTool.Recorder(mAudioDevice, mCodecInfo, 15, 100));


            // arrayOfRecordingTools.Add(new Audio());
        }
        private void MakeNeedForms()
        {
            if(mDocablePanel == null)
            {
                mDocablePanel = new MainWindow();
                mDocablePanel.Show();
            }
            for (int count = 0; count < arrayOfRecordingTools.Count; count++)
            {
  //              mDocablePanel.MakeFormFromRecordingToolBase(arrayOfRecordingTools[count]);

            }
        }

        private void InitAvailableCodecs()
        {
            List<CodecInfo> codecs = new List<CodecInfo>();

            codecs.Add(new CodecInfo(KnownFourCCs.Codecs.Uncompressed, "(none)"));
            codecs.Add(new CodecInfo(KnownFourCCs.Codecs.MotionJpeg, "Motion JPEG"));
            codecs.AddRange(Mpeg4VideoEncoderVcm.GetAvailableCodecs());


            this.comboBox_screenCodecs.DataSource = codecs;
            this.comboBox_screenCodecs.DisplayMember = "Name";
            this.comboBox_screenCodecs.ValueMember = "Codec";
            this.comboBox_screenCodecs.DropDownStyle = ComboBoxStyle.DropDownList;


        }

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
            this.comboBox_screenAudioDevice.DataSource = deviceList;
            this.comboBox_screenAudioDevice.DisplayMember = "Name";
            this.comboBox_screenAudioDevice.ValueMember = "Value";
            this.comboBox_screenAudioDevice.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private readonly SupportedWaveFormat[] audioFormats = new[]
        {
            SupportedWaveFormat.WAVE_FORMAT_44M16,
            SupportedWaveFormat.WAVE_FORMAT_44S16
        };

        private void comboBox_screenCodecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            mCodecInfo = (CodecInfo)comboBox_screenCodecs.SelectedItem;
        }

        private void comboBox_screenAudioDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAudioDevice = (AudioDevice)comboBox_screenAudioDevice.SelectedItem;
        }

        private void moreOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AffectivaOptions Aff = new AffectivaOptions();
         
            if (DialogResult.OK == Aff.ShowDialog())
            {

                this.SetupAffectiva = Aff.InformationSetup;
              
            }
        }

        private void buttonFilePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderSelect = new FolderBrowserDialog();
             if (FolderSelect.ShowDialog() == DialogResult.OK)
             {
      
                labelPath.Text = FolderSelect.SelectedPath;
                
             }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lbl_displayQualityValue.Text = trackBar_quality.Value.ToString();
        }
    }
}
