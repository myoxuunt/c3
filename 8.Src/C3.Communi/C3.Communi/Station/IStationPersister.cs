
using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStationPersister
    {
        void Add(IStation station);
        void Update(IStation station);
        void Delete(IStation station);
    }


    /// <summary>
    /// 
    /// </summary>
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
