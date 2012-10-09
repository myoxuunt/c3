using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Communi.Interface;

namespace HZ.CRC
{
    /// <summary>
    /// 
    /// </summary>
    public class AsciiSum : ICRCer 
    {
        #region ICRCer 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="begin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] Calc(byte[] bytes, int begin, int length)
        {
            int b = begin;
            int e = begin + length;
            int sum = 0;
            for (int i = b; i < e; i++)
            {
                byte byt = bytes[i];
                char c = (char)byt;
                if (char.IsDigit(c))
                {
                    int n = int.Parse(c.ToString());
                    sum += n;
                }
            }
            Console.WriteLine(sum);

            sum %= 1000;

            string sumString =  sum.ToString("000");
            byte[] bs = new byte[3];
            bs[0] = (byte)sumString[0];
            bs[1] = (byte)sumString[1];
            bs[2] = (byte)sumString[2];
            return bs;
        }

        #endregion
    }
}
