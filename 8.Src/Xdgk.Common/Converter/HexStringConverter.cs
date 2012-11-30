using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    /// <summary>
    /// 提供十六进制字符串到byte[]之间的转换
    /// </summary>
    //public class HexStringConverter : C3.Communi.BytesConverter
    public class HexStringConverter 
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static HexStringConverter Default = new HexStringConverter();

        static public readonly char[] SplitChars = new char[] {' ', '\t'};


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        static public byte[] HexStringToBytes(string hexString)
        {
            if (hexString == null)
            {
                return new byte[0];
            }

            string str = hexString.ToString().Trim();
            string[] items = str.Split(SplitChars);
            if (items == null || items.Length == 0)
            {
                return new byte[0];
            }

            ArrayList list = new ArrayList();
            byte[] bs = new byte[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                string temp = items[i].Trim();
                if (temp.Length > 0)
                {
                    byte b = Convert.ToByte(temp, 16);
                    list.Add(b);
                }
            }
            return (byte[])list.ToArray(typeof(byte));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        static public string BytesToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }
            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
                if (i != bytes.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        #region IBytesConverter 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] ConvertToBytes(object obj)
        {
            return HexStringToBytes(obj.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public object ConvertToObject(byte[] bytes)
        {
            return BytesToHexString(bytes);
        }
        #endregion
    }
}
