
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DeviceSourceBase : IDeviceSource
    {

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

    }

}
