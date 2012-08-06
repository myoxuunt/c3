using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceTypeCollection : Xdgk.Common.Collection<DeviceType>
    {
        #region Add
        new internal void Add(DeviceType value)
        {
            base.Add(value);
        }
        #endregion //Add

        #region Insert
        new internal void Insert(int index, DeviceType value)
        {
            base.Insert(index, value);
        }
        #endregion //Insert
    }

}
