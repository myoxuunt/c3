
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common
{
    abstract public class NumberFormatterBase : DataFormatterBase
    {

        protected NumberFormatterBase(Type dataType)
            : base(dataType)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digits"></param>
        protected NumberFormatterBase(Type dataType,int digits)
            : this(dataType)
        {
            this.Digits = digits;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Digits
        {
            get { return _digits; }
            set 
            {
                if (value < 0)
                    value = 0;
                _digits = value; 
            }
        } private int _digits = 2;

    }

}
