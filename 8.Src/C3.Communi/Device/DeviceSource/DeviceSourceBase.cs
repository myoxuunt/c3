
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DeviceSourceBase : IDeviceSource
    {

        #region StationGuid
        /// <summary>
        /// 
        /// </summary>
        public Guid StationGuid
        {
            get
            {
                return _stationGuid;
            }
            set
            {
                _stationGuid = value;
            }
        } private Guid _stationGuid;
        #endregion //StationGuid

        #region Guid
        /// <summary>
        /// 
        /// </summary>
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        } private Guid _guid;
        #endregion //Guid

        #region Address
        /// <summary>
        /// 
        /// </summary>
        public UInt64 Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        } private UInt64 _address;
        #endregion //Address

        #region DevcieTypeName
        /// <summary>
        /// 
        /// </summary>
        public string DevcieTypeName
        {
            get
            {
                if (_devcieTypeName == null)
                {
                    _devcieTypeName = string.Empty;
                }
                return _devcieTypeName;
            }
            set
            {
                _devcieTypeName = value;
            }
        } private string _devcieTypeName;
        #endregion //DevcieTypeName

        #region DeviceName
        /// <summary>
        /// 
        /// </summary>
        public string DeviceName
        {
            get
            {
                if (_deviceName == null)
                {
                    _deviceName = string.Empty;
                }
                return _deviceName;
            }
            set
            {
                _deviceName = value;
            }
        } private string _deviceName;
        #endregion //DeviceName

        #region DeviceExtendParameters
        /// <summary>
        /// 
        /// </summary>
        public string DeviceExtendParameters
        {
            get
            {
                if (_deviceExtendParameters == null)
                {
                    _deviceExtendParameters = string.Empty;
                }
                return _deviceExtendParameters;
            }
            set
            {
                _deviceExtendParameters = value;
            }
        } private string _deviceExtendParameters;
        #endregion //DeviceExtendParameters

    }

}
