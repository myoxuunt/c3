
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceCollection : Collection<IDevice>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public bool ExistAddress(UInt64 address, IDevice ignore)
        {
            bool r = false;
            foreach (IDevice device in this)
            {
                if (device != ignore)
                {
                    if (device.Address == address)
                    {
                        r = true;
                        break;
                    }
                }
            }
            return r;
        }
    }

}
