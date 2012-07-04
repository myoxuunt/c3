
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class XmlTaskFactory : ITaskFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskConfigPath"></param>
        public XmlTaskFactory(string taskConfigPath)
        {
            this.TaskConfigPath = taskConfigPath;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TaskConfigPath
        {
            get { return _taskConfigPath; }
            set { _taskConfigPath = value; }
        } private string _taskConfigPath;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TaskCollection Create(IDevice device)
        {
            TaskCollection tasks = new TaskCollection();

            foreach (TaskDefine td in TaskDefines)
            {
                bool b = StringHelper.Equal(td.DeviceType, device.GetType().Name);
                if (b)
                {
                    IOpera opera = this.OperaFactory.Create(td.OperaName);

                    Strategy strategy = td.StrategyDefine.Create();
                    Task t = new Task(device, opera, strategy, td.TimeOut);
                    device.Tasks.Enqueue(t);

                    tasks.Add(t);
                }
            }
            return tasks;
        }

        /// <summary>
        /// 
        /// </summary>
        TaskDefineCollection TaskDefines
        {
            get
            {
                if (_taskDefines == null)
                {
                    _taskDefines = new TaskDefineCollection();
                    _taskDefines.LoadFromFile(this.TaskConfigPath);
                }
                return _taskDefines;
            }
        } private TaskDefineCollection _taskDefines;

        /// <summary>
        /// 
        /// </summary>
        public IOperaFactory OperaFactory
        {
            get { return _operaFactory; }
            set { _operaFactory = value; }
        } private IOperaFactory _operaFactory;

    }

}
