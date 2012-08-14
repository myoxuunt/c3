using System;
using System.Reflection;

namespace C3.Communi
{
    /// <summary>
    ///
    /// </summary>
    abstract public class DeviceBase : IDevice
    {
        private const string
            PN_ADDRESS = "address",
            PN_NAME = "name";

        private const int
            PO_ADDRESS = -90,
            PO_NAME = -80;

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

        /// <summary>
        /// 
        /// </summary>
        protected DeviceBase()
        {
            // init parameters
            //
            object obj = this.GetAddressParameter();
            obj = this.GetNameParameter();
        }

        #region Address
        /// <summary>
        /// 
        /// </summary>
        public UInt64 Address
        {
            get
            {
                IParameter p = GetAddressParameter();
                return (UInt64)p.Value;
            }
            set
            {
                IParameter p = GetAddressParameter();
                p.Value = value;
            }
        }
        #endregion //Address

        #region GetAddressParameter
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IParameter GetAddressParameter()
        {
            IParameter p = this.GeneralGroup.Parameters[PN_ADDRESS];
            if (p == null)
            {
                // TODO:
                // number parameter
                //
                p = new Parameter(PN_ADDRESS, typeof(UInt64), (UInt64)0, PO_ADDRESS);
                //p.ParameterUI = new NumberParameterUI();
                this.GeneralGroup.Parameters.Add(p);
            }
            return p;
        }
        #endregion //GetAddressParameter

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                IParameter p = GetNameParameter();
                return (string)p.Value;
            }
            set
            {
                IParameter p = GetNameParameter();
                p.Value = value;
            }
        }
        #endregion //Name

        #region GetNameParameter
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IParameter GetNameParameter()
        {
            IParameter p = this.GeneralGroup.Parameters[PN_NAME];
            if (p == null)
            {
                p = new Parameter(PN_NAME, typeof(string), string.Empty, PO_NAME);
                this.GeneralGroup.Parameters.Add(p);
            }
            return p;
        }
        #endregion //GetNameParameter

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
        [DeviceInfo("guid", 1)]
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
                string text = string.Format("{0}({1})", this.Name, this.DeviceType.Text);
                return text;
            }
        } 
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
                if (_deviceDataManager == null)
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
                if (_taskManager == null)
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

        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //public ReportItemCollection GetDeviceInfos()
        //{
        //    ReportItemCollection reportItems = new ReportItemCollection();
        //    foreach (DeviceInfoAttribute item in this.DeviceInfoAttributes)
        //    {
        //        PropertyInfo pi = item.PropertyInfo;
        //        object value = pi.GetValue(this, null);
        //        ReportItem ri = new ReportItem(item.Name, value, Unit.FindByName(Unit.None));
        //        reportItems.Add(ri);
        //    }
        //    return reportItems;
        //}
        //#endregion

        //#region DeviceInfoAttributes
        ///// <summary>
        ///// 
        ///// </summary>
        //private DeviceInfoAttributeCollection DeviceInfoAttributes
        //{
        //    get
        //    {
        //        if (_deviceInfoAttributes == null)
        //        {
        //            _deviceInfoAttributes = new DeviceInfoAttributeCollection();

        //            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
        //            foreach (PropertyInfo pi in propertyInfos)
        //            {
        //                object[] atts = pi.GetCustomAttributes(typeof(DeviceInfoAttribute), false);
        //                if (atts.Length > 0)
        //                {
        //                    DeviceInfoAttribute att = (DeviceInfoAttribute)atts[0];
        //                    att.PropertyInfo = pi;

        //                    _deviceInfoAttributes.Add(att);
        //                }
        //            }

        //            // sort
        //            //
        //            _deviceInfoAttributes.Sort();
        //        }
        //        return _deviceInfoAttributes;
        //    }
        //} private DeviceInfoAttributeCollection _deviceInfoAttributes;
        //#endregion //DeviceInfoAttributes

        //#region Parameters
        ///// <summary>
        ///// 
        ///// </summary>
        //public ParameterCollection Parameters
        //{
        //    get
        //    {
        //        if (_deviceParameters == null)
        //        {
        //            _deviceParameters = new ParameterCollection();
        //        }
        //        return _deviceParameters;
        //    }
        //    //set
        //    //{
        //    //    throw new NotImplementedException("Parameters");
        //    //    _deviceParameters = value;
        //    //}
        //} private ParameterCollection _deviceParameters;
        //#endregion //Parameters


        protected IGroup GeneralGroup
        {
            get
            {
                IGroup g = this.Groups.GetGroup("General");
                if (g == null)
                {
                    g = new Group();
                    g.Name = "General";
                    this.Groups.Add (g);
                }
                return g;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public GroupCollection Groups
        {
            get
            {
                if (_parameterGroups == null)
                {
                    _parameterGroups = new GroupCollection();
                }
                return _parameterGroups ;
            }
        } private GroupCollection _parameterGroups;

    }
}
