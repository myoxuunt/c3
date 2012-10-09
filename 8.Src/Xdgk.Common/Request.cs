using System;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExecuteArgs
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

        /// <summary>
        /// 
        /// </summary>
        public KeyValueCollection KeyValues
        {
            get
            {
                if (_keyValues == null)
                {
                    _keyValues = new KeyValueCollection();
                }
                return _keyValues;  
            }
            set { _keyValues = value; }
        } private KeyValueCollection _keyValues;

        //#region HashTable
        ///// <summary>
        ///// 
        ///// </summary>
        //public Hashtable HashTable
        //{
        //    get
        //    {
        //        if (_hashTable == null)
        //        {
        //            _hashTable = new Hashtable();
        //        }
        //        return _hashTable;
        //    }
        //    set
        //    {
        //        _hashTable = value;
        //    }
        //} private Hashtable _hashTable;
        //#endregion //HashTable

    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ResultArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ExecuteArgs ExecuteArgs
        {
            get
            {
                if (_executeArgs == null)
                {
                    throw new InvalidOperationException("ExecuteArgs == null");
                }
                return _executeArgs; 
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ExecuteArgs");
                }
                _executeArgs = value; 
            }
        } private ExecuteArgs _executeArgs;

        #region IsComplete
        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        } private bool _isComplete;
        #endregion //IsComplete

        #region IsSuccess
        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return _isSuccess;
            }
            set
            {
                _isSuccess = value;
            }
        } private bool _isSuccess;
        #endregion //IsSuccess

        #region Message
        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get
            {
                if (_message == null)
                {
                    _message = string.Empty;
                }
                return _message;
            }
            set
            {
                _message = value;
            }
        } private string _message;
        #endregion //Message

        /// <summary>
        /// 
        /// </summary>
        public KeyValueCollection KeyValues
        {
            get { return _keyValues;  }
            set { _keyValues = value; }
        } private KeyValueCollection _keyValues;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    public delegate void ResultDelegate(ResultArgs args);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CallbackWrapper : MarshalByRefObject
    {
        private ResultDelegate _callbackDelegate;

        public CallbackWrapper(ResultDelegate cbTarget)
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
        public void Callback(ResultArgs args)
        {
            if (_callbackDelegate != null)
            {
                this._callbackDelegate(args);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExecuteResult
    {

        static public ExecuteResult CreateSuccessExecuteResult()
        {
            return new ExecuteResult(true, string.Empty);
        }

        static public ExecuteResult CreateFailExecuteResult(string message)
        {
            return new ExecuteResult(false, message);
        }
        private ExecuteResult(bool success, string message)
        {
            this.IsSuccess = success;
            this.FailMessage = message;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        } private bool _isSuccess;

        /// <summary>
        /// 
        /// </summary>
        public string FailMessage
        {
            get
            {
                if (_failMessage == null)
                {
                    _failMessage = string.Empty;
                }
                return _failMessage;
            }
            set { _failMessage = value; }
        } private string _failMessage;

        /// <summary>
        /// 
        /// </summary>
        public object Tag
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
        public ExecuteArgs ExecuteArgs;
        public ExecuteResult Result;
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
        public ExecuteResult Execute(ExecuteArgs executeArgs, CallbackWrapper cbWrapper)
        {
            if (Executeing != null)
            {
                ExecuteEventArgs e = new ExecuteEventArgs();
                e.ExecuteArgs = executeArgs;
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
