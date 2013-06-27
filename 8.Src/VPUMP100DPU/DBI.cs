
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using C3.Communi.SimpleDPU;
using VPump100Common;


namespace VPUMP100DPU
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
        } static private DBI _instance;

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
        /// <param name="d"></param>
        public void InsertVPump100Data(int deviceID, VPump100Data data)
        {
            string s = " insert into tblPumpData(deviceid, DT, instantFlux, efficiency, TotalAmount, RemainAmount, pumpStatus, forceStatus, vibrateStatus, powerStatus) " +
                " values(@deviceID, @dt, @instantFlux, @efficiency, @totalAmount, @remainAmount, @pumpStatus, @forceStatus, @vibrateStatus, @powerStatus)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("Dt", data.DT);
            list.Add("InstantFlux", data.InstantFlux);
            list.Add("Efficiency", data.Efficiency);
            list.Add("TotalAmount", data.TotalAmount);
            list.Add("RemainAmount", data.RemainAmount);
            list.Add("pumpStatus", data.PumpStatus);
            list.Add("forceStatus", data.ForceStartStatus);
            list.Add("vibrateStatus", data.VibrateStatus);
            list.Add("powerStatus", data.PowerStatus);

            ExecuteScalar(s, list);

            if (base.IsAccessDataBase())
            {
                int gateDataIDLast = GetPumpDataIDMax(deviceID);
                this.DeletePumpDataLast(deviceID);
                this.InsertPumpDataLast(deviceID, gateDataIDLast);
            }
        }

        public DateTime GetVPumpLastDateTime(int deviceID)
        {
            string s = "select Max(DT) from tblPumpData where DeviceID = @deviceID";
            ListDictionary list = new ListDictionary();
            list.Add("deviceID", deviceID);

            object obj = ExecuteScalar(s, list);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDateTime(obj);
            }
            else
            {
                return DateTime.Now.Date;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        public void DeletePumpDataLast(int deviceID)
        {
            string s = "Delete from tblPumpDataLast where DeviceID = @deviceID";

            ListDictionary list = new ListDictionary ();
            list.Add ("DeviceID", deviceID );
            ExecuteScalar(s, list);
        }

        public void InsertPumpDataLast(int deviceID, int gateDataID)
        {
            string s = "insert into tblPumpDataLast(DeviceID, PumpDataID) values (@deviceID, @gateDataLastID)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("PumpDataID", gateDataID);

            ExecuteScalar(s, list);
        }

        public int GetPumpDataIDMax(int deviceID)
        {
            string s = "select max (PumpDataID) from tblPumpData where DeviceID = @deviceID";
            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);

            object obj = ExecuteScalar(s, list);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                throw new InvalidOperationException(string.Format("not find max id in tblPumpData with device id '{0}'", deviceID));
            }
        }
    }

}
