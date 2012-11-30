
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common
{
    public class DecimalFormatter : NumberFormatterBase
    {
        public DecimalFormatter()
            : base( typeof(double) )
        {

        }


        public DecimalFormatter( int digits )
            : base( typeof(double), digits )
        {

        }

        public override object Format(object value)
        {
            decimal v = (decimal)value;
            return Math.Round(v, Digits);
        }
    }

}
