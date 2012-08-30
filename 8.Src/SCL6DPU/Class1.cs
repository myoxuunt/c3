using System;
using Xdgk.Common;
using System.Data;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

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
        /// <returns></returns>
        public DataTable ExecuteScl6DataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'scl6'";
            return ExecuteDataTable(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="data"></param>
        public void InsertScl6Data(int deviceID, Scl6Data data)
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
    public class Scl6Dpu : DPUBase
    {
        public Scl6Dpu()
        {
            this.Name = "Scl6Dpu";
            this.DeviceFactory = new Scl6Factory(this);
            this.DevicePersister = new Scl6Persister();
            this.DeviceSourceProvider = new Scl6SourceProvider();
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Scl6",
                "Scl6(Text)",
                typeof(Scl6));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Scl6Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Scl6).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    public class Scl6Data : IDeviceData
    {
        #region Sum
        /// <summary>
        /// 
        /// </summary>
        public double Sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
            }
        } private double _sum;
        #endregion //Sum

        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        public double InstantFlux
        {
            get
            {
                return _instantFlux;
            }
            set
            {
                _instantFlux = value;
            }
        } private double _instantFlux;
        #endregion //InstantFlux


        #region IDeviceData 成员

        public DateTime DT
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        } DateTime _dt = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReportItemCollection GetReportItems()
        {
            ReportItemCollection ris = new ReportItemCollection();
            ris.Add(new ReportItem("时间", this.DT, Unit.FindByName(Unit.None)));
            ris.Add(new ReportItem("瞬时", this.InstantFlux, Unit.FindByName(Unit.M3PerSecond)));
            ris.Add(new ReportItem("累计", this.Sum, Unit.FindByName(Unit.M3)));
            return ris;
        }

        #endregion
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
                }
            }
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            // nothing
            //
        }
    }

    public class Scl6Persister : DevicePersisterBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnAdd(IDevice device)
        {
            string s = string.Format(
                "insert into tblDevice(address, deviceType, stationID) values({0}, '{1}', {2}); select @@identity;",
                device.Address,
                device.DeviceType.Name,
                GuidHelper.ConvertToInt32(device.Station.Guid)
                );

            object obj = DBI.Instance.ExecuteScalar(s);
            device.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        protected override void OnUpdate(IDevice device)
        {
            string s = string.Format(
                "update tblDevice set address = {0} where DeviceID = {1}",
                device.Address,
                GuidHelper.ConvertToInt32(device.Guid));

            DBI.Instance.ExecuteScalar(s);
        }

        protected override void OnDelete(IDevice device)
        {
            int id = GuidHelper.ConvertToInt32 ( device.Guid );
            string s = string.Format("delet from tblDevice where deviceid = {0}", id);
            DBI.Instance.ExecuteScalar(s);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Scl6SourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteScl6DataTable();
            foreach (DataRow row in tbl.Rows)
            {
                Scl6Source item = new Scl6Source(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

    public class Scl6Factory : DeviceFactoryBase
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
            Scl6Source source = (Scl6Source )deviceSource;
            Scl6 d = new Scl6();
            d.Address = source.Address;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Scl6 : DeviceBase
    {

    }

    internal class Scl6Source : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public Scl6Source(DataRow row)
        {
            this.DataRow = row;
        }
        /// <summary>
        /// 
        /// </summary>
        public DataRow DataRow
        {
            get
            {
                return _dataRow;
            }
            set
            {
                _dataRow = value;
                this.Address = Convert.ToUInt64(_dataRow["address"]);

                this.DevcieTypeName = _dataRow["DeviceType"].ToString().Trim();

                this.Guid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["DeviceID"])
                    );

                this.StationGuid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["StationID"])
                    );
            }
        } private DataRow _dataRow;

    }


}
