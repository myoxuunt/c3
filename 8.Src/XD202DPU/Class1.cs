using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Xdgk.Common;
using System.Data.SqlClient;
using C3.Communi;

namespace XD202DPU
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
        public void InsertXd202Data(int deviceID, Xd202Data data)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO tblMeasureSluiceData(
[DeviceID], [DT], [BeforeWL], [BehindWL], [InstantFlux], [Height], [RemainedAmount], [UsedAmount])
VALUES(
@DeviceID, @DT, @BeforeWL, @BehindWL, @InstantFlux, @Height, @RemainedAmount, @UsedAmount)";

            DBIBase.AddSqlParameter(cmd, "deviceID", deviceID);
            DBIBase.AddSqlParameter(cmd, "dt", data.DT);
            DBIBase.AddSqlParameter(cmd, "BeforeWL", data.BeforeWL);
            DBIBase.AddSqlParameter(cmd, "BehindWL", data.BehindWL);
            DBIBase.AddSqlParameter(cmd, "InstantFlux", data.InstantFlux);
            DBIBase.AddSqlParameter(cmd, "Height", data.Height);
            DBIBase.AddSqlParameter(cmd, "RemainedAmount", data.RemainedAmount);
            DBIBase.AddSqlParameter(cmd, "UsedAmount", data.UsedAmount);
            ExecuteScalar(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteHDDeviceDataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'Xd202'";
            return ExecuteDataTable(s);
        }
    }

    internal class Xd202 : DeviceBase
    {

        public Xd202()
        {
            this.DeviceType = DeviceTypeManager.AddDeviceType("Xd202", typeof(Xd202));
        }
    }


    internal class Xd202DeviceFactory : DeviceFactoryBase
    {
        public Xd202DeviceFactory(IDPU dpu)
            : base(dpu)
        {

        }
        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            Xd202DeviceSource source = (Xd202DeviceSource)deviceSource;

            Xd202 d = new Xd202();
            d.Address = source.Address;
            d.Name = source.DeviceName;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;

            return d;
        }
    }

    internal class Xd202DeviceProcessor : TaskProcessorBase
    {
        static private double CM2M(double value)
        {
            return value / 100;
        }
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                if (StringHelper.Equal(task.Opera.Name, "ReadReal"))
                {
                    Xd202Data data = new Xd202Data();

                    data.BeforeWL = Convert.ToDouble(task.LastParseResult.Results["BeforeWL"]);
                    data.BehindWL = Convert.ToDouble(task.LastParseResult.Results["BehindWL"]);
                    data.Height = Convert.ToDouble(task.LastParseResult.Results["Height"]);
                    data.InstantFlux = Convert.ToDouble(task.LastParseResult.Results["IF"]);
                    data.RemainedAmount = Convert.ToDouble(task.LastParseResult.Results["RemainedAmount"]);
                    data.UsedAmount = 0 - data.RemainedAmount;

                    data.BeforeWL = CM2M(data.BeforeWL);
                    data.BehindWL = CM2M(data.BehindWL);
                    data.Height = CM2M(data.Height);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertXd202Data(id, data);
                }
            }
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            // do nothing
            //
        }
    }

    internal class Xd202Dpu : DPUBase
    {
        public Xd202Dpu()
        {
            this.Name = "Xd202Dpu";
            this.DeviceFactory = new Xd202DeviceFactory(this);
            this.DevicePersister = new Xd202DevicePersister();
            this.DeviceSourceProvider = new Xd202DeviceSourceProvider();
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Xd202",
                typeof(Xd202));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Xd202DeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Xd202).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Xd202Data : DataBase
    {
        private const string FloatFormat = "f2";
        #region BeforeWL
        /// <summary>
        /// 
        /// </summary>
        [DataItem("闸前水位", 10, Unit.M, FloatFormat)]
        public double BeforeWL
        {
            get
            {
                return _beforeWL;
            }
            set
            {
                _beforeWL = value;
            }
        } private double _beforeWL;
        #endregion //BeforeWL

        #region BehindWL
        /// <summary>
        /// 
        /// </summary>
        [DataItem("闸后水位", 20, Unit.M, FloatFormat)]
        public double BehindWL
        {
            get
            {
                return _behindWL;
            }
            set
            {
                _behindWL = value;
            }
        } private double _behindWL;
        #endregion //BehindWL

        #region Height
        /// <summary>
        /// 
        /// </summary>
        [DataItem("闸高", 30, Unit.M, FloatFormat)]
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        } private double _height;
        #endregion //Height

        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时流量", 40, Unit.M3PerSecond)]
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

        #region UsedAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem("已用水量", 50, Unit.M3)]
        public double UsedAmount
        {
            get
            {
                return _usedAmount;
            }
            set
            {
                _usedAmount = value;
            }
        } private double _usedAmount;
        #endregion //UsedAmount

        #region RemainedAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem("剩余水量", 60, Unit.M3)]
        public double RemainedAmount
        {
            get
            {
                return _remainedAmount;
            }
            set
            {
                _remainedAmount = value;
            }
        } private double _remainedAmount;
        #endregion //RemainedAmount

    }


    internal class Xd202DeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal Xd202DeviceSource(DataRow row)
        {
            this.DataRow = row;
        }

        #region DataRow
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
                this.Address = Convert.ToUInt64(_dataRow["DeviceAddress"]);
                this.DevcieTypeName = _dataRow["DeviceType"].ToString().Trim();
                this.Guid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["DeviceID"])
                    );
                this.StationGuid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["StationID"])
                    );

                this.DeviceName = _dataRow["DeviceName"].ToString().Trim();
            }
        } private DataRow _dataRow;
        #endregion //DataRow

    }

    internal class Xd202DeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteHDDeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                Xd202DeviceSource item = new Xd202DeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

    internal class Xd202DevicePersister : DevicePersisterBase
    {
        protected override void OnAdd(IDevice device)
        {
            Xd202 d = (Xd202)device;

            string s = string.Format(
                    "insert into tblDevice(DeviceAddress, deviceType, stationID, DeviceName) values({0}, '{1}', {2}, '{3}'); select @@identity;",
                    d.Address,
                    d.DeviceType.Type.Name,
                    GuidHelper.ConvertToInt32(d.Station.Guid),
                    d.Name
                    );

            object obj = DBI.Instance.ExecuteScalar(s);
            d.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        protected override void OnDelete(IDevice device)
        {
            string s = string.Format(
                    "delete from tblDevice where DeviceID = {0}",
                    GuidHelper.ConvertToInt32(device.Guid));
            DBI.Instance.ExecuteScalar(s);
        }

        protected override void OnUpdate(IDevice device)
        {
            string s = string.Format(
                    "update tblDevice set DeviceAddress = {0}, DeviceName = '{1}' where DeviceID = {2}",
                    device.Address,
                    device.Name,
                    GuidHelper.ConvertToInt32(device.Guid)
                    );

            DBI.Instance.ExecuteScalar(s);
        }
    }
}