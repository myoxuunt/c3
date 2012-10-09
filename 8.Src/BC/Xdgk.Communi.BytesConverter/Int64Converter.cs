using System;

namespace Xdgk.Communi
{
    public class Int64Converter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            Int64 value = Convert.ToInt64(obj);
            byte[] bs = BitConverter.GetBytes(value);
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
            Int64 value = BitConverter.ToInt64(bytes, 0);
            return value;
        }
    }

}
