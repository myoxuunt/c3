using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
    public class XGDateTimeConverterTest
    {
        [Test]
        public void t1()
        {
            XGDateTimeConverter c = new XGDateTimeConverter();
            DateTime dt = DateTime.Now;
            byte[] bs = c.ConvertToBytes(dt);
            Console.WriteLine(BitConverter.ToString(bs));
            DateTime dt2 = (DateTime)c.ConvertToObject(bs);

            Assert.AreEqual(dt.Date, dt2.Date);
            Assert.AreEqual((int)dt.TimeOfDay.TotalSeconds,
                (int)dt2.TimeOfDay.TotalSeconds);

        }
    }
}
