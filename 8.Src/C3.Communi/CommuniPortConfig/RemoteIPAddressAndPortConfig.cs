
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using NLog;


namespace C3.Communi
{
    public class RemoteIPAddressAndPortConfig : INetCommuniPortConfig
    {

        static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public ConnectionType ConnectionType
        {
            get { return _connectionType; }
            set { _connectionType = value; }
        } private ConnectionType _connectionType = ConnectionType.Client;

        /// <summary>
        /// 
        /// </summary>
        public RemoteIPAddressAndPortConfig()
            : this(IPAddress.Any, 0)
        {

        }

        public RemoteIPAddressAndPortConfig(IPAddress remoteIPAddress, int remotePort)
        {

        }

#region ICommuniPortConfig ≥…‘±

        /// <summary>
        /// 
        /// </summary>
        public bool CanCreate
        {
            get
            {
                return this._connectionType == ConnectionType.Client;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommuniPort Create()
        {
            if (this.ConnectionType == ConnectionType.Server)
            {

                string msg = string.Format(
                        "cannot create communiPort with ConnectionType '{0}'",
                        this.ConnectionType);
                throw new InvalidOperationException(msg);
            }

            Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

            SocketCommuniPort sckCP = null;

            try
            {
                socket.Connect(this.RemoteIPAddress, this.RemotePort);
                sckCP = new SocketCommuniPort(socket);
            }
            catch (SocketException sckEx)
            {
                log.Error("Create socket fail.\r\n" + sckEx);
            }
            catch (Exception ex)
            {
                log.Error("Create socket fail.\r\n" + ex);
                throw;
            }

            return sckCP;
        }

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

                r = ipAddress.Equals(this.RemoteIPAddress) &&
                    ipep.Port == this.RemotePort;
            }
            return r;
        }

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

#endregion

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
            public IPAddress RemoteIPAddress
            {
                get
                {
                    IPAddress remoteIPAddress = IPAddress.Parse(this.RemoteIPAddressString);
                    return remoteIPAddress;
                }
                set { this._remoteIPAddressString = value.ToString(); }
            }

        /// <summary>
        /// for serialize
        /// </summary>
        public string RemoteIPAddressString
        {
            get { return _remoteIPAddressString; }
            set { _remoteIPAddressString = value; }
        } private string _remoteIPAddressString;


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
    }

}
