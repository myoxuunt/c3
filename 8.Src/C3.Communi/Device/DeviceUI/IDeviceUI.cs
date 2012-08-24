using System.Windows.Forms;

namespace C3.Communi
{
    public interface IDeviceUI
    {
        IDPU Dpu { get; set; }
        DialogResult Add(DeviceType deviceType, IStation station, out IDevice newDevice);
        DialogResult Edit(IDevice device);

    }

}
