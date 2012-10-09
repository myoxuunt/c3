using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xdgk.Communi.BytesConverter;
using NUnit.Framework;
using C3.Communi;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture ]
    public class GRDevicePumpStateConverterTest
    {
        [Test]
        public void BytesToPumpState()
        {
            GRDevicePumpStateConverter c= new GRDevicePumpStateConverter();
            object obj = c.ConvertToObject(new byte[] { 00, 02 });
            bool[] pumpStatusArray = (bool[])obj;
            foreach ( bool b in pumpStatusArray )
            {
                printPumpState(b);
            }

            byte[] bs = c.ConvertToBytes(pumpStatusArray);
            Assert.AreEqual(2, bs.Length);
            Assert.AreEqual(0, bs[0]);
            Assert.AreEqual(02, bs[1]);
        }

        private void printPumpState(bool value)
        {
            Console.WriteLine(value);
        }
    }

    [TestFixture]
    public class GRAlarmConverterTest
    {
        [Test]
        public void test1()
        {
            GRAlarmDataConverter c = new GRAlarmDataConverter();
            object obj = c.ConvertToObject(new byte []{ 01, 0x13});
            Console.WriteLine(obj.ToString());

            ArrayList lis = obj as ArrayList;
            foreach (object item in lis)
            {
                Console.WriteLine(item);
            }
        }
    }
}
