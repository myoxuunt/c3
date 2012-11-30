using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    abstract public class DataFormatterBase : IDataFormatter
    {
        abstract public object Format(object value);

        /// <summary>
        /// 
        /// </summary>
        protected DataFormatterBase(Type dataType)
        {
            this.DataType = dataType;
        }
        /// <summary>
        /// 
        /// </summary>
        public Type DataType
        {
            get { return _dataType; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("DataType");
                _dataType = value;
            }
        } private Type _dataType;
    }

}
