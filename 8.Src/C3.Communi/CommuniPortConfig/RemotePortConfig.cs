
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;


namespace C3.Communi
{
    public class RemotePortConfig : INetCommuniPortConfig
    {

        public RemotePortConfig()
            : this(0)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotePort"></param>
        public RemotePortConfig(int remotePort)
        {
            this.RemotePort = remotePort;
        }

        #region RemotePort
        /// <summary>
        /// 
        /// </summary>
        public int RemotePort
        {
            get
            {
                return _remotePort;
            }
            set
            {
                _remotePort = value;
            }
        } private int _remotePort;
        #endregion //RemotePort

        #region ICommuniPortConfig ≥…‘±

        public bool CanCreate
        {
            get { return false; }
        }

        public ICommuniPort Create()
        {
            throw new NotImplementedException();
        }

        public bool IsMatch(ICommuniPort cp)
        {
            bool r = false;
            SocketCommuniPort scp = cp as SocketCommuniPort;
            if (scp != null)
            {
                r = this.RemotePort == ((IPEndPoint)scp.RemoteEndPoint).Port;
            }
            return r;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public uint TimeoutMilliSecond
        {
            get
            {
                return _timeoutMillsSecond;
            }
            set
            {
                TimeoutDefauleValues.Verify(value);
                this._timeoutMillsSecond = value;
            }
        } private uint _timeoutMillsSecond = TimeoutDefauleValues.DefaultTimeoutMillsSecond;
    }

}
