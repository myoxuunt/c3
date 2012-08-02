
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DeviceFactoryBase : IDeviceFactory
    {
        public DeviceFactoryBase(IDPU dpu)
        {
            this.Dpu = dpu;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        public IDevice Create(IDeviceSource deviceSource)
        {
            IDevice device = OnCreate(deviceSource);

            //TaskCollection tasks = TaskFactory.Create();

            //device.Tasks.Enqueue(tasks);
            TaskFactory.Create(device);
            return device;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDPU Dpu
        {
            get 
            { 
                return _dpu; 
            }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Dpu");
                }
                _dpu = value; 
            }
        } private IDPU _dpu;

        /// <summary>
        /// 
        /// </summary>
        abstract public ITaskFactory TaskFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        abstract public IDevice OnCreate(IDeviceSource deviceSource);
    }

    abstract public class DeviceFactoryBaseXmlTask : DeviceFactoryBase
    {
        public DeviceFactoryBaseXmlTask(IDPU dpu, string configPath)
            : base(dpu)
        {
            this._configPath = configPath;
        }
        string _configPath;
        public override ITaskFactory TaskFactory
        {
            get
            {
                if (_taskFactory == null)
                {
                    _taskFactory = new XmlTaskFactory(_configPath);
                }
                return _taskFactory;
            }
            set
            {
            }
        } private ITaskFactory _taskFactory;
    }

}
