using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;


namespace C3.Communi
{
    abstract public class DeviceUIBase : IDeviceUI
    {

        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get { return _device; }
            protected set { _device = value; }
        } private IDevice _device;



        public DialogResult Add(DeviceType deviceType, IStation station, out IDevice newDevice)
        {
            if (deviceType == null)
            {
                throw new ArgumentNullException("deviceType");
            }

            if (station == null)
            {
                throw new ArgumentNullException("station");
            }

            return OnAdd(deviceType, station, out newDevice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="newDevice"></param>
        /// <returns></returns>
        abstract protected DialogResult OnAdd(DeviceType deviceType, IStation station, out IDevice newDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public DialogResult Edit(IDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }
            return OnEdit(device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        abstract protected DialogResult OnEdit(IDevice device);

    }

}
