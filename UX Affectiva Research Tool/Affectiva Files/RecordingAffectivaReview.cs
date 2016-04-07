using System;
using System.Collections.Generic;

using Affdex;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    public partial class RecordingAffectivaReview : DockPanelFormBase, Affdex.ProcessStatusListener
    {
        public double chartx, charty;
        AffectivaDataRecordingEmotionsandExpressions AfCFRAVR;
        public delegate void LetOthersTakeMyData(AffectivaDataRecordingEmotionsandExpressions _Var);
        public LetOthersTakeMyData PassOffMyDataDelegate;
        public delegate void GraphWorkDelegate();
        int SeriIndex = -1, EmotionIndex = -1;
        Series LastSeris;
        public GraphWorkDelegate DrawingWorkDelegate;
        DataPoint LastPoint;
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

        public RecordingAffectivaReview()
        {
            InitializeComponent();

          
        }

        public RecordingAffectivaReview(AffectivaDataRecordingEmotionsandExpressions _AFCAMandVIdeo, bool _PostProcesses)
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
                chart1.Series[count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

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
        MouseEventArgs pt;
       

        

       

        

        

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            pt = e;
            HitTestResult result = chart1.HitTest(e.X, e.Y);
            MessageBox.Show(e.X.ToString()+" "+ e.Y.ToString());
            EmotionIndex = result.PointIndex;
            LastSeris = result.Series;


            if (result.PointIndex > -1)
            {
                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    if (chart1.Series[i] == LastSeris)
                    {
                        //MessageBox.Show(chart1.Series[i].Points[result.PointIndex].Label);
                        SeriIndex = i;
                        break;
                    }
                }
                PointOnGraph();
            }
        }

        private void button1_Edit_Click(object sender, EventArgs e)
        {
       

             LastPoint.Label = textBoxLabel.Text;
             LastPoint.XValue = Convert.ToDouble(textBoxXValue.Text);
             LastPoint.YValues[0] = Convert.ToDouble(textBoxYValue.Text);
            
            LastSeris.Points.RemoveAt(EmotionIndex);
            InsertDataPoint(LastPoint);
        }
        void InsertDataPoint(DataPoint temp)
        {
            bool added = false;
            for (int i = 0; i < LastSeris.Points.Count; i++)
            {
                if (LastSeris.Points[i].XValue >= temp.XValue)
                {

                    LastSeris.Points.Insert(i, temp);
                    chart1.Invalidate();
                    added = true;
                    break;
                }
            }
            if(!added)
            {
                LastSeris.Points.Add(temp);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
          
            LastPoint = new DataPoint(Convert.ToDouble(textBoxXValue.Text), Convert.ToDouble(textBoxYValue.Text));
            LastPoint.Label = textBoxLabel.Text;
            InsertDataPoint(LastPoint);
            

        }
        double s = 100;
        double c = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            zoom(.01f);
            chart1.Invalidate();
        }
        void zoom(double _amount)
        {
            s -= _amount;
            c += _amount;
           
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(c, s);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(c, s);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zoom(-.01f);
        }

        private void chart1_Paint(object sender, PaintEventArgs e)
        {

            //if (pt != null)
            //{
              
             //   chartx = chart1.ChartAreas[0].AxisX.PixelPositionToValue(pt.X);
             //  charty = chart1.ChartAreas[0].AxisY.PixelPositionToValue(pt.Y);
                
            //   // MessageBox.Show(chartx.ToString() + " " + charty.ToString());
            //}
        }
    

        private void PointOnGraph()
        {
            if (SeriIndex > 0)
            {
                LastPoint = chart1.Series[SeriIndex].Points[EmotionIndex];
                textBoxLabel.Text = chart1.Series[SeriIndex].Points[EmotionIndex].Label;
                textBoxXValue.Text = chart1.Series[SeriIndex].Points[EmotionIndex].XValue.ToString();
                textBoxYValue.Text = chart1.Series[SeriIndex].Points[EmotionIndex].YValues[0].ToString();
            }
         

            //   chart1.Invalidate();
        }
        
    }
}
