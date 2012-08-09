
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStationFactory
    {
        ISPU Spu { get; set; }
        IStation Create(IStationSource stationSource);
    }

    

}
