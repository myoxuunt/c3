using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{
    public class SApp : AppBase
    {

        /// <summary>
        /// 
        /// </summary>
        static public SApp App
        {
            get
            {
                if (AppBase.DefaultInstance == null)
                {
                    AppBase.DefaultInstance = new SApp();
                }
                return AppBase.DefaultInstance as SApp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override Form MainForm
        {
            get
            {
                if (_mainForm == null)
                {
                    _mainForm = new FrmMain();
                }
                return _mainForm;
            }
        } private FrmMain _mainForm;


        public SocketListenerManager SocketListenerManager
        {
            get
            {
                if (_socketListenerManager == null)
                {
                    _socketListenerManager = new SocketListenerManager();
                    string path = PathUtils.SocketListenerConfigFileName;
                    XmlSocketListenBuilder builder = new XmlSocketListenBuilder(path);
                    builder.Build(_socketListenerManager);

                    _socketListenerManager.NewCommuniPortEvent += new EventHandler(_socketListenerManager_NewCommuniPortEvent);
                }
                return _socketListenerManager;
            }
        } private SocketListenerManager _socketListenerManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _socketListenerManager_NewCommuniPortEvent(object sender, EventArgs e)
        {
            SocketListenerManager sckListenMan = sender as SocketListenerManager;
            ICommuniPort cp = sckListenMan.NewCommuniPort;
            this.CommuniPortManager.Add(cp);
        }



        #region CommuniPortManager
        /// <summary>
        /// 
        /// </summary>
        public CommuniPortManager CommuniPortManager
        {
            get
            {
                if (_communiPortManager == null)
                {
                    _communiPortManager = new CommuniPortManager();
                }
                return _communiPortManager;
            }
        } private CommuniPortManager _communiPortManager;
        #endregion //CommuniPortManager

        #region ClientManager
        /// <summary>
        /// 
        /// </summary>
        public ClientManager ClientManager
        {
            get
            {
                if (_clientManager == null)
                {
                    _clientManager = new ClientManager();
                }
                return _clientManager;
            }
        } private ClientManager _clientManager;
        #endregion //ClientManager

        //#region ErrorManager
        //public ErrorManager ErrorManager
        //{
        //    get
        //    {
        //        if (_errorManager == null)
        //        {
        //            _errorManager = new ErrorManager(this);
        //        }
        //        return _errorManager;
        //    }
        //} private ErrorManager _errorManager;
        //#endregion //

    }

    internal class CommuniPortEventProcessor
    {
        private CommuniPortManager _cpManager;
        private ClientManager _clientManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpManager"></param>
        internal CommuniPortEventProcessor(CommuniPortManager cpManager)
        {
            Debug.Assert(cpManager != null);
            this._cpManager = cpManager;


            _cpManager.AddedCommuniPort += new CommuniPortEventHandler(_cpManager_AddedCommuniPort);
            _cpManager.ClosedCommuniPort += new CommuniPortEventHandler(_cpManager_ClosedCommuniPort);
            _cpManager.DeterminedCommuniPort += new CommuniPortEventHandler(_cpManager_DeterminedCommuniPort);
            _cpManager.ReceivedCommuniPort += new CommuniPortEventHandler(_cpManager_ReceivedCommuniPort);
            _cpManager.RemovedCommuniPort += new CommuniPortEventHandler(_cpManager_RemovedCommuniPort);
        }

        void _cpManager_RemovedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            this._clientManager.RemoveByCommuniPort(e.CommuniPort);
        }

        void _cpManager_ReceivedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _cpManager_DeterminedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _cpManager_ClosedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _cpManager_AddedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            Client c = new Client(e.CommuniPort);
            this._clientManager.Add(c);
        }
    }
}
