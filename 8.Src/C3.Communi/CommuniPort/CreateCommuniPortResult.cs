
using System;
using System.Threading;
using System.Diagnostics;
using NLog;


namespace C3.Communi
{
    public class CreateCommuniPortResult
    {
        public CreateCommuniPortResult (bool success, ICommuniPortConfig communiPortConfig, string message, Exception exception)
        {
            this.Success = success;
            this.CommuniPortConfig = communiPortConfig;
            this.Message = message;
            this.Exception = exception;
        }

#region Success
        /// <summary>
        /// 
        /// </summary>
        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
            }
        } private bool _success;
#endregion //Success

#region DT
        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get
            {
                return _dT;
            }
            set
            {
                _dT = value;
            }
        } private DateTime _dT;
#endregion //DT

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

#region Exception
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception
        {
            get
            {
                return _exception;
            }
            set
            {
                _exception = value;
            }
        } private Exception _exception;
#endregion //Exception

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
