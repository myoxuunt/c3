using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

//using Xdgk.Communi;

namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
    public class HexConverterTest
    {
        [Test]
        public void t()
        {
            string s = "0     \t      \t 0 a0 10  0   0\t0";
            h(s);
            s = " " + s;
            h(s);
            s = " \t" + s + " \t";
            h(s);
        }

        void h(string s )
        {
            byte[] bs = Xdgk.Common.HexStringConverter.Default.ConvertToBytes(s);
            Assert.AreEqual(7, bs.Length);
            Console.WriteLine(bs.Length);
        }
    }
}
