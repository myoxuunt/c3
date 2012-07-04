
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IDevicePersister
    {
        void Add(IDevice device);
        void Update(IDevice device);
        void Delete(IDevice device);
    }


}
