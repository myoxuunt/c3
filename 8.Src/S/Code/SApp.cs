using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;
using System.Threading;

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
                //ReceivePart rp = ReceivePartFacotry.Create("f:\\C3\\8.Src\\S\\bin\\Debug\\Config\\Def.xml ", "vFlux","read");

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
        private SApp()
        {
            base.NotifyIconManager.EnabledNotifyIcon = true;
            base.NotifyIconManager.Icon = Resource.notifyIcon;
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
                    _communiPortManager.AddedCommuniPort += new CommuniPortEventHandler(_communiPortManager_AddedCommuniPort);
                    _communiPortManager.ClosedCommuniPort += new CommuniPortEventHandler(_communiPortManager_ClosedCommuniPort);       
                    //_communiPortManager.DeterminedCommuniPort 
                    //_communiPortManager.ReceivedCommuniPort +
                    _communiPortManager.RemovedCommuniPort += new CommuniPortEventHandler(_communiPortManager_RemovedCommuniPort);
                }
                return _communiPortManager;
            }
        }

        void _communiPortManager_RemovedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            this.ClientManager.RemoveByCommuniPort(e.CommuniPort);
        }

        void _communiPortManager_ClosedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            // do nothing
            //
        }

        void _communiPortManager_AddedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            this.ClientManager.Add(new Client(e.CommuniPort));
        } 
        
        private CommuniPortManager _communiPortManager;
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

        #region UISynchronizationContext
        /// <summary>
        /// 获取或设置UI线程上下文
        /// </summary>
        static public SynchronizationContext UISynchronizationContext
        {
            get
            {
                //System.
                if (_uiSynchronizationContext == null)
                {
                    //_uiSynchronizationContext = SynchronizationContext.Current;
                    if (WindowsFormsSynchronizationContext.Current != null)
                    {
                        _uiSynchronizationContext = WindowsFormsSynchronizationContext.Current;
                    }
                    else
                    {
                        _uiSynchronizationContext = new WindowsFormsSynchronizationContext();
                    }
                }
                return _uiSynchronizationContext;
            }
            set
            {
                _uiSynchronizationContext = value;
            }
        } static private SynchronizationContext _uiSynchronizationContext;
        #endregion //UISynchronizationContext


        #region Post
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendOrPostCallback"></param>
        /// <param name="eventArgs"></param>
        internal static void Post(SendOrPostCallback sendOrPostCallback, object state)
        {
            if (UISynchronizationContext == null)
            {
                string s = "Soft.UISynchronizationContext is null";
                throw new InvalidOperationException(s);
            }

            if (sendOrPostCallback == null)
            {
                throw new ArgumentNullException("sendOrPostCallback");
            }

            UISynchronizationContext.Post(sendOrPostCallback, state);
        }
        #endregion //Post

        #region Send
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendOrPostCallback"></param>
        /// <param name="state"></param>
        internal static void Send(SendOrPostCallback sendOrPostCallback, object state)
        {
            UISynchronizationContext.Send(sendOrPostCallback, state);
        }
        #endregion //
    }
}
