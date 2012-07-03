using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace C3.SPUTest
{
    public class Tspu : SPUBase
    {
        public Tspu()
        {
            this.StationType = typeof(TStation);
            this.StationFactory = new TStationFactory();
            this.StationPersister = new TStationPersister();
            this.StationSourceProvider = new TStationSourceProvider();
        }
    }

    class TStationSource : StationSourceBase 
    {
    }

    /// <summary>
    /// 
    /// </summary>
    class TStationSourceProvider : StationSourceProviderBase
    {
        protected override IStationSource[] OnGetStationSources()
        {
            TStationSource[] ss = new TStationSource[] {
                new TStationSource (),
                new TStationSource ()
            };
            return ss;
        }
    }

}
