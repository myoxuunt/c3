using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using DbNetLink;
using NUnit.Framework;

namespace DbNetLinkTest
{
    [TestFixture]
    public class Test
    {
        private string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=e:\\_t_\\test.mdb";
        //private string connString = "server=(local);uid=sa;pwd=sa;database=btgrdb2013;";
        private DbNetLink.DbNetData GetDB()
        {
            return new DbNetData(connString);
        }

        [Test]
        public void TestInsert()
        {
            DbNetData db = GetDB();
            db.Open();
            string sql = @"insert into tblStation(stationOridnal, stationName, stationRemark, stationcpConfig) values (1, 'name', 'remark', 'cp config')";
            CommandConfig cmd = new CommandConfig (sql );
            db.ExecuteScalar(cmd);
        }

        [Test]
        public void TestInsertWithParams()
        {
            DbNetData db = GetDB();
            db.Open();
            string sql = 
@"insert into tblStation(stationOrdinal, stationName, stationRemark, stationcpConfig) 
values (@ori, @name, @remark, @cp)";

            CommandConfig cmd = new CommandConfig(sql);
            cmd.Params.Add("cp", "cp_param");
            cmd.Params.Add("ori", 123);
            cmd.Params.Add("name", "name_param");
            cmd.Params.Add("remark", "remark_param");

            db.ExecuteScalar(cmd);
        }


        [Test]
        public void UpdateWithParam()
        {
            DbNetData db = GetDB();
            db.Open();
            string sql = 
@"update tblStation set stationOrdinal = @ori, stationName = @name, stationRemark=@remark, stationcpConfig=@cp
where stationid = @id";

            UpdateCommandConfig  cmd = new UpdateCommandConfig (sql);
            cmd.Params.Add("id", 1);
            cmd.Params.Add("ori", 9);
            cmd.Params.Add("name", "name_param_up" + DateTime.Now.ToString());
            cmd.Params.Add("remark", "remark_param-up");
            cmd.Params.Add("cp", "cp_param_up");

            db.ExecuteScalar(cmd);

        }

        [Test]
        public void UpdateLXDB()
        {
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=e:\\_t_\\lx.mdb";
            DbNetData db = new DbNetData(conn);
            db.Open();

            UpdateStation(db, 7, "a", "xml ", 1, "strr", "remm");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="xml"></param>
        /// <param name="ordinal"></param>
        /// <param name="street"></param>
        /// <param name="remark"></param>
        internal void UpdateStation(DbNetData db, int id, string name, string xml, int ordinal, string street, string remark)
        {
            string s = @"update tblStation set StationName = @StationName, StationCPConfig = @StationCPConfig, 
                    StationOrdinal = @StationOrdinal, Street = @street, StationRemark = @stationRemark 
                    where stationid = @stationID";

            ListDictionary list = new ListDictionary();

            //list.Add("stationID", id);
            list.Add("stationName", name);
            list.Add("stationCPConfig", xml + DateTime.Now.ToString());
            list.Add("stationOrdinal", ordinal);
            list.Add("stationRemark", remark + DateTime.Now.ToString());
            list["stationID"] = id;
            list.Add("street", street);

            CommandConfig cmd = new CommandConfig(s);
            cmd.Params = list;
            //Instance.ExecuteScalar(s, list);
            db.ExecuteScalar(cmd);
        }


        [Test]
        public void TestOleDBCommand()
        {
            OleDbConnection con = new OleDbConnection(this.connString);
            con.Open();


            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = 
@"insert into tblStation(stationOrdinal, stationName, stationRemark, stationcpConfig) 
values (@ori, @name, @remark, @cp)";


            cmd.Parameters.Add(create ("name", "nnnn-oledb"));
            cmd.Parameters.Add(create ("remark", "nnnn-oledb"));
            cmd.Parameters.Add(create ("cp", "nnnn-oledb"));
            cmd.Parameters.Add(create ("ori", 1));

            cmd.ExecuteScalar();
        }

        private OleDbParameter create(string n, object v)
        {
            OleDbParameter p1 = new OleDbParameter();
            p1.ParameterName = n;
            p1.Value = v;
            return p1;
        }
    }
}
