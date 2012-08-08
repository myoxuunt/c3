
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DPUCollection : Xdgk.Common.Collection<IDPU>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public IDPU this[DeviceType deviceType]
        {
            get
            {
                IDPU r = null;
                foreach (IDPU item in this)
                {
                    if (item.DeviceType == deviceType)
                    {
                        r = item;
                        break;
                    }
                }
                return r;
            }
        }
    }

}
