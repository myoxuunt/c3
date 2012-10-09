using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Xdgk.Communi;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture ]
    public class BCDConverterTest
    {
        BCDConverter bc = new BCDConverter();

        [Test]
        public void t1()
        {
            bc.ByteCount = 2;
            impl(99);
            impl(20);
            impl(40);
            bc.ByteCount = 4;
            impl(12345678);
            impl(87654321);
        }

        private void impl(int n )
        {
            Console.WriteLine("value: " + n);

            byte[] bs = bc.ConvertToBytes(n);

            Console.WriteLine("bytes: " + BitConverter.ToString(bs));
            int r = (int)bc.ConvertToObject(bs);

            Console.WriteLine("rrrrr: " + r);
            Assert.AreEqual(n, r);

            Console.WriteLine("------------------------------------");
        }

        [Test]
        public void bcdconvert_bcd4byteconvert_byte_count_property()
        {
            BCDConverter c = new BCDConverter();
            BCD4ByteConverter c4 = new BCD4ByteConverter();

            BCDConverter pt = c;
            Assert.AreEqual(1, pt.ByteCount);
            pt = c4;
            //pt.ByteCount = 2;
            Assert.AreEqual(4, pt.ByteCount);
            Assert.AreEqual(4, c4.ByteCount);
        }
    }

    [TestFixture]
    public class Int64ConverterTest
    {
        [Test]
        public void t()
        {
            DateTime dt = DateTime.Now;
            long ticks = dt.Ticks;

            Int64Converter c = new Int64Converter();
            byte[] bs = c.ConvertToBytes(ticks);

            long t2 = (long)c.ConvertToObject(bs);

            Assert.AreEqual(ticks, t2);

            DateTime dt2 = new DateTime(t2);

            Assert.AreEqual(dt, dt2);
        }
    }
}
