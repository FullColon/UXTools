using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{/// <summary>
/// This is for setting up data points to put single frame of information in, to later to put into a series by a AffectData object
/// </summary>
   public class AffectDataPoint
    {
        /// Change to face
        
        Affdex.Emotions frameEmotions;
        Affdex.Expressions frameExpressions;
        float timeStamp;
        int typeOfPoint;
        string description;
        string name;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Emotions"></param>
        /// <param name="_frameExpressions"></param>
        /// <param name="_timeStamp"></param>
        /// <param name="_description"></param>
        /// <param name="_typeOfPoint"></param>
        /// <param name="_name"></param>
       public AffectDataPoint(Affdex.Emotions _Emotions, Affdex.Expressions _frameExpressions,float _timeStamp,string _description, int  _typeOfPoint, string _name)
        {
            frameEmotions = _Emotions;
            frameExpressions = _frameExpressions;
            timeStamp = _timeStamp;
            description = _description;
            typeOfPoint = _typeOfPoint;
            name = _name;
        }
        public string getName()
        {
            return name;
        }
        public string getDescription()
        {
            return description;
        }
        public int GetTypeOfPoint()
        {
            return typeOfPoint;
        }
        public float getTimeStamp()
        {
            return timeStamp;
        }
        public Affdex.Emotions getEmotion()
        {
            return frameEmotions;
        }
    }
}
