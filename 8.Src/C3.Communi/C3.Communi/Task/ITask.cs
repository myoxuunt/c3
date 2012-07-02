
using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public enum TaskStatus
    {
        Wating,
        Ready,
        Executing,
        Timeout,
        Executed,
        Completed,
    }

    public interface ITask
    {
        IDevice Device { get; set; }

        /*
        bool IsTimeOut();
        TimeSpan TimeOut { get; set; }

        IParseResult Parse(byte[] received);
        bool NeedExecute(DateTime dt);
        DateTime LastExecute { get; set; }
        bool IsComplete { get; }
        */


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
