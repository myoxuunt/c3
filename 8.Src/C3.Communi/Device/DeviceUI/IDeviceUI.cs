
using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;


namespace C3.Communi
{
    public interface IDeviceUI
    {
        //IStation Station { get; }
        //IDevice Device { get; }
        DialogResult Add(Type deviceType, IStation station, out IDevice newDevice);
        DialogResult Edit(IDevice device);

        // TODO: need delete(...)?
        //
        // void Delete(Device);
    }

}
