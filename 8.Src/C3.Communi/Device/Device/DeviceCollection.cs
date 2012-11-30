
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public enum HasDataOption
    {
        All,
        One,
    }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        internal IDevice Find(int deviceID)
        {
            IDevice r = null;
            foreach (IDevice d in this)
            {
                int id = GuidHelper.ConvertToInt32(d.Guid);
                if (id == deviceID)
                {
                    r = d;
                    break;
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceKind"></param>
        /// <returns></returns>
        public DeviceCollection GetDevices(string deviceKind)
        {
            DeviceCollection r = new DeviceCollection();
            foreach (IDevice device in this)
            {
                Type t = device.GetType();
                object[] atts = t.GetCustomAttributes(typeof(DeviceKindAttribute), true);

                foreach (object att in atts)
                {
                    DeviceKindAttribute kind = (DeviceKindAttribute)att;
                    if (StringHelper.Equal(kind.Name, deviceKind))
                    {
                        r.Add(device);
                    }
                }
            }
            return r;
        }

        public bool HasData(HasDataOption option)
        {
            if (this.Count == 0)
            {
                return false;
            }

            bool r = false;

            if (option == HasDataOption.One)
            {
                r = HasDataOne();
            }
            else if (option == HasDataOption.All)
            {
                r = HasDataAll();
            }
            else
            {
                throw new ArgumentException(option.ToString());
            }
            return r;
        }

        private bool HasDataOne()
        {
            bool r = false;
            foreach (IDevice device in this)
            {
                if (device.HasData())
                {
                    r = true;
                    break;
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool HasDataAll()
        {
            bool r = true;
            foreach (IDevice device in this)
            {
                if (!device.HasData())
                {
                    r = false;
                    break;
                }
            }
            return r;
        }
    }

}
