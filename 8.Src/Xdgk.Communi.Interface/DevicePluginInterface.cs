using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Communi.Interface
{
    /// <summary>
    /// remove to communi.dll?
    /// </summary>
    public interface DevicePluginInterface
    {
        DeviceFactory GetDeviceFactory();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface DeviceFactory
    {
        /// <summary>
        /// 
        /// </summary>
        string ForDeviceType { get; }
        // device create()
    }

    /// <summary>
    /// 
    /// </summary>
    public interface DeviceUI
    {
        /// <summary>
        /// 
        /// </summary>
        string ForDeviceType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        object AddDevice(object station);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        void EditDevice(object device);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        bool DeleteDevice(object device);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface DevcieProcessor
    {

    }
}
