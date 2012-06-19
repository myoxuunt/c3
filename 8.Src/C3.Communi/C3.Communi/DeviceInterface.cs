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
        public SourceConfig(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
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

    public interface IParseResult
    {

    }
    public interface ITask
    {
        IDevice Device { get; set; }
        bool IsTimeOut();

        IParseResult Parse(byte[] received);
        bool NeedExecute(DateTime dt);
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
        IDeviceData LastData{ get; set; }
        DeviceDataCollection DeviceDatas { get; }

        IDeviceSource DeviceSource { get; set; }

        TaskCollection Tasks { get; set; }
        ITask CurrentTask { get; set; }
        IDPU Dpu { get; set; }
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

    public interface ITaskProcessor
    {
        void Process(IParseResult pr);
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
        static private readonly int DEFAULT_CAPABILITY = 1000;
        static private readonly int MIN_CAPABILITY = 10;

        /// <summary>
        /// 
        /// </summary>
        public int Capability
        {
            get { return _capability; }
            set 
            {
                if (value < MIN_CAPABILITY)
                {
                    value = MIN_CAPABILITY;
                }
                _capability = value; 
            }
        } private int _capability = DEFAULT_CAPABILITY;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, IDeviceData item)
        {
            base.InsertItem(index, item);
            if (this.Count > this.Capability)
            {
                // TODO:
                //
                this.RemoveAt(0);
            }
        }
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
        ITaskProcessor Processor { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DPUCollection : Xdgk.Common.Collection<IDPU>
    {
    }
}
