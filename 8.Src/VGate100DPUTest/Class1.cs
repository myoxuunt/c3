using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VGATE100DPU;
using VGate100Common;

namespace VGate100DPUTest
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestVGate100Data()
        {
            VGate100Data data = new VGate100Data();
            data.BeforeWL = 1.23d;
            data.BehindWL = 4.46d;
            data.Height = 11.22d;
            data.InstantFlux = 100.11d;
            data.RemainAmount = 200.12d;
            data.TotalAmount = 30.234d;

            byte[] bs = data.ToBytes();
            Console.WriteLine(bs.Length);


            VGate100Data d2 = VGate100Data.ToVGate100Data(bs, 0);
            Console.WriteLine(d2.Height );
            Console.WriteLine(d2.InstantFlux);
            Assert.AreEqual(data.BeforeWL, d2.BeforeWL, 0.1);
            Assert.AreEqual(data.BehindWL, d2.BehindWL, 0.1);
            Assert.AreEqual(data.Height, d2.Height, 0.1);
            Assert.AreEqual(data.InstantFlux, d2.InstantFlux, 0.1);
            Assert.AreEqual(data.RemainAmount, d2.RemainAmount, 0.1);
            Assert.AreEqual(data.TotalAmount, d2.TotalAmount, 0.1);
        }
    }
}
