using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
    public class GRDeviceAlarmTest
    {
        [Test]
        public void t1()
        {
            impl(new byte[]{00, 02});
            impl(new byte[]{03, 00});
            impl(new byte[] {0xff, 0xff });
        }

        [Test]
        public void TestPowerAlarm()
        {
            GRAlarmDataConverter c = new GRAlarmDataConverter();
            byte[] bs = new byte[]{0x80, 0x00};
            Int16ModbusConverter int16mc = new Int16ModbusConverter();
            Int16 value  =  (Int16)int16mc.ConvertToObject(bs);
            Console.WriteLine(value.ToString("X"));

            IList alarms = c.ConvertToObject(bs) as IList ;
            Assert.AreEqual (1,alarms.Count );
            Assert.AreEqual("电源故障", alarms[0]);

            TestOneAlarm("电源故障", new byte[] {0x80, 0x00 });
            TestOneAlarm("水位开关高", new byte[] {0x40, 0x00});
            TestOneAlarm("水位开关低", new byte[] { 0x20, 0x00 });
            TestOneAlarm("补水泵2故障", new byte[] { 0x10, 0x00 });
            TestOneAlarm("补水泵1故障", new byte[] { 0x08, 0x00 });
            TestOneAlarm("循环泵3故障", new byte[] { 0x04, 0x00 });
            TestOneAlarm("循环泵2故障", new byte[] { 0x02, 0x00 });
            TestOneAlarm("循环泵1故障", new byte[] { 0x01, 0x00 });

            TestOneAlarm("水箱水位低", new byte[] { 0x00, 0x80 });
            TestOneAlarm("水箱水位高", new byte[] { 0x00, 0x40 });
            TestOneAlarm("二次供温高", new byte[] { 0x00, 0x20 });
            TestOneAlarm("一次供温低", new byte[] { 0x00, 0x10 });
            TestOneAlarm("二次回压低", new byte[] { 0x00, 0x08 });
            TestOneAlarm("二次回压高", new byte[] { 0x00, 0x04 });
            TestOneAlarm("二次供压高", new byte[] { 0x00, 0x02 });
            TestOneAlarm("一次供压低", new byte[] { 0x00, 0x01 });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alarmName"></param>
        /// <param name="bs"></param>
        private void TestOneAlarm(string alarmName, byte[] bs)
        {
            Console.WriteLine(alarmName + " : " + bs[0].ToString("X2") + bs[1].ToString ("X2"));
            Int16ModbusConverter int16c = new Int16ModbusConverter();
            GRAlarmDataConverter c = new GRAlarmDataConverter();

            //Int16 int16 = int16c.ConvertToObject(bs);
            IList list = (IList)c.ConvertToObject(bs);
            Assert.AreEqual(1,list.Count);
            Assert.AreEqual(alarmName, list[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        private void impl(byte[] bs )
        {
            GRAlarmDataConverter c = new GRAlarmDataConverter();
            //byte[] bs = new byte[] { 00, 02 };
            object value =  c.ConvertToObject(bs);
            IList alarms = value as IList ;
            //Assert.AreEqual(1, alarms.Count);
            Console.WriteLine("====");
            //Console.WriteLine(alarms[0]);
            foreach (object item in alarms)
            {
                Console.WriteLine(item);
            }
        }
    }
}
