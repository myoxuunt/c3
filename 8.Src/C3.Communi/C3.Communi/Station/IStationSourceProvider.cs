
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStationSourceProvider
    {
        SourceConfigCollection SourceConfigs { get; set; }
        IStationSource[] GetStationSources();
    }

}
