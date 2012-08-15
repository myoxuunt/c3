
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;


namespace C3.Communi
{
    public class LocalPortConfig : INetCommuniPortConfig
    {
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
                r = this.LocalPort == ((IPEndPoint)scp.LocalEndPoint).Port;
            }
            return r;
        }

#endregion
    }

}
