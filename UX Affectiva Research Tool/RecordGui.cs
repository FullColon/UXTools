using System;
using System.Collections.Generic;
using System.Management;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SQLite;
using NAudio.CoreAudioApi;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace UX_Affectiva_Research_Tool
{
    public partial class RecordGui : Form
    {

        List<RecordingToolBase> arrayOfRecordingTools;
        Stopwatch stopWatch = new Stopwatch();
        MainWindow mDocablePanel;
        String mAudioCodecName;
        AudioDevice mAudioDevice;
        CodecInfo mCodecInfo;
        Rectangle mSelectArea;
        AffectOptions SetupAffectiva = new AffectOptions();

        private List<string> mListOfScreens;
        //  TesterDB temp;
        int mqualty = 50;




        string mBaseFilePath = "C:\\";




        public RecordGui()
        {
            InitializeComponent();
           SetUpOptions();
            InitAvailableDisplays();

            InitAvailableCodecs();
         InitAvailableAudioSources();
           
          //  temp = new TesterDB();
        }
        public void SetUpOptions()
        {
            SetupAffectiva = new AffectOptions();
        }
        public RecordGui(MainWindow _DocPanel)
        {
        
            InitializeComponent();
            mDocablePanel = _DocPanel;

            InitAvailableDisplays();
            InitAvailableCodecs();
            InitAvailableAudioSources();
            SetUpRecordingTools();
        }


        /// <summary>
        /// Sets the screen area.
        /// </summary>
        private void SetScreenArea()
        {
            // get entire desktop area size
            string screenName = this.cmbx_selectDisplay.SelectedValue.ToString();
            if (string.Compare(screenName, @"Select ALL", StringComparison.OrdinalIgnoreCase) == 0)
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    this.mSelectArea = Rectangle.Union(mSelectArea, screen.Bounds);
                    int x = 5;
                }
            }
            else
            {
                this.mSelectArea = Screen.AllScreens.First(scr => scr.DeviceName.Equals(screenName)).Bounds;
                
            }
        }

        private void InitAvailableDisplays()
        {
            mListOfScreens = new List<string>();
            mListOfScreens.Add(@"Select ALL");
            foreach (var screen in Screen.AllScreens)
            {
                mListOfScreens.Add(screen.DeviceName);
            }

            this.cmbx_selectDisplay.DataSource = mListOfScreens;
            this.cmbx_selectDisplay.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            
            mBaseFilePath =  labelPath.Text + "\\" + textBoxName.Text;
            SetUpRecordingTools();
            //Wes test code for DataBase (Saved on External for REF)
            stopWatch.Start();
           for (int count = 0; count < arrayOfRecordingTools.Count; count++)
           {
               arrayOfRecordingTools[count].startRecording();
             
           }

            SetScreenArea();


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
            arrayOfRecordingTools.Clear();
        }

        public void SetVisibility(bool _visibility)
        {
            this.startButton.Enabled = !_visibility;
            this.stopButton.Enabled = _visibility;
       
        }
       
        public void SetUpRecordingTools()
        {
            SetScreenArea();
            arrayOfRecordingTools = new List<RecordingToolBase>();
            
            arrayOfRecordingTools.Add(new AffectivaCameraFaceRecordingAndVideoRecording(mBaseFilePath,(float) SetupAffectiva.DectectionValence, .1f, 0, (int)(FPSUPDOWN.Value), SetupAffectiva.ProcessPerSec, affectivaToolStripMenuItem.Checked));
            if(affectivaToolStripMenuItem.Checked || SetupAffectiva.Post)
                arrayOfRecordingTools.Add(new ManuelTagRecordingTool(stopWatch, SetupAffectiva.ProcessPerSec, SetupAffectiva.Post));
            arrayOfRecordingTools.Add(new RecordingTool.Recorder(mAudioDevice, mAudioCodecName,mCodecInfo, mSelectArea,(int)( FPSUPDOWN.Value), mqualty, mBaseFilePath));
         //   arrayOfRecordingTools.Add(new Audio());
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
                mDocablePanel.MakeFormFromRecordingToolBase(arrayOfRecordingTools[count]);

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

      
    }
}
