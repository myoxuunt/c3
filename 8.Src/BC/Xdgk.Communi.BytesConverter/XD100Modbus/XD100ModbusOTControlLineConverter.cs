using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using C3.Communi;

namespace Xdgk.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class XD100ModbusOTControlLineConverter : BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        private const int POINT_COUNT = 8;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            KeyValuePair<int, int>[] values = obj as KeyValuePair<int, int>[];
            if (values == null)
                throw new ArgumentException("obj is not KeyValuePair<int,int>");

            MemoryStream ms = new MemoryStream();

            for (int i = 0; i < POINT_COUNT; i++)
            {
                KeyValuePair<int, int> v = values[i];
                byte[] xbs = GetInt16Bytes(v.Key);
                ms.Write(xbs, 0, xbs.Length);
                byte[] ybs = GetInt16Bytes(v.Value);
                ms.Write(ybs, 0, ybs.Length);
            }
            byte[] bytes = ms.ToArray();
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private byte[] GetInt16Bytes(int n)
        {
            Int16 n16 = Convert.ToInt16(n);
            byte[] bs = BitConverter.GetBytes(n16);
            Array.Reverse(bs);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            // bytes length: 8 * 2 * 2 = 32 byte
            //
            KeyValuePair<int, int>[] values = new KeyValuePair<int, int>[POINT_COUNT];
            for (int i = 0; i < POINT_COUNT; i++)
            {
                // x - outside temperature
                //
                int x = GetInt16Value(bytes, i * 4);

                // y - gt2, bt2 or diffT2
                //
                int y = GetInt16Value(bytes, i * 4 + 2);
                values[i] = new KeyValuePair<int, int>(x, y);
            }
            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private Int16 GetInt16Value(byte[] bytes, int position)
        {
            byte[] bs = new byte[] { bytes[position], bytes[position + 1] };
            Array.Reverse(bs);
            return BitConverter.ToInt16(bs, 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class XD100ModbusTimeControlLineConverter: BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        private const int POINT_COUNT = 12;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            //return base.ConvertToBytes(obj);
            MemoryStream ms = new MemoryStream();
            int[] values = obj as int[];
            for (int i = 0; i < POINT_COUNT; i++)
            {
                int n = values[i];
                // 写入时候放大10倍
                //
                n = n * 10;
                byte[] bs = Int16ModbusConverter.Int16ToBytes((Int16)n);
                ms.Write(bs, 0, bs.Length);
            }
            return ms.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            int[] values = new int[POINT_COUNT];
            for (int i = 0; i < POINT_COUNT; i++)
            {
                byte[] temp = new byte[] { bytes[i * 2], bytes[i * 2 + 1] };
                Int16 n = Int16ModbusConverter.BytesToInt16(temp);
                // 读时缩小10倍
                //
                n = Convert.ToInt16(n / 10);
                values[i] = n;
            }
            return values;
        }
    }
}
