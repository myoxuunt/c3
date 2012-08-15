using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;

namespace C3.Communi
{
    public class OrderNumberCollection<T> : Xdgk.Common.Collection<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public void Sort()
        {
            if (this.Count > 1)
            {
                T[] array = new T[this.Count];
                this.CopyTo(array, 0);

                Array.Sort(array, new Comparer());
                this.Clear();

                foreach (T item in array)
                {
                    this.Add(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Comparer : System.Collections.IComparer 
        {
            #region IComparer ≥…‘±

            public int Compare(object x, object y)
            {
                if (x == null)
                {
                    throw new ArgumentNullException("x");
                }

                if (y == null)
                {
                    throw new ArgumentNullException("y");
                }

                IOrderNumber x1 = x as IOrderNumber;
                IOrderNumber y1 = y as IOrderNumber;

                if (x1 == null)
                {
                    throw new ArgumentException("x is not IOrderNumber");
                }

                if (y1 == null)
                {
                    throw new ArgumentException("y is not IOrderNumber");
                }

                return x1.OrderNumber - y1.OrderNumber;
            }

            #endregion
        }
    }
}
