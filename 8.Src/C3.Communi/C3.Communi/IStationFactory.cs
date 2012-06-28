
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStationFactory
    {
        IStation Create(IStationSource stationSource);
    }

}
