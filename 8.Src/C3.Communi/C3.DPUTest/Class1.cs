using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace C3.DPUTest
{
    public class GuidFactory
    {
        public static Guid Create(int n)
        {
            byte[] bs = BitConverter.GetBytes(n);
            byte[] bs16 = new byte[16];

            for (int i = bs.Length - 1; i >= 0; i--)
            {
                bs16[16 - bs.Length + i] = bs[i];
            }
            Guid id = new Guid(bs16);
            return id;
        }
    }

    public class TDpu : DPUBase
    {
        public TDpu()
        {
            this.DeviceType = typeof(TDevice);
            this.DeviceFactory = new TDeviceFactory();
            this.DevicePersister = new TDevicePersister();
            this.DeviceSourceProvider = new TDeviceSourceProvider();
            this.Processor = new Processor();
        }
    }

    public class TDevice : DeviceBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class TDeviceFactory : DeviceFactoryBase
    {
        static private int n = 0;

        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            TDevice d = new TDevice();
            d.Address = 1;
            d.Name = "D" + n++;
            d.Guid = deviceSource.Guid;
            d.StationGuid = deviceSource.StationGuid;
            //d.Tasks = 
            return d;
        }
    }

    public class TDeviceSource : DeviceSourceBase
    {
    }

    public class TDeviceSourceProvider : DeviceSourceProviderBase
    {

        public override IDeviceSource[] OnGetDeviceSources()
        {
            TDeviceSource s = new TDeviceSource();
            s.Address = 123;
            s.DevcieTypeName = "Tdevice";
            s.Guid = GuidFactory.Create(01);
            s.StationGuid = GuidFactory.Create(11);

            return new IDeviceSource[] { s };

        }
    }

    public class TDevicePersister : DevicePersisterBase
    {

        public override void OnAdd(IDevice device)
        {
            throw new NotImplementedException();
        }

        public override void OnUpdate(IDevice device)
        {
            throw new NotImplementedException();
        }

        public override void OnDelete(IDevice device)
        {
            throw new NotImplementedException();
        }
    }

    public class Processor : TaskProcessorBase
    {

        public override void OnProcess(ITask task, IParseResult pr)
        {
            throw new NotImplementedException();
        }
    }


}
