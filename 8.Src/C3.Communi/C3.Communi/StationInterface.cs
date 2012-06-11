using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStationSource
    {
        Guid Guid { get; set; }
    }

    public interface IStationSourceProvider
    {
        SourceConfigCollection SourceConfigs { get; set; }
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
        public IStation Find(Guid guid)
        {
            IStation r = null;
            foreach (IStation station in this)
            {
                IStationSource stationSource = station.StationSource;
                if (stationSource.Guid == guid)
                {
                    r = station;
                    break;
                }
            }

            return r;
        }
    }

    public interface ISPU
    {
        Type StationType {get;set;}
        IStationFactory StationFactory { get; set; }
        IStationPersister StationPersister { get; set; }
        IStationSourceProvider StationSourceProvider { get; set; }
    }

    public class ISPUCollection : Collection<ISPU>
    {
    }
}
