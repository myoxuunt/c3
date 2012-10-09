using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Communi.Interface;

namespace Xdgk.Communi.XD2300Converter
{
    /// <summary>
    /// 
    /// </summary>
    public class DTConverter : BytesConverter 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            DateTime dt = (DateTime)obj;

            byte[] bs = GetBytes(new int[]
                    {
                        dt.Year,
                        dt.Month,
                        dt.Day,
                        dt.Hour ,
                        dt.Minute ,
                        dt.Second 
                    }
                    );
            Console.WriteLine(bs.Length);
            return bs;
            //return base.ConvertToBytes(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (bytes.Length != 12)
            {
                throw new ArgumentException("bytes.Length != 12");
            }

            int[] values = new int[6];
            for (int i = 0; i < 6; i++)
            {
                values[i] = GetInt16(bytes, i * 2);
            }

            DateTime dt = new DateTime(
                values[0],
                values[1],
                values[2],
                values[3],
                values[4],
                values[5]);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetInt16(byte[] bytes, int index)
        {
            // 转为低位在前,高位在后
            //
            byte[] temp = new byte[] { bytes[index + 1], bytes[index] };
            return BitConverter.ToInt16(temp, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private byte[] GetBytes(int value)
        {
            Int16 value16 = (Int16)value;
            byte[] bs = BitConverter.GetBytes(value16);
            Array.Reverse(bs);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private byte[] GetBytes(int[] values)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            foreach( int value in values )
            {
                byte[] temp = GetBytes(value);
                ms.Write(temp, 0, temp.Length);
            }

            return ms.ToArray();
        }
    }
}
