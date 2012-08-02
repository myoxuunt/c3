using System;
using System.Reflection;

namespace C3.Communi
{
    /// <summary>
    ///
    /// </summary>
    abstract public class DeviceBase : IDevice
    {
        //#region DeviceBase
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="deviceType"></param>
        //protected DeviceBase(string name, DeviceType deviceType, Int64 address)
        //{
        //    this.Name = name;
        //    this.DeviceType = deviceType;
        //    this.Address = address;
        //}
        //#endregion //DeviceBase

        #region Address
        /// <summary>
        /// 
        /// </summary>
        [DeviceInfo("Address",-1)]
        public Int64 Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        } private Int64 _address;
        #endregion //Address

        #region Name
        /// <summary>
        /// 
        /// </summary>
        [DeviceInfo ("Name",-2)]
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    // for reset text
                    //
                    _text = null;
                }
            }
        } private string _name;
        #endregion //Name

        #region Station
        /// <summary>
        /// 
        /// </summary>
        public IStation Station
        {
            get
            {
                return _station;
            }
            set
            {
                _station = value;
            }
        } private IStation _station;
        #endregion //Station

        #region DeviceSource
        /// <summary>
        /// 
        /// </summary>
        public IDeviceSource DeviceSource
        {
            get
            {
                return _deviceSource;
            }
            set
            {
                _deviceSource = value;
            }
        } private IDeviceSource _deviceSource;
        #endregion //DeviceSource

        #region Dpu
        /// <summary>
        /// 
        /// </summary>
        public IDPU Dpu
        {
            get
            {
                return _dpu;
            }
            set
            {
                _dpu = value;
            }
        } private IDPU _dpu;
        #endregion //Dpu

        #region CommuniDetails
        /// <summary>
        /// 
        /// </summary>
        public CommuniDetailCollection CommuniDetails
        {
            get
            {
                if (_communiDetails == null)
                {
                    _communiDetails = new CommuniDetailCollection();
                }
                return _communiDetails;
            }
            set
            {
                _communiDetails = value;
            }
        } private CommuniDetailCollection _communiDetails;
        #endregion //CommuniDetails

        #region Guid
        /// <summary>
        /// 
        /// </summary>
        [DeviceInfo("guid",1)]
        public Guid Guid
        {
            get
            {
                if (_guid == null)
                {
                    _guid = new Guid();
                }
                return _guid;
            }
            set
            {
                _guid = value;
            }
        } private Guid _guid;
        #endregion //Guid

        #region StationGuid
        /// <summary>
        /// 
        /// </summary>
        public Guid StationGuid
        {
            get
            {
                if (_stationGuid == null)
                {
                    _stationGuid = new Guid();
                }
                return _stationGuid;
            }
            set
            {
                _stationGuid = value;
            }
        } private Guid _stationGuid;
        #endregion //StationGuid

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    //if (this.Name.Length > 0)
                    //{
                    //    _text = string.Format("{0}({1})", this.Name, this.GetType().Name);
                    //}
                    //else
                    //{
                    //    _text = string.Format("({0})", this.GetType().Name);
                    //}
                    _text = string.Format("{0}({1})", this.Name, this.DeviceType.Name);
                }
                return _text;
            }
            set
            {
                _text = value;
            }
        } private string _text;
        #endregion //Text

        #region Tag

        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        } private object _tag;

        #endregion //

        #region DeviceDataManager
        /// <summary>
        /// 
        /// </summary>
        public DeviceDataManager DeviceDataManager
        {
            get 
            {
                if (_deviceDataManager==null)
                {
                    _deviceDataManager = new DeviceDataManager();
                }
                return _deviceDataManager;
            }
        } private DeviceDataManager _deviceDataManager;

        #endregion //DeviceDataManager

        #region TaskManager
        /// <summary>
        /// 
        /// </summary>
        public TaskManager TaskManager
        {
            get
            {
                if (_taskManager ==null)
                {
                    _taskManager = new TaskManager();
                }
                return _taskManager;
            }
        } private TaskManager _taskManager;

        #endregion //TaskManager

        #region DeviceType
        /// <summary>
        /// 
        /// </summary>
        public DeviceType DeviceType
        {
            get
            {
                if (_deviceType == null)
                {
                    // TODO: 2012-08-01
                    //
                    _deviceType = DeviceTypeManager.GetDeviceType(this.GetType().Name);
                }
                return _deviceType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DeviceType");
                }
                _deviceType = value;
            }
        } private DeviceType _deviceType;
        #endregion //DeviceType

        #region 
        /// <summary>
        /// 
        /// </summary>
        public ReportItemCollection GetDeviceInfos()
        {
            ReportItemCollection reportItems = new ReportItemCollection();
            foreach (DeviceInfoAttribute item in this.DeviceInfoAttributes)
            {
                PropertyInfo pi = item.PropertyInfo;
                object value = pi.GetValue(this, null);
                ReportItem ri = new ReportItem(item.Name, value, Unit.FindByName(Unit.None));
                reportItems.Add(ri);
            }
            return reportItems;
        }
        #endregion

        #region DeviceInfoAttributes
        /// <summary>
        /// 
        /// </summary>
        private DeviceInfoAttributeCollection DeviceInfoAttributes
        {
            get
            {
                if (_deviceInfoAttributes == null)
                {
                    _deviceInfoAttributes = new DeviceInfoAttributeCollection();

                    PropertyInfo[] propertyInfos = this.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertyInfos)
                    {
                        object[] atts = pi.GetCustomAttributes(typeof(DeviceInfoAttribute), false);
                        if (atts.Length > 0)
                        {
                            DeviceInfoAttribute att = (DeviceInfoAttribute)atts[0];
                            att.PropertyInfo = pi;

                            _deviceInfoAttributes.Add(att);
                        }
                    }

                    // sort
                    //
                    _deviceInfoAttributes.Sort();
                }
                return _deviceInfoAttributes;
            }
        } private DeviceInfoAttributeCollection _deviceInfoAttributes;
        #endregion //DeviceInfoAttributes

        #region 
        /// <summary>
        /// 
        /// </summary>
        public DeviceParameterCollection DeviceParameters
        {
            get
            {
                if (_deviceParameters == null)
                {
                    _deviceParameters = new DeviceParameterCollection();
                }
                return _deviceParameters;
            }
            set
            {
                _deviceParameters = value;
            }
        } private DeviceParameterCollection _deviceParameters;

        #endregion
    }
}
