
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IDevice
    {
        Int64 Address { get; set; }
        string Name { get; set; }
        IStation Station { get; set; }
        IDeviceData LastData{ get; set; }
        DeviceDataCollection DeviceDatas { get; }

        IDeviceSource DeviceSource { get; set; }

        TaskQueue Tasks { get; set; }
        ITask CurrentTask { get; set; }
        IDPU Dpu { get; set; }
        
    }

}
