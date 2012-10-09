using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Xdgk.Communi.BytesConverter;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture ]
    public class ModbusConverterTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestUInt32ModbusConvert()
        {
            UInt32ModbusConverter c = new UInt32ModbusConverter();
            UInt32 value = 9999999;
            byte[] bs = c.ConvertToBytes(value);

            object obj = c.ConvertToObject(bs);


            Assert.AreEqual(value, obj);
        }



        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestInt32ModbusConvert()
        {
            Int32ModbusConverter c = new Int32ModbusConverter();
            Int32[] values = new int[]{
                9999999,
                0,
                -123456,
                0x00010203,
                (int)0x7fddeecc
            };

            foreach (int value in values)
            {
                byte[] bs = c.ConvertToBytes(value);
                object obj = c.ConvertToObject(bs);

                string s = string.Format("{0}->{1}", value, BitConverter.ToString (bs));
                Console.WriteLine(s);
                Assert.AreEqual(value, obj);
            }
        }
        [Test]
        public void TestInt32AndUInt32ModbusConverter()
        {
            Int32ModbusConverter int32c = new Int32ModbusConverter();
            UInt32ModbusConverter uint32c = new UInt32ModbusConverter();

            byte[] bs = new byte[] { 0xfe, 0xcc, 0xff, 0xff};

            object obj = int32c.ConvertToObject(bs);
            Console.WriteLine("int32modbus convert: " + obj);

            obj = uint32c.ConvertToObject(bs);
            Console.WriteLine("uint32modbus convert:" + obj);




        }
    }
}
