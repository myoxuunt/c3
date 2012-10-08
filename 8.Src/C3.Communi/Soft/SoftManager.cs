using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class SoftManager
    {
        static private Soft _soft;//= new Soft();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Soft GetSoft()
        {
            //Debug.Assert(_soft != null , "softManager._soft == null");
            if (_soft == null)
            {
                _soft = new Soft();
            }
            return _soft;
        }
    }
}
