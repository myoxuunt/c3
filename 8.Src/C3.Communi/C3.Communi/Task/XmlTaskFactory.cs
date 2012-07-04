
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class XmlTaskFactory : ITaskFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TaskCollection Create()
        {
            TaskCollection tasks = new TaskCollection();

            // TODO:
            //
            return tasks;
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
