using System;
using C3.Communi;
using Xdgk.Common;

namespace C3.SPUTest
{
    public class Tspu : SPUBase
    {
        public Tspu()
        {
            StationTypeManager.AddStationType("Tspu",  typeof(TStation));

            this.Name = "Tspu";
            this.Description = "T description";
            this.StationType = StationTypeManager.GetStationType("Tspu");
            this.StationFactory = new TStationFactory(this);
            this.StationPersister = new TStationPersister();
            this.StationSourceProvider = new TStationSourceProvider();
            this.StationUI = new StationUI(this);
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
            TStationSource s = new TStationSource();
            s.Guid = GuidHelper.Create(11);
            TStationSource s2 = new TStationSource();
            s2.Guid = GuidHelper.Create(12);
            TStationSource[] ss = new TStationSource[] {
                s,s2
            };
            return ss;
        }
    }

}
