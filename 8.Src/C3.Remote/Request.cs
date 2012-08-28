using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace C3.Remote
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    public delegate void CallbackDelegate(object status);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CallbackWrapper : MarshalByRefObject
    {
        private CallbackDelegate _callbackDelegate;

        public CallbackWrapper(CallbackDelegate cbTarget)
        {
            if (cbTarget == null)
            {
                throw new ArgumentNullException("cbTarget");
            }

            this._callbackDelegate = cbTarget;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Callback(object status)
        {
            if (_callbackDelegate != null)
            {
                this._callbackDelegate(status);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExecuteParameter
    {
        #region StationName
        /// <summary>
        /// 
        /// </summary>
        public string StationName
        {
            get
            {
                if (_stationName == null)
                {
                    _stationName = string.Empty;
                }
                return _stationName;
            }
            set
            {
                _stationName = value;
            }
        } private string _stationName;
        #endregion //StationName

        #region DeviceAddress
        /// <summary>
        /// 
        /// </summary>
        public UInt64 DeviceAddress
        {
            get
            {
                return _deviceAddress;
            }
            set
            {
                _deviceAddress = value;
            }
        } private UInt64 _deviceAddress;
        #endregion //DeviceAddress

        #region ExecuteName
        /// <summary>
        /// 
        /// </summary>
        public string ExecuteName
        {
            get
            {
                if (_executeName == null)
                {
                    _executeName = string.Empty;
                }
                return _executeName;
            }
            set
            {
                _executeName = value;
            }
        } private string _executeName;
        #endregion //ExecuteName

        #region HashTable
        /// <summary>
        /// 
        /// </summary>
        public Hashtable HashTable
        {
            get
            {
                if (_hashTable == null)
                {
                    _hashTable = new Hashtable();
                }
                return _hashTable;
            }
            set
            {
                _hashTable = value;
            }
        } private Hashtable _hashTable;
        #endregion //HashTable

    }

    [Serializable]
    public enum ResultEnum
    {
        Success,
        Fail,
        Unsure,
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Result
    {

        /// <summary>
        /// 
        /// </summary>
        public ResultEnum ResultEnum
        {
            get { return _resultEnum; }
            set { _resultEnum = value; }
        } private ResultEnum _resultEnum;

        /// <summary>
        /// 
        /// </summary>
        public string FailMessage
        {
            get { return _failMessage; }
            set { _failMessage = value; }
        } private string _failMessage;

        /// <summary>
        /// 
        /// </summary>
        public object Values
        {
            get { return _values; }
            set { _values = value; }
        } private object _values;
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExecuteEventArgs : EventArgs
    {
        public ExecuteParameter Parameter;
        public Result Result;
        public CallbackWrapper CallbackWrapper;
    }

    public delegate void ExecuteEventHandler(object sender, ExecuteEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class RemoteObject : MarshalByRefObject
    {
        static public event ExecuteEventHandler Executeing;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cbWrapper"></param>
        /// <returns></returns>
        public Result Execute(ExecuteParameter parameter, CallbackWrapper cbWrapper)
        {
            if (Executeing != null)
            {
                ExecuteEventArgs e = new ExecuteEventArgs();
                e.Parameter = parameter;
                if (cbWrapper != null)
                {
                    e.CallbackWrapper = cbWrapper;
                }

                Executeing(this, e);

                return e.Result;
            }
            else
            {
                return null;
            }
        }
    }
}
