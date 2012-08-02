
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class TaskProcessorBase : ITaskProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public void Process(ITask task, IParseResult pr)
        {
            OnProcess(task, pr);
        }

        abstract public void OnProcess(ITask task, IParseResult pr);

    }

}
