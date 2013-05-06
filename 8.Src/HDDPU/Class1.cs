using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using C3.Communi;

namespace HDDPU
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
        public void InsertHDData(int deviceID, HDDeviceData data)
        {
            string s = string.Format(
                "insert into tblHDData(deviceID, DT, Value) values({0}, '{1}', {2})", 
                deviceID,
                data.DT.ToString(),
                data.Value ? 1 : 0);
            ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteHDDeviceDataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'HDDevice'";
            return ExecuteDataTable(s);
        }
    }

    internal class HDDevice : DeviceBase
    {

        public HDDevice()
        {
            this.DeviceType = DeviceTypeManager.AddDeviceType("HDDevice", typeof(HDDevice));
        }
    }


    internal class HDDeviceFactory : DeviceFactoryBase
    {
        public HDDeviceFactory(IDPU dpu)
            : base(dpu)
        {

        }
        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            HDDeviceSource source = (HDDeviceSource)deviceSource;

            HDDevice d = new HDDevice();
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

    internal class HDDeviceProcessor : TaskProcessorBase
    {
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                if (StringHelper.Equal(task.Opera.Name, "Read"))
                {
                    byte byteValue = (byte)task.LastParseResult.Results["value"];
                    bool value = byteValue != 0;
                    HDDeviceData data = new HDDeviceData();
                    data.Value = value;
                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32 ( task.Device.Guid );
                    DBI.Instance.InsertHDData(id, data);
                }
            }
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            // do nothing
            //
        }
    }

    internal class HDDpu : DPUBase
    {
        public HDDpu()
        {
            this.Name = "HDDpu";
            this.DeviceFactory = new HDDeviceFactory(this);
            this.DevicePersister = new HDDevicePersister ();
            this.DeviceSourceProvider = new HDDeviceSourceProvider();
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "HDDevice",
                typeof(HDDevice));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new HDDeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(HDDevice).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class HDDeviceData : DataBase
    {
        [DataItem("市电状态", 10, Unit.None)]
        public bool Value
        {
            get { return _value; }
            set { _value = value; }
        } private bool _value;
    }


    internal class HDDeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal HDDeviceSource(DataRow row)
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

    internal class HDDeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteHDDeviceDataTable ();
            foreach (DataRow row in tbl.Rows)
            {
                HDDeviceSource item = new HDDeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

    internal class HDDevicePersister : DevicePersisterBase
    {
        protected override void OnAdd(IDevice device)
        {
            HDDevice d = (HDDevice)device;

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
