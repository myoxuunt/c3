
using System;
using System.Threading;
using System.Diagnostics;
using NLog;


namespace C3.Communi
{
    public class CreateCommuniPortResultCollection : Xdgk.Common.LimitationCollection<CreateCommuniPortResult>
    {
        public CreateCommuniPortResultCollection()
        {
            this.MaxCount = 50;
        }
    }

}
