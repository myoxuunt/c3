using System;
using System.Threading;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CPCreateLog
    {
        public CPCreateLog(DateTime dt, string log)
        {
            this._dt = dt;
            this._log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get { return _dt; }
        } private DateTime _dt;

        /// <summary>
        /// 
        /// </summary>
        public string Log
        {
            get { return _log; }
        } private string _log;
    }

}
