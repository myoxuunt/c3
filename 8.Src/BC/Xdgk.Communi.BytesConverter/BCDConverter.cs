using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace Xdgk.Communi
{
    /// <summary>
    /// 提供BCD转换
    /// </summary>
    public class BCDConverter : BytesConverter
    {
        #region Static
        /// <summary>
        /// 十进制值转换为十六进制值， 10 -> 0x10
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        static public int DECToBCD(int dec)
        {
            return Convert.ToInt32(dec.ToString(), 16);
        }

        /// <summary>
        /// 十六进制值转换为十进制值， 0x10 -> 10
        /// </summary>
        /// <param name="bcd"></param>
        /// <returns></returns>
        static public int BCDToDEC(int bcd)
        {
            return int.Parse(bcd.ToString("X"));
        }
        #endregion //Static

        /// <summary>
        /// 转换的字节个数
        /// </summary>
        public virtual int ByteCount
        {
            get { return _byteCount; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("ByteCount must >= 1");
                }
                _byteCount = value;
            }
        } private int _byteCount = 1;

        /// <summary>
        /// BCD低位在前
        /// </summary>
        /// 
        public bool LowByteFirst
        {
            get { return _lowByteFirst; }
            set { _lowByteFirst = value; }
        } private bool _lowByteFirst = true;

        #region IBytesConverter 成员
        /// <summary>
        /// 将BCD形式bytes数组的转换为数字,仅第一个字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>int</returns>
        override public object ConvertToObject(byte[] bytes)
        {
            // 1. byte[] -> object
            // 2. byte[0] -> int
            // 3. (bcd) -> int
            //
            //byte b = bytes[0];
            //int n = BCDToDEC(b);
            //return n;
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length != this.ByteCount)
            {
                string exmsg = string.Format("bytes.Length != BytesCount, expect: {0}", this.ByteCount);
                throw new ArgumentNullException(exmsg);
            }

            if (_lowByteFirst)
            {
                bytes = (byte[])bytes.Clone();
                Array.Reverse(bytes);
            }

            string s = "";
            for (int i = 0; i < this.ByteCount; i++)
            {
                s += BCDToDEC(bytes[i]).ToString("00");
            }
            return int.Parse(s);
        }

        /// <summary>
        /// 将数字转换为BCD形式的byte数组
        /// </summary>
        /// <param name="obj">int</param>
        /// <returns>一个字节的数组</returns>
        override public byte[] ConvertToBytes(object obj)
        {
            // obj -> byte[]
            // int -> byte[0]
            // int -> hex
            //
            //int n = Convert.ToInt32(obj);
            //int m = DECToBCD(n);
            //return new byte[] { (byte)m };
            string numberformat = new string('0', this.ByteCount * 2);
            int n = Convert.ToInt32(obj);
            string numStr = n.ToString(numberformat);
            byte[] r = new byte[this.ByteCount];

            for (int i = 0; i < this.ByteCount; i++)
            {
                r[i] = (byte)DECToBCD(int.Parse(numStr.Substring(i * 2, 2)));
            }

            if (_lowByteFirst)
            {
                Array.Reverse(r);
            }
            return r;
        }

        #endregion
    }


    /// <summary>
    /// 
    /// </summary>
    public class BCD4ByteConverter : BCDConverter
    {
        /// <summary>
        /// 
        /// </summary>
        public BCD4ByteConverter()
        {
            this.ByteCount = 4;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BCD2ByteConverter : BCDConverter
    {
        public BCD2ByteConverter()
        {
            this.ByteCount = 2;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BCD1ByteConverter : BCDConverter
    {
        public BCD1ByteConverter()
        {
            this.ByteCount = 1;
        }
    }

}
