using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Xdgk.Common;

namespace S
{
    internal class DB : DBIBase 
    {
        static private DB GetDB()
        {
            if (_db == null)
            {
                string conn = ConfigurationManager.ConnectionStrings[1].ConnectionString;
                _db = new DB(conn);
            }
            return _db;
        } static private DB _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        private DB(string conn)
            : base(conn)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateName"></param>
        /// <param name="fromDT"></param>
        /// <returns></returns>
        static internal DataTable GetGateDataTable(string gateName, DateTime fromDT)
        {
            string s = string.Format(
                "select * from v_GateDatas where Address='{0}' and StrTime > '{1}' order by strTime",
                gateName , fromDT
                );

            DataTable t = GetDB().ExecuteDataTable(s);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateName"></param>
        /// <returns></returns>
        static internal bool ExistGate(string gateName)
        {
            string s = string.Format(
                "select count(*) from v_gate where Address = '{0}'",
                gateName);

            object obj = GetDB().ExecuteScalar(s);
            if (obj != null && obj != DBNull.Value)
            {
                int count = Convert.ToInt32(obj);
                return count > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
