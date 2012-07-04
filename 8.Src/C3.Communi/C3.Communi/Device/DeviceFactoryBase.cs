
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
            IDevice device = OnCreate(deviceSource);

            TaskCollection tasks = TaskFactory.Create();

            device.Tasks.Enqueue(tasks);
            return device;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual ITaskFactory TaskFactory
        {
            get { return _taskFactory; }
            set { _taskFactory = value; }
        } private ITaskFactory _taskFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        abstract public IDevice OnCreate(IDeviceSource deviceSource);
    }

}
