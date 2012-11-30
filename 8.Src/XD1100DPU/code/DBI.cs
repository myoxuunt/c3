
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using C3.Communi;
using Xdgk.Common;

namespace XD1100DPU
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
        public DataTable ExecuteXD1100DeviceDataTable()
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
        internal void InsertXD1100Data(int id, XD1100Data data)
        {
            KeyValueCollection kvs = new KeyValueCollection();
            SqlCommand cmd = new SqlCommand();

            string s = " INSERT INTO tblGRData(DT, GT1, BT1, GT2, BT2, OT, GTBase2, GP1, BP1, WL, GP2, BP2, I1, I2, IR, S1, S2, SR, OD, PA2, IH1, SH1, CM1, CM2, CM3, RM1, RM2, DeviceID)" +
                " VALUES(@dt, @gt1, @BT1, @GT2, @BT2, @OT, @GTBase2, @GP1, @BP1, @WL, @GP2, @BP2, @I1, @I2, @IR, @S1, @S2, @SR, @OD, @PA2, @IH1, @SH1, @CM1, @CM2, @CM3, @RM1, @RM2, @DeviceID)";

            cmd.CommandText = s;
            SqlParameterCollection p = cmd.Parameters ;

            AddSqlParameter(p, "DT", data.DT);
            AddSqlParameter(p, "GT1", data.GT1);
            AddSqlParameter(p, "BT1", data.BT1);
            AddSqlParameter(p, "GT2", data.GT2);
            AddSqlParameter(p, "BT2", data.BT2);
            AddSqlParameter(p, "OT", data.OT);
            AddSqlParameter(p, "GTBase2", data.GTBase2);
            AddSqlParameter(p, "GP1", data.GP1);
            AddSqlParameter(p, "BP1", data.BP1);
            AddSqlParameter(p, "WL", data.WL);
            AddSqlParameter(p, "GP2", data.GP2);
            AddSqlParameter(p, "BP2", data.BP2);
            AddSqlParameter(p, "I1", data.I1);
            AddSqlParameter(p, "I2", data.I2);
            AddSqlParameter(p, "IR", data.IR);
            AddSqlParameter(p, "S1", data.S1);
            AddSqlParameter(p, "S2", data.S2);
            AddSqlParameter(p, "SR", data.SR);
            AddSqlParameter(p, "OD", data.OD);
            AddSqlParameter(p, "PA2", data.PA2);
            AddSqlParameter(p, "IH1", data.IH1);
            AddSqlParameter(p, "SH1", data.SH1);

            AddSqlParameter(p, "CM1", data.CM1.PumpStatusEnum);
            AddSqlParameter(p, "CM2", data.CM2.PumpStatusEnum);
            AddSqlParameter(p, "CM3", data.CM3.PumpStatusEnum);
            AddSqlParameter(p, "RM1", data.RM1.PumpStatusEnum);
            AddSqlParameter(p, "RM2", data.RM2.PumpStatusEnum);
            AddSqlParameter(p, "DeviceID", id);

            ExecuteScalar(cmd);

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
