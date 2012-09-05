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
            private set
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
        public ExecuteResult Execute(IDevice device, string operaName, KeyValueCollection keyValues)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            if (keyValues == null)
            {
                keyValues = new KeyValueCollection();
            }

            if (!(device.Station.CommuniPort != null
                && device.Station.CommuniPort.IsOpened ))
            {
                return ExecuteResult.CreateFailExecuteResult("not connected");
            }

            IOpera opera = device.Dpu.OperaFactory.Create(device.GetType().Name,
                operaName);

            foreach (KeyValue kv in keyValues)
            {
                opera.SendPart[kv.Key] = kv.Value;
            }

            TimeSpan timeout = TimeSpan.FromMilliseconds(device.Station.CommuniPortConfig.TimeoutMilliSecond );
            this.Task = new Task(device, opera, Strategy.CreateImmediateStrategy(), timeout);

            device.TaskManager.Tasks.Enqueue(this.Task);

            return ExecuteResult.CreateSuccessExecuteResult();

        }
    }
}
