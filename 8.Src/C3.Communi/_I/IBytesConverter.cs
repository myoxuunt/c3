
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace C3.Communi
{
    public interface IBytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytest"></param>
        /// <returns></returns>
        object ConvertToObject(byte[] bytes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        byte[] ConvertToBytes(object obj);

        /// <summary>
        /// 
        /// </summary>
        bool IsLittleEndian
        {
            get;
            set;
        }

        IBytesConverter InnerBytesConverter
        {
            get;
            set;
        }

    }

}
