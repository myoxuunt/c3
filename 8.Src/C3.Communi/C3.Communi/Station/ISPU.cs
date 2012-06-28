
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ISPU
    {
        Type StationType {get;set;}
        IStationFactory StationFactory { get; set; }
        IStationPersister StationPersister { get; set; }
        IStationSourceProvider StationSourceProvider { get; set; }
    }

}
