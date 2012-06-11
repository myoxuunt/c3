using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public interface IStationSource
    {
    }

    public interface IStationSourceProvider
    {
        IStationSource[] GetStationSources();
    }

    public interface IStation
    {
        string Name { get; set; }
        DeviceCollection Devices { get; set; }
        ICommuniPort CommuniPort { get; set; }
        IStationSource StationSource { get; set; }
    }

    public interface IStationPersister
    {
        void Add(IStation station);
        void Update(IStation station);
        void Delete(IStation station);
    }

    public interface IStationFactory
    {
        IStation Create(IStationSource stationSource);
    }

    public class StationCollection : Xdgk.Common.Collection<IStation>
    {

    }
}
