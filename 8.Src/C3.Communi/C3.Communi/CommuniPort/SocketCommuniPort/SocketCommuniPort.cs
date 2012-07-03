
using System;
using System.Net.Sockets;
using Xdgk.Common;

namespace C3.Communi
{
    public class SocketCommuniPort : CommuniPortBase
    {

        public Socket Socket
        {
            get { return _socket; }
        } private Socket _socket;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        public SocketCommuniPort(Socket socket)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }
            this._socket = socket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override byte[] OnRead()
        {
            return null;
        }

        protected override void OnClose()
        {
        }

        protected override bool OnWrite(byte[] bytes)
        {
            return false;
        }
    }

}
