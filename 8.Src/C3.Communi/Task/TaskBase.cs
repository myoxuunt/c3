using System;
using NLog;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskBase : ITask
    {

        /// <summary>
        /// 
        /// </summary>
        static private Logger log = LogManager.GetCurrentClassLogger();

        #region TaskBase
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategy"></param>
        public TaskBase()
        {
            //this.Stragegy = strategy;
        }
        #endregion //TaskBase

        #region TimeoutValues
        /// <summary>
        /// 
        /// </summary>
        static readonly public TimeSpan DefaultTimeout = TimeSpan.FromMilliseconds(10 * 1000);

        /// <summary>
        /// 
        /// </summary>
        static readonly public TimeSpan MaxTimeout = TimeSpan.FromMilliseconds(60 * 1000);

        /// <summary>
        /// 
        /// </summary>
        static readonly public TimeSpan MinTimeout = TimeSpan.FromMilliseconds(50);

        #endregion //

        #region Stragegy
        /// <summary>
        /// 
        /// </summary>
        public Strategy Stragegy
        {
            get { return _stragegy; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("stragegy can not null");
                }
                if (_stragegy != value)
                {
                    _stragegy = value;
                    value.Owning = this;
                }
            }
        } private Strategy _stragegy;
        #endregion //Stragegy

        #region Device
        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
            }
        } private IDevice _device;
        #endregion //Device

        #region LastParseResult
        /// <summary>
        /// 
        /// </summary>
        public IParseResult LastParseResult
        {
            get
            {
                return _lastParseResult;
            }
            set
            {
                _lastParseResult = value;
            }
        } private IParseResult _lastParseResult = null;
        #endregion //LastParseResult

        #region Timeout
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {

                if (value > MaxTimeout)
                {
                    _timeout = MaxTimeout;
                }
                else if (value < MinTimeout)
                {
                    _timeout = MinTimeout;
                }
                else
                {
                    _timeout = value;
                }
            }
        } private TimeSpan _timeout = DefaultTimeout;
        #endregion //Timeout

        #region Opera
        /// <summary>
        /// 
        /// </summary>
        public IOpera Opera
        {
            get
            {
                return _opera;
            }
            set
            {
                _opera = value;
            }
        } private IOpera _opera;
        #endregion //Opera

        #region Status
        /// <summary>
        /// 
        /// </summary>
        public TaskStatus Status
        {
            get { return _status; }
        } private TaskStatus _status;
        #endregion //

        #region SetStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void SetStatus( TaskStatus value )
        {
            if (_status != value)
            {
                TaskStatus old = this._status;

                _status = value;

                //if (_status == TaskStatus.Executed)
                //{
                //    if (this.Stragegy.CanRemove)
                //    {
                //        // TODO: 2012-09-01 
                //        //
                //        _status = TaskStatus.Completed;
                //    }
                //    else
                //    {
                //        _status = TaskStatus.Wating;
                //    }
                //}

                string msg = string.Format("task changed: from '{0}' -> '{1}'", old, _status);
                log.Debug(msg);

                OnStatusChanged(EventArgs.Empty);
            }
        } 
        #endregion //Status

        #region IsTimeOut
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsTimeOut()
        {
            TimeSpan ts = DateTime.Now - LastExecute;
            if (ts < TimeSpan.Zero || ts > Timeout)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion //IsTimeOut

        #region NeedExecute
        public bool NeedExecute()
        {
            return NeedExecute(DateTime.Now);
        }
        #endregion //NeedExecute

        #region NeedExecute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool NeedExecute(DateTime dt)
        {
            return this.Stragegy.NeedExecute(dt);
        }
        #endregion //NeedExecute

        #region Check
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TaskStatus Check()
        {
            switch (this.Status)
            {
                case TaskStatus.Wating:
                    if (NeedExecute())
                    {
                        this.SetStatus(TaskStatus.Ready);
                    }
                    break;

                case TaskStatus.Ready:
                    break;

                case TaskStatus.Executing:
                    if (IsTimeOut())
                    {
                        this.SetStatus(TaskStatus.Timeout);
                    }
                    break;

                case TaskStatus.Timeout:
                    this.SetStatus(TaskStatus.Executed);
                    break;

                case TaskStatus.Executed:
                    if (this.Stragegy.CanRemove)
                    {
                        this.SetStatus(TaskStatus.Completed);
                    }
                    else
                    {
                        this.SetStatus(TaskStatus.Wating);
                    }
                    break;

                case TaskStatus.Completed:
                    break;

                default:
                    {
                        string s = string.Format("exception status status '{0}'", this.Status);
                        throw new InvalidOperationException (s);
                    }
                    break;
            }
            return Status;
        }
        #endregion //Check

        #region LastExecute
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastExecute
        {
            get { return LastSendDateTime; }
            set { LastSendDateTime = value; }
        }
        #endregion //LastExecute

        #region LastSendBytes
        /// <summary>
        /// 
        /// </summary>
        public byte[] LastSendBytes
        {
            get { return _lastSendBytes; }
            set { _lastSendBytes = value; }
        } private byte[] _lastSendBytes;
        #endregion //LastSendBytes

        #region LastSendDateTime
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastSendDateTime
        {
            get
            {
                return _lastSendDateTime;
            }
            set
            {
                _lastSendDateTime = value;
            }
        } private DateTime _lastSendDateTime = DateTime.MinValue;
        #endregion //LastSendDateTime

        #region LastReceivedDateTime
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastReceivedDateTime
        {
            get { return _lastReceivedDateTime; }
            set { _lastReceivedDateTime = value; }
        } private DateTime _lastReceivedDateTime;
        #endregion //LastReceivedDateTime

        #region LastReceivedBytes
        /// <summary>
        /// 
        /// </summary>
        public byte[] LastReceivedBytes
        {
            get { return _lastReceivedBytes; }
            set { _lastReceivedBytes = value; }
        } private byte[] _lastReceivedBytes;
        #endregion //LastReceivedBytes

        #region Begin
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        public void Begin(ICommuniPort cp)
        {
            if (this.Status == TaskStatus.Ready)
            {
                OnBegining(EventArgs.Empty);

                
                byte[] bytes = this.Opera.CreateSendBytes(this.Device);

                this.LastSendBytes = bytes;
                this.LastExecute = DateTime.Now;

                bool success = cp.Write(bytes);
                if (success)
                {
                    cp.Occupy(this.Timeout);
                    this.SetStatus(TaskStatus.Executing);
                }
                else
                {
                    this.SetStatus(TaskStatus.Executed);
                }

                OnBegined(EventArgs.Empty);
            }
            else
            {
                throw new InvalidOperationException("status must be 'Ready' when call Begin(...)");
            }
        }
        #endregion //Begin

        #region OnBegining
        virtual protected void OnBegining(EventArgs eventArgs)
        {
            if (this.Begining != null)
            {
                this.Begining(this, eventArgs);
            }
        }
        #endregion //OnBegining

        #region OnBegined
        virtual protected void OnBegined(EventArgs eventArgs)
        {
            if (this.Begined != null)
            {
                this.Begined(this, eventArgs);
            }
        }
        #endregion //OnBegined

        #region End
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        public void End(ICommuniPort cp)
        {
            if (this.Status == TaskStatus.Timeout)
            {
                OnEnding(EventArgs.Empty);
                byte[] bytes = new byte[0];

                if (cp != null)
                {
                    bytes = cp.Read();
                }

                this.LastReceivedBytes = bytes;
                this.LastReceivedDateTime = DateTime.Now;

                IParseResult pr = this.Opera.ParseReceivedBytes(this.Device, bytes);
                this.LastParseResult = pr;


                //
                //
                CommuniDetail cd = new CommuniDetail(
                    this.Opera.Text,
                    LastSendBytes,
                    LastExecute,
                    LastReceivedBytes,
                    LastReceivedDateTime,
                    pr.ToString(),
                    pr.IsSuccess
                );
                this.Device.CommuniDetails.Add(cd);

                //CommuniLogger
                //this.device

                // 
                //
                // this.SetStatus(TaskStatus.Executed);

                OnEnded(EventArgs.Empty);
            }
            else
            {
                throw new InvalidOperationException("status must be 'Timeout' when call End(...)");
            }
        }
        #endregion //End

        #region OnEnding
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnEnding(EventArgs eventArgs)
        {
            if (Ending != null)
            {
                Ending(this, eventArgs);
            }
        }
        #endregion //OnEnding

        #region OnEnded
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnEnded(EventArgs eventArgs)
        {
            if (Ended != null)
            {
                Ended(this, eventArgs);
            }
        }
        #endregion //OnEnded

        #region OnStatusChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnStatusChanged(EventArgs eventArgs)
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, eventArgs);
            }
        }
        #endregion // OnStatusChanged

        #region Events

        public event EventHandler Begining;

        public event EventHandler Begined;

        public event EventHandler Ending;

        public event EventHandler Ended;

        public event EventHandler StatusChanged;

        #endregion //Events
    }
}
