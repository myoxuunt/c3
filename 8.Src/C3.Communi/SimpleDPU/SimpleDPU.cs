﻿using System;
using System.Collections.Specialized;
using System.Data;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;

namespace C3.Communi.SimpleDPU
{
    public class SimpleDevicePersister : DevicePersisterBase
    {
        /// <summary>
        /// 
        /// </summary>
        private Xdgk.Common.DBIBase _dbi;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbi"></param>
        public SimpleDevicePersister(Xdgk.Common.DBIBase dbi)
        {
            if (dbi == null)
            {
                throw new ArgumentNullException("dbi");
            }

            this._dbi = dbi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnAdd(IDevice device)
        {
            string s = @"insert into tblDevice(DeviceAddress, DeviceName, deviceType, deviceExtend, DeviceRemark, stationID) 
                values(@DeviceAddress, @DeviceName, @DeviceType, @DeviceExtend, @DeviceRemark, @StationID)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceAddress", device.Address);
            list.Add("DeviceName", device.Name);
            list.Add("DeviceType", device.DeviceType.Type.Name);
            list.Add("DeviceExtend", device.GetStringParameters());
            list.Add("DeviceRemark", device.Remark);
            list.Add("StationID", GuidHelper.ConvertToInt32(device.Station.Guid));

            _dbi.ExecuteScalar(s, list);

            object obj = GetMaxDeviceID(_dbi);
            device.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnUpdate(IDevice device)
        {
            string s = @"update tblDevice 
                    set DeviceAddress = @DeviceAddress, DeviceName = @DeviceName, DeviceExtend=@DeviceExtend, DeviceRemark = @deviceRemark
                    where DeviceID = @DeviceID";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceAddress", device.Address);
            list.Add("DeviceName", device.Name);
            list.Add("DeviceExtend", device.GetStringParameters());
            list.Add("DeviceRemark", device.Remark);
            list.Add("DeviceID", GuidHelper.ConvertToInt32(device.Guid));

            _dbi.ExecuteScalar(s, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnDelete(IDevice device)
        {
            int id = GuidHelper.ConvertToInt32(device.Guid);
            string s = string.Format("delete from tblDevice where deviceid = {0}", id);
            _dbi.ExecuteScalar(s);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SimpleDeviceSourceProvider : DeviceSourceProviderBase
    {
        private DBIBase _dbi;
        private Type _deviceType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbi"></param>
        /// <param name="deviceType"></param>
        public SimpleDeviceSourceProvider(DBIBase dbi, Type deviceType)
        {
            if (dbi == null)
            {
                throw new ArgumentNullException("dbi");
            }

            if (deviceType == null)
            {
                throw new ArgumentNullException("deviceType");
            }

            this._dbi = dbi;
            this._deviceType = deviceType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = _dbi.ExecuteDataTable (GetSql());
            foreach (DataRow row in tbl.Rows)
            {
                //SimpleDeviceSource s = new SimpleDeviceSource(row);
                //Scl6Source this1 = new Scl6Source(row);
                SimpleDeviceSource item = new SimpleDeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            string s = string.Format(
                "select * from tblDevice where DeviceType = '{0}'", 
                this._deviceType.Name);
            return s;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SimpleDeviceSource : DeviceSourceBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public SimpleDeviceSource(DataRow row)
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
                if (value == null)
                {
                    throw new ArgumentNullException("DataRow");
                }

                _dataRow = value;
                this.Address = Convert.ToUInt64(_dataRow["DeviceAddress"]);

                this.DevcieTypeName = _dataRow["DeviceType"].ToString().Trim();

                this.DeviceName = _dataRow["DeviceName"].ToString().Trim();
                this.DeviceExtendParameters = _dataRow["DeviceExtend"].ToString().Trim();

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
