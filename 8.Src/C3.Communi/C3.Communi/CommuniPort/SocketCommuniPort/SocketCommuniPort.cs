using System;
using System.Net;
using System.Net.Sockets;
using Xdgk.Common;
using NLog;

namespace C3.Communi
{
    public class SocketCommuniPort : CommuniPortBase
    {

        #region Members
        /// <summary>
        /// 
        /// </summary>
        private const int BUFFER_SIZE = 1024 * 2;

        /// <summary>
        /// 
        /// </summary>
        private byte[] _receBuffer = new byte[BUFFER_SIZE];
        static Logger log = LogManager.GetCurrentClassLogger();
        private System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream();
        #endregion //Members

        #region SocketCommuniPort
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
            if (!socket.Connected)
            {
                throw new ArgumentException("socket not connected");
            }

            this._socket = socket;
            this.LocalEndPoint = this._socket.LocalEndPoint;
            this.RemoteEndPoint = this._socket.RemoteEndPoint;
            //IPEndPoint ipep = this._socket.LocalEndPoint;

            //this.CommuniPortState = new SocketCommuniPortConnectedState(this);
            BeginReceiveHelper();
        }
        #endregion //SocketCommuniPort

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override byte[] OnRead()
        {
            byte[] bs = _memoryStream.ToArray();
            _memoryStream.SetLength(0);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnClose()
        {
            this.CloseHelper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected override bool OnWrite(byte[] bytes)
        {
            try
            {
                this._socket.Send(bytes);
                return true;
            }
            catch (SocketException sckEx)
            {
                OnException(sckEx);
                CloseHelper();
            }
            catch (ObjectDisposedException odEx)
            {
                OnException(odEx);
            }
            catch
            {
                // argument null exception
                //
                throw;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool OnGetIsOpened()
        {
            return !_isClosed;
        }

        void OnException(Exception ex)
        {

        }
        #endregion //Methods

        #region Properties


        #region LocalEndPoint
        /// <summary>
        /// 
        /// </summary>
        public EndPoint LocalEndPoint
        {
            get { return _localEndPoint; }
            set { _localEndPoint = CloneEndPoint(value); }
        } private EndPoint _localEndPoint;
        #endregion //LocalEndPoint

        #region RemoteEndPoint
        /// <summary>
        /// 
        /// </summary>
        public EndPoint RemoteEndPoint
        {
            get { return _remoteEndPoint; }
            set { _remoteEndPoint = CloneEndPoint(value); }
        } private EndPoint _remoteEndPoint;
        #endregion //RemoteEndPoint

        #region CloneEndPoint
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ep"></param>
        /// <returns></returns>
        static private EndPoint CloneEndPoint(EndPoint ep)
        {
            if (ep == null)
                throw new ArgumentNullException("ep");

            IPEndPoint ipep = ep as IPEndPoint;
            if (ipep == null)
                throw new ArgumentException("ep is not IPEndPoint");

            IPEndPoint n = new IPEndPoint(ipep.Address.Address, ipep.Port);
            return n;
        }
        #endregion //CloneEndPoint

        #region BeginReceiveHelper
        /// <summary>
        /// 
        /// </summary>
        public void BeginReceiveHelper()
        {
            try
            {
                // start apm receive
                // 
                // TODO: exception handler
                //
                AsyncCallback cb = this.ReceiveCallback;
                IAsyncResult ia = _socket.BeginReceive(_receBuffer, 0, BUFFER_SIZE,
                        SocketFlags.None, cb, _socket);
            }
            catch (Exception ex)
            {
                //log.Error("SocketCommuniPort.BeginReceiveHelper exception:", ex);
                throw ex;
            }
        }
        #endregion //BeginReceiveHelper

        #region ReceiveCallback
        /// <summary>
        /// 
        /// </summary>
        private AsyncCallback ReceiveCallback
        {
            get
            {
                if (_receiveCallback == null)
                    _receiveCallback = new AsyncCallback(BeginReceiveCallback);
                return _receiveCallback;
            }
        } private AsyncCallback _receiveCallback;
        #endregion //ReceiveCallback

        #region BeginReveiveCallback
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ia"></param>
        private void BeginReceiveCallback(IAsyncResult ia)
        {
            Socket sck = ia.AsyncState as Socket;
            int n = 0;
            try
            {
                n = sck.EndReceive(ia);
            }
            catch (ObjectDisposedException odEx)
            {
                OnException(odEx);
                return;
            }
            catch (SocketException sckEx)
            {
                OnException(sckEx);
                this.CloseHelper();
                return;
            }
            catch
            {
                throw;
            }
            log.Debug("Received bytes length: " + n);

            if (n > 0)
            {
                _memoryStream.Write(_receBuffer, 0, n);
                if (this.IsOccupy)
                {
                    // do nothing 
                }
                else
                {
                    FireReceivedEvent(EventArgs.Empty);
                }

                BeginReceiveHelper();
            }
            else
            {
                CloseHelper();
            }
        }
        #endregion //BeginReveiveCallback

        #region OnException
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ex"></param>
        //private void OnException(Exception ex)
        //{
        //    ExceptionEventArgs e = new ExceptionEventArgs(ex);
        //    if (this.ExceptionEvent != null)
        //    {
        //        ExceptionEventHandler t = this.ExceptionEvent;
        //        t(this, e);
        //    }
        //}
        #endregion //OnException

        #region IsClosed
        /// <summary>
        /// 
        /// </summary>
        public bool IsClosed
        {
            get { return _isClosed; }
        } private bool _isClosed;
        #endregion //IsClosed

        #region CloseHelper
        /// <summary>
        /// 
        /// </summary>
        private void CloseHelper()
        {
            if (!this._isClosed)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                this._isClosed = true;
                //StationCommuniPortBinder.ClearBind(this);
                FireClosedEvent(EventArgs.Empty);
            }
        }
        #endregion //CloseHelper

        #region Socket
        /// <summary>
        /// 
        /// </summary>
        public Socket Socket
        {
            get { return _socket; }
        } private Socket _socket;
        #endregion //Socket

        #endregion //Properties
    }
}