using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class DPUBase : IDPU 
    {

        #region Name
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
                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region DeviceType
        public Type DeviceType
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
        } private Type _deviceType;
        #endregion //DeviceType

        #region DeviceFactory
        public IDeviceFactory DeviceFactory
        {
            get
            {
                return _deviceFacory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DeviceFactory");
                }
                _deviceFacory = value;
            }
        } private IDeviceFactory _deviceFacory;
        #endregion //DeviceFactory

        #region DevicePersister
        public IDevicePersister DevicePersister
        {
            get
            {
                return _devicePersister;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DevicePersister");
                }
                _devicePersister = value;
            }
        } private IDevicePersister _devicePersister;
        #endregion //DevicePersister

        #region DeviceSourceProvider
        public IDeviceSourceProvider DeviceSourceProvider
        {
            get
            {
                return _deviceSourceProvider;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DeviceSourceProvider");
                }
                _deviceSourceProvider = value;
            }
        } private IDeviceSourceProvider _deviceSourceProvider;
        #endregion //DeviceSourceProvider

        #region Processor
        public ITaskProcessor Processor
        {
            get
            {
                return _taskProcessor;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Processor");
                }
                _taskProcessor = value;

            }
        } private ITaskProcessor _taskProcessor;
        #endregion //Processor

    }
}
