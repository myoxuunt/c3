using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Communi.Interface;

namespace Xdgk.Communi.CRCer
{
    /// <summary>
    /// 
    /// </summary>
    public class SUM: ICRCer 
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
            int e = begin + length ;
            int n = 0;
            for (int i = begin; i < e; i++)
            {
                n += bytes[i];
            }
            byte b = (byte)(n % 0x100);
            return new byte[] { b };
        }
        #endregion
    }
}
