
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DevicePersisterBase : IDevicePersister
    {


        public void Add(IDevice device)
        {
            OnAdd(device);
        }


        public void Update(IDevice device)
        {
            OnUpdate(device);
        }

        public void Delete(IDevice device)
        {
            OnDelete(device);
        }

        abstract public void OnAdd(IDevice device);
        abstract public void OnUpdate(IDevice device);
        abstract public void OnDelete(IDevice device);
    }

}
