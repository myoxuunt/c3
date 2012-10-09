using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
	public class WordFloatConverterTest
	{
        [Test]
        public void t1()
        {
            Int16FloatConverter c = new Int16FloatConverter(0.1f);

            // 10.1 -> 101
            //
            Byte[] bs = c.ConvertToBytes(10.1);
            Int16 n = BitConverter.ToInt16(bs, 0);
            object obj = c.ConvertToObject(bs);
            float f = (float)obj;

            Assert.AreEqual(101, n);
            Assert.AreEqual(10.1, f, 0.0001);
            
        }
	}
}
