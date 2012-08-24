using System;
using System.Data;
using System.Collections.Generic;
using C3.Communi;
using Xdgk.Common;

namespace XGDPU
{
    internal class DBI : DBIBase
    {
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
        public DataTable ExecuteXGDeviceDataTable()
        {
            string s = "select * from tblDevice where DeviceType = 'xgdevice'";
            return ExecuteDataTable(s);
        }

    }

    public class XGDevicePersister : DevicePersisterBase
    {
        protected override void OnAdd(IDevice device)
        {
            XGDevice d = (XGDevice)device;

            string s = string.Format(
                "insert into tblDevice(address, deviceType, stationID) values({0}, '{1}', {2}); select @@identity;",
                d.Address,
                d.DeviceType.Name,
                GuidHelper.ConvertToInt32(d.Station.Guid)
                );

            object obj = DBI.Instance.ExecuteScalar(s);
            d.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        protected override void OnUpdate(IDevice device)
        {
            string s = string.Format(
                "update tblDevice set address = {0} where DeviceID = {1}",
                device.Address, GuidHelper.ConvertToInt32(device.Guid));

            DBI.Instance.ExecuteScalar(s);
        }

        protected override void OnDelete(IDevice device)
        {
            string s = string.Format(
                "delete from tblDevice where DeviceID = {0}",
                GuidHelper.ConvertToInt32(device.Guid));
            DBI.Instance.ExecuteScalar(s);

        }
    }

    internal class XGDeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal XGDeviceSource(DataRow row)
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
            }
        } private DataRow _dataRow;
        #endregion //DataRow
    }

    internal class XGDeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteXGDeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                XGDeviceSource item = new XGDeviceSource(row);
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

    internal class XGDevice : DeviceBase
    {
        public override object GetLazyDataFieldValue(string name)
        {
            if (StringHelper.Equal(name, "date"))
            {
                return DateTime.Now.Date;
            }
            else if (StringHelper.Equal(name, "time"))
            {
                return DateTime.Now.TimeOfDay;
            }

            return base.GetLazyDataFieldValue(name);
        }
    }

    internal class XGDeviceProcessor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="bs"></param>
        /// <returns></returns>
        //public override IUploadParseResult OnProcessUpload(IDevice device, byte[] bs)
        public override void  OnProcessUpload(IDevice device, IParseResult pr)
        {
            XGDevice xg = (XGDevice)device;
            DateTime dt = (DateTime)pr.Results["DT"];
            string cardSN = pr.Results["cardSN"].ToString();

            xg.DeviceDataManager.Last = new XGData(dt, cardSN);
        }

        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                XGDevice xgdevice = (XGDevice)task.Device;

                switch (opera)
                {
                    case XGOperaNames.ReadCount:
                        ProcessXGReadRecordCountResult(xgdevice, pr);
                        break;

                    case XGOperaNames.ReadRecord :
                        ProcessXGReadRecordResult(xgdevice, pr);
                        break;

                    case XGOperaNames.RecordUpload:
                        ProcessXGUploadRecordResult(xgdevice, pr);
                        break;

                    case XGOperaNames.ClearRecord:
                        ProcessXGClearRecordResult(pr);
                        break;

                    case XGOperaNames.RemoveUpload :
                        ProcessXGRemoveUploadResult(pr);
                        break;

                    case XGOperaNames.ReadXGDate:
                        ProcessXGReadDateResult(xgdevice, pr);
                        break;

                    case XGOperaNames.ReadXGTime:
                        ProcessXGReadTimeResult(xgdevice, pr);
                        break;

                    case XGOperaNames.WriteXGDate:
                    case XGOperaNames.WriteXGTime:
                        // do nothing
                        //
                        break;

                    default:
                        {
                            string errmsg = string.Format("{0} {1}",
                                                          xgdevice.DeviceType.Text,
                                                          opera);
                            throw new NotSupportedException(errmsg);
                        }
                }
            }
        }

        private void ProcessXGReadTimeResult(XGDevice xgdevice, IParseResult pr)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgdevice"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGReadDateResult(XGDevice xgdevice, IParseResult pr)
        {
            //CZGRApp.Default.MainForm.UpdateXGDate(xgdevice, pr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseResult"></param>
        private void ProcessXGRemoveUploadResult(IParseResult pr)
        {
            // do nothing
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseResult"></param>
        private void ProcessXGClearRecordResult(IParseResult pr)
        {
            // do nothing
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseResult"></param>
        private void ProcessXGUploadRecordResult(XGDevice xgdevice, IParseResult pr)
        {
            ProcessXGDeviceRecordHelp(xgdevice, pr, false);

            IOperaFactory operaFactory = xgdevice.Dpu.OperaFactory;
            IOpera op = operaFactory.Create(xgdevice.GetType().Name, XGOperaNames.RemoveUpload);
            //ITaskFactory taskFactory = xgdevice.Dpu.TaskFactory;

            TimeSpan tsTimeout = TimeSpan.FromMilliseconds(xgdevice.Station.CommuniPortConfig.TimeoutMilliSecond);
            Task task = new Task(xgdevice, op, new ImmediateStrategy(), tsTimeout);

            xgdevice.TaskManager.Tasks.Enqueue(task);

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGReadRecordResult(XGDevice xgdevice, IParseResult pr)
        {
            ProcessXGDeviceRecordHelp(xgdevice, pr, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgdevice"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGDeviceRecordHelp(XGDevice xgdevice, IParseResult parseResult, bool isFD )
        {
            // 1. uploadrecord
            // 2. readrecord 
            //    2.1 return record               
            //    2.2 return record count if record idx out of range
/*
            if (parseResult.IsSuccess)
            {
                if (StringHelper.Equal(parseResult.ReceivePartName, "recordcount"))
                {
                    string errmsg = string.Format("record index '{0}' out of range",
                        parseResult.NameObjects.GetObject("recordcount"));
                    //CZGRApp.Default.MainForm.UpdateXGDeviceState(xgdevice, errmsg);   
                    log.Warning(errmsg);
                    
                }
                else
                {
                    // upload record or readrecord
                    //
                    DBXGDevice dbxgdevice = xgdevice.DBXGDevice;
                    DateTime dt = Convert.ToDateTime(parseResult.NameObjects.GetObject("DT"));
                    string cardsn = parseResult.NameObjects.GetObject("cardsn").ToString();
                    DBXGData dbxgdata = new DBXGData(dbxgdevice, DBCard.FindCard(cardsn), dt);
                    dbxgdata.Create();

                    // update xgdatalistview
                    //
                    CZGRApp.Default.MainForm.UpdateXGData(xgdevice, parseResult, dbxgdata, isFD);
                }
            }
 */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGReadRecordCountResult(XGDevice xgdevice, IParseResult pr)
        {
    /*
            CommuniSoft soft = CZGRApp.Default.Soft;
            Debug.Assert(soft != null,"CZGRApp.Default.Soft == null");

            int count = Convert.ToInt32(parseResult.NameObjects.GetObject("recordcount"));
            if (count > 0)
            {
                for (int i = 1; i < count + 1; i++)
                {
                    //Opera op = soft.OperaFactory.Create(DeviceTypes.XGDevice,
                    //    XGOperaNames.ReadRecord);
                    Opera op = xgdevice.DeviceDefine.CreateOpera(XGOperaNames.ReadRecord);
                    op.SendPart["recordidx"] = i;
                    //Task task = new Task(Helper.GetXGDevice(xgdevice), op, new ImmediateStrategy());
                    Task task = new Task(xgdevice, op, new ImmediateStrategy());
                    soft.TaskManager.Tasks.Add(task);
                }

                //Opera clearOP = soft.OperaFactory.Create(DeviceTypes.XGDevice,
                //    XGOperaNames.ClearRecord);
                Opera clearOP = xgdevice.DeviceDefine.CreateOpera(XGOperaNames.ClearRecord);

                Task clearTask = new Task(xgdevice, clearOP, new ImmediateStrategy());
                soft.TaskManager.Tasks.Add(clearTask);
            }
     */
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class XGOperaNames
    {
        public const string ReadCount = "readcount";
        public const string ReadRecord = "readrecord";
        public const string ClearRecord = "clearrecord";
        public const string RecordUpload = "recordupload";
        public const string RemoveUpload = "removeupload";
        public const string ReadXGDate = "readdate";
        public const string ReadXGTime = "readtime";
        public const string WriteXGDate = "writedate";
        public const string WriteXGTime = "writetime";
    }

    /// <summary>
    /// 
    /// </summary>
    public class XGDpu : DPUBase
    {
        public XGDpu()
        {
            this.Name = "XDDpu";
            this.DeviceFactory = new XGDeviceFactory(this);
            this.DevicePersister = new XGDevicePersister();
            this.DeviceSourceProvider = new XGDeviceSourceProvider();
            this.DeviceType = DeviceTypeManager.AddDeviceType("XGDevice",
                "Xun Geng(Text)",
                typeof(XGDevice));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new XGDeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(XGDevice).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }
}
