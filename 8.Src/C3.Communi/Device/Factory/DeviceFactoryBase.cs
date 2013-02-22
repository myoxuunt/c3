using System;
using System.Collections.Generic;
using System.IO;
using Xdgk.Common;

namespace C3.Communi
{
    using Path = System.IO.Path;

    
    /// <summary>
    /// 
    /// </summary>
    abstract public class DeviceFactoryBase : IDeviceFactory
    {
        /// <summary>
        /// 
        /// </summary>
        static readonly private string FILTER_RELATIVE_PATH = "Filter\\Filter.xml";


        /// <summary>
        /// 
        /// </summary>
        static private Dictionary<Type, FilterCollection> _deviceTypeFiltersDict = 
            new Dictionary<Type, FilterCollection>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
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


            device.Filters = GetFiltersFromCache(device);
            //TaskCollection tasks = TaskFactory.Create();

            //device.Tasks.Enqueue(tasks);
            //TaskFactory.Create(device);
            return device;
        }

        private FilterCollection GetFiltersFromCache(IDevice device)
        {
            Type type = device.GetType ();
            if (_deviceTypeFiltersDict.ContainsKey(type))
            {
                FilterCollection r = _deviceTypeFiltersDict[device.GetType()];
                return r;
            }
            else
            {
                string path = GetFilterConfigFile(device);
                FilterCollection filters = new FilterCollection();
                if (File.Exists(path))
                {
                    filters = FilterFactory.CreateFromConfigFile(path);
                }
                _deviceTypeFiltersDict[type] = filters;
                return filters;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetFilterConfigFile(IDevice device)
        {
            string path = device.GetType().Assembly.Location;
            string directoryName = Path.GetDirectoryName(path);
            return Path.Combine(directoryName, FILTER_RELATIVE_PATH);
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
        //abstract public ITaskFactory TaskFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        abstract public IDevice OnCreate(IDeviceSource deviceSource);
    }

    /// <summary>
    /// 
    /// </summary>
    abstract public class DeviceFactoryBaseXmlTask : DeviceFactoryBase
    {
        public DeviceFactoryBaseXmlTask(IDPU dpu, string configPath)
            : base(dpu)
        {
            this._configPath = configPath;
        }
        string _configPath;
        //public override ITaskFactory TaskFactory
        //{
        //    get
        //    {
        //        if (_taskFactory == null)
        //        {
        //            _taskFactory = new XmlTaskFactory(_configPath);
        //        }
        //        return _taskFactory;
        //    }
        //    set
        //    {
        //    }
        //} private ITaskFactory _taskFactory;
    }

}
