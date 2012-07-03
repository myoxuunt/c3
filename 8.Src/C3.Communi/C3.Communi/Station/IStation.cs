
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStation
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        DeviceCollection Devices { get; set; }
        ICommuniPort CommuniPort { get; set; }
        ICommuniPortConfig CommuniPortConfig{ get; set; }
        IStationSource StationSource { get; set; }
        StationCollection Stations { get; set; }
    }

}
