using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
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

                    SourceConfig sc = SoftManager.GetSoft().SourceConfigs.Find("Connection");
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
    }

    public class XD1100DevicePersister : DevicePersisterBase
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnAdd(IDevice device)
        {
            XD1100Device d = (XD1100Device)device;

            string s = string.Format(
                "insert into tblDevice(address, deviceType, stationID, extend) values({0}, '{1}', {2}, '{3}'); select @@identity;",
                d.Address,
                d.DeviceType.Name,
                GuidHelper.ConvertToInt32(d.Station.Guid),
                GetExtend(d)
                );

            object obj = DBI.Instance.ExecuteScalar(s);
            d.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static string GetExtend(XD1100Device d)
        {
            StringStringDictionary ssDict = new StringStringDictionary();
            ssDict["HtmMode"] = d.HtmMode.ModeValue.ToString();

            string extend = StringStringDictionaryConverter.ToString(ssDict);
            return extend;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnUpdate(IDevice device)
        {
            // TODO:
            //
            string s = string.Format(
                "update tblDevice set address = {0}, extend = '{1}' where DeviceID = {2}",
                device.Address,
                GetExtend((XD1100Device)device),
                GuidHelper.ConvertToInt32(device.Guid));

            DBI.Instance.ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnDelete(IDevice device)
        {
            string s = string.Format(
                "delete from tblDevice where DeviceID = {0}",
                GuidHelper.ConvertToInt32(device.Guid));
            DBI.Instance.ExecuteScalar(s);

        }
    }

    internal class XD1100DeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal XD1100DeviceSource(DataRow row)
        {
            this.DataRow = row;
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
                this.Address = Convert.ToUInt64(_dataRow["address"]);

                this.DevcieTypeName = _dataRow["DeviceType"].ToString().Trim();

                this.Guid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["DeviceID"])
                    );

                this.StationGuid = GuidHelper.Create(
                    Convert.ToInt32(_dataRow["StationID"])
                    );

                string ex = _dataRow["Extend"].ToString();
                StringStringDictionary extend = StringStringDictionaryConverter.Parse(ex);

                foreach (string obj in extend.Keys)
                {
                    if (StringHelper.Equal(obj.ToString(), "htmMode"))
                    {
                        string k = extend[obj];
                        this.HtmModeValue = (ModeValue)Enum.Parse(typeof(ModeValue), k);
                    }
                }
            }
        } private DataRow _dataRow;

        #endregion //DataRow

        #region HtmModeValue
        /// <summary>
        /// 
        /// </summary>
        public ModeValue HtmModeValue
        {
            get
            {
                return _htmModeValue;
            }
            set
            {
                _htmModeValue = value;
            }
        } private ModeValue  _htmModeValue;
        #endregion //HtmModeValue

    }

    internal class XD1100DeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteXD1100DeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                XD1100DeviceSource  item = new XD1100DeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

    internal class XGData : IDeviceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardSn"></param>
        public XGData(DateTime dt, string cardSn)
        {
            if (cardSn == null) throw new ArgumentNullException("cardSn");
            if( cardSn.Trim( ).Length == 0 )
            {
                throw new ArgumentException("cardSn exception");
            }
            this.DT = dt;
            this.CardSN = cardSn;
        }

        #region IDeviceData 成员

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        } private DateTime _dt;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReportItemCollection GetReportItems()
        {
            ReportItemCollection r = new ReportItemCollection();
            r.Add(new ReportItem("时间", this.DT, Unit.FindByName(Unit.None)));
            r.Add(new ReportItem("卡号", this.CardSN, Unit.FindByName(Unit.None)));
            return r;
        }

        #region CardSN
        /// <summary>
        /// 
        /// </summary>
        public string CardSN
        {
            get
            {
                if (_cardSN == null)
                {
                    _cardSN = string.Empty;
                }
                return _cardSN;
            }
            set
            {
                _cardSN = value;
            }
        } private string _cardSN;
        #endregion //CardSN


        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ModeValue
    {
        [EnumText("DirectText")]
        Direct,
        [EnumText("InDirectText")]
        Indirect,
        [EnumText("MixedText")]
        Mixed,
    }

    /// <summary>
    /// 
    /// </summary>
    abstract public class HeatTransferMode
    {
        static private HeatTransferMode
            _direct = new HtmDirect(),
            _indirect = new HtmIndirect(),
            _mixed = new HtmMixed();

        /// <summary>
        /// 
        /// </summary>
        static private HeatTransferMode[] a = new HeatTransferMode[]
        {
            _direct, _indirect , _mixed 
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public HeatTransferMode Parse(int value)
        {
            ModeValue mv = (ModeValue)value;
            return Parse(mv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modeValue"></param>
        /// <returns></returns>
        static public HeatTransferMode Parse(ModeValue modeValue)
        {
            HeatTransferMode r = null;
            foreach (HeatTransferMode item in a)
            {
                if (modeValue == item.ModeValue)
                {
                    r = item;
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        abstract public ModeValue ModeValue{get;}

        
    }

    internal class HtmDirect : HeatTransferMode 
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Direct; }
        }
        
    }

    internal class HtmIndirect: HeatTransferMode
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Indirect ; }
        }
    }

    internal class HtmMixed: HeatTransferMode
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Mixed; }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    internal class XD1100Device : DeviceBase
    {
        private const string PN_HEATTRANSFERMODE = "heatTransferMode";
        private const int PO_HEATTRANSFERMODE = 1;

        /// <summary>
        /// 
        /// </summary>
        public XD1100Device()
        {
            // init 
            //
            IParameter p = GetHeatTransferModeParameter();
        }

        /// <summary>
        /// 
        /// </summary>
        public HeatTransferMode HtmMode
        {
            get
            {
                ModeValue mv = (ModeValue)GetHeatTransferModeParameter().Value ;
                return HeatTransferMode.Parse(mv);
            }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HtmMode");
                }
                IParameter p = GetHeatTransferModeParameter();
                p.Value = value.ModeValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IParameter GetHeatTransferModeParameter()
        {
            IParameter p = this.GeneralGroup.Parameters[PN_HEATTRANSFERMODE];

            if (p == null)
            {
                p = new EnumParameter(
                    PN_HEATTRANSFERMODE,
                    typeof(ModeValue),
                    ModeValue.Mixed,
                    PO_HEATTRANSFERMODE);

                p.Text = "htm text";
                this.GeneralGroup.Parameters.Add(p);
            }
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override object GetLazyDataFieldValue(string name)
        {
            if (StringHelper.Equal(name, "dt"))
            {
                return DateTime.Now;
            }

            return base.GetLazyDataFieldValue(name);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    internal class XD1100OperaNames
    {
    }

    public class XD1100DeviceFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public XD1100DeviceFactory(IDPU dpu)
            : base(dpu)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            XD1100DeviceSource source = (XD1100DeviceSource)deviceSource;
            XD1100Device d = new XD1100Device();
            d.Address = source.Address;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            d.HtmMode = HeatTransferMode.Parse(source.HtmModeValue);
            d.Pickers = Dpu.OperaFactory.CreatePickers(this.Dpu.DeviceType.Name);
            return d;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class XD1100Dpu : DPUBase
    {
        public XD1100Dpu()
        {
            this.Name = "XD1100Dpu";
            this.DeviceFactory = new XD1100DeviceFactory (this);
            this.DevicePersister = new XD1100DevicePersister ();
            this.DeviceSourceProvider = new XD1100DeviceSourceProvider ();
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "XD1100Device",
                "XD1100Device(Text)",
                typeof(XD1100Device));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new XD1100DeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(XD1100Device).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class XD1100DeviceProcessor : TaskProcessorBase
    {
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="pr"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            throw new NotImplementedException();
        }
    }
}
