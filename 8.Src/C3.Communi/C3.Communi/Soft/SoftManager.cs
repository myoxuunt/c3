using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class SoftManager
    {
        static private Soft _soft = new Soft();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Soft GetSoft()
        {
            return _soft;
        }
    }
}
