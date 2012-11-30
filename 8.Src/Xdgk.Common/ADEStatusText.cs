using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
  /// <summary>
    /// 
    /// </summary>
    public class ADEStatusText
    {
        /// <summary>
        /// 
        /// </summary>
        private ADEStatusText()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adeStatus"></param>
        /// <returns></returns>
        static public string GetText(ADEStatus adeStatus)
        {
            string r = adeStatus.ToString();
            switch (adeStatus)
            {
                case ADEStatus.Add :
                    r = ADEStatusStrings.Add;
                    break;

                case ADEStatus.Edit :
                    r = ADEStatusStrings.Edit;
                    break;

                case ADEStatus.Delete :
                    r = ADEStatusStrings.Delete;
                    break;
            }
            return r;
        }
    }
}