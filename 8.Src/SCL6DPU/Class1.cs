using System;
using C3.Communi;
using C3.Communi.SimpleDPU;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace SCL6DPU
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
        public void InsertScl6Data(int deviceID, Scl6Data data)
        {
            string s = string.Format(
                "insert into tblFluxData(deviceid, DT, instantFlux, sum) values({0}, '{1}', {2}, {3})",
                deviceID, data.DT, data.InstantFlux, data.Sum);

            ExecuteScalar(s);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl6Dpu : DPUBase
    {
        public Scl6Dpu()
        {
            this.Name = "Scl6Dpu";
            this.DeviceFactory = new Scl6Factory(this);
            this.DevicePersister = new Scl6Persister(DBI.Instance);
            this.DeviceSourceProvider = //new Scl6SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Scl6));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Scl6",
                typeof(Scl6));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl6Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Scl6).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Scl6Data : FlowmeterData  
    {
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
                string opera = task.Opera.Name ;
                if (StringHelper.Equal(opera, "read"))
                {
                    Scl6Data data = new Scl6Data();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32 (task.Device.Guid );
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


    /// <summary>
    /// 
    /// </summary>
    public class Scl6Persister : SimpleDevicePersister 
    {
        public Scl6Persister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    public class Scl6Factory : PlaceDeviceFactoryBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public Scl6Factory(IDPU dpu)
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
            Scl6 d = new Scl6();
            SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DeviceKind("FluxDevice")]
    internal class Scl6 : PlaceDeviceBase 
    {
    }
}
