using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class Int16Converter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            Int16 n = Convert.ToInt16(obj);
            byte[] bs = BitConverter.GetBytes(n);
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
            Int16 n = BitConverter.ToInt16(bytes, 0);
            return n;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Int16ModbusConverter : C3.Communi.BytesConverter
    {
        public override byte[] ConvertToBytes(object obj)
        {
            Int16 n = Convert.ToInt16(obj);
            return Int16ToBytes(n);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static public byte[] Int16ToBytes(Int16 n)
        {
            byte[] temp = BitConverter.GetBytes(n);
            Array.Reverse(temp);
            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static public Int16 BytesToInt16(byte[] bytes)
        {
            byte[] temp = (byte[])bytes.Clone();
            Array.Reverse(temp);
            Int16 n = BitConverter.ToInt16(temp, 0);
            return n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            return BytesToInt16(bytes);
        }
    }
}
