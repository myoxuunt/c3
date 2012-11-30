using System;
using C3.Communi;
using C3.Communi.SimpleDPU;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace CRLGDDPU
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
        public void InsertCrlgdData(int deviceID, CrlgdData data)
        {
            base.InsertHeatData(deviceID, data);
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
                typeof(Crlgd));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl6Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Crlgd).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class CrlgdFactory : PlaceDeviceFactoryBase 
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
            Crlgd d = new Crlgd();
            SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    [DeviceKind("HeatDevice")]
    public class Crlgd : PlaceDeviceBase 
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgdData : HeatData 
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
                    data.InstantHeat = Convert.ToDouble(pr.Results["ih"]);
                    data.SumHeat = Convert.ToDouble(pr.Results["sh"]);
                    data.GT = Convert.ToDouble(pr.Results["gt"]);
                    data.BT = Convert.ToDouble(pr.Results["bt"]);

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
