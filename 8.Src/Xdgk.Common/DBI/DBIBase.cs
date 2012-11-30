using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DBIBase
    {
        private string _connString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        public DBIBase(string connString)
        {
            this._connString = connString;
        }

        #region ExecuteDataTable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql);
            return ExecuteDataTable(cmd);
        }
        #endregion //ExecuteDataTable

        #region ExecuteDataTable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            using (SqlConnection cn = new SqlConnection(this._connString))
            {
                cn.Open();
                cmd.Connection = cn;
                DataSet ds = new DataSet();
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                sqlda.Fill(ds);
                return ds.Tables[0];
            }
        }
        #endregion //ExecuteDataTable

        #region ExecuteScalar
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            // 执行查询, 并返回查询所返回的结果集中第一行的第一列, 如果结果集为空, 则为空引用.
            //
            SqlCommand cmd = new SqlCommand(sql);
            return ExecuteScalar(cmd);
        }
        #endregion //ExecuteScalar

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, KeyValueCollection parameters)
        {
            return ExecuteScalar(new SqlCommand(sql), parameters);
        }

        #region ExecuteScalar
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public object ExecuteScalar(SqlCommand cmd)
        {
            return ExecuteScalar(cmd, null);
        }
        #endregion //ExecuteScalar

        public object ExecuteScalar(SqlCommand cmd, KeyValueCollection parameters)
        {
            using (SqlConnection cn = new SqlConnection(this._connString))
            {
                cn.Open();
                cmd.Connection = cn;

                if ( parameters != null )
                {
                    foreach (KeyValue kv in parameters)
                    {
                        SqlParameter sp = new SqlParameter(kv.Key, kv.Value);
                        cmd.Parameters.Add(sp);
                    }
                }

                return cmd.ExecuteScalar();
            }
        }

        #region GetDBInfo
        /// <summary>
        /// 获取数据库信息，如不存在返回null
        /// </summary>
        /// <returns></returns>
        public DBInfo GetDBInfo()
        {
            DBInfo dbinfo = null;
            string sql = "select top 1 * from tblDBInfo order by DBInfoID desc";
            DataTable tbl = this.ExecuteDataTable(sql);
            if (tbl.Rows.Count > 0)
            {
                DataRow row = tbl.Rows[0];
                dbinfo = new DBInfo();
                dbinfo.MajorVersion = Convert.ToInt32(row["MajorVersion"]);
                dbinfo.MinorVersion = Convert.ToInt32(row["MinorVersion"]);
                dbinfo.RevisionVersion = Convert.ToInt32(row["RevisionVersion"]);
                dbinfo.Project = row["Project"].ToString().Trim();
                dbinfo.DT = Convert.ToDateTime(row["DT"]);
            }
            return dbinfo;
        }
        #endregion //GetDBInfo


        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <param name="majorVersion"></param>
        /// <param name="minorVersion"></param>
        /// <param name="revisionVersion"></param>
        public void VerifyDBInfo(string project, int majorVersion,
            int minorVersion, int revisionVersion)
        {
            DBInfo dbinfo = this.GetDBInfo();

            if (dbinfo == null)
                throw new DBInfoException("not find DBInfo");

            if (StringHelper.Equal(project, dbinfo.Project) &&
                majorVersion == dbinfo.MajorVersion &&
                minorVersion == dbinfo.MinorVersion &&
                revisionVersion == dbinfo.RevisionVersion )
            {
                return;
            }
            else
            {
                string expectedVersion = string.Format("{0}.{1}.{2}", majorVersion, minorVersion, revisionVersion);
                string s = string.Format("Expected project '{0} V{1}', but was '{2} V{3}'",
                    project, expectedVersion, dbinfo.Project, dbinfo.VersionString);
                throw new DBInfoException(s);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetIdentity()
        {
            string sql = "select @@IDENTITY";
            object obj = ExecuteScalar(sql);
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        static public void AddSqlParameter(SqlCommand cmd, string parameterName, object value)
        {
            SqlParameter p = new SqlParameter(parameterName, value);
            cmd.Parameters.Add(p);
        }
    }

    /// <summary>
    /// 
    /// </summary>
}
