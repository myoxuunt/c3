using System;
using C3.Communi;
using C3.Communi.SimpleDPU;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace CRLGDPU
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
            string s = string.Format(
                "insert into tblHeatData(deviceid, DT, instantFlux, sum, ih, sh) " + 
                "values({0}, '{1}', {2}, {3}, {4}, {5})",
                deviceID, data.DT, data.InstantFlux,
                data.Sum, data.IH, data.SH);

            ExecuteScalar(s);
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

    public class CrlgFactory : FluxDeviceFactoryBase
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

    public class Crlg : FluxDeviceBase
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgData : FlowmeterData
    {
        #region IH
        /// <summary>
        /// 
        /// </summary>
        /// 
        //[DataItem ("瞬时热量",30, "GJ/h")]
        public double IH
        {
            get
            {
                return _iH;
            }
            set
            {
                _iH = value;
            }
        } private double _iH;
        #endregion //IH

        #region SH
        /// <summary>
        /// 
        /// </summary>
        //[DataItem ("累计热量",40, "GJ")]
        public double SH
        {
            get
            {
                return _sH;
            }
            set
            {
                _sH = value;
            }
        } private double _sH;
        #endregion //SH
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
        /// <param name="pr"></param>
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
                    data.IH = Convert.ToDouble(pr.Results["ih"]);
                    data.SH = Convert.ToDouble(pr.Results["sh"]);

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
