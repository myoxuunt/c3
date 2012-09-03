using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskExecutor
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Ended;

        /// <summary>
        /// 
        /// </summary>
        public ITask Task
        {
            get { return _task; }
            set
            {
                if (_task != value)
                {
                    if (_task != null)
                    {
                        _task.Ended -= new EventHandler(_task_Ended);
                    }

                    _task = value;

                    if (_task != null)
                    {
                        _task.Ended += new EventHandler(_task_Ended);
                    }
                }
            }
        }private ITask _task; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _task_Ended(object sender, EventArgs e)
        {
            if (this.Ended != null)
            {
                this.Ended(this, e);
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="operaName"></param>
        /// <param name="keyValues"></param>
        public void Execute(IDevice device, string operaName, KeyValueCollection keyValues)
        {
            IOpera opera = device.Dpu.OperaFactory.Create(device.GetType().Name,
                operaName);

            TimeSpan timeout = TimeSpan.FromMilliseconds(device.Station.CommuniPortConfig.TimeoutMilliSecond );
            this.Task = new Task(device, opera, Strategy.CreateImmediateStrategy(), timeout);

            device.TaskManager.Tasks.Enqueue(this.Task);

        }
    }
}
