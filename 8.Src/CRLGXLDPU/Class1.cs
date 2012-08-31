using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Data;
using SimpleDPU;

namespace CRLGXLDPU
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
        public void InsertScl6Data(int deviceID, CrlgxlData data)
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
    public class CrlgxlDpu : DPUBase
    {
        public CrlgxlDpu()
        {
            this.Name = "CrlgxlDpu";
            this.DeviceFactory = new CrlgxlFactory(this);
            this.DevicePersister = new CrlgxlPersister(DBI.Instance);
            this.DeviceSourceProvider = //new Scl6SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Crlgxl));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Crlgxl",
                "Crlgxl(Text)",
                typeof(Crlgxl));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl6Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Crlgxl).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class CrlgxlFactory : DeviceFactoryBase
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
            SimpleDeviceSource source = (SimpleDeviceSource)deviceSource;
            Crlgxl d = new Crlgxl();
            d.Address = source.Address;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            return d;
        }
    }

    public class Crlgxl : DeviceBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrlgxlData : FlowmeterData
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
                    CrlgxlData data = new CrlgxlData();
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
}
