
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ITask
    {
        IDevice Device { get; set; }

        bool IsTimeOut();
        TimeSpan TimeOut { get; set; }

        IParseResult Parse(byte[] received);
        bool NeedExecute(DateTime dt);
        DateTime LastExecute { get; set; }
        bool IsComplete { get; }

        IOpera Opera { get; set; }
    }

}
