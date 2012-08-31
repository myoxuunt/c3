using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Xdgk.Common;

namespace C3.Data
{
    public class AttributePropertyInfoPairCollection
        //: Collection<AttributePropertyInfoPair>
        : OrderNumberCollection<AttributePropertyInfoPair>
    {
        /*
        #region Sort
        /// <summary>
        /// 
        /// </summary>
        public void Sort()
        {
            AttributePropertyInfoPair[] array = new AttributePropertyInfoPair[this.Count];
            this.CopyTo(array, 0);

            Array.Sort(array, new Comparer());

            this.Clear();

            foreach (AttributePropertyInfoPair item in array)
            {
                this.Add(item);
            }
        }
        #endregion //Sort

        #region Comparer
        /// <summary>
        /// 
        /// </summary>
        private class Comparer : IComparer
        {
            #region IComparer ≥…‘±
            public int Compare(object x, object y)
            {
                AttributePropertyInfoPair
                    a = x as AttributePropertyInfoPair,
                      b = y as AttributePropertyInfoPair;

                if (a == null || b == null)
                {
                    throw new ArgumentNullException("x == null or y == null, or type error");
                }

                int r = a.Attribute.OrderNumber - b.Attribute.OrderNumber;
                if (r == 0)
                {
                    r = string.Compare(a.Attribute.Name, b.Attribute.Name);
                }

                return r;
            }

            #endregion
        }
        #endregion //Comparer
*/
    }

}
