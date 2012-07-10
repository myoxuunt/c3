using System;
using System.Collections.Generic;
using System.Text;
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
            protected set
            {
                if (_status != value)
                {
                    TaskStatus old = this._status;

                    _status = value;

                    if (_status == TaskStatus.Executed)
                    {
                        if (this.Stragegy.CanRemove)
                        {
                            _status = TaskStatus.Completed;
                        }
                        else
                        {
                            _status = TaskStatus.Wating;
                        }
                    }

                    string msg = string.Format("task changed: from '{0}' -> '{1}'", old, _status);
                    log.Debug(msg);                   

                }
            }
        } private TaskStatus _status;
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
                        this.Status = TaskStatus.Ready;
                    }
                    break;

                case TaskStatus.Ready:
                    break;

                case TaskStatus.Executing:
                    if (IsTimeOut())
                    {
                        Status = TaskStatus.Timeout;
                    }
                    break;

                case TaskStatus.Timeout:
                    break;

                case TaskStatus.Executed:
                    break;

                case TaskStatus.Completed:
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
                byte[] bytes = this.Opera.CreateSendBytes(this.Device);

                this.LastSendBytes = bytes;
                this.LastExecute = DateTime.Now;

                // TODO:
                //
                bool success = cp.Write(bytes);
                if (success)
                {
                    this.Status = TaskStatus.Executing;
                }
                else
                {
                    this.Status = TaskStatus.Executed;
                }
            }
            else
            {
                throw new InvalidOperationException("status must be 'Ready' when call Begin(...)");
            }
        }
        #endregion //Begin

        #region End
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        public void End(ICommuniPort cp)
        {
            if (this.Status == TaskStatus.Timeout)
            {
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

                // 
                //
                this.Status = TaskStatus.Executed;
            }
            else
            {
                throw new InvalidOperationException("status must be 'Timeout' when call End(...)");
            }
        }
        #endregion //End

    }

    /// <summary>
    /// 
    /// </summary>
    public class Task : TaskBase
    {
        public Task(IDevice device, IOpera opera, Strategy strategy, TimeSpan timeout)
        {
            this.Device = device;
            this.Opera = opera;
            this.Stragegy = strategy;
            this.Timeout = timeout;
        }
    }
}
