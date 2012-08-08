
using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;


namespace C3.Communi
{
    public interface IDeviceUI
    {
        IDPU Dpu { get; set; }
        //IStation Station { get; }
        //IDevice Device { get; }
        DialogResult Add(DeviceType deviceType, IStation station, out IDevice newDevice);
        DialogResult Edit(IDevice device);

        // TODO: need delete(...)?
        //
        // void Delete(Device);
    }

}
