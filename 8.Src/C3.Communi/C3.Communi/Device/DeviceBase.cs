using System;
using System.Threading;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
    /// <summary>
    ///
    /// </summary>
    abstract public class DeviceBase : IDevice
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        protected DeviceBase(string name, DeviceType deviceType, Int64 address)
        {
            this.Name = name;
            this.DeviceType = deviceType;
            this.Address = address;
        }

        #region Address
        /// <summary>
        /// 
        /// </summary>
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
    }
}
