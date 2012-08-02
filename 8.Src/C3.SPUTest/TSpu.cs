using System;
using C3.Communi;

namespace C3.SPUTest
{
    public class Tspu : SPUBase
    {
        public Tspu()
        {
            this.Name = "Tspu";
            this.Description = "T description";
            this.StationType = typeof(TStation);
            this.StationFactory = new TStationFactory();
            this.StationPersister = new TStationPersister();
            this.StationSourceProvider = new TStationSourceProvider();
        }
    }

    class TStationSource : StationSourceBase 
    {
    }
    public class GuidFactory
    {
        public static Guid Create(int n)
        {
            byte[] bs = BitConverter.GetBytes(n);
            byte[] bs16 = new byte[16];

            for (int i = bs.Length - 1; i >= 0; i--)
            {
                bs16[16 - bs.Length + i] = bs[i];
            }
            Guid id = new Guid(bs16);
            return id;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    class TStationSourceProvider : StationSourceProviderBase
    {
        protected override IStationSource[] OnGetStationSources()
        {
            TStationSource s = new TStationSource();
            s.Guid = GuidFactory.Create(11);
            TStationSource[] ss = new TStationSource[] {
                s
            };
            return ss;
        }
    }

}
