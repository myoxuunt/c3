
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class StationPersisterBase : IStationPersister 
    {


        public void Add(IStation station)
        {
            OnAdd(station);
        }


        public void Update(IStation station)
        {
            OnUpdate(station);
        }

        public void Delete(IStation station)
        {
            OnDelete(station);
        }

        abstract public void OnAdd(IStation station);
        abstract public void OnUpdate(IStation station);
        abstract public void OnDelete(IStation station);
    }

}
