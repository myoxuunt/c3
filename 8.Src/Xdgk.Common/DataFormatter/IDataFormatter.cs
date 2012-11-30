
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common
{
    public interface IDataFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        Type DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object Format(object value);
    }

}
