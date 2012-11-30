
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Xdgk.Common
{
    public class GuidHelper
    {
        private GuidHelper()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Guid Create(Int32 n)
        {
            byte[] bs = BitConverter.GetBytes(n);
            return Create(bs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Guid Create(UInt32 n)
        {
            byte[] bs = BitConverter.GetBytes(n);

            return Create(bs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        private static Guid Create(byte[] bs)
        {
            byte[] bs16 = new byte[16];

            Array.Reverse(bs);
            for (int i = bs.Length - 1; i >= 0; i--)
            {
                bs16[16 - bs.Length + i] = bs[i];
            }

            Guid id = new Guid(bs16);
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static UInt32 ConvertToUInt32(Guid guid)
        {
            byte[] bs16 = guid.ToByteArray();
            int startIndex = 16 - 4;
            Array.Reverse(bs16, startIndex, 4);

            UInt32 r = BitConverter.ToUInt32(bs16, startIndex);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(Guid guid)
        {
            byte[] bs16 = guid.ToByteArray();
            int startIndex = 16 - 4;
            Array.Reverse(bs16, startIndex, 4);

            Int32  r = BitConverter.ToInt32(bs16, startIndex);
            return r;
        }
    }

}
