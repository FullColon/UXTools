using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UX_Affectiva_Research_Tool.Affectiva_Files;
using WeifenLuo.WinFormsUI.Docking;
using mySharpAVI;

namespace UX_Affectiva_Research_Tool
{
    public partial class MainWindow : Form
    {
        List<DockContent> Panels;
       
        string lk = "C:\\DFiles\\WorkFolderFinalProdject\\ux-affectiva.git.0\\UX Affectiva Research Tool\\UX Affectiva Research Tool\\SaveFolder\\TestSave.mp4";
        private ScreenCapturePlaybackWindow audioFeed;
        private ScreenCapturePlaybackWindow cameraFeed;
        private RecordingAffectivaReview graphWindow;
      
        public MainWindow()
        {
            Panels = new List<DockContent>();
            InitializeComponent();


       //     ScreenCapturePlaybackWindow screenCapturePlaybackWindow = new ScreenCapturePlaybackWindow();
       //     screenCapturePlaybackWindow.Show();
       //      screenCapturePlaybackWindow.Show(mainDockPanel, DockState.DockTop);
       //      CameraFeedPlaybackWindow cameraFeeddPlaybackWindow = new CameraFeedPlaybackWindow();
       //      cameraFeeddPlaybackWindow.Show(screenCapturePlaybackWindow.Pane, DockAlignment.Right, 0.5);

       //     Affectiva_Files.WebRecording cam = new Affectiva_Files.WebRecording();
      //      cam.Show(screenCapturePlaybackWindow.Pane, DockAlignment.Bottom, 0.5);


        }
       
        private void openRecordWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordGui recordGui = new RecordGui(this);
            recordGui.Show();
        }
        public void MakeFormFromRecordingToolBase(RecordingToolBase _RecorderType)
        {
            if (_RecorderType.GetType() == typeof(mySharpAVI.myRecorder))
            {
               Console.WriteLine("Screen");
               ScreenCapturePlaybackWindow gameFeed;
               gameFeed = new ScreenCapturePlaybackWindow(((mySharpAVI.myRecorder)_RecorderType).GetFullName());

               gameFeed.Show(cameraFeed.Pane, DockAlignment.Left, 0.5);
               Panels.Add(gameFeed);

            }

            else if (_RecorderType.GetType() == typeof(Affectiva_Files.AffectivaCameraFaceRecordingAndVideoRecording))
            {
               Console.WriteLine("CamAff");
                if (graphWindow == null)
                {
                    graphWindow = new RecordingAffectivaReview(((AffectivaCameraFaceRecordingAndVideoRecording)_RecorderType).GetAffectiveData(), false);

                    graphWindow.Show(this.GetMainDockPanel(), DockState.DockBottom);
                    Panels.Add(graphWindow);
                }
                else
                {
                  
                    graphWindow.MergeInData(((Affectiva_Files.AffectivaCameraFaceRecordingAndVideoRecording)_RecorderType).GetAffectiveData());
                }
               cameraFeed = new ScreenCapturePlaybackWindow(((AffectivaCameraFaceRecordingAndVideoRecording)_RecorderType).getFileWriterVideo().getFilePath());

               cameraFeed.Show(this.GetMainDockPanel(), DockState.DockTop);
               Panels.Add(cameraFeed);


            }
            else if (_RecorderType.GetType() == typeof(Affectiva_Files.ManuelTagRecordingTool))
            {
                if (graphWindow == null)
                {
                    graphWindow = new RecordingAffectivaReview(((Affectiva_Files.ManuelTagRecordingTool)_RecorderType).getAffRecordingData(), false);
                    graphWindow.Show(this.GetMainDockPanel(), DockState.DockBottom);
                }
                else
                    graphWindow.MergeInData(((Affectiva_Files.ManuelTagRecordingTool)_RecorderType).getAffRecordingData());
            }
                        else if (_RecorderType.GetType() == typeof(Audio))
                      {
                          Console.WriteLine("Audio");


                          audioFeed = new ScreenCapturePlaybackWindow(((Audio)_RecorderType).getFilePath());
                          audioFeed.Show(this.GetMainDockPanel(), DockState.DockLeftAutoHide);

                          Panels.Add(audioFeed);
                      }
               
                     
            }
        public void AddPanel(DockContent _Panel)
        {
            Panels.Add(_Panel);
        }
    }
}