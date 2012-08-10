using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    abstract public class StationBase : IStation
    {
        private const string PN_NAME = "Name",
            PN_COMMUNIPORTCONFIG = "CommuniPortConfig";

        private const int PO_NAME = -90,
            PO_COMMUNIPORTCONFIG = -80;

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NameChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CommuniPortChanged;
        #endregion //Events


        #region StationBase
        protected StationBase()
        {
            // init
            //
            this.GetNameParameter();
            this.GetCommuniPortConfig();
        }
        #endregion //StationBase

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                IParameter p = this.GetNameParameter();
                return (string)p.Value;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name");
                }

                value = value.Trim();
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                IParameter p = GetNameParameter();
                p.Value = value;
                OnNameChanged(EventArgs.Empty);
            }
        }
        #endregion //Name

        #region OnNameChanged
        /// <summary>
        /// 
        /// </summary>
        private void OnNameChanged(EventArgs e)
        {
            if (this.NameChanged != null)
            {
                this.NameChanged(this, e);
            }
        }
        #endregion //OnNameChanged

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IParameter GetNameParameter()
        {
            IParameter p = this.GeneralGroup.Parameters[PN_NAME];
            if (p == null)
            {
                p = new Parameter(PN_NAME, "unknown", PO_NAME);
                this.GeneralGroup.Parameters.Add(p);
            }
            return p;
        }

        #region Devices
        /// <summary>
        /// 
        /// </summary>
        public DeviceCollection Devices
        {
            get
            {
                if (_devices == null)
                {
                    _devices = new DeviceCollection();
                }
                return _devices;
            }
            set
            {
                _devices = value;
            }
        } private DeviceCollection _devices;
        #endregion //Devices

        #region CommuniPort
        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get
            {
                if (_communiPort == null)
                {
                    if (this.CommuniPortConfig.CanCreate)
                    {
                        Soft soft = SoftManager.GetSoft();
                        CommuniPortManager cpManager = soft.CommuniPortManager;
                        ICommuniPort cp = cpManager.CreateCommuniPort(this.CommuniPortConfig);
                        if (cp != null)
                        {
                            this.CommuniPort = cp;
                        }
                    }
                }
                return _communiPort;
            }
            set
            {
                if (_communiPort != value)
                {
                    _communiPort = value;
                    if (Soft.IsUseUISynchronizationContext)
                    {
                        Soft.Post(this.OnCommuniPortChangedCallBack, EventArgs.Empty);
                    }
                    else
                    {
                        OnCommuniPortChanged(EventArgs.Empty);
                    }
                }
            }
        } private ICommuniPort _communiPort;
        #endregion //CommuniPort

        #region OnCommuniPortChangedCallBack
        /// <summary>
        /// 
        /// </summary>
        private SendOrPostCallback OnCommuniPortChangedCallBack
        {
            get
            {
                if (_onCommuniPortChangedCallBack == null)
                {
                    _onCommuniPortChangedCallBack = new SendOrPostCallback(this.OnCommuniPortChanged);
                }
                return _onCommuniPortChangedCallBack;
            }
        } private SendOrPostCallback _onCommuniPortChangedCallBack;
        #endregion //OnCommuniPortChangedCallBack

        #region OnCommuniPortChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnCommuniPortChanged(object e)
        {
            EventArgs e2 = e as EventArgs;
            this.OnCommuniPortChanged(e2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnCommuniPortChanged(EventArgs e)
        {
            if (this.CommuniPortChanged != null)
            {
                this.CommuniPortChanged(this, e);
            }
        }
        #endregion //OnCommuniPortChanged

        #region StationSource
        /// <summary>
        /// 
        /// </summary>
        public IStationSource StationSource
        {
            get
            {
                return _stationSource;
            }
            set
            {
                _stationSource = value;
            }
        } private IStationSource _stationSource;
        #endregion //StationSource

        #region Stations
        /// <summary>
        /// 
        /// </summary>
        public StationCollection Stations
        {
            get { return _stationCollection; }
            set { _stationCollection = value; }
        } private StationCollection _stationCollection;
        #endregion //Stations

        #region CommuniPortConfig
        /// <summary>
        /// 
        /// </summary>
        public ICommuniPortConfig CommuniPortConfig
        {
            get
            {
                if (_communiPortConfig == null)
                {
                    _communiPortConfig = NullCommuniPortConfig.Default;
                }
                return _communiPortConfig;
            }
            set
            {
                _communiPortConfig = value;
                // TODO: 2012-08-10 clear communiPort
                //
            }
        } private ICommuniPortConfig _communiPortConfig;
        #endregion //CommuniPortConfig

        private IParameter GetCommuniPortConfig()
        {
            IParameter p = this.CommuniPortConfigGroup.Parameters[PN_COMMUNIPORTCONFIG];
            if (p == null)
            {
                p = new Parameter(PN_COMMUNIPORTCONFIG, typeof(ICommuniPortConfig), 
                    this.CommuniPortConfig, PO_COMMUNIPORTCONFIG);
                p.ParameterUI = new CommuniPortConfigUI();
                this.CommuniPortConfigGroup.Parameters.Add(p);
            }
            return p;
        }

        #region Guid
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        } private Guid _guid;
        #endregion //Guid

        #region Tag
        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        } private object _tag;
        #endregion //Tag

        #region IStation 成员


        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public StationType StationType
        {
            get
            {
                return _stationType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("StationType");
                }
                _stationType = value;
            }
        } private StationType _stationType;

        #endregion

        #region GeneralGroup
        /// <summary>
        /// 
        /// </summary>
        protected IGroup GeneralGroup
        {
            get
            {
                // TODO: string
                //
                IGroup g = this.Groups.GetGroup("General");
                if (g == null)
                {
                    g = new Group();
                    g.Name = "General";
                    this.Groups.Add(g);
                }
                return g;
            }
        }
        #endregion //GeneralGroup

        /// <summary>
        /// 
        /// </summary>
        private IGroup CommuniPortConfigGroup
        {
            get
            {
                IGroup g = this.Groups.GetGroup("CommuniPortConfig");
                if (g == null)
                {
                    g = new Group();
                    g.Name = PN_COMMUNIPORTCONFIG;
                    this.Groups.Add(g);
                }
                return g;
            }
        }

        #region Groups
        /// <summary>
        /// 
        /// </summary>
        public GroupCollection Groups
        {
            get
            {
                if (_parameterGroups == null)
                {
                    _parameterGroups = new GroupCollection();
                }
                return _parameterGroups;
            }
        } private GroupCollection _parameterGroups;
        #endregion //Groups

        #region Spu
        /// <summary>
        /// 
        /// </summary>
        public ISPU Spu
        {
            get
            {
                return _spu;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Spu");
                }
                _spu = value;
            }
        } private ISPU _spu;
        #endregion //Spu
    }
}
