using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    abstract public class StationBase : IStation
    {
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public StationBase(string name)
        {
            this.Name = name;
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
                return _name;
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
                if (_name != value)
                {
                    _name = value;
                    OnNameChanged(EventArgs.Empty);
                }
            }
        } private string _name;
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
                    OnCommuniPortChanged(EventArgs.Empty);
                }
            }
        } private ICommuniPort _communiPort;
        #endregion //CommuniPort

        #region OnCommuniPortChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnCommuniPortChanged(EventArgs e)
        {
            if (this.CommuniPortChanged != null)
            {
                this.CommuniPortChanged ( this,e);
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
            set { _stationCollection = value;}
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
                return _communiPortConfig;
            }
            set
            {
                _communiPortConfig = value;
            }
        } private ICommuniPortConfig _communiPortConfig;
        #endregion //CommuniPortConfig

    }
}
