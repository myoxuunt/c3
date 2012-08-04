using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NUnit.Framework;


namespace C3.Communi.Test
{
    [TestFixture]
    public class TestSortCollection
    {
        public class OrderClass : IOrderNumber
        {

            internal OrderClass(int p)
            {
                _n = p;
            }
            public int OrderNumber
            {
                get
                {
                    return _n;
                }
                set
                {
                    _n = value;
                }
            } private int _n = 0;

        }

        public class SortClassCollection : OrderNumberCollection<OrderClass>
        {

        }
        [Test]
            public void Test()
            {
                SortClassCollection s = new SortClassCollection();

                s.Add(new OrderClass(4));
                s.Add(new OrderClass(1));
                s.Add(new OrderClass(-5));
                s.Add(new OrderClass(0));
                s.Add(new OrderClass(5));

                s.Sort();
                for (int i = 0; i < s.Count - 1; i++)
                {
                    Console.WriteLine(s[i].OrderNumber);
                    Assert.IsTrue(s[i].OrderNumber <= s[i+1].OrderNumber );
                }
            }
    }

}
