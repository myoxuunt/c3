using System;
using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    public class TaskDefineCollection : Collection<TaskDefine>
    {
        #region TaskFactoryNodeNames
        /// <summary>
        /// 
        /// </summary>
        internal class TaskFactoryNodeNames
        {
            public const string
                TaskFactoryCollection = "tasks",
                                      TaskFactory = "task",
                                      TaskOperaName = "operaname",
                                      StrategyType = "strategytype",
                                      CycleStrategy = "cyclestrategy",
                                      CycleValue = "value",
                                      DeviceType = "devicetype",
                                      Timeout = "timeout",
                                      RetryTimes = "retrytimes";
        }
        #endregion // TaskFactoryNodeNames

        #region LoadFromFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFromFile(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNode tasksNode = doc.SelectSingleNode(TaskFactoryNodeNames.TaskFactoryCollection);
            if (tasksNode != null)
            {
                //XmlAttribute att = tasksNode.Attributes[TaskFactoryNodeNames.Timeout];
                //if (att != null)
                //{
                //    TimeSpan taskDefaultTimeout = TimeSpan.Parse(att.Value);
                //    TaskDefine.DefaultTaskTimeout = taskDefaultTimeout;
                //}

                foreach (XmlNode node in tasksNode)
                {
                    switch (node.Name)
                    {
                        case TaskFactoryNodeNames.TaskFactory:
                            TaskDefine f = CreateTaskDefine(node);
                            this.Add(f);
                            break;
                    }
                }
            }
        }
        #endregion //LoadFromFile

        #region CreateTaskDefine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TaskDefine CreateTaskDefine(XmlNode taskDefineNode)
        {
            string devicetype = string.Empty;
            string opname = string.Empty;
            //TimeSpan timeout = TaskDefine.DefaultTaskTimeout;
            int retryTimes = 1;

            StrategyDefine strategyDefine = null;

            foreach (XmlNode node in taskDefineNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case TaskFactoryNodeNames.DeviceType:
                        devicetype = node.InnerText;
                        break;

                    case TaskFactoryNodeNames.TaskOperaName:
                        opname = node.InnerText;
                        break;

                    case TaskFactoryNodeNames.StrategyType:
                        {
                            string type = node.InnerText;
                            if (StringHelper.Equal(type, TaskFactoryNodeNames.CycleStrategy))
                            {
                                XmlNode n2 = taskDefineNode.SelectSingleNode(type);

                                strategyDefine = CreateCycleStrategyDefine(n2);
                            }
                        }
                        break;

                    case TaskFactoryNodeNames.RetryTimes:
                        retryTimes = int.Parse(node.InnerText);
                        break;

                    //case TaskFactoryNodeNames.Timeout:
                    //    timeout = TimeSpan.Parse(node.InnerText);
                    //    break;
                }
            }

            if (strategyDefine == null)
            {
                string errmsg = string.Format(
                        "cannot find stragegy, devicetype = '{0}', operaname = '{1}'",
                        devicetype, opname);
                throw new ConfigException(errmsg);
            }

            //return new TaskDefine(devicetype, opname, strategyDefine, timeout);
            return new TaskDefine(devicetype, opname, strategyDefine, retryTimes);
        }
        #endregion //CreateTaskDefine

        #region CreateCycleStrategyDefine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private CycleStrategyDefine CreateCycleStrategyDefine(XmlNode cyclenode)
        {
            if (cyclenode == null)
                throw new ArgumentNullException("cycleNode");

            XmlNode node = cyclenode.SelectSingleNode(TaskFactoryNodeNames.CycleValue);
            TimeSpan ts = TimeSpan.Parse(node.InnerText);
            return new CycleStrategyDefine(ts);
        }
        #endregion //CreateCycleStrategyDefine


        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationCollection"></param>
        /// <returns></returns>
        public TaskCollection Create(StationCollection stationCollection)
        {
            TaskCollection result = new TaskCollection();

            foreach (TaskDefine td in this)
            {
                TaskCollection tasks = td.Create(stationCollection);
                AddTasks(result, tasks);
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        private void AddTasks(TaskCollection to, TaskCollection from)
        {
            foreach (ITask t in from)
            {
                to.Add(t);
            }
        }
         */
    }

}
