using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public class ErrorManager
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        public ErrorManager(Soft soft)
        {
            if (soft == null)
            {
                throw new ArgumentNullException("soft");
            }
            _soft = soft;
        }
        #endregion //Constructor

        #region Soft
        /// <summary>
        /// 
        /// </summary>
        public Soft Soft
        {
            get
            {
                return _soft;
            }
        } private Soft _soft;
        #endregion //Soft

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        public void Process(Exception ex, string msg)
        {
            this.Process(ex, msg, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void Process(Exception ex)
        {
            this.Process(ex, string.Empty, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="exit"></param>
        public void Process(Exception ex, string msg, bool exit)
        {
            Error e = new Error(ex, msg);
            this.Errors.Add(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public ErrorCollection Errors
        {
            get
            {
                if (_errors == null)
                {
                    _errors = new ErrorCollection();
                }
                return _errors; 
            }
        } private ErrorCollection _errors;
    }

    /// <summary>
    /// 
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        } private Exception _exception;

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get { return _dt; }
        }private DateTime _dt;

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
        } private string _message;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public Error(Exception ex)
        {

            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            this._exception = ex;
            this._dt = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public Error(Exception ex, string message)
            : this ( ex )
        {
            this._message = message;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ErrorCollection : Collection<Error>
    {

    }
}
