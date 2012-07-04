
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DeviceFactoryBase : IDeviceFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        public IDevice Create(IDeviceSource deviceSource)
        {
            return OnCreate(deviceSource);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        abstract public IDevice OnCreate(IDeviceSource deviceSource);
    }

}
