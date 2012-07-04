
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ITaskProcessor
    {
        void Process( ITask task, IParseResult pr);
    }
}
