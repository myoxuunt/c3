using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using System.Diagnostics;

namespace Xdgk.Communi
{
    /// <summary>
    /// XD-100 pump state converter
    /// </summary>
    public class GRDevicePumpStateConverter : BytesConverter 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="bitIndex"></param>
        /// <returns></returns>
        static private bool GetPumpStateEnum( byte state, int bitIndex )
        {
            byte mask = (byte)Math.Pow( 2, bitIndex );
            int r = mask & state;
            return r > 0;
        }

        #region IBytesConverter 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        override public object ConvertToObject(byte[] bytes)
        {
            //byte b = bytes[0];
            byte b = bytes[1];

            bool[] r = new bool[8];

            for (int i = 0; i < 5; i++)
            {
                r[i] = GetPumpStateEnum(b, i);
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <returns></returns>
        private byte GetValue(int bitIndex)
        {
            return (byte)System.Math.Pow(2, bitIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        override public byte[] ConvertToBytes(object obj)
        {
            bool[] pumpStatusArray = (bool[])obj;
            byte b = 0;

            for (int i = 0; i < 5; i++)
            {
                if (pumpStatusArray[i])
                {
                    b |= GetValue(i);
                }
            }
            return new byte[] { 0, b };
        }
        #endregion
    }
}
