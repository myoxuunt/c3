
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class StationSourceBase : IStationSource
    {
        #region IStationSource ��Ա

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
