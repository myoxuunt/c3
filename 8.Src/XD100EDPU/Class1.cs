using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

using Xdgk.Common;
using C3.Communi.SimpleDPU;

namespace XD100EDPU
{
    /// <summary>
    /// 
    /// </summary>
    public class Xd100eDpu : DPUBase
    {
        public Xd100eDpu()
        {
            this.Name = "Xd100edDpu";
            this.DeviceFactory = new Xd100eFactory(this);
            this.DevicePersister = new Xd100ePersister(DBI.Instance);
            this.DeviceSourceProvider = //new Scl6SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(Xd100e));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "Xd100e",
                "Xd100e(Text)",
                typeof(Xd100e));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new Xd100eProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(Xd100e).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }



    /// <summary>
    /// 
    /// </summary>
    internal class DBI : DBIBase
    {
        internal DBI(string conn)
            : base(conn)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static internal DBI Instance
        {
            get
            {
                if (_instance == null)
                {
                    string conn = C3.Communi.SoftManager.GetSoft().SourceConfigs.Find("Connection").Value;
                    _instance = new DBI(conn);
                }
                return _instance;
            }
        } static private DBI _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        internal void InsertXd100eData(int deviceID, Xd100eData data)
        {
            string columns = //string.Empty;
                "DeviceID, DT,";
            string values =// string.Empty;
                string.Format("{0}, '{1}',", deviceID, data.DT.ToString());

            for (int i = Xd100eData.BeginChannelNO; i <= Xd100eData.EndChannelNO; i++)
            {
                columns += "ai" + i + ",";
                values += data.GetChannelDataAI(i).ToString() + ",";

                columns += "di" + i + ",";
                values += (data.GetChannelDataDI(i) ? "1" : "0") + ",";
            }

            columns = RemoveLastChar(columns);
            values = RemoveLastChar(values);

            string s = string.Format("insert into tblXd100eData({0}) values({1})", columns, values);
            ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string RemoveLastChar(string s)
        {
            if (s != null && s.Length > 1)
            {
                return s.Substring(0, s.Length - 1);
            }
            return s;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Xd100eFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        internal Xd100eFactory(IDPU dpu)
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
            SimpleDeviceSource s = (SimpleDeviceSource)deviceSource;
            Xd100e d = new Xd100e();
            d.Address = s.Address;
            d.DeviceSource = deviceSource;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = s.Guid;
            d.StationGuid = s.StationGuid;

            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Xd100e : DeviceBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Xd100eData : DataBase
    {
        private DateTime _createDT = DateTime.Now;
        #region IsSetAI
        /// <summary>
        /// 
        /// </summary>
        public bool IsSetAI
        {
            get
            {
                return _isSetAI;
            }
            set
            {
                _isSetAI = value;
            }
        } private bool _isSetAI;
        #endregion //IsSetAI

        #region IsSetDI
        /// <summary>
        /// 
        /// </summary>
        public bool IsSetDI
        {
            get
            {
                return _isSetDI;
            }
            set
            {
                _isSetDI = value;
            }
        } private bool _isSetDI;
        #endregion //IsSetDI

        public bool IsTimeout()
        {
            TimeSpan ts = TimeSpan.FromMinutes(5d);

            TimeSpan t = DateTime.Now - this._createDT;
            if (t < TimeSpan.Zero || t > ts)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            return IsSetAI && IsSetDI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Xd100eData GetCachedData()
        {
            if (_cachedData == null ||
                _cachedData.IsComplete() ||
                _cachedData.IsTimeout())
            {
                _cachedData = new Xd100eData();
            }
            return _cachedData;
        } static private Xd100eData _cachedData;

        /// <summary>
        /// 
        /// </summary>
        public const int
            BeginChannelNO = 1,
            EndChannelNO = 8;

        /// <summary>
        /// 
        /// </summary>
        float[] _channelDatasAI = new float[9];
        bool[] _channelDatasDI = new bool[9];

        /// <summary>
        /// 
        /// </summary>
        private Xd100eData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelNO"></param>
        /// <returns></returns>
        public float GetChannelDataAI(int channelNO)
        {
            Debug.Assert(channelNO >= 1 && channelNO <= 8);
            return _channelDatasAI[channelNO];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelNO"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetChannelDataAI(int channelNO, float value)
        {
            Debug.Assert(channelNO >= 1 && channelNO <= 8);
            _channelDatasAI[channelNO] = value;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelNO"></param>
        /// <returns></returns>
        public bool GetChannelDataDI(int channelNO)
        {
            Debug.Assert(channelNO >= 1 && channelNO <= 8);
            return _channelDatasDI[channelNO];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelNO"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetChannelDataDI(int channelNO, bool value)
        {
            Debug.Assert(channelNO >= 1 && channelNO <= 8);
            _channelDatasDI[channelNO] = value;
        }


        #region AI1
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI1",11,"")]
        public float AI1
        {
            get
            {
                return _channelDatasAI[1];
            }
            set
            {
                _channelDatasAI[1] = value;
            }
        }
        #endregion //AI1

        #region AI2
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI2",12,"")]
        public float AI2
        {
            get
            {
                return _channelDatasAI[2];
            }
            set
            {
                _channelDatasAI[2] = value;
            }
        }
        #endregion //AI2

        #region AI3
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI3",13,"")]
        public float AI3
        {
            get
            {
                return _channelDatasAI[3];
            }
            set
            {
                _channelDatasAI[3] = value;
            }
        }
        #endregion //AI3

        #region AI4
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI4",14,"")]
        public float AI4
        {
            get
            {
                return _channelDatasAI[4];
            }
            set
            {
                _channelDatasAI[4] = value;
            }
        }
        #endregion //AI4

        #region AI5
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI5",15,"")]
        public float AI5
        {
            get
            {
                return _channelDatasAI[5];
            }
            set
            {
                _channelDatasAI[5] = value;
            }
        }
        #endregion //AI5

        #region AI6
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI6",16,"")]
        public float AI6
        {
            get
            {
                return _channelDatasAI[6];
            }
            set
            {
                _channelDatasAI[6] = value;
            }
        }
        #endregion //AI6

        #region AI7
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI7",17,"")]
        public float AI7
        {
            get
            {
                return _channelDatasAI[7];
            }
            set
            {
                _channelDatasAI[7] = value;
            }
        }
        #endregion //AI7

        #region AI8
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("AI8",18,"")]
        public float AI8
        {
            get
            {
                return _channelDatasAI[8];
            }
            set
            {
                _channelDatasAI[8] = value;
            }
        }
        #endregion //AI8

        #region DI1
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI1",21,"")]
        public bool DI1
        {
            get
            {
                return _channelDatasDI[1];
            }
            set
            {
                _channelDatasDI[1] = value;
            }
        }
        #endregion //DI1

        #region DI2
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI2",22,"")]
        public bool DI2
        {
            get
            {
                return _channelDatasDI[2];
            }
            set
            {
                _channelDatasDI[2] = value;
            }
        }
        #endregion //DI2

        #region DI3
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI3",23,"")]
        public bool DI3
        {
            get
            {
                return _channelDatasDI[3];
            }
            set
            {
                _channelDatasDI[3] = value;
            }
        }
        #endregion //DI3

        #region DI4
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI4",24,"")]
        public bool DI4
        {
            get
            {
                return _channelDatasDI[4];
            }
            set
            {
                _channelDatasDI[4] = value;
            }
        }
        #endregion //DI4

        #region DI5
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI5",25,"")]
        public bool DI5
        {
            get
            {
                return _channelDatasDI[5];
            }
            set
            {
                _channelDatasDI[5] = value;
            }
        }
        #endregion //DI5

        #region DI6
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI6",26,"")]
        public bool DI6
        {
            get
            {
                return _channelDatasDI[6];
            }
            set
            {
                _channelDatasDI[6] = value;
            }
        }
        #endregion //DI6

        #region DI7
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI7",27,"")]
        public bool DI7
        {
            get
            {
                return _channelDatasDI[7];
            }
            set
            {
                _channelDatasDI[7] = value;
            }
        }
        #endregion //DI7

        #region DI8
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("DI8",28,"")]
        public bool DI8
        {
            get
            {
                return _channelDatasDI[8];
            }
            set
            {
                _channelDatasDI[8] = value;
            }
        }
        #endregion //DI8


    }

    /// <summary>
    /// 
    /// </summary>
    public class Xd100ePersister : SimpleDevicePersister
    {
        public Xd100ePersister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Xd100eProcessor : TaskProcessorBase
    {
        private string ReadReal = "ReadReal",
            ReadRealDI = "ReadRealDI";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                Xd100eData data = Xd100eData.GetCachedData();

                Xd100e xd100eDevice = (Xd100e)task.Device;
                string opera = task.Opera.Name;

                if (StringHelper.Equal(opera, ReadReal))
                {
                    for (int i = Xd100eData.BeginChannelNO; i <= Xd100eData.EndChannelNO; i++)
                    {
                        int no = i;
                        string channelName = GetChannelName(no);

                        float val = Convert.ToSingle(pr.Results[channelName]);
                        val /= 100f;

                        data.SetChannelDataAI(no, val);
                        data.IsSetAI = true;
                    }
                }

                else if (StringHelper.Equal(opera, ReadRealDI))
                {
                    //XD100EData data = xd100eDevice.XD100EData;
                    for (int i = Xd100eData.BeginChannelNO; i <= Xd100eData.EndChannelNO; i++)
                    {
                        int no = i;
                        string channelName = GetChannelName(no);
                        byte value = Convert.ToByte(pr.Results[channelName]);
                        data.SetChannelDataDI(no, value > 0);
                        data.IsSetDI = true;
                    }

                }

                if (data.IsComplete())
                {
                    data.DT = DateTime.Now;
                    xd100eDevice.DeviceDataManager.Last = data;

                    int deviceID = GuidHelper.ConvertToInt32(xd100eDevice.Guid);
                    DBI.Instance.InsertXd100eData(deviceID, data);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        private string GetChannelName(int no)
        {
            string r = string.Format("Channal{0}Value", no);
            return r;
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {

            //throw new NotImplementedException();
        }
    }
}
