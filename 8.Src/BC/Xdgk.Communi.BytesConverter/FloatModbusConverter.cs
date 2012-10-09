using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class FloatModbusConverter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            float v = Convert.ToSingle(obj);
            byte[] bs= BitConverter.GetBytes(v);
            byte[] result = new byte[] {  bs[1], bs[0], bs[3], bs[2]};
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            byte[] bstemp = new byte[4] ;
            for (int i = 0; i < 4; i += 2)
            {
                byte b0 = bytes[i];
                byte b1 = bytes[i + 1];
                bstemp[i] = b1;
                bstemp[i + 1] = b0;
            }
            float v = BitConverter.ToSingle(bstemp, 0);
            return v;
        }
    }
}
