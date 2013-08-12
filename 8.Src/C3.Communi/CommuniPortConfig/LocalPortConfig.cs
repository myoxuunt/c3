using System;
using System.Net;

namespace C3.Communi
{
    public class LocalPortConfig : CommuniPortConfigBase, INetCommuniPortConfig
    {
        public LocalPortConfig()
            : this(0)
        {
        }

        public LocalPortConfig(int localPort)
        {
            this.LocalPort = localPort;
        }

        #region LocalPort
        /// <summary>
        /// 
        /// </summary>
        public int LocalPort
        {
            get
            {
                return _localPort;
            }
            set
            {
                _localPort = value;
            }
        } private int _localPort;
        #endregion //LocalPort

        #region ICommuniPortConfig ≥…‘±

        override public bool CanCreate
        {
            get { return false; }
        }

        override public ICommuniPort Create()
        {
            string s = "Cannot create from LocalPortConfig";
            throw new NotSupportedException(s);
        }

        override public bool IsMatch(ICommuniPort cp)
        {
            bool r = false;
            SocketCommuniPort scp = cp as SocketCommuniPort;
            if (scp != null)
            {
                r = this.LocalPort == ((IPEndPoint)scp.LocalEndPoint).Port;
            }
            return r;
        }

        #endregion

        ///// <summary>
        ///// 
        ///// </summary>
        //public uint TimeoutMilliSecond
        //{
        //    get
        //    {
        //        return _timeoutMillsSecond;
        //    }
        //    set
        //    {
        //        TimeoutDefauleValues.Verify(value);
        //        this._timeoutMillsSecond = value;
        //    }
        //} private uint _timeoutMillsSecond = TimeoutDefauleValues.DefaultTimeoutMillsSecond;
    }

}
