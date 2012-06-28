
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class Hardware
    {
        public StationCollection Stations
        {
            get
            {
                if (_stations == null)
                {
                    _stations = new StationCollection();
                }
                return _stations;
            }
            set 
            { 
                _stations = value;
            }
        } private StationCollection _stations;
    }

}
