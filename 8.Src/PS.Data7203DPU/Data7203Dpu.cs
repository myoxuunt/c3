using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using C3.Communi.SimpleDPU;

namespace PS.DATA7203DPU
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
        public void InsertData7203Data(int deviceID, Data7203Data data)
        {
            InsertFlowmeterData(deviceID, data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private object InsertFlowmeterData(int deviceID, FlowmeterData data)
        {
            string s = string.Format(
                    "insert into tblFluxData(deviceid, DT, instantFlux, sum) values({0}, '{1}', {2}, {3})",
                    deviceID, data.DT, data.InstantFlux, data.Sum);

            return ExecuteScalar(s);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Data7203Dpu : DPUBase
    {
        public Data7203Dpu()
        {
            this.Name = "Data7203Dpu";
            this.DeviceFactory = new Data7203Factory(this);
            this.DevicePersister = new Data7203Persister(DBI.Instance);
            this.DeviceSourceProvider = //new Data7203SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Data7203));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Data7203",
                typeof(Data7203));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Data7203Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Data7203).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Data7203Data : FlowmeterData  
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class Data7203Processor : TaskProcessorBase
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
                    Data7203Data data = new Data7203Data();
                    data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    data.Sum = Convert.ToDouble(pr.Results["sum"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32 (task.Device.Guid );
                    DBI.Instance.InsertData7203Data(id, data);
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
    public class Data7203Persister : SimpleDevicePersister 
    {
        public Data7203Persister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    public class Data7203Factory : DeviceFactoryBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public Data7203Factory(IDPU dpu)
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
            Data7203 d = new Data7203();
            //d.DeviceSource = deviceSource;
            //SetDeviceProperties(d, deviceSource);
            base.SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DeviceKind("FluxDevice")]
    internal class Data7203 : DeviceBase 
    {
    }
}
