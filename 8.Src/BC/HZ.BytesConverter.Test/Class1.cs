using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HZ.BytesConverter;

namespace HZ.BytesConverter.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture ]
    public class Class1
    {
        [Test]
        public void t1()
        {
            impl("12345", 12345);
            impl("-123" ,-123);
            impl("123.4", 123.4);
            impl("1234567890123", 1234567890123);
        }

        void impl(string input, double  result)
        {
            AsciiValueConverter c = new AsciiValueConverter();
            string s = //"12345";
                input;
            byte[] bs = new byte[s.Length];
            for(int i=0;i<s.Length;i++)
            {
                bs[i] = (byte)s[i];
            }
            object r = c.ConvertToObject(bs);
            Assert.AreEqual(result, r);
        }
    }
}
