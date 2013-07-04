using System;
using System.Threading;

namespace C3.Communi
{
    public class CPCreateLogCollection : Xdgk.Common.LimitationCollection<CPCreateLog>
    {
        public CPCreateLogCollection()
            : base(100)
        {

        }
    }
}
