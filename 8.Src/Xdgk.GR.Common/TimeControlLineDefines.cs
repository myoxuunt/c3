
using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;


namespace Xdgk.GR.Common
{
    public class TimeControlLineDefines
    {
        public const int MinAdjustValue = -9;
        public const int MaxAdjustValue = +9;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public bool CheckTimeAdjustValue(int value)
        {
            return value >= MinAdjustValue && value <= MaxAdjustValue;
        }
    }

}
