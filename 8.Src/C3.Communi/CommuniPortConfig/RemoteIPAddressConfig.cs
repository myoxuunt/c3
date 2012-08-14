using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace C3.Communi
{
    public interface INetCommuniPortConfig : ICommuniPortConfig
    {

    }

    public class RemoteIPAddressConfig : INetCommuniPortConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipaddress"></param>
        public RemoteIPAddressConfig(IPAddress ipAddress)
        {
            this.RemoteIPAddress = ipAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanCreate
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommuniPort Create()
        {
            string s = string.Format(
                "can not create ICommuniPort from '{0}'",
                this.GetType().Name
                );
            throw new NotSupportedException(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public bool IsMatch(ICommuniPort cp)
        {
            if (cp == null)
            {
                return false;
            }

            bool r = false;
            if (cp is SocketCommuniPort)
            {
                SocketCommuniPort scp = cp as SocketCommuniPort;

                EndPoint ep = scp.RemoteEndPoint;
                IPEndPoint ipep = ep as IPEndPoint;
                IPAddress ipAddress = ipep.Address;

                r = ipAddress.Equals(RemoteIPAddress);
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        public IPAddress RemoteIPAddress
        {
            get { return _remoteIPAddress; }
            set { _remoteIPAddress = value; }
        } private IPAddress _remoteIPAddress;

    }

    public class RemotePortConfig : INetCommuniPortConfig
    {
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

        #region ICommuniPortConfig 成员

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
    }

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

        #region ICommuniPortConfig 成员

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
