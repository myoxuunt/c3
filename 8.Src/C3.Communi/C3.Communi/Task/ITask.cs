
using System;
using Xdgk.Common;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public interface ITask
    {
        IDevice Device { get; set; }

        IParseResult LastParseResult { get; set; }
        DateTime LastExecute { get; set; }
        TimeSpan Timeout { get; set; }
        IOpera Opera { get; set; }

        TaskStatus Status { get; set; }
        TaskStatus Check();

        void Begin(ICommuniPort cp);
        void End(ICommuniPort cp);
    }
}
