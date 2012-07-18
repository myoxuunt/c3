using System;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
    abstract public class DeviceBase : IDevice
    {
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

        #region LastData
        /// <summary>
        /// 
        /// </summary>
        public IDeviceData LastData
        {
            get
            {
                return _lastData;
            }
            set
            {
                _lastData = value;
            }
        } private IDeviceData _lastData;
        #endregion //LastData

        #region DeviceDatas
        /// <summary>
        /// 
        /// </summary>
        public DeviceDataCollection DeviceDatas
        {
            get
            {
                if (_deviceDatas == null)
                {
                    _deviceDatas = new DeviceDataCollection();
                }
                return _deviceDatas;
            }
        } private DeviceDataCollection _deviceDatas;
        #endregion //DeviceDatas

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

        #region Tasks
        /// <summary>
        /// 
        /// </summary>
        public TaskQueue Tasks
        {
            get
            {
                if (_tasks == null)
                {
                    _tasks = new TaskQueue();
                }
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        } private TaskQueue _tasks;
        #endregion //Tasks

        #region CurrentTask
        /// <summary>
        /// 
        /// </summary>
        public ITask CurrentTask
        {
            get
            {
                return _currentTask;
            }
            set
            {
                if (_currentTask != value)
                {
                    if (value != null)
                    {
                        // check task status
                        //
                        if (value.Status != TaskStatus.Executing)
                        {

                            string msg = string.Format(
                                "task status must is Executing, but is '{0}'", 
                                value.Status);

                            throw new ArgumentException(msg);
                        }
                        // Debug.Assert(value.Status == TaskStatus.Executing);
                    }

                    _currentTask = value;
                }
            }
        } private ITask _currentTask;
        #endregion //CurrentTask

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
                    if (this.Name.Length > 0)
                    {
                        _text = string.Format("{0}({1})", this.Name, this.GetType().Name);
                    }
                    else
                    {
                        _text = string.Format("({0})", this.GetType().Name);
                    }
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
    }
}