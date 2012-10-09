using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xdgk.Communi.BytesConverter;
using NUnit.Framework;
using Xdgk.Communi.Interface;

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
            PumpStateCollection pss = obj as PumpStateCollection;

            foreach (PumpState ps in pss)
            {
                printPumpState(ps);
            }

            byte[] bs = c.ConvertToBytes(pss);
            Assert.AreEqual(2, bs.Length);
            Assert.AreEqual(0, bs[0]);
            Assert.AreEqual(02, bs[1]);
        }

        private void printPumpState(PumpState ps)
        {
            String s = string.Format("{0} {1} {2}",
                ps.PumpNO, ps.PumpTypeEnum, ps.PumpStateEnum) ;

            Console.WriteLine(s);
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
