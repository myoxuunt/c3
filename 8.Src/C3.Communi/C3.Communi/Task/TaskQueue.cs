
using System.Collections.Generic;

namespace C3.Communi
{
    public class TaskQueue: Queue <ITask>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        public void Enqueue(TaskCollection tasks)
        {
            foreach (ITask item in tasks)
            {
                this.Enqueue(item);
            }
        }
    }

}
