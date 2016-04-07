using System;
using System.Collections.Generic;
using System.Linq;
using Affdex;

namespace UX_Affectiva_Research_Tool.Affectiva_Files
{
    class PostAffectivaEvaltionTool : RecordingToolBase, Affdex.FaceListener,  Affdex.ImageListener
    {



        
        string mpath;
        /// <summary>
        /// Affectiva requires a certain file to work, its called a data
        /// </summary>
        private string mDataDirectory;
        /// <summary>
        /// Affectiva requires a certain file to work, its called a data
        /// </summary>
        public void setDataDirectory(string _dir)
        {
            mDataDirectory = _dir;
        }
        /// <summary>
        /// Affectiva requires a certain file to work, its called a data
        /// </summary>
        public string getDataDirectory()
        {
            return mDataDirectory;
        }
        /// <summary>
        /// affectiva requires a lincense
        /// </summary>
        private string mLincenseDirectory;
        /// <summary>
        /// affectiva requires a lincense
        /// </summary>
        public void setLincenseDirectory(string _dir)
        {
            mLincenseDirectory = _dir;
        }
        public string getLincenseDirectory()
        {
            return mLincenseDirectory;
        }
        /// <summary>
        /// this is to help detected the new emotion by giving a buffer
        /// </summary>
        private float mValenceOfEmotion;
        /// <summary>
        /// this is to help detected the new emotion by giving a buffer
        /// </summary>
        public void setValenceOfEmotion(float _valence)
        {
            mValenceOfEmotion = _valence;
        }
        /// <summary>
        /// this is to help detected the new emotion by giving a buffer
        /// </summary>
        public float getValenceOfEmotion()
        {
            return mValenceOfEmotion;
        }
        /// <summary>
        /// this is for making autotags dont overflow the system
        /// </summary>
        private float mTimeStep;
        public void setTimeStep(float _step)
        {
            mTimeStep = _step;
        }
        public float getTimeStep()
        {
            return mTimeStep;
        }
        /// <summary>
        /// This Structure is to help keep track of the last highest emotion value
        /// So Auto Tags can be used
        /// </summary>
        protected struct ValueChecks
        {
            public float oldvalue;
            public int type;
            public float time;
            public Face oldFace;
            public Frame oldFrame;
        }
        protected void setoldValues()
        {
            mOldValues = new ValueChecks();
            mOldValues.oldvalue = 0;
            mOldValues.type = 0;
            mOldValues.time = 0;
        }
        ValueChecks mOldValues;
        /// <summary>
        /// Data Used to store values
        /// </summary>
        private AffectivaDataRecordingEmotionsandExpressions maffectData;

        /// <summary>
        /// this is used to get current information from the camera
        /// 
        /// </summary>
        Affdex.VideoDetector mVideoDetector;
        /// <summary>
        /// Here is a default setup for Affectiva
        /// </summary>
        public PostAffectivaEvaltionTool(string path, double processrate, Affdex.ProcessStatusListener _Processer)
        {
          


            try
            {

                setValenceOfEmotion(4.0f);
                setoldValues();
                setTimeStep(.1f);
                setDataDirectory(Environment.CurrentDirectory + "\\AffectivaFiles\\data");
                setLincenseDirectory(Environment.CurrentDirectory + "\\AffectivaFiles\\sdk_shawns@fullsail.com.license.txt");
                mVideoDetector = new VideoDetector(processrate);
                setLicensePath(getLincenseDirectory());
                setClassiferFolderPath(getDataDirectory());
                /// turn on detectors for defualt
                mVideoDetector.setDetectAllEmotions(true);
                mVideoDetector.setDetectAllExpressions(true);
                /// set types of detectors for Affdex
                mVideoDetector.setFaceListener(this);
                mVideoDetector.setImageListener(this);
                mVideoDetector.setProcessStatusListener(_Processer);
                maffectData = new AffectivaDataRecordingEmotionsandExpressions();//[maxAmountOfFaces];
                this.mpath = path;
               
               
            

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

        }
        public PostAffectivaEvaltionTool(float _valenceEmotion, float _timeStep, int _cameraId, double _camperaCaptureRate, double _processRate)
        {
           


            try
            {

                setValenceOfEmotion(_valenceEmotion);
                setoldValues();
                setTimeStep(_timeStep);
                setDataDirectory(Environment.CurrentDirectory + "\\AffectivaFiles\\data");
                setLincenseDirectory(Environment.CurrentDirectory + "\\AffectivaFiles\\sdk_shawns@fullsail.com.license.txt");
                mVideoDetector = new VideoDetector();
                setLicensePath(getLincenseDirectory());
                setClassiferFolderPath(getDataDirectory());
                /// turn on detectors for defualt
                mVideoDetector.setDetectAllEmotions(true);
                mVideoDetector.setDetectAllExpressions(true);
                /// set types of detectors for Affdex
                mVideoDetector.setFaceListener(this);
                mVideoDetector.setImageListener(this);
              //  mVideoDetector.setProcessStatusListener(this);
                //   mcamDetector.setProcessStatusListener(this);
                maffectData = new AffectivaDataRecordingEmotionsandExpressions();

             


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        ///  Affectiva Has its own lincensfile to work with
        ///  this is used if lincense is out of place
        /// </summary>
        /// <param name="_LincensPath"></param>
        public void setLicensePath(string _LincensPath)
        {
            mVideoDetector.setLicensePath(_LincensPath);

        }
        /// <summary>
        ///Classifer folder allows affectiva to work with faces and cameras
        ///
        /// </summary>
        /// <param name="_ClassiferFolderPath"></param>
        public void setClassiferFolderPath(string _ClassiferFolderPath)
        {
            mVideoDetector.setClassifierPath(_ClassiferFolderPath);
        }

        /// <summary>
        /// Start the Recording, This will start and catch exceptions for valid ClassiferPaths and Lincesing path as well.
        /// </summary>
        /// <returns></returns>
        public override bool startRecording()
        {
            mVideoDetector.start();
            mVideoDetector.process(mpath);
            return true;
        }
        public override bool stopRecording()
        {


            try
            {

               
                mVideoDetector.stop();
                mVideoDetector.reset();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }

            return true;
        }

        public override BaseClasses.BaseData getRecordingData()
        {

            return maffectData;
        }
        /// <summary>
        /// Saves old data objects Makes a new Data Objects
        /// </summary>
        /// <returns></returns>
        public override bool resetRecording()
        {
            return base.resetRecording();
        }
        public override bool saveRecordingData()
        {

            return base.saveRecordingData();
        }
        public override bool isRunningRecording()
        {
            return mVideoDetector.isRunning();

        }
        /// <summary>
        ///  Given a Peice of data to record from, but does not save old
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public override bool setRecordingData(ref object _data)
        {
            return base.setRecordingData(ref _data);
        }
        public void onFaceFound(float timestamp, int faceId)
        {
            /// throw new NotImplementedException();
          
                maffectData.addAutoEvent(timestamp, "Face_Found:" + faceId.ToString(), "faceId.ToString()");
        }

        public void onFaceLost(float timestamp, int faceId)
        {
           
                // throw new NotImplementedException();
                maffectData.addAutoEvent(timestamp, "Face_Lost:" + faceId.ToString(), "faceId.ToString()");
        }


        /// <summary>
        /// AutoTagging is implemented here when a drastic change of emotions happens
        /// </summary>
        /// <param name="faces"></param>
        /// <param name="frame"></param>
        public void onImageResults(Dictionary<int, Face> faces, Frame frame)
        {
            
                if (faces.Count > 0)
                {
                    if (mOldValues.time < frame.getTimestamp())
                    {
                        Face lastFace = faces.Last().Value;
                        mOldValues.oldFace = lastFace;
                        mOldValues.oldFrame = frame;
                        mOldValues.time = frame.getTimestamp() + getTimeStep();




                        if (DrasticChangeCheck(lastFace))
                        {

                            string newemotion = AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsStrings[mOldValues.type];
                            maffectData.addAutoTag(lastFace.Emotions, lastFace.Expressions, frame.getTimestamp(), "New Top Emotion Detected:" + newemotion, newemotion);
                            Console.WriteLine(newemotion + mOldValues.time.ToString());


                        }
                        else
                            maffectData.addGraphPoints(lastFace.Emotions, lastFace.Expressions, frame.getTimestamp());
                    }
                }
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        public void onImageCapture(Frame frame)
        {

            Console.WriteLine(frame.getTimestamp());
        }
        /// <summary>
        /// made to Detect a change of the new Highest emotion, if no new emotion assign new value of old emtion
        /// </summary>
        /// <param name="_facenew"></param>
        /// <returns></returns>
        public bool DrasticChangeCheck(Face _facenew)
        {

            float _tempValue = 0.0f;
            int _tempType = 0;
            getHighValueFromCurrentFace(_facenew, ref _tempValue, ref _tempType);
            if (_tempType != mOldValues.type)
            {
                if (_tempValue - getValenceOfEmotion() > mOldValues.oldvalue)
                {
                    mOldValues.type = _tempType;
                    mOldValues.oldvalue = _tempValue;
                    return true;
                }

            }
            mOldValues.oldvalue = GetValueForCurrentEmotions(_tempType, _facenew);
            return false;
        }
        /// <summary>
        /// Finds the value for a type of emotion
        /// </summary>
        /// <param name="_compareMeTypeEmotion"></param>
        /// <param name="_currentEmotion"></param>
        /// <returns></returns>
        public float GetValueForCurrentEmotions(int _compareMeTypeEmotion, Face _currentEmotion)
        {
            switch (_compareMeTypeEmotion)
            {
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Anger):
                    {

                        return _currentEmotion.Emotions.Anger;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Contempt):
                    {

                        return _currentEmotion.Emotions.Contempt;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Disgust):
                    {

                        return _currentEmotion.Emotions.Disgust;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Engagment):
                    {

                        return _currentEmotion.Emotions.Engagement;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Fear):
                    {

                        return _currentEmotion.Emotions.Fear;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Joy):
                    {

                        return _currentEmotion.Emotions.Joy;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Sadness):
                    {

                        return _currentEmotion.Emotions.Sadness;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Surprise):
                    {

                        return _currentEmotion.Emotions.Surprise;
                    }
                case ((int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Valence):
                    {

                        return _currentEmotion.Emotions.Valence;
                    }

            }


            return 0.0f;

        }
        /// <summary>
        /// Gets Current High emotion of given face for the frame
        /// </summary>
        /// <param name="_facenew"></param>
        /// <param name="_tempValue"></param>
        /// <param name="_tempType"></param>
        public void getHighValueFromCurrentFace(Face _facenew, ref float _tempValue, ref int _tempType)
        {

            if (_tempValue < _facenew.Emotions.Anger)
            {
                _tempValue = _facenew.Emotions.Anger;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Anger;
            }
            if (_tempValue < _facenew.Emotions.Contempt)
            {
                _tempValue = _facenew.Emotions.Contempt;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Contempt;
            }
            if (_tempValue < _facenew.Emotions.Disgust)
            {
                _tempValue = _facenew.Emotions.Disgust;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Disgust;
            }
            if (_tempValue < _facenew.Emotions.Engagement)
            {
                _tempValue = _facenew.Emotions.Engagement;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Engagment;
            }
            if (_tempValue < _facenew.Emotions.Fear)
            {
                _tempValue = _facenew.Emotions.Fear;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Fear;
            }
            if (_tempValue < _facenew.Emotions.Joy)
            {
                _tempValue = _facenew.Emotions.Joy;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Joy;
            }
            if (_tempValue < _facenew.Emotions.Sadness)
            {
                _tempValue = _facenew.Emotions.Sadness;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Joy;
            }
            if (_tempValue < _facenew.Emotions.Surprise)
            {
                _tempValue = _facenew.Emotions.Surprise;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Surprise;
            }

            if (_tempValue < _facenew.Emotions.Valence)
            {
                _tempValue = _facenew.Emotions.Valence;
                _tempType = (int)AffectivaDataRecordingEmotionsandExpressions.typesOfDataPointsEnum.Valence;
            }


        }

        public void addManuelTag(string _des, uint _typeOfTag, string _NameTag)
        {
            if (mOldValues.oldFace != null)
                maffectData.addManuelTag(mOldValues.oldFace.Emotions, mOldValues.oldFace.Expressions, mOldValues.oldFrame.getTimestamp(), _des, _typeOfTag, _NameTag);
        }
        public void addManuelEvent(string _des, uint _typeOfTag, string _NameTag)
        {
            maffectData.addManuelEvent(mOldValues.oldFrame.getTimestamp(), _des, _NameTag);
        }
        public AffectivaDataRecordingEmotionsandExpressions GetAffectiveData()
        {
            return maffectData;
        }

        
    }
}
