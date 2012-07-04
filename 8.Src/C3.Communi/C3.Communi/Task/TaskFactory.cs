
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ITaskFactory
    {
        TaskCollection Create();
    }

    public class XmlTaskFactory : ITaskFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TaskCollection Create()
        {
            TaskCollection tasks = new TaskCollection();
        }

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
