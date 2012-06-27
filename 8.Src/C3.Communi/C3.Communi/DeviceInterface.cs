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



    public interface IParseResult
    {

    }


    public interface IOpera
    {
        byte[] CreateSend(IDevice device);
    }

    public interface ITask
    {
        IDevice Device { get; set; }

        bool IsTimeOut();
        TimeSpan TimeOut { get; set; }

        IParseResult Parse(byte[] received);
        bool NeedExecute(DateTime dt);
        DateTime LastExecute { get; set; }
        bool IsComplete { get; }

        IOpera Opera { get; set; }
    }

    public class TaskCollection : Collection<ITask>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class TaskQueue: Queue <ITask>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        public void Enqueue(TaskCollection tasks)
        {
            foreach (ITask item in tasks)
            {
                this.Enqueue(item);
            }
        }
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

        TaskQueue Tasks { get; set; }
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
