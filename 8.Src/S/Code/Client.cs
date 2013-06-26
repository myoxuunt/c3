using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{

    public class Client : ITag
    {
        // TODO: unregister received event
        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="communiPort"></param>
        public Client(ICommuniPort communiPort)
        {
            this.CommuniPort = communiPort;
            this.CommuniPort.Received += new EventHandler(CommuniPort_Received);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CommuniPort_Received(object sender, EventArgs e)
        {
            ICommuniPort cp = (ICommuniPort)sender;
            byte[] bs = cp.Read();

            RequestProcessManager.Default.Process(this, bs);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get { return _communiPort; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CommuniPort");
                }
                _communiPort = value;
            }
        } private ICommuniPort _communiPort;

        #region ITag ≥…‘±

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
        #endregion

        #region LogItems
        /// <summary>
        /// 
        /// </summary>
        public LogItemCollection LogItems
        {
            get
            {
                if (_logItems == null)
                {
                    _logItems = new LogItemCollection();
                }
                return _logItems;
            }
            set
            {
                _logItems = value;
            }
        } private LogItemCollection _logItems;
        #endregion //LogItems

    }
}
