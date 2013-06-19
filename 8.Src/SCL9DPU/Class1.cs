﻿using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;
using C3.Communi.SimpleDPU;

namespace SCL9DPU
{

    internal class DBI : DBIForHZ 
    {
        /// <summary>
        /// 
        /// </summary>
        static internal DBI Instance
        {
            get
            {
                if (_instance == null)
                {

                    SourceConfig sc = SourceConfigManager.SourceConfigs.Find("Connection");
                    if (sc != null)
                    {
                        _instance = new DBI(sc.Value);
                    }
                    else
                    {
                        throw new ConfigException("connection");
                    }
                }
                return _instance;
            }
        }

        static private DBI _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        private DBI(string s)
            : base(s)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="data"></param>
        public void InsertScl9Data(int deviceID, Scl9Data data)
        {
            base.InsertFlowmeterData(deviceID, data);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl9Dpu : DPUBase
    {
        public Scl9Dpu()
        {
            this.Name = "Scl9Dpu";
            this.DeviceFactory = new Scl9Factory(this);
            this.DevicePersister = new Scl9Persister(DBI.Instance);
            this.DeviceSourceProvider = new SimpleDeviceSourceProvider(DBI.Instance, typeof(Scl9));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Scl9",
                typeof(Scl9));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl9Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Scl9).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class Scl9Factory : PlaceDeviceFactoryBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public Scl9Factory(IDPU dpu)
            : base(dpu)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            Scl9 d = new Scl9();
            SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    [DeviceKind ("FluxDevice")]
    public class Scl9 : PlaceDeviceBase 
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl9Data : FlowmeterData
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl9Persister : SimpleDevicePersister
    {
        public Scl9Persister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl9Processor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parseResult"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, "read"))
                {
                    Scl9Data data = new Scl9Data();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertScl9Data(id, data);
                }
            }
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            // nothing
            //
        }
    }
}
