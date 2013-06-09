using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using C3.Communi.SimpleDPU;

namespace VGATE100DPU
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
        } static private DBI _instance;

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
        /// <param name="deviceID"></param>
        /// <param name="data"></param>
        public void InsertVGate100Data(int deviceID, VGate100Data data)
        {
            string s = " insert into tblGateData(deviceid, DT, BeforeWL, BehindWL, Height, instantFlux, TotalAmount, RemianAmount) " +
                       " values(@deviceID, @dt, @beforeWL, @behindWL, @height, @instantFlux, @totalAmount, @remainAmount)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("Dt", data.DT);
            list.Add("BeforeWL", data.BeforeWL);
            list.Add("BehindWL", data.BehindWL);
            list.Add("Height", data.Height);
            list.Add("InstantFlux", data.InstantFlux);
            list.Add("TotalAmount", data.TotalAmount);
            list.Add("RemainAmount", data.RemainAmount);

            ExecuteScalar(s, list);
        }

        public DateTime GetVGateLastDateTime(int deviceID)
        {
            string s = "select Max(DT) from tblGateData where DeviceID = @deviceID";
            ListDictionary list = new ListDictionary();
            list.Add("deviceID", deviceID);

            object obj = ExecuteScalar(s, list);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDateTime(obj);
            }
            else
            {
                return DateTime.Now.Date;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VGate100Dpu : DPUBase
    {
        public VGate100Dpu()
        {
            this.Name = "VGate100Dpu";
            this.DeviceFactory = new VGate100Factory(this);
            this.DevicePersister = new VGate100Persister(DBI.Instance);
            this.DeviceSourceProvider = //new VGate100SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(VGate100));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "VGate100",
                typeof(VGate100));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new VGate100Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(VGate100).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VGate100Data : DataBase
    {
        #region BeforeWL
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("闸前水位", 20, "m")]
        public double BeforeWL
        {
            get
            {
                return _beforeWL;
            }
            set
            {
                _beforeWL = value;
            }
        } private double _beforeWL;
        #endregion //BeforeWL

        #region BehindWL
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("闸后水位", 30, "m")]
        public double BehindWL
        {
            get
            {
                return _behindWL;
            }
            set
            {
                _behindWL = value;
            }
        } private double _behindWL;
        #endregion //BehindWL

        #region Height
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("闸高", 40, "m")]
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        } private double _height;
        #endregion //Height

        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("瞬时流量", 50, "m3/s")]
        public double InstantFlux
        {
            get
            {
                return _instantFlux;
            }
            set
            {
                _instantFlux = value;
            }
        } private double _instantFlux;
        #endregion //InstantFlux

        #region TotalAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("累计流量", 60, "m3")]
        public double TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
            }
        } private double _totalAmount;
        #endregion //TotalAmount

        #region RemianAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem ("剩余水量", 70, "m3")]
        public double RemainAmount
        {
            get
            {
                return _remainAmount;
            }
            set
            {
                _remainAmount = value;
            }
        } private double _remainAmount;
        #endregion //RemianAmount


    }

    /// <summary>
    /// 
    /// </summary>
    public class VGate100Processor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, "read"))
                {
                    VGate100Data data = new VGate100Data();
                    //data.InstantFlux = Convert.ToDouble(pr.Results["if"]);
                    //data.Sum = Convert.ToDouble(pr.Results["sum"]);

                    task.Device.DeviceDataManager.Last = data;

                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                    DBI.Instance.InsertVGate100Data(id, data);
                }
            }
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            // nothing
            //
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class VGate100Persister : SimpleDevicePersister
    {
        public VGate100Persister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    public class VGate100Factory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public VGate100Factory(IDPU dpu)
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
            VGate100 d = new VGate100();
            //d.DeviceSource = deviceSource;
            //SetDeviceProperties(d, deviceSource);
            base.SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DeviceKind("FluxDevice")]
    internal class VGate100 : DeviceBase
    {
        public override object GetLazyDataFieldValue(string name)
        {
            if (StringHelper.Equal(name, "name"))
            {
                return this.Station.Name;
            }
            else if (StringHelper.Equal(name, "dt"))
            {
                //return DateTime.Now;
                int deviceID = GuidHelper.ConvertToInt32(this.Guid);
                return DBI.Instance.GetVGateLastDateTime(deviceID);
            }
            else
            {
                string msg = "not find lazy name: " + name;
                throw new InvalidOperationException(msg);
            }
        }
    }
}