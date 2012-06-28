using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IDPU
    {
        Type DeviceType {get;set;}
        IDeviceFactory DeviceFactory { get; set; }
        IDevicePersister DevicePersister { get; set; }
        IDeviceSourceProvider DeviceSourceProvider { get; set; }
        ITaskProcessor Processor { get; set; }
    }

}
