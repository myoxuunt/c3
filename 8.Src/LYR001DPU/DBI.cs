using System;
using System.Collections.Specialized;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace LYR001DPU
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
        /// <returns></returns>
        public DataTable ExecuteLYR001DeviceDataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'xd1100device'";
            return ExecuteDataTable(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        public void SetOutsideTemperatureProviderDevice(int deviceID)
        {
            string s = "delete from tblOTDevice";
            ExecuteScalar(s);

            s = string.Format(
                    "insert into tblOTDevice(DeviceID) values({0})",
                    deviceID);

            ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetOutsideTemperatureProviderDevice()
        {
            string s = "select DeviceID from tblOTDevice";
            object obj = ExecuteScalar(s);

            int r = -1;
            if (obj != null && obj != DBNull.Value)
            {
                r = Convert.ToInt32(obj);
            }
            return r;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        internal void InsertGRData(int id, GRData data)
        {
            string s = " INSERT INTO tblGRData(DT, GT1, BT1, GT2, BT2, OT, GTBase2, GP1, BP1, WL, GP2, BP2, I1, I2, IR, S1, S2, SR, OD, PA2, IH1, SH1, CM1, CM2, CM3, RM1, RM2, DeviceID)" +
                " VALUES(@dt, @gt1, @BT1, @GT2, @BT2, @OT, @GTBase2, @GP1, @BP1, @WL, @GP2, @BP2, @I1, @I2, @IR, @S1, @S2, @SR, @OD, @PA2, @IH1, @SH1, @CM1, @CM2, @CM3, @RM1, @RM2, @DeviceID)";

            ListDictionary listDict = new ListDictionary();

            listDict.Add("DT", data.DT);
            listDict.Add("GT1", data.GT1);
            listDict.Add("BT1", data.BT1);
            listDict.Add("GT2", data.GT2);
            listDict.Add("BT2", data.BT2);
            listDict.Add("OT", data.OT);
            listDict.Add("GTBase2", data.GTBase2);
            listDict.Add("GP1", data.GP1);
            listDict.Add("BP1", data.BP1);
            listDict.Add("WL", data.WL);
            listDict.Add("GP2", data.GP2);
            listDict.Add("BP2", data.BP2);
            listDict.Add("I1", data.I1);
            listDict.Add("I2", data.I2);
            listDict.Add("IR", data.IR);
            listDict.Add("S1", data.S1);
            listDict.Add("S2", data.S2);
            listDict.Add("SR", data.SR);
            listDict.Add("OD", data.OD);
            listDict.Add("PA2", data.PA2);
            listDict.Add("IH1", data.IH1);
            listDict.Add("SH1", data.SH1);

            listDict.Add("CM1", data.CM1.PumpStatusEnum);
            listDict.Add("CM2", data.CM2.PumpStatusEnum);
            listDict.Add("CM3", data.CM3.PumpStatusEnum);
            listDict.Add("RM1", data.RM1.PumpStatusEnum);
            listDict.Add("RM2", data.RM2.PumpStatusEnum);
            listDict.Add("DeviceID", id);

            ExecuteScalar(s,listDict);

            InsertGRAlarmData(id, data.DT, data.Warn.WarnList);
        }

        internal void InsertGRAlarmData(int deviceID, DateTime dt, IList warnList )
        {
            if (warnList != null)
            {
                foreach (object obj in warnList)
                {
                    InsertGRAlarmData(deviceID, dt, obj.ToString());
                }
            }
        }

        internal void InsertGRAlarmData(int deviceID, DateTime dt, string warnMessage)
        {
            string s = string.Format(
                "insert into tblGRAlarmData(deviceid, dt, content) values({0}, '{1}', '{2}')", 
                deviceID, dt, warnMessage);
            this.ExecuteScalar(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal void AddSqlParameter(SqlParameterCollection parameters, string name, object value)
        {
            parameters.Add(new SqlParameter(name, value));
        }

        /// <summary>
        /// 
        /// </summary>
        internal void ClearOutsideTemperatureProviderDevice()
        {
            string s = "delete from tblOTDevice";
            ExecuteScalar(s);
        }
    }

}
