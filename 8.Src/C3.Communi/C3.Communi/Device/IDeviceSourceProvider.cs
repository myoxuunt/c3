
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IDeviceSourceProvider
    {
        SourceConfigCollection SourceConfigs { get; set; }
        IDeviceSource[] GetDeviceSources();
    }

}
