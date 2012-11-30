using System;

namespace Xdgk.Common
{
    public class SingleFormatter : NumberFormatterBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SingleFormatter()
            : base(typeof(Single))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dotNumber"></param>
        public SingleFormatter(int digits)
            : base(typeof(Single), digits)
        {
        }

        /// <summary>
        /// ½«Single×ª»»ÎªDouble
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        override public object Format(object value)
        {
            if (value == DBNull.Value)
            {
                return -1d;
            }
            Single v = (Single)value;
            return (double)Math.Round(v, this.Digits);
        }
    }
}
