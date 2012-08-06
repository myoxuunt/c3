
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DeviceSourceProviderBase : IDeviceSourceProvider
    {

        public SourceConfigCollection SourceConfigs
        {
            get
            {
                if (_sourceConfigs == null)
                {
                    _sourceConfigs = new SourceConfigCollection();
                }
                return _sourceConfigs;
            }
            set
            {
                _sourceConfigs = value;
            }
        } private SourceConfigCollection _sourceConfigs;

        public IDeviceSource[] GetDeviceSources()
        {
            return OnGetDeviceSources();
        }

        abstract public IDeviceSource[] OnGetDeviceSources();

    }

}
