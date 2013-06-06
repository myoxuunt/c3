using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class UInt16Converter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            uint u = Convert.ToUInt16(obj);
            byte[] bs = BitConverter.GetBytes(u);
            bs = this.ReverseWithIsLittleEndian(bs);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            bytes = this.ReverseWithIsLittleEndian(bytes);
            UInt16 n = BitConverter.ToUInt16(bytes, 0);
            return n;
        }
    }


    public class UInt64Converter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            UInt64  u = Convert.ToUInt64(obj);
            byte[] bs = BitConverter.GetBytes(u);
            bs = this.ReverseWithIsLittleEndian(bs);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            bytes = this.ReverseWithIsLittleEndian(bytes);
            UInt64 n = BitConverter.ToUInt64 (bytes, 0);
            return n;
        }
    }

    public class DoubleConverter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            double d = Convert.ToDouble (obj);
            byte[] bs = BitConverter.GetBytes(d);
            bs = this.ReverseWithIsLittleEndian(bs);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            bytes = this.ReverseWithIsLittleEndian(bytes);
            double d = BitConverter.ToDouble(bytes, 0);
            return d;
        }
    }
}
