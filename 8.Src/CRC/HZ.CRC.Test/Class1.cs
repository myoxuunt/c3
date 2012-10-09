using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework ;

namespace HZ.CRC.Test
{
    [TestFixture ]
    public class HZCRCTester
    {
        [Test]
        public void t1()
        {
            string s = "0123456789" ;
            impl(s+"+-.", "045");
            impl(s+"", "045");
            s = "0651110211705000000000000000000000000000000000000000000000000000000000000000001602868286804340439162";
            impl(s, "121");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        private void impl(string ex, string result)
        {
            //string s = "0123456789" + ex;
            string s = ex;
            byte[] bs = new byte[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                bs[i] = BitConverter.GetBytes(s[i])[0];
            }

            HZ.CRC.AsciiSum crc = new HZ.CRC.AsciiSum();
            byte[] bscrc = crc.Calc(bs, 0, s.Length);
            Assert.AreEqual((byte)result[0], bscrc[0]);
            Assert.AreEqual((byte)result[1], bscrc[1]);
            Assert.AreEqual((byte)result[2], bscrc[2]);
        }


        

    }
}
