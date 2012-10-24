
using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStation :ITag 
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        string Text { get; set; }
        StationType StationType { get; set; }
        DeviceCollection Devices { get; set; }
        ICommuniPort CommuniPort { get; set; }
        ICommuniPortConfig CommuniPortConfig{ get; set; }
        IStationSource StationSource { get; set; }
        StationCollection Stations { get; set; }
        ISPU Spu { get; set; }
        event EventHandler CommuniPortChanged;
        GroupCollection Groups { get; }
        int Ordinal { get; set; }
    }

}
