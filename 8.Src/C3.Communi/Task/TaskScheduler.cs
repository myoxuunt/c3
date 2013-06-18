using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class TaskScheduler
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

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

        #region Start
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
        #endregion //Start

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
                DoCurrentTask(current);
            }
            else
            {
                DoTasks(device.TaskManager.Tasks);
            }
        }
        #endregion //DoDevice device

        #region DoCurrentTask task
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        private void DoCurrentTask(ITask current)
        {
            TaskStatus status = current.Check();

            switch (status)
            {
                case TaskStatus.Ready:
                    {
                        ICommuniPort cp = GetCommuniPort(current);
                        if ((cp != null) &&
                            (!cp.IsOccupy))
                        //IsHighestLevel(headTask, cp))
                        {
                            current.Begin(cp);
                        }
                    }
                    break;

                case TaskStatus.Executing:
                    break;

                case TaskStatus.Timeout:
                    {
                        ICommuniPort cp = GetCommuniPort(current);
                        current.End(cp);
                        DoCurrentTask(current);
                    }
                    break;

                case TaskStatus.Executed:
                    {
                        IParseResult pr = current.LastParseResult;

                        ITaskProcessor processor = GetTaskProcessor(current);
                        processor.Process(current, pr);

                        DoCurrentTask(current);

                    }
                    break;

                case TaskStatus.Wating:
                    {
                        //ClearDeviceCurrentTask(current);

                        //IDevice device = current.Device;
                        //device.TaskManager.Tasks.Enqueue(current);

                        TaskManager taskMan = current.Device.TaskManager;
                        taskMan.ReleaseCurrent();
                    }
                    break;

                case TaskStatus.Completed:
                    {
                        // clear current task
                        //
                        // current may not equal to current.Device.TaskManager.Current
                        // so an other executing task will be remove
                        // 
                        //ClearDeviceCurrentTask(current);

                        TaskManager taskMan = current.Device.TaskManager;
                        taskMan.ReleaseCurrent();
                    }
                    break;


                default:
                    {
                        //
                        // clear
                        //
                        throw new NotSupportedException(status.ToString());
                    }
            }

#if DEBUG
            if (current.Strategy is CycleStrategy)
            {
                if (current.Device.TaskManager.Current == null)
                {
                    bool b = current.Device.TaskManager.Tasks.Contains(current);
                    Debug.Assert(b == true);
                }
            }
#endif
        }
        #endregion //DoCurrentTask task

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="current"></param>
        //private void ClearDeviceCurrentTask( ITask current )
        //{
        //    IDevice device = current.Device;
        //    if (current == device.TaskManager.Current)
        //    {
        //        device.TaskManager.Current = null;
        //    }
        //}
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

            bool find = false ;
            ITask forExecutingTask = null;
            TaskCollection tempTasks = new TaskCollection();

            while (tasks.Count > 0)
            {
                ITask headTask = tasks.Dequeue();

                find = CanExecutingTask(headTask);
                if (find)
                {
                    forExecutingTask = headTask;
                    break;
                }
                else
                {
                    tempTasks.Add(headTask);
                }
            }

            //
            //
            tasks.Enqueue(tempTasks);

            //
            //
            if (find)
            {
                ExecutingTask(forExecutingTask);
            }
        }

        private void ExecutingTask(ITask headTask)
        {
            ICommuniPort cp = GetCommuniPort(headTask);
            IDevice device = headTask.Device;
            headTask.Begin(cp);

            //device.TaskManager.Current = headTask;
            device.TaskManager.CaptureCurrent(headTask);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headTask"></param>
        /// <returns></returns>
        //private bool DoNotExecutingTask(ITask headTask)
        private bool CanExecutingTask(ITask headTask)
        {
            TaskStatus status = headTask.Check();

            if (status == TaskStatus.Ready)
            {
                ICommuniPort cp = GetCommuniPort(headTask);
                if ((cp != null) &&
                    (!cp.IsOccupy) &&
                    IsHighestLevel(headTask, cp))
                {

                    //IDevice device = headTask.Device;
                    //headTask.Begin(cp);

                    ////device.TaskManager.Current = headTask;
                    //device.TaskManager.CaptureCurrent(headTask);
                    return true;
                }
            }
            return false;
        }
        #endregion //DoTasks queue

        #region IsHighestLevel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="cp"></param>
        /// <returns></returns>
        private bool IsHighestLevel(ITask task, ICommuniPort cp)
        {
            bool r = false;
            StationCollection stations = GetStations(cp);

            TaskCollection tasks = GetNeedExecuteTasks(stations);
            r = IsHighestLevel(task, tasks);
            return r; 
        }
        #endregion //IsHighestLevel

        #region IsHighestLevel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private bool IsHighestLevel(ITask task, TaskCollection tasks)
        {
            bool r = true;
            foreach (ITask item in tasks)
            {
                if (task.LastExecute > item.LastExecute)
                {
                    r = false;
                    break;
                }
            }
            return r;
        }
        #endregion //IsHighestLevel

        #region GetNeedExecuteTasks
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stations"></param>
        /// <returns></returns>
        private TaskCollection GetNeedExecuteTasks(StationCollection stations)
        {
            TaskCollection r = new TaskCollection();
            foreach (IStation st in stations)
            {
                foreach (IDevice device in st.Devices)
                {
                    //Debug.Assert(device.TaskManager.Current == null);

                    foreach (ITask task in device.TaskManager.Tasks)
                    {
                        if (task.Check() == TaskStatus.Ready)
                        {
                            r.Add(task);
                        }
                    }
                }
            }
            return r;
        }
        #endregion //GetNeedExecuteTasks

        #region GetStations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        private StationCollection GetStations(ICommuniPort cp)
        {
            StationCollection r = new StationCollection();
            foreach (IStation st in Soft.Hardware.Stations)
            {
                if (st.CommuniPort == cp)
                {
                    r.Add(st);
                }
            }
            return r;
        }
        #endregion //GetStations

        #region GetCommuniPort
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
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
