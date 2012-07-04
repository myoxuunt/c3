
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class StationSourceProviderBase : IStationSourceProvider
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

        public IStationSource[] GetStationSources()
        {
            return OnGetStationSources();
        }

        abstract protected IStationSource[] OnGetStationSources();

    }

}
