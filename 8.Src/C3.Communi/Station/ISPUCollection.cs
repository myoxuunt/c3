
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class SPUCollection : Collection<ISPU>
    {
        public ISPU this[StationType stationType]
        {
            get 
            {
                ISPU r = null;
                foreach (ISPU item in this)
                {
                    if (item.StationType == stationType)
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
