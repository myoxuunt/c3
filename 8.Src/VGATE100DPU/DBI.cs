using System;
using System.Collections.Specialized;
using C3.Communi;
using Xdgk.Common;
using VGate100Common;


namespace VGATE100DPU
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
        public void InsertVGate100Data(int deviceID, VGate100Data data)
        {
            string s = " insert into tblGateData(deviceid, DT, BeforeWL, BehindWL, Height, instantFlux, TotalAmount, RemainAmount) " +
                " values(@deviceID, @dt, @beforeWL, @behindWL, @height, @instantFlux, @totalAmount, @remainAmount)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("Dt", data.DT);
            list.Add("BeforeWL", data.BeforeWL);
            list.Add("BehindWL", data.BehindWL);
            list.Add("Height", data.Height);
            list.Add("InstantFlux", data.InstantFlux);
            list.Add("TotalAmount", data.TotalAmount);
            list.Add("RemainAmount", data.RemainAmount);

            ExecuteScalar(s, list);

            if (base.IsAccessDataBase())
            {
                int gateDataIDLast = GetGateDataIDMax(deviceID);
                this.DeleteGateDataLast(deviceID);
                this.InsertGateDataLast(deviceID, gateDataIDLast);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public DateTime GetVGateLastDateTime(int deviceID)
        {
            string s = "select Max(DT) from tblGateData where DeviceID = @deviceID";
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
        public void DeleteGateDataLast(int deviceID)
        {
            string s = "Delete from tblGateDataLast where DeviceID = @deviceID";

            ListDictionary list = new ListDictionary ();
            list.Add ("DeviceID", deviceID );
            ExecuteScalar(s, list);
        }

        public void InsertGateDataLast(int deviceID, int gateDataID)
        {
            string s = "insert into tblGateDataLast(DeviceID, GateDataID) values (@deviceID, @gateDataLastID)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("GateDataID", gateDataID);

            ExecuteScalar(s, list);
        }

        public int GetGateDataIDMax(int deviceID)
        {
            string s = "select max (GateDataID) from tblGateData where DeviceID = @deviceID";
            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);

            object obj = ExecuteScalar(s, list);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                throw new InvalidOperationException(string.Format("not find max id in tblGateData with device id '{0}'", deviceID));
            }
        }
    }
}