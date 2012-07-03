
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IStationSource
    {
        Guid Guid { get; set; }
    }



    public class StationSourceBase : IStationSource
    {
        #region IStationSource ≥…‘±

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
}
