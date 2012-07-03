
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class CommuniPortBase : ICommuniPort
    {

        public event EventHandler Received;
        public event EventHandler Determined;
        public event EventHandler Closed;


        /// <summary>
        /// 
        /// </summary>
        public CommuniPortBase()
        {
            _createDateTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDateTime
        {
            get { return _createDateTime; }
        } private DateTime _createDateTime;

        public void Close()
        {
            OnClose();
        }


        abstract protected void OnClose();

        public bool Write(byte[] bytes)
        {
            return OnWrite(bytes);
        }
        abstract protected bool OnWrite(byte[] bytes);


        public byte[] Read()
        {
            return OnRead();
        }
        abstract protected byte[] OnRead();

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
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Occupy(TimeSpan ts)
        {
            if (ts < TimeSpan.Zero)
            {
                ts = TimeSpan.Zero;
            }

            this._occupyDateTime = DateTime.Now;
            this._occupyTimeSpan = ts;
        }

        private DateTime _occupyDateTime;
        private TimeSpan _occupyTimeSpan;

        /// <summary>
        /// 
        /// </summary>
        public string Identity
        {
            get
            {
                if(_identity == null)
                {
                    _identity = string.Empty;
                }
                return _identity ;
            }
            set
            {
                _identity = value;
            }
        } private string _identity;

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
    }

}
