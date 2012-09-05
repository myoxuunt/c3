using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using C3.Communi;
using Xdgk.GR.UI;
//using ControllerIn;

namespace XD1100DPU
{
//    public class LocalController : IExecuteController
//    {

//        public LocalController(IDevice d)
//        {
//            this._device = d;
//        }

//        public IDevice Device
//        {
//            get { return _device; }
//        } private IDevice _device;

//        private _controllerStatus _status;

//        #region IExecuteController 成员

//        public _controllerStatus ControllerStatus
//        {
//            get
//            {
//                return _status;
//            }
//            set
//            {
//                _status = value;
//            }
//        }

//        public bool CanExecute()
//        {
//            return _status == _controllerStatus.None;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="pb"></param>
//        /// <returns></returns>
//        public bool Execute(ParameterBag pb)
//        {
//            //Guid guid = GuidHelper.Create(pb.DeviceID);
//            //Soft soft = SoftManager.GetSoft();
//            //IDevice device = soft.Hardware.FindDevice(pb.DeviceID);
//            //if (device == null)
//            //{
//            //    return false;
//            //}

//            IOpera opera = CreateOpera(pb.OperaName);
//            Task task = new Task(this.Device, opera, new ImmediateStrategy(), TimeSpan.FromSeconds(5));
//            this.Task = task;
//            this.Device.TaskManager.Tasks.Enqueue(task);
//            return true;
//        }

//        #region CreateOpera
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private IOpera CreateOpera(string operaName)
//        {
//            IOpera opera = this.Device.Dpu.OperaFactory.Create(this.Device.GetType().Name, operaName);
//            return opera;
//        }
//        #endregion //CreateOpera

//        public event EventHandler Executing;

//        public event EventHandler Executed;

//        #endregion

//        /// <summary>
//        /// 
//        /// </summary>
//        private ITask Task
//        {
//            get { return _task; }
//            set
//            {
//                if (_task != value)
//                {
//                    if (_task != null)
//                    {
//                        _task.Ended -= new EventHandler(_task_Ended);
//                    }

//                    _task = value;

//                    if (_task != null)
//                    {
//                        _task.Ended += new EventHandler(_task_Ended);
//                    }
//                }
//            }
//        } private ITask _task;

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        void _task_Ended(object sender, EventArgs e)
//        {
//            string s = "ended " + this.Task.Opera.Name;
//            NUnit.UiKit.UserMessage.Display(s);
//            //// TODO: check is this task
//            ////
//            //switch (this._state)
//            //{
//            //    case State.Read:
//            //        ProcessReadMode();
//            //        break;

//            //    //case State.ReadLine:
//            //    //    ProcessReadLine(e);
//            //    //    break;

//            //    case State.Write:
//            //        //ProcessWriteMode(e);
//            //        break;

//            //    //case State.WriteLine :
//            //    //    ProcessWriteLine(e);
//            //    //    break;

//            //    default:
//            //        break;
//            //}
//        }
//    }


//    /// <summary>
//    /// 
//    /// </summary>
//    public class RemoteController : IExecuteController
//    {
//        int _deviceID;
//        private ITask Task
//        {
//            get { return _task; }
//            set
//            {
//                if (_task != value)
//                {
//                    if (_task != null)
//                    {
//                        //_task.Ended -= new EventHandler(_task_Ended);
//                    }

//                    _task = value;

//                    if (_task != null)
//                    {
//                        //_task.Ended += new EventHandler(_task_Ended);
//                    }
//                }
//            }
//        } private ITask _task;
//        public IDevice Device
//        {
//            get { return _device; }
//        } private IDevice _device;

//        #region IExecuteController 成员

//        _controllerStatus _status;

//        public _controllerStatus ControllerStatus
//        {
//            get
//            {
//                return _status;
//            }
//            set
//            {
//                _status = value;
//            }
//        }

//        public bool CanExecute()
//        {
//            return _status == _controllerStatus.None;
//        }

//        public bool Execute(ParameterBag pb)
//        {
//            IDevice device = SoftManager.GetSoft().Hardware.FindDevice(pb.DeviceID);
//            if (device == null)
//            {
//                return false;
//            }

//            if (device.Station.CommuniPort == null)
//            {
//                return false;
//            }

//            IOpera opera = CreateOpera(pb.OperaName);
//            Task task = new Task(this.Device, opera, new ImmediateStrategy(), TimeSpan.FromSeconds(5));
//            this.Task = task;
//            this.Device.TaskManager.Tasks.Enqueue(task);
//            return true;

//        }

//        #region CreateOpera
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private IOpera CreateOpera(string operaName)
//        {
//            IOpera opera = this.Device.Dpu.OperaFactory.Create(this.Device.GetType().Name, operaName);
//            return opera;
//        }
//        #endregion //CreateOpera
//        public event EventHandler Executing;

//        public event EventHandler Executed;

//        #endregion
//    }
}
