using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affdex;
using System.Windows.Forms.DataVisualization.Charting;
using UX_Affectiva_Research_Tool.BaseClasses;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    /// <summary>
    /// This class is meant to place data inside of for recording and after recording place into a series for charts to take from
    /// </summary>
   public class AffectivaDataRecordingEmotionsandExpressions : BaseData
    {
      
        
        /// <summary>
        /// These are for making data objects with spefic tags and event bars
        ///  Events are for book marks for thins outside of the study or for looking for points in the study
        /// </summary>
        public static readonly string[] typesOfDataPointsStrings
            =new string[]             {  "Sadness","Anger","Disgust","Fear","Joy","Surprise","Contempt", "Engagment", "Valence", "ManuelTag","AutoTag","ManuelEventBar","AutoEventBar", "GraphPoint" };
        public enum typesOfDataPointsEnum {Sadness, Anger,  Disgust,  Fear, Joy,   Surprise,  Contempt,  Engagment,    Valence,   ManuelTag,  AutoTag,   ManuelEventBar, AutoEventBar,   GraphPoint };
        
       // Series points to be converted for graph display and buffer bar display;
        List<AffectDataPoint> mrecordedAffDataList;
     
        public AffectivaDataRecordingEmotionsandExpressions()
        {
            mrecordedAffDataList = new List<AffectDataPoint>();

          
            
        }
        /// <summary>
        /// Turn Affectiva data to a list of series so it can pass to a graph
        /// </summary>
        /// <returns></returns>
        public List<Series> GetChartSeriesOfData()
        {
          // mrecordedAffDataList =  SortSeries( mrecordedAffDataList);
            List<Series> ListofSeriestoBeReturned= new List<Series>();
            for (int count = 0; count <= (int)typesOfDataPointsEnum.Valence;count++)
            {
                ListofSeriestoBeReturned.Add(new Series(typesOfDataPointsStrings[count]));
            }

           while(mrecordedAffDataList.Count > 0)
            {
                AffectDataPoint currentPointOfInformation = mrecordedAffDataList.Last();

                // b[point.GetTypeOfPoint()].Points.Add(NewPoint);
                Emotions currentEmotion = currentPointOfInformation.getEmotion();
                float timeStamp = currentPointOfInformation.getTimeStamp();
               

              

                switch (currentPointOfInformation.GetTypeOfPoint())
                {
                    case (int)typesOfDataPointsEnum.AutoTag:
                        {
                            AddGraphPoints(ListofSeriestoBeReturned, currentPointOfInformation, currentEmotion);

                            int HighestEmotion = 0;
                            float ValueOfEmotion = 0;
                            getHighestEmotionFromFace(currentPointOfInformation.getEmotion(), ref HighestEmotion, ref ValueOfEmotion);
                            
                            ListofSeriestoBeReturned[HighestEmotion].Points.Last().ToolTip = currentPointOfInformation.getDescription();
                            ListofSeriestoBeReturned[HighestEmotion].Points.Last().Label ="AutoTag";
                            ListofSeriestoBeReturned[HighestEmotion].Points.Last().Color = System.Drawing.Color.Black;
                            ListofSeriestoBeReturned[HighestEmotion].Points.Last().MarkerStyle = MarkerStyle.Cross;
                            ListofSeriestoBeReturned[HighestEmotion].Points.Last().MarkerSize = 30;
                            





                            break;
                        }
                    case (int)typesOfDataPointsEnum.GraphPoint:
                        {


                            AddGraphPoints(ListofSeriestoBeReturned, currentPointOfInformation, currentEmotion);
                            break;
                        }
                    case (int)typesOfDataPointsEnum.AutoEventBar:
                        {


                         
                            break;
                        }
                    case (int)typesOfDataPointsEnum.ManuelEventBar:
                        {


                         
                            break;
                        }
                    default:
                        {

                            AddGraphPoints(ListofSeriestoBeReturned, currentPointOfInformation, currentEmotion);
                            int type = currentPointOfInformation.GetTypeOfPoint();
                            ListofSeriestoBeReturned[type].Points.Last().ToolTip = currentPointOfInformation.getDescription();
                            ListofSeriestoBeReturned[type].Points.Last().Label =currentPointOfInformation.getName();
                            ListofSeriestoBeReturned[type].Points.Last().Color = System.Drawing.Color.Blue;
                            ListofSeriestoBeReturned[type].Points.Last().MarkerStyle = MarkerStyle.Diamond;
                            ListofSeriestoBeReturned[type].Points.Last().MarkerSize = 30;


                            break;
                        }
                }
                mrecordedAffDataList.Remove(mrecordedAffDataList.Last());
                
            }
            for (int count = 0; count < ListofSeriestoBeReturned.Count; count++)
            {
              
                ListofSeriestoBeReturned[count].Sort(PointSortOrder.Ascending, "X"); //     b[0].Points.Add(c);
            }
            return ListofSeriestoBeReturned;
        }
        public void AddGraphPoints(List<Series> ListofSeriestoBeReturned, AffectDataPoint currentPointOfInformation, Emotions currentEmotion)
        {
            DataPoint NewPoint;
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Anger);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Anger].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Contempt);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Contempt].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Disgust);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Disgust].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Engagement);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Engagment].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Fear);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Fear].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Joy);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Joy].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Sadness);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Sadness].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Surprise);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Surprise].Points.Add(NewPoint);
            NewPoint = new DataPoint(currentPointOfInformation.getTimeStamp(), currentEmotion.Valence);
            ListofSeriestoBeReturned[(int)typesOfDataPointsEnum.Valence].Points.Add(NewPoint);
        }
        public int getHighestEmotionFromFace(Emotions _facenew, ref int _tempType, ref float _tempValue)
        {
            _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Anger;
            _tempValue = _facenew.Anger;
            
            if (_tempValue < _facenew.Contempt)
            {
                _tempValue = _facenew.Contempt;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Contempt;
            }
            if (_tempValue < _facenew.Disgust)
            {
                _tempValue = _facenew.Disgust;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Disgust;
            }
            if (_tempValue < _facenew.Engagement)
            {
                _tempValue = _facenew.Engagement;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Engagment;
            }
            if (_tempValue < _facenew.Fear)
            {
                _tempValue = _facenew.Fear;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Fear;
            }
            if (_tempValue < _facenew.Joy)
            {
                _tempValue = _facenew.Joy;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Joy;
            }
            if (_tempValue < _facenew.Sadness)
            {
                _tempValue = _facenew.Sadness;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Joy;
            }
            if (_tempValue < _facenew.Surprise)
            {
                _tempValue = _facenew.Surprise;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Surprise;
            }

            if (_tempValue < _facenew.Valence)
            {
                _tempValue = _facenew.Valence;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Valence;
            }

            return _tempType;
        }
        /// <summary>
        /// adds a genric point for all lines to take a reading from to make a chart, these should loaded automaticly
        /// </summary>  
        /// <param name="_Emotions"></param>
        /// <param name="_Expressions"></param>
        /// <param name="_timestamp"></param>
        public void addGraphPoints(Affdex.Emotions _Emotions, Affdex.Expressions _Expressions, float _timestamp )
        {
            mrecordedAffDataList.Add(new AffectDataPoint(_Emotions, _Expressions, _timestamp, "N\\A", (int)typesOfDataPointsEnum.GraphPoint,"GraphPoint"));
        }
        /// <summary>
        /// Adds a tag to identity a drastic change in emotion, these should be made automatic
        /// </summary>
        /// <param name="_Emotions"></param>
        /// <param name="_Expressions"></param>
        /// <param name="_timestamp"></param>
        /// <param name="_Description"></param>
        public void addAutoTag(Affdex.Emotions _Emotions, Affdex.Expressions _Expressions, float _timestamp, string _Description, string _NameofTag)
        {
            mrecordedAffDataList.Add(new AffectDataPoint(_Emotions, _Expressions, _timestamp, _Description, (int)typesOfDataPointsEnum.AutoTag, _NameofTag));
        }
        /// <summary>
        /// This function is meant for descriptive types of tags for emotions spefically, should not be automatic
        /// </summary>
        /// <param name="_Emotions"></param>
        /// <param name="_Expressions"></param>
        /// <param name="_timestamp"></param>
        /// <param name="_Description"></param>
        /// <param name="_TypeofTag"></param>
        public void addManuelTag(Affdex.Emotions _Emotions, Affdex.Expressions _Expressions, float _timestamp, string _Description, uint _TypeofTag, string _NameofTag)
        {
            mrecordedAffDataList.Add(new AffectDataPoint(_Emotions, _Expressions, _timestamp, _Description,(int) _TypeofTag, _NameofTag));
        }
        public void addManuelTag( float _timestamp, string _Description, uint _TypeofTag, string _NameofTag)
        {
            mrecordedAffDataList.Add(new AffectDataPoint(new Emotions(), new Expressions(), _timestamp, _Description, (int)_TypeofTag, _NameofTag));
        }
        /// <summary>
        ///
        /// This is should be when the users looks away from the camera,
        /// </summary>
        /// <param name="_timestamp"></param>
        /// <param name="_Description"></param>
        public void addAutoEvent(float _timestamp, string _Description, string _NameofTag)
        {
            mrecordedAffDataList.Add(new AffectDataPoint(null, null, _timestamp, _Description,(int)typesOfDataPointsEnum.AutoEventBar, _NameofTag));
        }
        /// <summary>
        /// 
        /// This Should be when the users needs to place point for easier find in the stuyd
        /// example like a boss fight,
        /// </summary>
        /// <param name="_timestamp"></param>
        /// <param name="_Description"></param>
        public void addManuelEvent( float _timestamp, string _Description, string _NameofTag)
        {
            mrecordedAffDataList.Add(new AffectDataPoint(null, null, _timestamp, _Description,(int)typesOfDataPointsEnum.ManuelEventBar, _NameofTag));
        }

        //private List<AffectDataPoint> SortSeries( List<AffectDataPoint> _b)
        //{
        //    for(int countdown = _b.Count-1; countdown > 0; countdown--)
        //    {
        //        //  AffectDataPoint curpont =   _b[countdown];
        //        float timecur = _b[countdown].getTimeStamp();
        //        int tempnum = countdown;
        //        for (int counttrack = countdown - 1; counttrack > 0; counttrack--)
        //        {
        //            if (timecur < _b[counttrack].getTimeStamp())
        //            {
        //                AffectDataPoint curpont = _b[counttrack];
        //                _b[counttrack] = _b[tempnum];
        //                _b[tempnum] = curpont;
        //                tempnum = counttrack;
        //            }
                   
        //        }

        //    }
        //    return _b;
        //}
    }

}
