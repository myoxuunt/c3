using System;
using System.Collections.Generic;
using System.Text;

namespace HZ.BytesConverter
{
    /// <summary>
    /// 
    /// </summary>
    public class AsciiValueConverter : 
        Xdgk.Communi.Interface.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            throw new NotImplementedException(
                "AsciiValueConverter.ConvertToBytes");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            string s = "";
            foreach (byte b in bytes)
            {
                s += (char)b;
            }
            double d = Convert.ToDouble(s);
            return d;
            //int n = Convert.ToInt32(d);
            //return n;
        }
    }
}
