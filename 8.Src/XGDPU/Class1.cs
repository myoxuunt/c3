using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;

namespace XGDPU
{
    internal class DBI : DBIBase
    {
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
        } static private DBI _instance;

        internal DBI(string s)
            : base(s)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteXGDeviceDataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'xgdevice'";
            return ExecuteDataTable(s);
        }

    }

    public class XGDeviceFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public XGDeviceFactory(IDPU dpu)
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
            XGDeviceSource source = (XGDeviceSource)deviceSource;
            XGDevice d = new XGDevice();
            d.Address = source.Address;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid ;
            d.StationGuid = source.StationGuid;
            return d;
        }
    }

    public class XGDevicePersister : DevicePersisterBase
    {

        public override void OnAdd(IDevice device)
        {
            XGDevice d = (XGDevice)device;

            string s = string.Format (
                "insert into tblDevice(address, deviceType, stationID) values({0}, '{1}', {2}); select @@identity;",
                d.Address,
                d.DeviceType.Name,
                GuidHelper.ConvertToInt32(d.Station.Guid)
                );

            object obj  =DBI.Instance .ExecuteScalar (s);
            d.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        public override void OnUpdate(IDevice device)
        {
            string s = string.Format(
                "update tblDevice set address = {0} where DeviceID = {1}",
                device.Address, GuidHelper.ConvertToInt32(device.Guid));

            DBI.Instance.ExecuteScalar(s);
        }

        public override void OnDelete(IDevice device)
        {
            string s  = string.Format (
                "delete from tblDevice where DeviceID = {0}",
                GuidHelper.ConvertToInt32(device.Guid ));
            DBI.Instance.ExecuteScalar(s);

        }
    }

    internal class XGDeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal XGDeviceSource(DataRow row)
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
        #endregion //DataRow
    }

    internal class XGDeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<XGDeviceSource> list = new List<XGDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteXGDeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                XGDeviceSource item = new XGDeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

    internal class XGDevice : DeviceBase
    {
    }

    internal class XGDeviceProcessor : TaskProcessorBase
    {
        public override void OnProcess(ITask task, IParseResult pr)
        {
            throw new NotImplementedException();
        }
    }

    public class XGDpu : DPUBase
    {
        public XGDpu()
        {
            this.Name = "XDDpu";
            this.DeviceFactory = new XGDeviceFactory(this);
            this.DevicePersister = new XGDevicePersister();
            this.DeviceSourceProvider = new XGDeviceSourceProvider();
            this.DeviceType = DeviceTypeManager.AddDeviceType("XGDevice",
                "Xun Geng(Text)",
                typeof(XGDevice));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new XGDeviceProcessor();
            this.TaskFactory = new XmlTaskFactory(
                PathUtils.GetAssemblyDirectory(typeof(XGDevice).Assembly));
        }
    }
}
