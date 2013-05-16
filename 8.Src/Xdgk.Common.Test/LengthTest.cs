
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Xdgk.Common.Test
{
    [TestFixture ]
    public class LengthTest
    {
        static public void doit()
        {
            new LengthTest().hashCode();

            Length l = new Length(10.1234, LengthType.mm);
            double d = 10.1234;
            for (int i = 0; i < 100; i++)
            {
                d = d / 7;
            }

            for (int i = 0; i < 100; i++)
            {
                d = d * 7;
            }
            Length l2 = new Length(d, LengthType.cm);
            Console.WriteLine(l);
            Console.WriteLine(new Length());

            Console.WriteLine(l.ToCM());
            Console.WriteLine(l.ToKM());
            Console.WriteLine(l.ToM());

            Console.WriteLine(new Length(1, LengthType.km).ToCM());
            //Console.WriteLine(l2.ToString("n0"));

            Console.WriteLine(l2.Value);
            Console.WriteLine(l.GetHashCode() + " " + l2.GetHashCode());
            Console.WriteLine(l.Equals(l2));
        }

        [Test]
        public void hashCode()
        {
            Length l = new Length(10.1234, LengthType.mm);
            double d = 10.1234;
            for (int i = 0; i < 100; i++)
            {
                d = d / 7;
            }

            for (int i = 0; i < 100; i++)
            {
                d = d * 7;
            }
            Length l2 = new Length(d, LengthType.mm);

            Console.WriteLine(l.Value.ToString("n15") + " : " + l2.Value.ToString("n15"));

            Console.WriteLine(l.GetHashCode());

            Assert.IsTrue(l.Value - l2.Value > 0);
            Console.WriteLine(l.Value - l2.Value );
            Assert.IsTrue(l.Value != l2.Value);
            Assert.IsTrue(l == l2);
            Assert.IsTrue(l.GetHashCode() == l2.GetHashCode());
        }

        [Test]
        public void test()
        {
            Length same1 = Length.FromM(1),
            same2 = Length.FromCM(100),
            same3 = Length.FromMM(1000),
            big = Length.FromM(4),
            small = Length.FromCM(50)
            ;

            Length diff = same1 - small;
            Assert.AreEqual(Length.FromM(0.5), diff);
            Assert.IsTrue(same1 == same2);
            Assert.IsTrue(same1 == same3);
            Assert.IsFalse(same1 != same2);

            Assert.IsFalse(same1 > same2);
            Assert.IsFalse(same1 < same2);

            Assert.IsTrue (same1 >= same2);
            Assert.IsTrue(same1 <= same2);

            Assert.IsTrue(same1 - small == same2 - small);

            Assert.AreEqual(2000, (same1 + same2).ToMM().Value);
            Assert.AreEqual((same1 - same2).Value, 0);

            Assert.AreEqual((same1 * 3).ToMM().Value, 3000);
            Assert.AreEqual((3 * same1).ToMM().Value, 3000);
            Assert.AreEqual((same1 / 5).ToMM().Value, 200);
            Assert.IsTrue((small - big).IsNegative);
            Console.WriteLine( small-big );
            Console.WriteLine((small - big).Abs());
        }

        [Test]
        public void sort()
        {
            List<Length> lis = new List<Length>();
            Length[] array = new Length[]{Length.FromCM(1), Length.FromMM(1), Length.FromM(1)
            ,Length.FromCM (20), Length.FromM (0.28)};
            lis.AddRange(array);
            lis.Sort();
            foreach (Length item in lis)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void fromString()
        {
            Length l = Length.FromString("1.234cm");
            Console.WriteLine(l);
            Assert.AreEqual(l.ToCM().Value, 1.234); 

            l = Length.FromString("1.234(cm)");
            Console.WriteLine(l);
            Assert.AreEqual(l.ToCM().Value, 1.234);


            l = Length.FromString("1.234(( km))");
            Console.WriteLine(l);

            l = Length.FromString("1.234 km");
            Console.WriteLine(l);

            l = Length.FromString("1.234mm");
            Console.WriteLine(l);
        }
    }

}
