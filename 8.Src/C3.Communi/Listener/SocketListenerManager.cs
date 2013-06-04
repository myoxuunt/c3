﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using NLog;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class SocketListenerManager
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        #region SocketListenerManager
        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        //public SocketListenerManager(Soft soft)
        //{
        //    if (soft == null)
        //        throw new ArgumentNullException("communiSoft");
        //    this._soft = soft;
        //}

        public SocketListenerManager ()
        {
        }
        #endregion //SocketListenerManager


        ///// <summary>
        ///// 
        ///// </summary>
        //public Soft Soft
        //{
        //    get { return _soft; }
        //} private Soft _soft;

        ///// <summary>
        ///// 
        ///// </summary>
        //public event NewConnectEventHandler NewConnectEvent;
        public event EventHandler NewCommuniPortEvent;

        #region SocketListeners
        /// <summary>
        /// 
        /// </summary>
        public SocketListenerCollection SocketListeners
        {
            get
            {
                if (this._socketListenerCollection == null)
                    this._socketListenerCollection = new SocketListenerCollection();
                return _socketListenerCollection;
            }
        } private SocketListenerCollection _socketListenerCollection;
        #endregion //SocketListeners

        #region Add
        /// <summary>
        /// 添加item到SocketListeners集合，并注册NewConnect事件
        /// </summary>
        /// <param name="this1"></param>
        public void Add(SocketListener item)
        {
            this.SocketListeners.Add(item);
            ReginsterEvents(item);
        }
        #endregion //Add

        #region ReginsterEvents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this1"></param>
        private void ReginsterEvents(SocketListener item)
        {
            item.ConnectedEvent += new EventHandler(item_ConnectedEvent);
        }
        #endregion //ReginsterEvents

        #region Remove
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this1"></param>
        public bool Remove(SocketListener item)
        {
            if (this.SocketListeners.Remove(item))
            {
                UnregisterEvents(item);
                return true;
            }
            return false;
        }
        #endregion //Remove

        #region UnregisterEvents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this1"></param>
        private void UnregisterEvents(SocketListener item)
        {
            item.ConnectedEvent -= new EventHandler(item_ConnectedEvent);
        }
        #endregion //UnregisterEvents

        #region item_ConnectedEvent
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_ConnectedEvent(object sender, EventArgs e)
        {
            SocketListener sl = sender as SocketListener;
            Socket newsocket = sl.NewSocket;
            if( newsocket == null)
                return;
            if (!newsocket.Connected)
            {
                return;
            }
            
            SocketCommuniPort scp = null;
            try
            {
                scp = new SocketCommuniPort(newsocket);
            }
            catch (Exception ex)
            {
                CloseSocket(newsocket);
                //this.Soft.ErrorManager.Process(ex);
                log.Error(ex);
                return;
            }

            this._newCommuniPort = scp;

            //this.Soft.CommuniPortManager.Add(scp);
            OnNewCommuniPortEvent();
        }

        private void OnNewCommuniPortEvent()
        {
            if (NewCommuniPortEvent  != null)
            {
                NewCommuniPortEvent(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort NewCommuniPort
        {
            get { return _newCommuniPort; }
        } private ICommuniPort _newCommuniPort;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sck"></param>
        private void CloseSocket(Socket sck)
        {
            try
            {
                sck.Shutdown(SocketShutdown.Both);
                sck.Close();
            }
            catch(Exception ex)
            {
                //this.Soft.ErrorManager.Process(ex, "SocketListenerManager.CloseSocket exception"); 
                log.Error(ex);
            }
        }

        #endregion //item_ConnectedEvent
    }
}
