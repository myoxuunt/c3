using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    abstract public class StationFactoryBase : IStationFactory
    {
        public IStation Create(IStationSource stationSource)
        {
            return OnCreate(stationSource);
        }

        abstract protected IStation OnCreate(IStationSource stationSource);

    }
}
