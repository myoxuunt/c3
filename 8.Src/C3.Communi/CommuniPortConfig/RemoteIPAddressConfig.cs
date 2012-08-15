using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace C3.Communi
{

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


}
