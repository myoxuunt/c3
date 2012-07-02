
using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public enum TaskStatus
    {
        Wating,
        Ready,
        Executing,
        Timeout,
        Executed,
        Completed,
    }

    public interface ITask
    {
        IDevice Device { get; set; }

        /*
        bool IsTimeOut();
        TimeSpan TimeOut { get; set; }

        IParseResult Parse(byte[] received);
        bool NeedExecute(DateTime dt);
        DateTime LastExecute { get; set; }
        bool IsComplete { get; }
        */


        IParseResult LastParseResult { get; set; }
        DateTime LastExecute { get; set; }
        TimeSpan Timeout { get; set; }
        IOpera Opera { get; set; }

        TaskStatus Status { get; set; }
        TaskStatus Check();

        void Begin(ICommuniPort cp);
        void End(ICommuniPort cp);
    }

    /// <summary>
    /// 
    /// </summary>
    public class TaskBase : ITask
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategy"></param>
        public TaskBase(Strategy strategy)
        {
            this.Stragegy = strategy;
        }

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
                _stragegy = value;
            }
        } private Strategy _stragegy;

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

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastExecute
        {
            get
            {
                return _lastExecute;
            }
            set
            {
                _lastExecute = value;
            }
        } private DateTime _lastExecute = DateTime.MinValue;


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


        /// <summary>
        /// 
        /// </summary>
        public TaskStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                }
            }
        } private TaskStatus _status;

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

        public bool NeedExecute()
        {
            return NeedExecute(DateTime.Now);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool NeedExecute(DateTime dt)
        {
            return this.Stragegy.NeedExecute(dt);
        }

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

        /// <summary>
        /// 
        /// </summary>
        public byte[] LastSend
        {
            get { return _lastSend; }
            set { _lastSend = value; }
        } private byte[] _lastSend;


        /// <summary>
        /// 
        /// </summary>
        public DateTime LastReceivedDateTime
        {
            get { return _lastReceivedDateTime; }
            set { _lastReceivedDateTime = value; }
        } private DateTime _lastReceivedDateTime;

        /// <summary>
        /// 
        /// </summary>
        public byte[] LastReceivedBytes
        {
            get { return _lastReceivedBytes; }
            set { _lastReceivedBytes = value; }
        } private byte[] _lastReceivedBytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        public void Begin(ICommuniPort cp)
        {
            if (this.Status == TaskStatus.Ready)
            {
                byte[] bytes = this.Opera.CreateSendBytes(this.Device);

                this.LastSend = bytes;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        public void End(ICommuniPort cp)
        {
            if (this.Status == TaskStatus.Timeout)
            {
                byte[] bytes = cp.Read();

                this.LastReceivedBytes = bytes;
                this.LastReceivedDateTime = DateTime.Now;

                IParseResult pr = this.Opera.ParseReceivedBytes(this.Device, bytes);
                this.LastParseResult = pr;

                this.Status = TaskStatus.Executed;

                CommuniDetail cd = new CommuniDetail(
                    this.Opera.Text,
                    LastSend,
                    LastExecute,
                    LastReceivedBytes,
                    LastReceivedDateTime,
                    pr.ToString(),
                    pr.IsSuccess
                );

                this.Device.CommuniDetails.Add(cd);
            }
            else
            {
                throw new InvalidOperationException("status must be 'Timeout' when call End(...)");
            }
        }

    }
}
