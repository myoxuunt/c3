
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ISPU
    {
        string Name { get; set; }
        string Description { get; set; }
        Type StationType {get;set;}
        IStationFactory StationFactory { get; set; }
        IStationPersister StationPersister { get; set; }
        IStationSourceProvider StationSourceProvider { get; set; }
    }

}
