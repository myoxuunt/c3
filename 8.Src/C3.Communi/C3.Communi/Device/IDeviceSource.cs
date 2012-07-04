
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IDeviceSource
    {
        Guid StationGuid { get; set; }
        Guid Guid { get; set; }
    }


}
