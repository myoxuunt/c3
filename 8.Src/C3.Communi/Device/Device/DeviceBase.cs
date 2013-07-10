using System;
using System.Diagnostics;
using Xdgk.Common;
//using Xdgk.
using NLog;

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

        #region Constructor
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
        #endregion //Constructor

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
                p = new NumberParameter(PN_ADDRESS, typeof(UInt64), (UInt64)0, PO_ADDRESS);
                p.Text = strings.Address;
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
                p = new StringParameter(PN_NAME, string.Empty, PO_NAME);
                p.Text = strings.DeviceName;
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

        #region HasData
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasData()
        {
            return this.DeviceDataManager.HasData();
        }
        #endregion //HasData

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
                    _deviceType = DeviceTypeManager.GetDeviceType(this.GetType().Name);
                    Debug.Assert(_deviceType != null);
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
        //    foreach (DeviceInfoAttribute this1 in this.DeviceInfoAttributes)
        //    {
        //        PropertyInfo pi = this1.PropertyInfo;
        //        object value = pi.GetValue(this, null);
        //        ReportItem ri = new ReportItem(this1.Name, value, Unit.FindByName(Unit.None));
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

        #region GeneralGroup
        /// <summary>
        /// 
        /// </summary>
        protected IGroup GeneralGroup
        {
            get
            {
                string name = "General";
                IGroup g = this.Groups.GetGroup(name);
                if (g == null)
                {
                    g = new Group(name, strings.General);
                    this.Groups.Add(g);
                }
                return g;
            }
        }
        #endregion //GeneralGroup

        #region Groups
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
                return _parameterGroups;
            }
        } private GroupCollection _parameterGroups;
        #endregion //Groups

        #region GetLazyDataFieldValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        virtual public object GetLazyDataFieldValue(string name)
        {
            return null;
        }
        #endregion //GetLazyDataFieldValue

        #region ProcessUpload
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public byte[] ProcessUpload(byte[] bs)
        {
            byte[] bsWork = bs;
            foreach (IPicker item in this.Pickers)
            {
                PickResult result = item.Pick(this, bsWork);
                if (result.IsSuccess)
                {
                    this.Dpu.Processor.ProcessUpload(this, result.ParseResult);
                }
                bsWork = result.Remain;
            }
            return bsWork;
        }
        #endregion //ProcessUpload

        #region Pickers
        /// <summary>
        /// 
        /// </summary>
        public PickerCollection Pickers
        {
            get
            {
                if (_pickers == null)
                {
                    _pickers = new PickerCollection();
                }
                return _pickers;
            }
            set
            {
                _pickers = value;
            }
        } private PickerCollection _pickers;
        #endregion //Pickers

        #region GetStringParameters
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetStringParameters()
        {
            string s = string.Empty;
            foreach (IGroup g in this.Groups)
            {
                foreach (IParameter p in g.Parameters)
                {
                    if (StringHelper.Equal(p.Name, PN_ADDRESS) ||
                        StringHelper.Equal(p.Name, PN_NAME))
                    {
                        continue;
                    }
                    else
                    {
                        s += string.Format("{0}{1}{2}{3}",
                            p.Name,
                            StringStringDictionaryConverter.KeyValueSplitChar,
                            p.Value.ToString(),
                            StringStringDictionaryConverter.ItemSplitChar);
                    }
                }
            }
            return s;
        }
        #endregion //GetStringParameters

        #region Filters
        /// <summary>
        /// 
        /// </summary>
        public FilterCollection Filters
        {
            get
            {
                if (_filters == null)
                {
                    _filters = new FilterCollection();
                }
                return _filters;
            }
            set
            {
                _filters = value;
            }
        } private FilterCollection _filters;

        #endregion //Filters


        #region IDevice ��Ա


        public string Remark
        {
            get
            {
                if (_remark == null)
                {
                    _remark = string.Empty;
                }
                return _remark;
            }
            set
            {
                _remark = value;
            }
        } private string _remark;

        #endregion
    }
}
