
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class NumberParameter : ParameterBase
    {
        public NumberParameter(string name, Type valueType, object value, int orderNumber)
            : base(name, valueType, value, orderNumber)
        {
            //TODO: check value type
            // 
            // is byte int32 short long float double ...
        }
    }

}
