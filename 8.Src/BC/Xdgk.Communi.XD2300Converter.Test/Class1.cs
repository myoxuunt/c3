using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Communi.Interface;
using NUnit.Framework;

namespace Xdgk.Communi.XD2300Converter.Test
{

    [TestFixture]
    public class Class1
    {
        [Test]
        public void t1()
        {
            DateTime dt = DateTime.Now;
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            DTConverter c = new DTConverter();

            byte[] bs = c.ConvertToBytes(dt);
            string bsStr = BitConverter.ToString(bs);
            Console.WriteLine(bsStr);
            DateTime dt2 = (DateTime)c.ConvertToObject(bs);

            Assert.AreEqual(dt, dt2);
        }

        [Test]
        public void t2()
        {
            byte[] bs = new byte[]{0x00, 0x0B, 0x00, 0x04, 0x00, 0x12, 0x00, 0x0F, 0x00, 0x32, 0x00, 0x0F};

            object obj = new DTConverter().ConvertToObject(bs);
            Console.WriteLine(obj);
        }
    }
}
