using System.Collections.Generic;
using Xdgk.Common;

namespace C3.Communi
{
    //public class TaskQueue: Queue <ITask>
    public class TaskQueue : Collection<ITask>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public void Enqueue(ITask task)
        {
            this.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        public void Enqueue(TaskCollection tasks)
        {
            foreach (ITask item in tasks)
            {
                //this.Enqueue(item);
                this.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITask Dequeue()
        {
            ITask task = this[0];
            this.RemoveAt(0);
            return task;
        }
    }

}
