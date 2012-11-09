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
}
