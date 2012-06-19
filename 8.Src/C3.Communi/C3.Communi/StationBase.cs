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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public StationBase(string name)
        {
            this.Name = name;
        }

        #region IStation 成员

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

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get
            {
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

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public StationCollection Stations
        {
            get { return _stationCollection; }
            set { _stationCollection = value;}
        } private StationCollection _stationCollection;
        #region IStation 成员

        #region Tag
        /// <summary>
        /// 
        /// </summary>
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

        #endregion

        #region IStation 成员


        /// <summary>
        /// 
        /// </summary>
        public ICommuniPortToken CommuniPortToken
        {
            get
            {
                return _communiPortToken;
            }
            set
            {
                _communiPortToken = value;
            }
        } private ICommuniPortToken _communiPortToken;

        #endregion
    }
}
