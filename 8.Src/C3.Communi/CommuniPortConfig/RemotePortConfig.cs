using System;
using System.Net;


namespace C3.Communi
{
    public class RemotePortConfig : CommuniPortConfigBase, INetCommuniPortConfig
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

        override public bool CanCreate
        {
            get { return false; }
        }

        override public ICommuniPort Create()
        {
            const string s = "Cannot create from RemotePortConfig";
            throw new NotSupportedException(s);
        }

        override public bool IsMatch(ICommuniPort cp)
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
    }
}
