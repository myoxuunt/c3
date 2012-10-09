using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
    class Program
    {
        [Test]
        public void TestBCDConverter()
        {

            BCDConverter c = new BCDConverter();
            byte[] bs = new byte[] { 0x10 };
            int n = 10;

            object obj = c.ConvertToObject(bs);
            Assert.AreEqual(n, (int)obj);

            byte[] bs2 = c.ConvertToBytes(n);
            Assert.AreEqual(1, bs2.Length);
            Assert.AreEqual(0x10, bs2[0]);


            bs2 = c.ConvertToBytes(99 + 0);
            Console.WriteLine(bs2[0]);
            
        }


        [Test]
        public void TestGRAlarmDataConverter()
        {
            GRAlarmDataConverter c = new GRAlarmDataConverter();
            object obj = c.ConvertToObject(new byte[] { 0xff, 0xff });
            IList list = obj as IList;
            Assert.AreEqual(8 + 8, list.Count);
            foreach (object o in list)
                Console.WriteLine(o);
            obj = c.ConvertToObject(new byte[] { 0x0, 0x0 });
            list = obj as IList;
            Assert.AreEqual(0, list.Count);
        }
    }
}
