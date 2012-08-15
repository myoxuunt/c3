
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class StringParameter : ParameterBase
    {
        public StringParameter(string name, string value, int orderNumber)
            : base(name, typeof(string), value, orderNumber)
        {

        }
    }

}
