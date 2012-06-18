using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace C3.SPUTest
{
    public class TStation : IStation
    {
        #region IStation 成员

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DeviceCollection Devices
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICommuniPort CommuniPort
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IStationSource StationSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }

    abstract public class StationFactoryBase : IStationFactory
    {
        public IStation Create(IStationSource stationSource)
        {
            return OnCreate(stationSource);
        }

        abstract protected IStation OnCreate(IStationSource stationSource);

    }
    public class TStationFactory : IStationFactory
    {

        #region IStationFactory 成员

        public IStation Create(IStationSource stationSource)
        {
            return new TStation();
        }

        #endregion
    }

    abstract public class StationSourceProviderBase : IStationSourceProvider
    {

        #region IStationSourceProvider 成员

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
        #endregion

        abstract protected IStationSource[] OnGetStationSources();

    }

    public class TStationSourceProvider : IStationSourceProvider
    {

        #region IStationSourceProvider 成员

        public SourceConfigCollection SourceConfigs
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IStationSource[] GetStationSources()
        {
            return null;
        }

        #endregion
    }

    public class StationSourceBase : IStationSource
    {
        #region IStationSource 成员

        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        } private Guid _guid = Guid.Empty;

        #endregion
    }
    public class TStationSource : IStationSource
    {

        public string Name = "";
        public TStationSource(string name)
        {
            this.Name = name;
        }

        #region IStationSource 成员

        public Guid Guid
        {
            get
            {
                return new Guid();
            }
            set
            {
            }
        }

        #endregion
    }

    public class TStationPersister : IStationPersister
    {

        #region IStationPersister 成员

        public void Add(IStation station)
        {
            throw new NotImplementedException();
        }

        public void Update(IStation station)
        {
            throw new NotImplementedException();
        }

        public void Delete(IStation station)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class SPUBase : ISPU
    {

        #region ISPU 成员

        public Type StationType
        {
            get
            {
                return _stationType;
            }
            set
            {
                _stationType = value;
            }
        } private Type  _stationType;

        virtual public IStationFactory StationFactory
        {
            get
            {
                return _stationFactory;
            }
            set
            {
                _stationFactory = value;
            }
        } private IStationFactory _stationFactory;

        virtual public IStationPersister StationPersister
        {
            get
            {
                return _stationPersister;
            }
            set
            {
                _stationPersister = value;
            }
        } private IStationPersister _stationPersister;

        virtual public IStationSourceProvider StationSourceProvider
        {
            get
            {
                return _stationSourceProvider;
            }
            set
            {
                _stationSourceProvider = value;
            }
        } private IStationSourceProvider _stationSourceProvider;

        #endregion
    }
    public class Tspu : ISPU 
    {
        #region ISPU 成员

        public Type StationType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IStationFactory _stationFactory = new TStationFactory();
        public IStationFactory StationFactory
        {
            get
            {
                return _stationFactory;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IStationPersister _stationPersister = new TStationPersister();
        public IStationPersister StationPersister
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IStationSourceProvider StationSourceProvider
        {
            get
            {
                return new TStationSourceProvider();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
