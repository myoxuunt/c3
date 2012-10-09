using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
    public class RateConverterTest
    {
        [Test]
        public void Test()
        {
            RateConverter r = new RateConverter();
            Int16ModbusConverter inc = new Int16ModbusConverter();
            r.InnerBytesConverter = inc;
            r.Rate = 100;
            byte[] bs = r.ConvertToBytes(45);
            Console.WriteLine("{0:x2} {1:x2}", bs[0],bs[1]);

            object obj = r.ConvertToObject(bs);
            Assert.AreEqual(45, obj);

            object obj2 = inc.ConvertToObject(bs);
            Assert.AreEqual(4500, obj2);
        }
    }
}
