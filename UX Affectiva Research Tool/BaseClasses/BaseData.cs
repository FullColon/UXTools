using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UX_Affectiva_Research_Tool.BaseClasses
{
    /// <summary>
    /// This is a start for custom data classes for easier grouping of information
    /// This will should be used to make custom classes 
    /// </summary>
  public abstract class BaseData
    {
        /// <summary>
        /// Each Data class will have custom ways to savethemselves,
        /// This this base should be considered for new tools that record data
        /// </summary>
        /// <returns></returns>
       public virtual bool saveDataToAFile()
        {
            throw new NotImplementedException();
        }
       public virtual bool loadDataFromFile()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// make a defualt verison of information to work with for debugging
        /// </summary>
        /// <returns></returns>
       public virtual bool genrate()
        {
            throw new NotImplementedException();
        }

    }
}
