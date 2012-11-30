
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common
{
    public class DoubleFormatter : NumberFormatterBase
    {
        public DoubleFormatter()
            : base( typeof(double) )
        {

        }


        public DoubleFormatter( int digits )
            : base( typeof(double), digits )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        override public object Format(object value)
        {
            double v = (double)value;
            return Math.Round(v, this.Digits);
        }
    }

}
