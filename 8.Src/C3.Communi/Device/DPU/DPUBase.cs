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

        #region DeviceUI
        /// <summary>
        /// 
        /// </summary>
        public IDeviceUI DeviceUI
        {
            get
            {
                if (_deviceUI == null)
                {
                    _deviceUI = new DeviceUI(this);
                }
                return _deviceUI;
            }
            set
            {
                _deviceUI = value;
            }
        } private IDeviceUI _deviceUI;

        #endregion //DeviceUI

        #region TaskFactory
        /// <summary>
        /// 
        /// </summary>
        public ITaskFactory TaskFactory
        {
            get
            {
                return _taskFactory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("TaskFactory");
                }
                _taskFactory = value;
            }
        } private ITaskFactory _taskFactory;
        #endregion //TaskFactory

        #region OperaFactory
        /// <summary>
        /// 
        /// </summary>
        public IOperaFactory OperaFactory
        {
            get
            {
                return _operaFactory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("OperaFactory");
                }
                _operaFactory = value;
            }
        } private IOperaFactory _operaFactory;
        #endregion //OperaFactory


        /// <summary>
        /// 
        /// </summary>
        public IUIEntryFactory UIEntry
        {
            get { return _uiEntry; }
            set { _uiEntry = value; }
        } private IUIEntryFactory _uiEntry;
    }
}
