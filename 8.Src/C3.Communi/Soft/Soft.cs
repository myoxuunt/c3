using System;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace C3.Communi
{
    using Timer = System.Windows.Forms.Timer;

    public class Soft
    {

        #region Members
        static private NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
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
            _timer.Interval = 50;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();

            Init();
        }
        #endregion //Constructor

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.RemoteServer.Start();
        }

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
                    _socketListenerManager = new SocketListenerManager(this);

                    string path = PathUtils.SocketListenerConfigFileName;
                    XmlSocketListenBuilder builder = new XmlSocketListenBuilder(path);
                    builder.Build(_socketListenerManager);
                }
                return _socketListenerManager;
            }
        } private SocketListenerManager _socketListenerManager;
        #endregion //SocketListenerManager

        #region CommuniPortManager
        public CommuniPortManager CommuniPortManager
        {
            get
            {
                if (_communiPortManager == null)
                {
                    _communiPortManager = new CommuniPortManager(this);
                }
                return _communiPortManager;
            }
        } private CommuniPortManager _communiPortManager;
        #endregion //CommuniPortManager

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

        public event EventHandler HardwareCreated;

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
                    factory.SourceConfigs = ReadSourceConfigs();
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

        #region SourceConfigs
        public SourceConfigCollection SourceConfigs
        {
            get
            {
                if (_sourceConfigs == null)
                {
                    _sourceConfigs = ReadSourceConfigs();
                }
                return _sourceConfigs;
            }
        } private SourceConfigCollection _sourceConfigs;
        #endregion //SourceConfigs

        #region ReadSourceConfigs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SourceConfigCollection ReadSourceConfigs()
        {
            SourceConfigCollection sourceConfigs = new SourceConfigCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(PathUtils.SourceConfigFileName);

            XmlNode sourcesNode = doc.SelectSingleNode("sources");
            foreach (XmlNode item in sourcesNode.ChildNodes)
            {
                XmlElement sourceNode = item as XmlElement;
                string key = sourceNode["key"].ChildNodes[0].Value;
                string value = sourceNode["value"].ChildNodes[0].Value;

                SourceConfig sourceConfig = new SourceConfig(key, value);
                sourceConfigs.Add(sourceConfig);
            }

            return sourceConfigs;
        }
        #endregion //ReadSourceConfigs

        #region BytesConverterManager
        /// <summary>
        /// 
        /// </summary>
        public BytesConverterManager BytesConverterManager
        {
            get
            {
                if (this._bytesConverterManager == null)
                    this._bytesConverterManager = new BytesConverterManager();
                return this._bytesConverterManager;
            }
        } private BytesConverterManager _bytesConverterManager;
        #endregion //BytesConverterManager

        #region CRCerManager
        /// <summary>
        /// 
        /// </summary>
        public CRCerManager CRCerManager
        {
            get
            {
                if (_crcerManager == null)
                    _crcerManager = new CRCerManager();
                return _crcerManager;
            }
        } private CRCerManager _crcerManager;
        #endregion //CRCerManager

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
    }
}
