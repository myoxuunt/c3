using System;
using C3.Communi;
using C3.Communi.SimpleDPU;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace CRLGDPU
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
        public void InsertCrlgData(int deviceID, CrlgData data)
        {
            base.InsertHeatData(deviceID, data);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgDpu : DPUBase
    {
        public CrlgDpu()
        {
            this.Name = "CrlgDpu";
            this.DeviceFactory = new CrlgFactory(this);
            this.DevicePersister = new CrlgPersister(DBI.Instance);
            this.DeviceSourceProvider = //new Scl6SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Crlg));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Crlg",
                typeof(Crlg));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new CrlgProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Crlg).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class CrlgFactory : PlaceDeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public CrlgFactory(IDPU dpu)
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
            Crlg d = new Crlg();
            SetDeviceProperties(d, deviceSource);
            return d;
        }
    }


    [DeviceKind("HeatDevice")]
    public class Crlg : PlaceDeviceBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgData : HeatData
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgPersister : SimpleDevicePersister
    {
        public CrlgPersister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgProcessor : TaskProcessorBase
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
                    CrlgData data = new CrlgData();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);
                    data.InstantHeat = Convert.ToDouble(pr.Results["ih"]);
                    data.SumHeat  = Convert.ToDouble(pr.Results["sh"]);
                    data.GT = Convert.ToDouble(pr.Results["gt"]);
                    data.BT = Convert.ToDouble(pr.Results["bt"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertCrlgData(id, data);
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
