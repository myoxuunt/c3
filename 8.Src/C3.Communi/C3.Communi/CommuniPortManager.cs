using System;
using System.Collections.Generic;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortManager
    {

        public event CommuniPortEventHandler  AddedCommuniPort;

        public event CommuniPortEventHandler RemovedCommuniPort;

        public event CommuniPortEventHandler DeterminedCommuniPort;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        public CommuniPortManager(Soft soft)
        {
            this.Soft = soft;
        }

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
            set
            {
                _soft = value;
            }
        } private Soft _soft;
        #endregion //Soft

        #region CommuniPorts
        /// <summary>
        /// 
        /// </summary>
        public CommuniPortCollection CommuniPorts
        {
            get
            {
                if (_communiPorts == null)
                {
                    _communiPorts = new CommuniPortCollection();
                }
                return _communiPorts;
            }
            set
            {
                _communiPorts = value;
            }
        } private CommuniPortCollection _communiPorts;
        #endregion //CommuniPorts

        #region Filters
        /// <summary>
        /// 
        /// </summary>
        public FilterCollection Filters
        {
            get 
            {
                if (_filters == null)
                {
                    _filters = new FilterCollection();
                }
                return _filters;
            }
            set { _filters = value; }
        } private FilterCollection _filters;
        #endregion //

        #region IdentityParsers
        /// <summary>
        /// 
        /// </summary>
        public IdentityParserCollection  IdentityParsers
        {
            get
            {
                if (_identityParsers == null)
                {
                    _identityParsers = new IdentityParserCollection();
                }
                return _identityParsers;
            }
            set
            {
                _identityParsers = value;
            }
        } private IdentityParserCollection _identityParsers;
        #endregion //Identities



        public void Add(ICommuniPort cp)
        {
            if (cp == null)
            {
                throw new ArgumentNullException("cp");
            }

            // TODO: exist cp remove
            //
            this.CommuniPorts.Add(cp);

            // 
            cp.Filters = this.Filters;
            cp.IdentityParsers = this.IdentityParsers;

            // TODO: register cp event
            //
            //cp.Received += new EventHandler(cp_Received);
            cp.Determined += new EventHandler(cp_Determined);
            cp.Closed += new EventHandler(cp_Closed);

            StationCommuniPortBinder.Bind(cp, this.Soft.Hardware);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cp_Closed(object sender, EventArgs e)
        {
            ICommuniPort cp = sender as ICommuniPort;
            Hardware hd = this.Soft.Hardware;
            StationCommuniPortBinder.Unbind(cp, hd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cp_Determined(object sender, EventArgs e)
        {
            ICommuniPort cp = sender as ICommuniPort;

            Hardware hd = this.Soft.Hardware;

            StationCommuniPortBinder.Bind(cp, hd);
        }
    }

}