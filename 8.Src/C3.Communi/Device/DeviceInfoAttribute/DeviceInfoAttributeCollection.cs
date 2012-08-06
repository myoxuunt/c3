
using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;


namespace C3.Communi
{
    public class DeviceInfoAttributeCollection : Xdgk.Common.Collection<DeviceInfoAttribute>
    {
        public void Sort()
        { 
            DeviceInfoAttribute[] array = new DeviceInfoAttribute[this.Count];
            this.CopyTo(array, 0);

            Array.Sort(array, new Comparer());

            this.Clear();

            foreach (DeviceInfoAttribute item in array)
            {
                this.Add(item);
            }
        }

#region Comparer
        /// <summary>
        /// 
        /// </summary>
        private class Comparer : IComparer<DeviceInfoAttribute >
        {


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
        }
#endregion //Comparer
    }

}
