
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common
{
    public class Int32Formatter : DataFormatterBase 
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32Formatter()
            : base(typeof(Int32))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        override public object Format(object value)
        {
            return (Int32)value;
        }
    }

}
