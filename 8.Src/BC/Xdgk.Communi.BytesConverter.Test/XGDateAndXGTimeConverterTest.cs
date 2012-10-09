using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Xdgk.Communi.BytesConverter;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture ]
    public class XGDateAndXGTimeConverterTest
    {
        [Test]
        public void xgdate()
        {
            DateTime dt = DateTime.Now;
            XGDateConverter c = new XGDateConverter();
            byte[] bs = c.ConvertToBytes(dt);
            Console.WriteLine(BitConverter.ToString(bs));
            DateTime dt2 = (DateTime)c.ConvertToObject(bs);
            Assert.AreEqual(dt2.Date, dt.Date);
        }


        [Test]
        public void xgtime()
        {
            DateTime dt = DateTime.Now;
            TimeSpan ts = dt.TimeOfDay;
            XGTimeConverter  c = new XGTimeConverter();
            byte[] bs = c.ConvertToBytes(ts);
            TimeSpan ts2 = (TimeSpan)c.ConvertToObject(bs);
            int i1 = (int)ts.TotalSeconds;
            int i2 = (int)ts2.TotalSeconds; 
            Assert.AreEqual(i1,i2);
        }
    }
}
