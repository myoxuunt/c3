using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture ]
    public class FloatModbusConvertTest
    {
        [Test]
        public void t1()
        {
            float f = 100.123F;
            FloatModbusConverter c = new FloatModbusConverter();
            byte[] bs = c.ConvertToBytes(f);
            float f2 = (float)c.ConvertToObject(bs);
            Assert.AreEqual(f, f2, 0.01);

            float f3 = (float)c.ConvertToObject(new byte[] {0x4b, 0x88, 0x42, 0x10 });
            Console.WriteLine(f3);

            byte[] bs2 = BitConverter.GetBytes(36.0738F);
            string s = BitConverter.ToString(bs2);
            Console.WriteLine(s);
        }
    }

    [TestFixture ]
    public class FloatAndInt32Test
    {
        [Test]
        public void t1()
        {
            // 一次累计流量 1027
            //
            byte[] bs = new byte[]{0x03, 0x04, 0x00, 0x00};
            Int32Converter c = new Int32Converter();
            int i = (int)c.ConvertToObject(bs);

            FloatConverter f = new FloatConverter();
            float fv = (float)f.ConvertToObject(bs);
            fv = (float)Math.Round(fv, 2);

            //Assert.AreEqual(i, fv);


            f.IsLittleEndian = false;
            bs = f.ConvertToBytes(10.01F);
            float f2 = (float)f.ConvertToObject(bs);
            Assert.AreEqual(10.01F, f2);
            
        }
    }
}
