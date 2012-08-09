using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace C3.SPUTest
{
    class TStationFactory: StationFactoryBase 
    {
        public TStationFactory(ISPU spu)
            : base(spu)
        {
        }

            int n = 0;
        protected override IStation OnCreate(IStationSource stationSource)
        {
            TStation station = new TStation();
            station.Name = "name from station factory:" + n++;
            station.Guid = stationSource.Guid;
            station.CommuniPortConfig = A;
            station.Spu = this.Spu;
            station.StationType = this.Spu.StationType;

            return station;
        }

        ICommuniPortConfig A
        {
            get
            {
                RemoteIPAddressConfig a = new RemoteIPAddressConfig();
                a.RemoteIPAddress = System.Net.IPAddress.Parse("127.0.0.1");
                return a;
            }
        }
    }
}
