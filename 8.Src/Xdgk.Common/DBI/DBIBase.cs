using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DbNetLink;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DBIBase
    {
        private string _connString;
        private DataProvider _dataProvider;
        private DatabaseType _dataBaseType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        public DBIBase(string connString)
            : this(connString, DataProvider.SqlClient, DatabaseType.SqlServer)
        {
            this._connString = connString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="dataProvider"></param>
        /// <param name="databaseType"></param>
        public DBIBase(string connString, DataProvider dataProvider, DatabaseType databaseType)
        {
            this._connString = connString;
            this._dataProvider = dataProvider;
            this._dataBaseType = databaseType;
        }

        #region ExecuteDataTable

        public DataTable ExecuteDataTable(QueryCommandConfig cmd)
        {
            using (DbNetData db = new DbNetData(_connString,
                 this._dataProvider, this._dataBaseType))
            {
                db.Open();
                DataTable tbl = db.GetDataTable(cmd);
                return tbl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, IDictionary paramsDict)
        {
            using (DbNetData db = new DbNetData(_connString,
                 this._dataProvider, this._dataBaseType))
            {
                db.Open();
                DataTable tbl = db.GetDataTable(sql, paramsDict);
                return tbl;
            }
        }

        public DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataTable(sql, new ListDictionary());
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
            return ExecuteScalar(new CommandConfig(sql));
        }

        public object ExecuteScalar(string sql, ListDictionary paramsDict)
        {
            CommandConfig cmd = new CommandConfig(sql);
            if (paramsDict != null)
            {
                cmd.Params = paramsDict;
            }
            return ExecuteScalar(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, KeyValueCollection parameters)
        {
            CommandConfig cmd = new CommandConfig(sql);
            foreach (KeyValue kv in parameters)
            {
                cmd.Params.Add(kv.Key, kv.Value);
            }
            return ExecuteScalar(cmd);
        }

        public object ExecuteScalar(CommandConfig cmd)
        {
            using (DbNetData db = new DbNetData(_connString, _dataProvider, _dataBaseType))
            {
                return db.ExecuteScalar(cmd);
            }
        }
        #endregion //ExecuteScalar

        #region ExecuteNonQuery
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExecuteNonQuery(string sql)
        {
            // 执行查询, 并返回查询所返回的结果集中第一行的第一列, 如果结果集为空, 则为空引用.
            //
            return ExecuteNonQuery(new CommandConfig(sql));
        }

        public object ExecuteNonQuery(string sql, ListDictionary paramsDict)
        {
            CommandConfig cmd = new CommandConfig(sql);
            if (paramsDict != null)
            {
                cmd.Params = paramsDict;
            }
            return ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteNonQuery(string sql, KeyValueCollection parameters)
        {
            CommandConfig cmd = new CommandConfig(sql);
            foreach (KeyValue kv in parameters)
            {
                cmd.Params.Add(kv.Key, kv.Value);
            }
            return ExecuteNonQuery(cmd);
        }

        public object ExecuteNonQuery(CommandConfig cmd)
        {
            using (DbNetData db = new DbNetData(_connString, _dataProvider, _dataBaseType))
            {
                return db.ExecuteNonQuery(cmd);
            }
        }
        #endregion //ExecuteNonQuery


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


        #region VerifyDBInfo
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
                revisionVersion == dbinfo.RevisionVersion)
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
        #endregion //VerifyDBInfo
    }
}
