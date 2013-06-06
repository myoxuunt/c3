using System;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// 
    /// </summary>
    public class Soft
    {
        #region Members
        static private NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private const int MinScaningCycle = 50;
        private const int DefaultScaningCycle = 1000;

        private Timer _timer;
        #endregion //Members

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        internal Soft()
        {
            log.Info("Soft Constructor");

            _timer = new Timer();
            _timer.Interval = DefaultScaningCycle;
            _timer.Tick += new EventHandler(_timer_Tick);

            //this.Prepare();

            //_timer.Start();

            Init();
        }
        #endregion //Constructor

        #region Prepare
        /// <summary>
        /// 
        /// </summary>
        public void Prepare()
        {
            Hardware temp = this.Hardware;
        }
        #endregion //Prepare

        #region Init
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            bool enabledRemoteService = false;

            if (enabledRemoteService)
            {
                this.RemoteServer.Start();
            }
        }
        #endregion //Init

        #region IsStarted
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsStarted()
        {
            return this._timer.Enabled;
        }
        #endregion //IsStarted

        #region Start
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            this._timer.Start();
        }
        #endregion //Start

        #region Stop
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            this._timer.Stop();
        }
        #endregion //Stop

        #region ScanningCycle
        /// <summary>
        /// unit is milliseconds 
        /// </summary>
        public int ScanningCycle
        {
            get { return _scaningCycle; }
            set
            {
                if (value < MinScaningCycle)
                {
                    value = MinScaningCycle;
                }
                _scaningCycle = value;
                this._timer.Interval = _scaningCycle;
            }
        } private int _scaningCycle;
        #endregion //ScanningCycle

        #region SocketListenerManager
        /// <summary>
        /// 
        /// </summary>
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
        #endregion //SocketListenerManager

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _socketListenerManager_NewCommuniPortEvent(object sender, EventArgs e)
        {
            SocketListenerManager slMan = sender as SocketListenerManager;
            ICommuniPort cp = slMan.NewCommuniPort;

            Debug.Assert(cp != null);

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
                    //CommuniPortEventProcessor cpep = this.GetCommuniPortEventProcessor ();

                    _communiPortManager = new CommuniPortManager();
                    //_communiPortManager .AddedCommuniPort += cpep
                    _cpEventProcessor = new CommuniPortEventProcessor(this, _communiPortManager);
                }
                return _communiPortManager;
            }
        } private CommuniPortManager _communiPortManager;
        #endregion //CommuniPortManager

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //private CommuniPortEventProcessor GetCommuniPortEventProcessor ()
        //{
        //    if ( _cpEventProcessor == null )
        //    {
        //        _cpEventProcessor = new CommuniPortEventProcessor(this,);
        //    }
        //    return _cpEventProcessor;
        //} 
        private CommuniPortEventProcessor _cpEventProcessor;

        #region _timer_Tick
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timer_Tick(object sender, EventArgs e)
        {
            this.TaskScheduler.Doit();
        }
        #endregion //_timer_Tick

        #region TaskScheduler
        /// <summary>
        /// 
        /// </summary>
        public TaskScheduler TaskScheduler
        {
            get
            {
                if (_taskScheduler == null)
                {
                    _taskScheduler = new TaskScheduler(this);
                }
                return _taskScheduler;
            }
        } private TaskScheduler _taskScheduler;
        #endregion //TaskScheduler

        #region GetReceived
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private byte[] GetReceived(ITask current)
        {
            IDevice device = current.Device;
            IStation station = device.Station;
            ICommuniPort cp = station.CommuniPort;

            byte[] received = new byte[0];
            if (cp != null)
            {
                received = cp.Read();
            }
            return received;
        }
        #endregion //GetReceived

        #region ErrorManager
        public ErrorManager ErrorManager
        {
            get
            {
                if (_errorManager == null)
                {
                    _errorManager = new ErrorManager(this);
                }
                return _errorManager;
            }
        } private ErrorManager _errorManager;
        #endregion //

        #region HardwareCreated
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler HardwareCreated;
        #endregion //HardwareCreated

        #region Hardware
        /// <summary>
        /// 
        /// </summary>
        public Hardware Hardware
        {
            get
            {
                if (_hardware == null)
                {
                    //HardwareFactory factory = new HardwareFactory();
                    HardwareFactory factory = this.HardwareFactory;
                    //factory.SourceConfigs = ReadSourceConfigs();
                    //factory.SourceConfigs = this.SourceConfigs;
                    factory.SourceConfigs = SourceConfigManager.SourceConfigs;

                    _hardware = factory.Create();

                    if (HardwareCreated != null)
                    {
                        HardwareCreated(this, EventArgs.Empty);
                    }
                }
                return _hardware;
            }
            //set
            //{
            //    _hardware = value;
            //}
        } private Hardware _hardware;
        #endregion //Hardware

        #region SPUs
        /// <summary>
        /// 
        /// </summary>
        public SPUCollection SPUs
        {
            get
            {
                return HardwareFactory.SPUs;
            }
        }
        #endregion //SPUs

        #region DPUs
        /// <summary>
        /// 
        /// </summary>
        public DPUCollection DPUs
        {
            get
            {
                return HardwareFactory.DPUs;
            }
        }
        #endregion //DPUs

        #region HardwareFactory
        /// <summary>
        /// 
        /// </summary>
        private HardwareFactory HardwareFactory
        {
            get
            {
                if (_hardwareFactory == null)
                {
                    _hardwareFactory = new HardwareFactory();
                }
                return _hardwareFactory;
            }
        } private HardwareFactory _hardwareFactory;
        #endregion //HardwareFactory

        //#region SourceConfigs
        //public SourceConfigCollection SourceConfigs
        //{
        //    get
        //    {
        //        if (_sourceConfigs == null)
        //        {
        //            //_sourceConfigs = ReadSourceConfigs();
        //        }
        //        return _sourceConfigs;
        //    }
        //} private SourceConfigCollection _sourceConfigs;
        //#endregion //SourceConfigs



        //#region BytesConverterManager
        ///// <summary>
        ///// 
        ///// </summary>
        //public BytesConverterManager BytesConverterManager
        //{
        //    get
        //    {
        //        if (this._bytesConverterManager == null)
        //            this._bytesConverterManager =  BytesConverterManager.;
        //        return this._bytesConverterManager;
        //    }
        //} private BytesConverterManager _bytesConverterManager;
        //#endregion //BytesConverterManager

        //#region CRCerManager
        ///// <summary>
        ///// 
        ///// </summary>
        //public CRCerManager CRCerManager
        //{
        //    get
        //    {
        //        if (_crcerManager == null)
        //            _crcerManager = new CRCerManager();
        //        return _crcerManager;
        //    }
        //} private CRCerManager _crcerManager;
        //#endregion //CRCerManager

        #region IsUseUISynchronizationContext
        /// <summary>
        /// 
        /// </summary>
        static public bool IsUseUISynchronizationContext
        {
            get 
            { 
                return _isUseUISynchronizationContext; 
            }
            set
            {
                _isUseUISynchronizationContext = value;
            }
        } static private bool _isUseUISynchronizationContext=true;
        #endregion //IsUseUISynchronizationContext

        #region UISynchronizationContext
        /// <summary>
        /// 获取或设置UI线程上下文
        /// </summary>
        static public SynchronizationContext UISynchronizationContext
        {
            get
            {
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

        #region RemoteServer
        /// <summary>
        /// 
        /// </summary>
        public RemoteServer RemoteServer
        {
            get
            {
                if (_remoteServer == null)
                {
                    _remoteServer = new RemoteServer();
                    _remoteServer.Start();

                    log.Info("remote server started.");
                }
                return _remoteServer;
            }
        } private RemoteServer _remoteServer;
        #endregion //RemoteServer
    }
}
