﻿using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;
using C3.Communi.SimpleDPU;

namespace SCL61DDPU
{

    internal class DBI : DBIBase
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

                    SourceConfig sc = SoftManager.GetSoft().SourceConfigs.Find("Connection");
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
        public void InsertScl6Data(int deviceID, Scl61dData data)
        {
            string s = string.Format(
                "insert into tblFluxData(deviceid, DT, instantFlux, sum) values({0}, '{1}', {2}, {3}",
                deviceID, data.DT, data.InstantFlux, data.Sum);

            ExecuteScalar(s);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl61dDpu : DPUBase
    {
        public Scl61dDpu()
        {
            this.Name = "Scl61dDpu";
            this.DeviceFactory = new Scl61dFactory(this);
            this.DevicePersister = new Scl61dPersister(DBI.Instance);
            this.DeviceSourceProvider = //new Scl6SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Scl61d));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Scl61d",
                "Scl61d(Text)",
                typeof(Scl61d));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl6Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Scl61d).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class Scl61dFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public Scl61dFactory(IDPU dpu)
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
            SimpleDeviceSource source = (SimpleDeviceSource)deviceSource;
            Scl61d d = new Scl61d();
            d.Address = source.Address;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            return d;
        }
    }

    public class Scl61d : DeviceBase, IFluxProvider
    {
        #region IFluxProvider 成员

        public double InstantFlux
        {
            get
            {
                double r = 0d;
                Scl61dData data = this.DeviceDataManager.Last as Scl61dData;
                if (data != null)
                {
                    return data.InstantFlux;
                }
                return r;
            }
        }

        public double Sum
        {

            get
            {
                double r = 0d;
                Scl61dData data = this.DeviceDataManager.Last as Scl61dData;
                if (data != null)
                {
                    return data.Sum;
                }
                return r;
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl61dData : FlowmeterData
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl61dPersister : SimpleDevicePersister
    {
        public Scl61dPersister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl6Processor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, "read"))
                {
                    Scl61dData data = new Scl61dData();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertScl6Data(id, data);
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
