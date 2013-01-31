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
        Strategy Strategy { get; set; }
        IParseResult LastParseResult { get; set; }
        DateTime LastExecute { get; set; }

        byte[] LastSendBytes { get; set; }
        byte[] LastReceivedBytes { get; set; }

        TimeSpan Timeout { get; set; }
        IOpera Opera { get; set; }

        Retry Retry { get; set; }

        TaskStatus Status { get; /*set;*/ }
        TaskStatus Check();
        void Begin(ICommuniPort cp);
        void End(ICommuniPort cp);


        event EventHandler Begining;
        event EventHandler Begined;

        event EventHandler Ending;
        event EventHandler Ended;

        event EventHandler StatusChanged;
    }
}
