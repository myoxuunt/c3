using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using System.Data;
using Xdgk.Common;

namespace DBSPU
{
    public class Station : StationBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class StationSource : StationSourceBase
    {
        public StationSource(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            this.DataRow = row;
            this.Guid = GuidHelper.Create((UInt32)(int)row["StationID"]);
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
            }
        } private DataRow _dataRow;
        #endregion //DataRow

    }

    public class StationFactory : StationFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spu"></param>
        public StationFactory(ISPU spu)
            : base(spu)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationSource"></param>
        /// <returns></returns>
        protected override IStation OnCreate(IStationSource stationSource)
        {
            StationSource source = stationSource as StationSource;
            DataRow row = source.DataRow;

            string stationName = row["StationName"].ToString();
            string xml = row["StationCPConfig"].ToString().Trim();
            int stationID = (int)row["StationID"];

            Station st = new Station();
            st.Name = stationName;
            st.Guid = GuidHelper.Create((uint)stationID);
            st.Spu = this.Spu;
            st.StationSource = stationSource;
            st.StationType = this.Spu.StationType;
            st.CommuniPortConfig = CommuniPortConfigSerializer.Deserialize(xml);

            return st;

        }
    }

    public class StationPersister : StationPersisterBase
    {

        public override void OnAdd(IStation station)
        {
            //throw new NotImplementedException();
            Station st = (Station)station;
            string xml = CommuniPortConfigSerializer.Serialize(st.CommuniPortConfig);
            int id = DBI.Instance.InsertStation(st.Name, xml);
            st.Guid = GuidHelper.Create((uint)id);
        }

        public override void OnUpdate(IStation station)
        {
            //throw new NotImplementedException();
            Station st = (Station)station;
            //st.Name;
            string xml = CommuniPortConfigSerializer.Serialize(st.CommuniPortConfig);
            int id = (int)GuidHelper.ConvertToUInt32 ( st.Guid );
            DBI.Instance.UpdateStation(id, st.Name, xml);
        }

        public override void OnDelete(IStation station)
        {
            Station st = (Station)station;
            int id = GuidHelper.ConvertToInt32(st.Guid);
            Debug.Assert(id != 0);

            DBI.Instance.DeleteStation(id);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StationSourceProvider : StationSourceProviderBase
    {
        protected override IStationSource[] OnGetStationSources()
        {
            DataTable tbl = DBI.Instance.ExecuteStationDataTable();
            List<StationSource> list = new List<StationSource>();

            foreach (DataRow row in tbl.Rows)
            {
                StationSource source = new StationSource(row);
                list.Add(source);
            }
            return list.ToArray();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DBI : DBIBase
    {
        static public DBI Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Assert(SoftManager.GetSoft() != null);

                    SourceConfig sc = SourceConfigManager.SourceConfigs.Find("Connection");
                    if (sc != null)
                    {
                        string conn = sc.Value;
                        _instance = new DBI(conn);
                    }
                    else
                    {
                        throw new ConfigException("not find config: connection");
                    }
                }
                return _instance;
            }
        } static private DBI _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public DBI(string conn)
            : base(conn)
        {

        }

        public DataTable ExecuteStationDataTable()
        {
            string s = "select * from tblStation";
            return Instance.ExecuteDataTable(s);
        }

        ///

        internal void UpdateStation(int id, string name, string xml)
        {
            string s = string.Format(
                "update tblStation set StationName='{1}',  StationCPConfig= '{2}' where stationid = {0}",
                id, name, xml);
            Instance.ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        internal int InsertStation(string name, string xml)
        {
            string s = string.Format (
            "insert into tblStation(StationName, StationCPConfig) values('{0}','{1}');select @@identity;",

            name, xml);

            object obj = ExecuteScalar(s);
            return Convert.ToInt32(obj);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteStation(int id)
        {
            string s = string.Format(
                "delete from tblStation where StationID = {0}",
                id);

            ExecuteScalar(s);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SPU : SPUBase
    {
        public SPU()
        {
            this.StationFactory = new StationFactory(this);
            this.Description = "description";
            this.Name = "db spu";
            this.StationPersister = new StationPersister();
            this.StationSourceProvider = new StationSourceProvider();
            this.StationUI = new StationUI(this);
            this.StationType = StationTypeManager.AddStationType(
                "Station", 
                typeof(Station));
        }
    }
}
