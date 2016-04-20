using System;
using System.Collections.Generic;

using Affdex;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using System.Data.SQLite;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    /// <summary>
    /// Graph Window For Revewing data
    /// </summary>
    public partial class RecordingAffectivaReview : DockPanelFormBase, Affdex.ProcessStatusListener
    {
        public double chartx, charty;
        AffectivaDataRecordingEmotionsandExpressions AfCFRAVR;
        public delegate void LetOthersTakeMyData(AffectivaDataRecordingEmotionsandExpressions _Var);
        public LetOthersTakeMyData PassOffMyDataDelegate;
        public delegate void GraphWorkDelegate();
        int EmotionIndex = -1;
        Series LastSeris;
        public GraphWorkDelegate DrawingWorkDelegate;
        DataPoint LastPoint;
        int markersize = 20;
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
        double MouseposX;
        double MouseposY;
        double ChartLength = 0;
        MouseEventArgs MouseEventsToHandle;
        /// <summary>
        /// bare bones for standard graph
        /// </summary>
        public RecordingAffectivaReview()
        {
            InitializeComponent();
            comboBoxEmotionSelect.SelectedIndex = 1;
            LastSeris = chart1.Series[comboBoxEmotionSelect.Text];
            comboBoxEmotionSelect.SelectedIndex = 0;
        }
        /// <summary>
        /// For setting up for post processing
        /// </summary>
        /// <param name="_AFCAMandVIdeo"></param>
        /// <param name="_PostProcesses"></param>
        public RecordingAffectivaReview(AffectivaDataRecordingEmotionsandExpressions _AFCAMandVIdeo, bool _PostProcesses)
        {
            InitializeComponent();
            comboBoxEmotionSelect.SelectedIndex = 0;
            AffData = _AFCAMandVIdeo;

            SetupChart(_AFCAMandVIdeo);

        }
        /// <summary>
        /// For Starting a chart from Recording Tool
        /// </summary>
        /// <param name="AfCFRAVR"></param>
       private void SetupChart(AffectivaDataRecordingEmotionsandExpressions AfCFRAVR)
       {
           SetupChartFromSeries(AfCFRAVR.GetChartSeriesOfData());
      
       }
        /// <summary>
        /// Start a chart from a series
        /// </summary>
        /// <param name="seri"></param>
        private void SetupChartFromSeries(List<System.Windows.Forms.DataVisualization.Charting.Series> seri)
        {

            for (int count = 0; count < seri.Count; count++)
            {
                chart1.Series.Add(seri[count]);
                chart1.Series[count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.Series[count].ChartArea = chart1.ChartAreas[0].Name;
            }


            chart1.Invalidate();
        }
        /// <summary>
        /// Push in data from another recording tool
        /// </summary>
        /// <param name="_MergeIn"></param>
        public void MergeInData(AffectivaDataRecordingEmotionsandExpressions _MergeIn)
        {
            if (chart1.Series.Count > 0)
            {
                MergeInSeriesOfList(_MergeIn.GetChartSeriesOfData());

            }
            else
            {
                SetupChart(_MergeIn);
            }
        }
        /// <summary>
        /// Get new Series to mereged list
        /// </summary>
        /// <param name="seri"></param>
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
        /// <summary>
        /// This is where the Processor listener returns information for post processing excetions
        /// </summary>
        /// <param name="ex"></param>
        public void onProcessingException(AffdexException ex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// This is where the Processor listener returns information for post processing
        /// </summary>
        /// <param name="ex"></param>
        public void onProcessingFinished()
        {
            PassOffMyDataDelegate += MergeInData;
            Invoke(PassOffMyDataDelegate, AffData);
        }


        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseEventsToHandle = e;
                HitTestResult result = chart1.HitTest(e.X, e.Y);

                EmotionIndex = result.PointIndex;
                LastSeris = result.Series;


                if (result.PointIndex > -1)
                {
                    for (int i = 0; i < chart1.Series.Count; i++)
                    {
                        if (chart1.Series[i] == LastSeris)
                        {
                            //MessageBox.Show(chart1.Series[i].Points[result.PointIndex].Label);
                            comboBoxEmotionSelect.SelectedIndex = i;
                            break;
                        }
                    }
                    PointOnGraph();

                }
            }
            if (e.Button == MouseButtons.Right)
            {
                // add drag feature
                if (LastPoint != null && LastSeris != null)
                {

                    LastPoint.Color = LastSeris.Color;
                    LastPoint.XValue = MouseposX;
                    LastPoint.YValues[0] = MouseposY;
                    RemovePoint(LastPoint, LastSeris);
                    InsertDataPoint(LastPoint, LastSeris);
                }
            }
        }

        private void button1_Edit_Click(object sender, EventArgs e)
        {

            if (LastPoint != null)
            {
                RemovePoint(LastPoint, LastSeris);
                LastPoint.ToolTip = richTextBoxDesrciption.Text;
                LastPoint.Label = textBoxLabel.Text;
                LastPoint.XValue = Convert.ToDouble(textBoxXValue.Text);
                LastPoint.YValues[0] = Convert.ToDouble(textBoxYValue.Text);
                LastPoint.MarkerStyle = MarkerStyle.Circle;
                LastPoint.MarkerSize = markersize;
                LastSeris = chart1.Series[comboBoxEmotionSelect.Text];
                InsertDataPoint(LastPoint, LastSeris);
            }
        }
        void InsertDataPoint(DataPoint temp, Series _LastSerie)
        {

            bool added = false;
            for (int i = 0; i < _LastSerie.Points.Count; i++)
            {
                if (_LastSerie.Points[i].XValue >= temp.XValue)
                {

                    _LastSerie.Points.Insert(i, temp);
                    chart1.Invalidate();
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                _LastSerie.Points.Add(temp);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            LastPoint = new DataPoint(Convert.ToDouble(textBoxXValue.Text), Convert.ToDouble(textBoxYValue.Text));
            LastPoint.Label = textBoxLabel.Text;
            LastPoint.ToolTip = richTextBoxDesrciption.Text;
            LastPoint.MarkerStyle = MarkerStyle.Triangle;
            LastPoint.MarkerSize = markersize;
            LastSeris = chart1.Series[comboBoxEmotionSelect.Text];
            InsertDataPoint(LastPoint, LastSeris);


        }
        /// <summary>
        /// Removes Points from series
        /// </summary>
        /// <param name="_Point"></param>
        /// <param name="_LastSerie"></param>
        private void RemovePoint(DataPoint _Point, Series _LastSerie)
        {
            if (_LastSerie != null)
                if (_LastSerie.Points.Contains(_Point))
                    _LastSerie.Points.Remove(_Point);
        }
        /// <summary>
        /// Button Click for removing a ppoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemovePeice(object sender, EventArgs e)
        {
            if (LastPoint != null)
            {
                RemovePoint(LastPoint, LastSeris);
                chart1.Invalidate();
            }
        }
        /// <summary>
        /// SetZoom for Chart Area of chart
        /// </summary>
        /// <param name="_amount"></param>
        void zoom(double _amount)
        {


            FindLenghtOfInformation();
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, ChartLength);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100);

        }
        /// <summary>
        /// Finds te longest x value point in the Chart
        /// </summary>
        void FindLenghtOfInformation()
        {
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            int count;
            double tempx;
            for (int countofseries = 0; countofseries < chart1.Series.Count; countofseries++)
            {
                count = chart1.Series[countofseries].Points.Count;
                tempx = chart1.Series[countofseries].Points[count - 1].XValue;
                if (ChartLength < tempx)
                    ChartLength = tempx;
            }
        }
        private void ZoomReset(object sender, EventArgs e)
        {
            zoom(0);
        }
        /// <summary>
        /// Made to see if mouse is right click while moveing to move a point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                MouseEventsToHandle = e;

                chart1.Invalidate();
            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MouseEventsToHandle = e;
                HitTestResult result = chart1.HitTest(e.X, e.Y);
                //  MessageBox.Show(e.X.ToString()+" "+ e.Y.ToString());
                EmotionIndex = result.PointIndex;
                LastSeris = result.Series;


                if (result.PointIndex > -1)
                {
                    for (int i = 0; i < chart1.Series.Count; i++)
                    {
                        if (chart1.Series[i] == LastSeris)
                        {
                            //MessageBox.Show(chart1.Series[i].Points[result.PointIndex].Label);
                            comboBoxEmotionSelect.SelectedIndex = i;
                            break;
                        }
                    }
                    PointOnGraph();
                    LastPoint.Color = System.Drawing.Color.AliceBlue;
                }
            }
        }
        /// <summary>
        /// Paint function is needed to find the postion on map realted to coordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_Paint(object sender, PaintEventArgs e)
        {

            if (MouseEventsToHandle != null)
                if (MouseEventsToHandle.Button == MouseButtons.Right)
                {
                    //if (!(MouseEventsToHandle.Location.X > 100 || MouseEventsToHandle.Location.X < 0 || MouseEventsToHandle.Location.Y > 100 || MouseEventsToHandle.Location.Y < 0))
                    //{
                    try
                    {
                        MouseposX = chart1.ChartAreas[0].AxisX.PixelPositionToValue(MouseEventsToHandle.Location.X);

                        MouseposY = chart1.ChartAreas[0].AxisY.PixelPositionToValue(MouseEventsToHandle.Location.Y);



                        textBoxXValue.Text = MouseposX.ToString();
                        textBoxYValue.Text = MouseposY.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    // }
                }

        }
        /// <summary>
        /// Make sure text boxes are only numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!Char.IsDigit(c) && c != 8 && c != '.')
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Finds Last point found by click from the index
        /// </summary>
        private void PointOnGraph()
        {
            if (EmotionIndex != -1)
            {
                LastPoint = LastSeris.Points[EmotionIndex];
                textBoxLabel.Text = LastSeris.Points[EmotionIndex].Label;
                textBoxXValue.Text = LastSeris.Points[EmotionIndex].XValue.ToString();
                textBoxYValue.Text = LastSeris.Points[EmotionIndex].YValues[0].ToString();
                richTextBoxDesrciption.Text = LastPoint.ToolTip;
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveDataToDataBase();
        }

        //Load data from DataBase (returns table, named dt that holds all the data from the DB)
        private void Load_Button_Click(object sender, EventArgs e)
        {
            try
            {
                TesterDB temp = new TesterDB();
                temp.LoadToChart(ref chart1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
                   
        }
        //------------------------------------------------------------------------------------------------
        

        private void SaveDataToDataBase()
        {
            TesterDB TempDataBase = new TesterDB();
            TempDataBase.CreateNewDBConnection();
            TempDataBase.NewTableCommand();
            List<Series> temp = new List<Series>();
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                temp.Add(chart1.Series[i]);
            }

            TempDataBase.PopulateNewTable(temp);
        }
    }
}
