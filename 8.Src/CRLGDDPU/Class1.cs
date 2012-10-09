using System;
using C3.Communi;
using C3.Communi.SimpleDPU;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace CRLGDDPU
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
        public void InsertCrlgdData(int deviceID, CrlgdData data)
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
    public class CrlgdDpu : DPUBase
    {
        public CrlgdDpu()
        {
            this.Name = "CrlgdDpu";
            this.DeviceFactory = new CrlgdFactory(this);
            this.DevicePersister = new CrlgdPersister(DBI.Instance);
            this.DeviceSourceProvider = //new Scl6SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Crlgd));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Crlgd",
                "Crlgd(Text)",
                typeof(Crlgd));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl6Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Crlgd).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class CrlgdFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public CrlgdFactory(IDPU dpu)
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
            Crlgd d = new Crlgd();
            d.Address = source.Address;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            return d;
        }
    }

    public class Crlgd : DeviceBase, IFluxProvider
    {
        #region IFluxProvider 成员

        public double InstantFlux
        {
            get
            {
                double r = 0d;
                CrlgdData data = this.DeviceDataManager.Last as CrlgdData;
                if (data != null)
                {
                    r = data.InstantFlux;
                }
                return r;
            }
        }

        public double Sum
        {
            get
            {
                double r = 0d;
                CrlgdData data = this.DeviceDataManager.Last as CrlgdData;
                if (data != null)
                {
                    r = data.InstantFlux;
                }
                return r;
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgdData : FlowmeterData
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgdPersister : SimpleDevicePersister
    {
        public CrlgdPersister(DBIBase dbi)
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
                    CrlgdData data = new CrlgdData();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertCrlgdData(id, data);
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
