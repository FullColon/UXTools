using System;
using System.Collections.Generic;

using Affdex;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    public partial class WebRecording : DockPanelFormBase, Affdex.ProcessStatusListener
    {
      
        AffectivaDataRecordingEmotionsandExpressions AfCFRAVR;
        public delegate void LetOthersTakeMyData(AffectivaDataRecordingEmotionsandExpressions _Var);
        public LetOthersTakeMyData PassOffMyDataDelegate;

        public AffectivaDataRecordingEmotionsandExpressions AffData
        {
            get
            {
                return AfCFRAVR;
            }

            set
            {
                AfCFRAVR = value;
            }
        }

        public WebRecording()
        {
            InitializeComponent();

          
        }

        public WebRecording(AffectivaDataRecordingEmotionsandExpressions _AFCAMandVIdeo, bool _PostProcesses)
        {
            InitializeComponent();

            AffData = _AFCAMandVIdeo;
            
            SetupChart(_AFCAMandVIdeo);

        }

        private void SetupChart(AffectivaDataRecordingEmotionsandExpressions AfCFRAVR)
        {
            SetupChartFromSeries( AfCFRAVR.GetChartSeriesOfData());
           
        }
        private void SetupChartFromSeries(List<System.Windows.Forms.DataVisualization.Charting.Series> seri )
        {
            
            for (int count = 0; count < seri.Count; count++)
            {
                chart1.Series.Add(seri[count]);
                chart1.Series[count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.Series[count].ChartArea = chart1.ChartAreas[0].Name;
            }


            chart1.Invalidate();
        }
        public void MergeInData(AffectivaDataRecordingEmotionsandExpressions _MergeIn)
        {
            if (chart1.Series.Count > 0)
            {
                MergeInSeriesOfList( _MergeIn.GetChartSeriesOfData());
                
            }
            else
            {
                SetupChart(_MergeIn);
            }
        }
        public void MergeInSeriesOfList(List<System.Windows.Forms.DataVisualization.Charting.Series> seri)
        {
            if (chart1.Series.Count > 0)
            {
             
                for (int countofseries = 0; countofseries < seri.Count; countofseries++)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series tempSeriesNew = seri[countofseries];
                    System.Windows.Forms.DataVisualization.Charting.Series tempSeriesOld = chart1.Series[countofseries];
                    for (int countofEmotionnew = 0; countofEmotionnew < tempSeriesNew.Points.Count; countofEmotionnew++)
                    {
                        bool added = false;
                        for (int countofEmotion = 0; countofEmotion < tempSeriesOld.Points.Count; countofEmotion++)
                        {
                            if (tempSeriesOld.Points[countofEmotion].XValue > tempSeriesNew.Points[countofEmotionnew].XValue)
                            {
                                if (tempSeriesNew.Points[countofEmotionnew].YValues[0] == 0)
                                {
                                    tempSeriesNew.Points[countofEmotionnew].YValues[0] = tempSeriesOld.Points[countofEmotion].YValues[0];
                                }
                                tempSeriesOld.Points.Insert(countofEmotion, tempSeriesNew.Points[countofEmotionnew]);
                                added = true;
                                break;
                            }


                        }
                        if (!added)
                            tempSeriesOld.Points.Add(tempSeriesNew.Points[countofEmotionnew]);

                    }
                }
                chart1.Invalidate();
            }
            else
            {
                SetupChartFromSeries(seri);
            }
        }
        public void onProcessingException(AffdexException ex)
        {
            throw new NotImplementedException();
        }

        public void onProcessingFinished()
        {
            PassOffMyDataDelegate += MergeInData;
            Invoke(PassOffMyDataDelegate, AffData);
        }
    }
}
