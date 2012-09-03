using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ControllerIn;
using C3.Remote;

namespace RemoteClient
{
   public class RemoteController : _1100ControllerInterface
    {
        #region _1100ControllerInterface 成员

        public string StationName
        {
            get
            {
                return _stationName;
            }
            set
            {
                _stationName = value;
            }
        } private string _stationName;

        #region DeviceID
        /// <summary>
        /// 
        /// </summary>
        public int DeviceID
        {
            get
            {
                return _deviceID;
            }
            set
            {
                _deviceID = value;
            }
        } private int _deviceID;
        #endregion //DeviceID

        #region OperaName
        /// <summary>
        /// 
        /// </summary>
        public string OperaName
        {
            get
            {
                if (_operaName == null)
                {
                    _operaName = string.Empty;
                }
                return _operaName;
            }
            set
            {
                _operaName = value;
            }
        } private string _operaName;
        #endregion //OperaName

        public System.Collections.Hashtable Parameters
        {
            get { return _parameters; ; }
        } private Hashtable _parameters = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        public void Doit()
        {
            RemoteObject obj = (RemoteObject)Activator.GetObject(
                           typeof(RemoteObject),
                           "tcp://127.0.0.1:9000/RO"
                           );
            ExecuteParameter ep = new ExecuteParameter();
            ep.DeviceAddress = 0;
            ep.ExecuteName = this.OperaName;
            ep.HashTable = this.Parameters;
            ep.StationName = this.StationName;

            CallbackWrapper w = new CallbackWrapper(new CallbackDelegate(this.Target));
            obj.Execute(ep, w);
        }
        #endregion


        private void Target(object status)
        {
            this._notifyObject = status;
            if (this.Notify != null)
            {
                this.Notify(this, EventArgs.Empty);
            }
        }

        #region _1100ControllerInterface 成员


        public event EventHandler Notify;

        #endregion

        #region _1100ControllerInterface 成员


        public object NotifyObject
        {
            get { return _notifyObject; ; }
        } private object _notifyObject;

        #endregion
    }
}
