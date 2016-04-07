using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affdex;
namespace UX_Affectiva_Research_Tool
{
    /// <summary>
    /// this is a base class to help oraginize others use tools together
    /// Make sure you put in your constructor everthing needed  to setup the tool
    /// if you see a return type, use that to help indicate if the function was succesful
    /// This class works the data class record information and be passed back as needed
    /// </summary>
   public abstract class RecordingToolBase
    {
        
            /// <summary>
            /// You should name the tool being used in the constructor of the child class
            /// </summary>
            protected string nameOfTool;

            /// <summary>
            /// To help others used to you tool,
            /// put here what is needed to start the tool here
            /// but to set up the tool should be in the constructor
            /// 
            /// </summary>
            /// <returns></returns>
            public virtual bool startRecording()
            {
                throw new NotImplementedException();
                // return false;
            }

            public virtual bool stopRecording()
            {
                throw new NotImplementedException();
            }
            /// <summary>
            /// this function should make the tool reusable
            /// for another recording
            /// </summary>
            /// <returns></returns>
            public virtual bool resetRecording()
            {
                throw new NotImplementedException();
            }

            public virtual bool saveRecordingData()
            {
                throw new NotImplementedException();
            }

            public virtual bool setRecordingData(ref object _data)
            {
                throw new NotImplementedException();
            }
            /// <summary>
            /// meant to make sure you have data object made and 
            /// return object if is made succesfully
            /// </summary>
            /// <param name="_data"></param>
            /// <returns></returns>

            public virtual BaseClasses.BaseData getRecordingData()
            {

                throw new NotImplementedException();
            }

            public virtual bool isRunningRecording()
            {
                throw new NotImplementedException();
            }
            public string getNameofTool()
            {
                return nameOfTool;
            }
            public void setNameofTool(string _Name)
            {
                nameOfTool = _Name;
            }

        }
}
