using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Communi.BytesConverter.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class IsLittleEndianTest
    {
        [Test]
        public void v1()
        {
            byte[] bs = BitConverter.GetBytes(100);
            //Console.WriteLine(s);
            //Console.WriteLine("bs1");
            foreach (byte b in bs)
            {
                Console.WriteLine(b);
            }

            Array.Reverse(bs);
            int n = BitConverter.ToInt32(bs,0);
            Console.WriteLine(n);

            byte[] bs2 = BitConverter.GetBytes(100);
            string s = Encoding.ASCII.GetString(bs);
            Console.WriteLine(s);

            Console.WriteLine("isLittleEndian: " + BitConverter.IsLittleEndian);
        }


        [Test]
        public void v2()
        {
            int n = 0x123456;
            Int32Converter c = new Int32Converter();
            byte[] bs = c.ConvertToBytes(n);
            Console.WriteLine(Xdgk.Common.HexStringConverter.Default.ConvertToObject(bs));
            int m = (Int32)c.ConvertToObject(bs);
            
            Assert.AreEqual(n,m);
            Assert.IsTrue(c.IsLittleEndian);
            Console.WriteLine( Xdgk.Common.HexStringConverter.Default.ConvertToObject(bs) );


            c.IsLittleEndian = false;
            byte[] bs2 = c.ConvertToBytes(n);
            m = (Int32)c.ConvertToObject(bs2);
            Assert.AreEqual(n,m);


            Assert.AreNotEqual(bs, bs2);
            Console.WriteLine( Xdgk.Common.HexStringConverter.Default.ConvertToObject(bs2) );

        }

    }
}
