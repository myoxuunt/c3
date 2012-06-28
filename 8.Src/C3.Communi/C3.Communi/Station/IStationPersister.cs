
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStationPersister
    {
        void Add(IStation station);
        void Update(IStation station);
        void Delete(IStation station);
    }

}
