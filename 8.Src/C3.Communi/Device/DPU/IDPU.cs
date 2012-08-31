using System;
using Xdgk.Common;
using System.Windows.Forms;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIEntry
    {
        void Set(ISelectedHardwareItem sel, ToolStripMenuItem parentMenuItem);
    }

    public interface IDPU
    {
        string Name { get; set; }
        DeviceType DeviceType {get;set;}
        IDeviceFactory DeviceFactory { get; set; }
        IDevicePersister DevicePersister { get; set; }
        IDeviceSourceProvider DeviceSourceProvider { get; set; }
        IOperaFactory OperaFactory { get; set; }
        ITaskFactory TaskFactory { get; set; }
        ITaskProcessor Processor { get; set; }
        IDeviceUI DeviceUI { get; set; }
        IUIEntry UIEntry { get; set; }
    }

}
