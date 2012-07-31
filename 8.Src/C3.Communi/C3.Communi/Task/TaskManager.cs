using System;
using System.Threading;
using System.Diagnostics;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskManager
    {
        #region Tasks
        /// <summary>
        /// 
        /// </summary>
        public TaskQueue Tasks
        {
            get
            {
                if (_tasks == null)
                {
                    _tasks = new TaskQueue();
                }
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        } private TaskQueue _tasks;
        #endregion //Tasks

        #region Current
        /// <summary>
        /// 
        /// </summary>
        public ITask Current
        {
            get
            {
                return _current;
            }
            set
            {
                if (_current != value)
                {
                    if (value != null)
                    {
                        VerifyTaskStatus(value, TaskStatus.Executing);
                    }

                    // UnregisterEvents
                    //
                    UnregisterEvents(_current);

                    //
                    //
                    _current = value;

                    //
                    //
                    RegisterEvents(_current);

                    if (Soft.IsUseUISynchronizationContext)
                    {
                        Soft.Post(this.CurrentChangedCallback, EventArgs.Empty);
                    }
                    else
                    {
                        OnCurrentChanged(EventArgs.Empty);
                    }
                }
            }
        } private ITask _current;
        #endregion //Current

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_current"></param>
        private void RegisterEvents(ITask _current)
        {
            if (_current != null)
            {
                _current.StatusChanged += new EventHandler(_current_StatusChanged);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_current"></param>
        private void UnregisterEvents(ITask _current)
        {
            if (_current != null)
            {
                _current.StatusChanged -= new EventHandler(_current_StatusChanged);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _current_StatusChanged(object sender, EventArgs e)
        {
            if (Soft.IsUseUISynchronizationContext)
            {
                Soft.Send(CurrentStatusChangedCallback, EventArgs.Empty);
            }
            else
            {
                OnCurrentStatusChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private SendOrPostCallback CurrentStatusChangedCallback
        {
            get
            {
                if (_currentStatusChangedCallback == null)
                {
                    _currentStatusChangedCallback = new SendOrPostCallback(this.OnCurrentStatusChanged);
                }
                return _currentStatusChangedCallback;
            }
        } private SendOrPostCallback _currentStatusChangedCallback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        private void OnCurrentStatusChanged(object o)
        {
            EventArgs e = (EventArgs)o;
            OnCurrentStatusChanged(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnCurrentStatusChanged(EventArgs e)
        {
            if (this.CurrentStatusChanged != null)
            {
                this.CurrentStatusChanged(this, e);
            }
        }

        #region CurrentChangedCallback
        /// <summary>
        /// 
        /// </summary>
        private SendOrPostCallback CurrentChangedCallback
        {
            get
            {
                if (_currentChangedCallback==null)
                {
                    _currentChangedCallback = new SendOrPostCallback(this.OnCurrentChanged);
                }
                return _currentChangedCallback;
            }
        } private SendOrPostCallback _currentChangedCallback;
        #endregion //CurrentChangedCallback

        #region OnCurrentChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        private void OnCurrentChanged(object o)
        {
            EventArgs e = (EventArgs)o;
            OnCurrentChanged(e);
        }
        #endregion //OnCurrentChanged

        #region OnCurrentChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnCurrentChanged(EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, e);
            }
        }
        #endregion //OnCurrentChanged

        #region VerifyTaskStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private static void VerifyTaskStatus(ITask value, TaskStatus status)
        {
            // check task status
            //
            if (value.Status != status)
            {
                string msg = string.Format(
                        "task status must is {0}, but is '{1}'",
                        status, value.Status);

                throw new ArgumentException(msg);
            }
        }
        #endregion //VerifyTaskStatus

        #region events
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CurrentChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CurrentStatusChanged;
        #endregion //events
    }

}
