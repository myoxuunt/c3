
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class CommuniPortBase : ICommuniPort
    {

        #region Members
        private DateTime _occupyDateTime;
        private TimeSpan _occupyTimeSpan;
        #endregion //Members

        #region Events
        public event EventHandler Received;
        public event EventHandler Determined;
        public event EventHandler Closed;
        #endregion //Events

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void FireReceivedEvent(EventArgs e)
        {
            if (this.Received != null)
            {
                this.Received(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void FireDetermindEvent(EventArgs e)
        {
            if (this.Determined != null)
            {
                this.Determined(this, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void FireClosedEvent(EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, e);
            }
        }
        #endregion //Methods


        #region CommuniPortBase
        /// <summary>
        /// 
        /// </summary>
        public CommuniPortBase()
        {
            _createDateTime = DateTime.Now;
        }
        #endregion //CommuniPortBase

        #region CreateDateTime
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDateTime
        {
            get { return _createDateTime; }
        } private DateTime _createDateTime;
        #endregion //CreateDateTime

        #region Close
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            OnClose();
        }
        #endregion //Close

        #region OnClose
        /// <summary>
        /// 
        /// </summary>
        abstract protected void OnClose();
        #endregion //OnClose


        #region Write
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public bool Write(byte[] bytes)
        {
            return OnWrite(bytes);
        }
        #endregion //Write

        #region OnWrite
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        abstract protected bool OnWrite(byte[] bytes);
        #endregion //OnWrite


        #region Read
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] Read()
        {
            byte[] bs = OnRead();
            bs = this.Filters.Filtrate(bs);
            return bs;
        }
        #endregion //Read

        #region OnRead
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        abstract protected byte[] OnRead();
        #endregion //OnRead

        #region IsOccupy
        /// <summary>
        /// 
        /// </summary>
        public bool IsOccupy
        {
            get
            {
                TimeSpan ts = DateTime.Now - this._occupyDateTime;
                if (ts < TimeSpan.Zero || ts > _occupyTimeSpan)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion //IsOccupy

        #region Occupy
        public void Occupy(TimeSpan ts)
        {
            if (ts < TimeSpan.Zero)
            {
                ts = TimeSpan.Zero;
            }

            this._occupyDateTime = DateTime.Now;
            this._occupyTimeSpan = ts;
        }
        #endregion //Occupy


        #region Identity
        /// <summary>
        /// 
        /// </summary>
        public string Identity
        {
            get
            {
                if (_identity == null)
                {
                    _identity = string.Empty;
                }
                return _identity;
            }
            set
            {
                _identity = value;
            }
        } private string _identity;
        #endregion //Identity

        #region IdentityParsers
        /// <summary>
        /// 
        /// </summary>
        public IdentityParserCollection IdentityParsers
        {
            get
            {
                if (_identityParsers == null)
                {
                    _identityParsers = new IdentityParserCollection();
                }
                return _identityParsers;
            }
            set
            {
                _identityParsers = value;
            }
        } private IdentityParserCollection _identityParsers;
        #endregion //IdentityParsers

        #region Filters
        /// <summary>
        /// 
        /// </summary>
        public FilterCollection Filters
        {
            get
            {
                if (_filters == null)
                {
                    _filters = new FilterCollection();
                }
                return _filters;
            }
            set
            {
                _filters = value;
            }
        } private FilterCollection _filters;
        #endregion //Filters

        #region IsOpened
        /// <summary>
        /// 
        /// </summary>
        public bool IsOpened
        {
            get
            {
                return OnGetIsOpened();
            }
        }
        #endregion //IsOpened

        #region OnGetIsOpened
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        abstract protected bool OnGetIsOpened();
        #endregion //OnGetIsOpened
    }

}
