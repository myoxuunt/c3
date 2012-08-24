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

        protected abstract void OnAdd(IDevice device);
        protected abstract void OnUpdate(IDevice device);
        protected abstract void OnDelete(IDevice device);
    }

}
