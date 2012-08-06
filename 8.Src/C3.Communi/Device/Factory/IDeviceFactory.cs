
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IDeviceFactory
    {
        IDevice Create(IDeviceSource deviceSource);
        IDPU Dpu { get; set; }
    }

}
