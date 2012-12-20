using System;
using C3.Communi;
using C3.Communi.SimpleDPU;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace CRLGXLDPU
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
        public void InsertCrlgxlData(int deviceID, CrlgxlData data)
        {
            base.InsertHeatData(deviceID, data);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgxlDpu : DPUBase
    {
        public CrlgxlDpu()
        {
            this.Name = "CrlgxlDpu";
            this.DeviceFactory = new CrlgxlFactory(this);
            this.DevicePersister = new CrlgxlPersister(DBI.Instance);
            this.DeviceSourceProvider = new SimpleDeviceSourceProvider(DBI.Instance, typeof(Crlgxl));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Crlgxl",
                typeof(Crlgxl));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new CrlgxlProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Crlgxl).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class CrlgxlFactory : PlaceDeviceFactoryBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public CrlgxlFactory(IDPU dpu)
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
            Crlgxl d = new Crlgxl();
            SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    [DeviceKind ("HeatDevice")]
    public class Crlgxl : PlaceDeviceBase 
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgxlData : HeatData
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgxlPersister : SimpleDevicePersister
    {
        public CrlgxlPersister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgxlProcessor : TaskProcessorBase
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
                    CrlgxlData data = new CrlgxlData();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);
                    data.InstantHeat = Convert.ToDouble(pr.Results["ih"]);
                    data.SumHeat = Convert.ToDouble(pr.Results["sh"]);
                    data.GT = Convert.ToDouble(pr.Results["gt"]);
                    data.BT = Convert.ToDouble(pr.Results["bt"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertCrlgxlData(id, data);
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
