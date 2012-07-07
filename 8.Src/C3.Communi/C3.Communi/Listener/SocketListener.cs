using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class SocketListener : TcpListener
    {
        #region Members

        static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ConnectedEvent;
        #endregion //

        #region NewSocket
        /// <summary>
        /// 
        /// </summary>
        public Socket NewSocket
        {
            get { return _newSocket; }
        } private Socket _newSocket;
        #endregion //NewSocket

        #region IsListening
        /// <summary>
        /// 
        /// </summary>
        public bool IsListening
        {
            get { return this.Active; }
        }
        #endregion //IsListening

        #region SocketListener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public SocketListener(int port)
            : base(port)
        {
            //this._tcpListener = new TcpListener(port);
            //this._tcpListener.Start();
        }
        #endregion //SocketListener

        #region Start
        /// <summary>
        /// 
        /// </summary>
        public new void Start()
        {
            try
            {
                base.Start();
            }
            catch (SocketException socketEx)
            { 
                string msg = string.Format (
                    "listen port '{0}' fail", 
                    this.LocalEndpoint
                    );

                C3Exception c3ex = new C3Exception(msg, socketEx);
                throw c3ex;
            }
            this.BeginAcceptSocketHelper();
        }
        #endregion //Start

        #region BeginAcceptSocketHelper
        /// <summary>
        /// 
        /// </summary>
        private void BeginAcceptSocketHelper()
        {
            AsyncCallback callback = new AsyncCallback( this.BeginAcceptSocketCallback);
            // TODO: 2009-11-04 beginAcceptSocket exception
            //
            //try
            //{
                this.BeginAcceptSocket(
                    callback,
                    this);
            //}
            
        }
        #endregion //BeginAcceptSocketHelper

        #region BeginAcceptSocketCallback
        /// <summary>
        /// 
        /// </summary>
        private void BeginAcceptSocketCallback(IAsyncResult ar)
        {
            // TODO: ex at here 2009-11-04
            //

            Socket socket = null;
            try
            {
                socket = this.EndAcceptSocket(ar);
            }
            catch (Exception ex)
            {
                log.Error("EndAcceptSocket exception", ex);
            }

            if (socket != null)
            {
                this._newSocket = socket;
                if (ConnectedEvent != null)
                {
                    this.ConnectedEvent(this, EventArgs.Empty);
                }
            }

            this.BeginAcceptSocketHelper();
        }
        #endregion //BeginAcceptSocketCallback
    }

}
