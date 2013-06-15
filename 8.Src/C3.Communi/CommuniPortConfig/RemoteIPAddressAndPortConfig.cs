
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
            : this(remoteIPAddress, remotePort, ConnectionType.Client)
        {

        }

        public RemoteIPAddressAndPortConfig(IPAddress remoteIPAddress, int remotePort, ConnectionType connType)
        {
            this.RemoteIPAddress = remoteIPAddress;
            this.RemotePort = remotePort;
            this.ConnectionType = connType;
        }

        #region ICommuniPortConfig ≥…‘±

        /// <summary>
        /// 
        /// </summary>
        public bool CanCreate
        {
            get
            {
                return this._connectionType == ConnectionType.Client &&
                    InCreateDateTime();
            }
        }

        private bool InCreateDateTime()
        {
            TimeSpan ts = DateTime.Now - this.LastCreateDateTime;
            return ts < TimeSpan.Zero || ts >= this.CreateInterval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommuniPort Create()
        {
            if (!CanCreate)
            {
                throw new InvalidOperationException(string.Format("Cannot create communi port with '{0}'", this));
            }

            this.LastCreateDateTime = DateTime.Now;

            SocketCommuniPort sckCP = null;

            Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(this.RemoteIPAddress, this.RemotePort);
            sckCP = new SocketCommuniPort(socket);
            return sckCP;


            //try
            //{
            //    socket.Connect(this.RemoteIPAddress, this.RemotePort);
            //    sckCP = new SocketCommuniPort(socket);
            //}
            //catch (SocketException sckEx)
            //{
            //    log.Error("Create socket fail.\r\n" + sckEx);
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Create socket fail.\r\n" + ex);
            //    throw;
            //}

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

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastCreateDateTime
        {
            get { return _lastCreateDateTime; }
            set { _lastCreateDateTime = value; }
        } private DateTime _lastCreateDateTime = DateTime.MinValue;


        public TimeSpan CreateInterval
        {
            get { return _createIntervalSecond; }
            set 
            {
                if (value < MinCreateInterval)
                {
                    string msg = string.Format(
                        "CreateIntervalSecond must <= '{0}'", 
                        MinCreateInterval);
                    throw new ArgumentOutOfRangeException(msg);
                }
                _createIntervalSecond = value; 
            }
        } private TimeSpan  _createIntervalSecond = MinCreateInterval;

        static private readonly TimeSpan MinCreateInterval = TimeSpan.FromSeconds(5);
    }

}
