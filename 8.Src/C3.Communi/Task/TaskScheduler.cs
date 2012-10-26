using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class TaskScheduler
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        public TaskScheduler(Soft soft)
        {
            this._soft = soft;
        }
        #endregion //Constructor

        #region Soft
        /// <summary>
        /// 
        /// </summary>
        public Soft Soft
        {
            get
            {
                return _soft;
            }
        } private Soft _soft;
        #endregion //Soft

        #region Hardware
        /// <summary>
        /// 
        /// </summary>
        private Hardware Hardware
        {
            get
            {
                return this.Soft.Hardware;
            }
        }
        #endregion //Hardware

        #region Doit
        /// <summary>
        /// 
        /// </summary>
        public void Doit()
        {
            foreach (IStation station in Hardware.Stations)
            {
                DoStation(station);
            }
        }
        #endregion //Doit

        #region DoStation station
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        private void DoStation(IStation station)
        {
            foreach (IDevice device in station.Devices)
            {
                DoDevice(device);
            }
        }
        #endregion //DoStation station

        #region DoDevice device
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        private void DoDevice(IDevice device)
        {
            // 1. current task
            //
            ITask current = device.TaskManager.Current;
            if (current != null)
            {
                DoTask(current);
            }
            else
            {
                DoTasks(device.TaskManager.Tasks);
            }
        }
        #endregion //DoDevice device

        #region DoTask task
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        private void DoTask(ITask current)
        {
            TaskStatus status = current.Check();

            switch (status)
            {
                case TaskStatus.Executing:
                    break;

                case TaskStatus.Timeout:
                    {
                        ICommuniPort cp = GetCommuniPort(current);
                        current.End(cp);
                        DoTask(current);
                    }
                    break;

                case TaskStatus.Executed:
                    {
                        IParseResult pr = current.LastParseResult;

                        ITaskProcessor processor = GetTaskProcessor(current);
                        processor.Process(current, pr);

                        DoTask(current);

                    }
                    break;

                case TaskStatus.Wating:
                    {
                        // clear current task
                        //
                        IDevice device = current.Device;
                        device.TaskManager.Current = null;

                        device.TaskManager.Tasks.Enqueue(current);
                    }
                    break;

                case TaskStatus.Completed:
                    {
                        // clear current task
                        //
                        IDevice device = current.Device;
                        device.TaskManager.Current = null;
                    }
                    break;


                default:
                    {
                        //
                        // clear
                        //
                        throw new NotSupportedException(status.ToString());
                    }
                    break;
            }
        }
        #endregion //DoTask task

        #region DoTasks queue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskCollection"></param>
        private void DoTasks(TaskQueue tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            TaskCollection tempTasks = new TaskCollection();
            //IDevice device = tasks;

            while (tasks.Count > 0)
            {
                ITask head = tasks.Dequeue();

                TaskStatus status = head.Check();

                if (status == TaskStatus.Ready)
                {
                    ICommuniPort cp = GetCommuniPort(head);
                    if ((cp != null) &&
                        (!cp.IsOccupy))
                    {

                        IDevice device = head.Device;
                        head.Begin(cp);

                        device.TaskManager.Current = head;
                        break;
                    }
                    else
                    {
                        tempTasks.Add(head);
                    }
                }
                else
                {
                    tempTasks.Add(head);
                }
            }
            tasks.Enqueue(tempTasks);
        }
        #endregion //DoTasks queue

        #region GetCommuniPort
        private ICommuniPort GetCommuniPort(ITask task)
        {
            IDevice device = task.Device;
            IStation station = device.Station;
            return station.CommuniPort;
        }
        #endregion //GetCommuniPort

        #region GetTaskProcessor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private ITaskProcessor GetTaskProcessor(ITask current)
        {
            IDevice device = current.Device;
            IDPU dpu = device.Dpu;
            ITaskProcessor processor = dpu.Processor;
            return processor;
        }
        #endregion //GetTaskProcessor
    }
}
