using System;
using System.Threading;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CommuniPortCreatedEventHandler(object sender, CommuniPortCreatedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="success"></param>
        /// <param name="cp"></param>
        /// <param name="ex"></param>
        public CommuniPortCreatedEventArgs(ICommuniPortConfig cfg, bool success, ICommuniPort cp, Exception ex)
        {
            this._communiPortConfig = cfg;
            this._success = success;
            this._communiPort = cp;
            this._exception = ex;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPortConfig CommuniPortConfig
        {
            get { return _communiPortConfig; }
        } private ICommuniPortConfig _communiPortConfig;

        /// <summary>
        /// 
        /// </summary>
        public bool Success
        {
            get { return _success; }
        } private bool _success;

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get { return _communiPort; }
        } private ICommuniPort _communiPort;

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
        } private Exception _exception;
    }
}
