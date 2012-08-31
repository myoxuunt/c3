using System;
using System.Collections.Generic;
using Xdgk.Common;

namespace C3.Data
{
    public class DeviceInfoAttributeCollection : Collection<DeviceInfoAttribute>
    {
        public void Sort()
        {
            var array = new DeviceInfoAttribute[Count];
            CopyTo(array, 0);

            Array.Sort(array, new Comparer());

            Clear();

            foreach (DeviceInfoAttribute item in array)
            {
                Add(item);
            }
        }

        #region Comparer

        /// <summary>
        /// 
        /// </summary>
        private class Comparer : IComparer<DeviceInfoAttribute>
        {
            #region IComparer<DeviceInfoAttribute> Members

            public int Compare(DeviceInfoAttribute x, DeviceInfoAttribute y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException("x == null or y == null");
                }
                int r = x.OrderNumber - y.OrderNumber;
                if (r == 0)
                {
                    r = string.Compare(x.Name, y.Name);
                }
                return r;
            }

            #endregion
        }

        #endregion //Comparer
    }
}