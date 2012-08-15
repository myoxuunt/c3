
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
}
