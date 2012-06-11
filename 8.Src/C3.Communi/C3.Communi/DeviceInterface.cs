using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public enum ElementType
    {
        Group,
        Station,
        Device,
    }

    public class SourceConfig
    {
        #region ElementType
        /// <summary>
        /// 
        /// </summary>
        public ElementType ElementType
        {
            get
            {
                return _elementType;
            }
            set
            {
                _elementType = value;
            }
        } private ElementType _elementType;
        #endregion //ElementType

        #region Key
        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = string.Empty;
                }
                return _key;
            }
            set
            {
                _key = value;
            }
        } private string _key;
        #endregion //Key

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get
            {
                if (_value == null)
                {
                    _value = string.Empty;
                }
                return _value;
            }
            set
            {
                _value = value;
            }
        } private string _value;
        #endregion //Value

    }

    public class SourceConfigCollection : Collection<SourceConfig>
    {
    }

    public interface ITask
    {

    }

    public class TaskCollection : Collection<ITask>
    {
    }

    public interface IDeviceSource
    {
         Guid StationGuid { get; set; }
         Guid Guid { get; set; }
    }

    public interface IDeviceSourceProvider
    {
        SourceConfigCollection SourceConfigs { get; set; }
        IDeviceSource[] GetDeviceSources();
    }

    public interface IDevice
    {
        string Name { get; set; }
        IStation Station { get; set; }
        IDeviceData Last{ get; set; }
        DeviceDataCollection DeviceDatas { get; }

        IDeviceSource DeviceSource { get; set; }

        TaskCollection Tasks { get; set; }
        ITask CurrentTask { get; set; }
    }

    public interface IDevicePersister
    {
        void Add(IDevice device);
        void Update(IDevice device);
        void Delete(IDevice device);
    }

    public interface IDeviceFactory
    {
        IDevice Create(IDeviceSource deviceSource);
    }

    public interface TaskProcessor
    {
        void Process(ITask task);
    }

    public interface IDeviceData
    {
        DateTime DT { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataCollection : Xdgk.Common.Collection<IDeviceData>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceCollection : Collection<IDevice>
    {
    }



    /// <summary>
    /// 
    /// </summary>
    public interface IDPU
    {
        Type DeviceType {get;set;}
        IDeviceFactory DeviceFactory { get; set; }
        IDevicePersister DevicePersister { get; set; }
        IDeviceSourceProvider DeviceSourceProvider { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DPUCollection : Xdgk.Common.Collection<IDPU>
    {
    }
}
