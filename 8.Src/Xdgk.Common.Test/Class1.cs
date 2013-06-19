using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using NUnit.Framework;

namespace Xdgk.Common.Test
{
    [TestFixture]
    public class DataCollectionTest
    {
        public class Data : DataBase
        {
        }

        [Test]
        public void T1()
        {
            DataCollection datas = new DataCollection();
            int n = datas.Capability + 1;

            string output = string.Empty;
            for (int i = 0; i < n + 100; i++)
            {
                //Console.WriteLine(i);

                output += i + " ";
                datas.Add(new Data());
            }
            Console.WriteLine(output);
        }
    }

    public enum EnumForTest
    {
        [EnumText ("A-Text")]
        A = 0,
        [EnumText ("B-Text")]
        B = 1,
        [EnumText ("C-Text")]
        C = 2,

    }
    [TestFixture]
    public class EnumTextAttributeTest
    {
        [Test]
        public void t1()
        {
            EnumForTest a = EnumForTest.A;
            EnumForTest b = EnumForTest.B;
            EnumForTest c = EnumForTest.C;

            string text = EnumTextAttribute.GetEnumTextAttributeValue(a);
            Console.WriteLine(text);
            Assert.AreEqual("A-Text", text);
        }
    }
}
