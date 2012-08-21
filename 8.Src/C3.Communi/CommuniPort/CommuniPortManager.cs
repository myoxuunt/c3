using System;
using System.Collections.Generic;
using NLog;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortManager
    {
        /// <summary>
        /// 
        /// </summary>
        static private Logger _log = LogManager.GetCurrentClassLogger();

        #region Events
        public event CommuniPortEventHandler AddedCommuniPort;

        public event CommuniPortEventHandler RemovedCommuniPort;

        public event CommuniPortEventHandler DeterminedCommuniPort;
        #endregion //Events

        #region Constructor
        #region CommuniPortManager
        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        public CommuniPortManager(Soft soft)
        {
            this.Soft = soft;
        }
        #endregion //CommuniPortManager
        #endregion //


        #region Properties

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

        #region CommuniPortFactory
        /// <summary>
        /// 
        /// </summary>
        public CommuniPortFactory CommuniPortFactory
        {
            get
            {
                if (_communiPortFactory == null)
                {
                    _communiPortFactory = new CommuniPortFactory();
                }
                return _communiPortFactory;
            }
            set
            {
                _communiPortFactory = value;
            }
        } private CommuniPortFactory _communiPortFactory;
        #endregion //CommuniPortFactory

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
        public IdentityParserCollection IdentityParsers
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
        #endregion //Properties


        #region Methods

        #region CreateCommuniPort
        public ICommuniPort CreateCommuniPort(ICommuniPortConfig cpConfig)
        {
            ICommuniPort cp =null;
            try
            {
                cp = this.CommuniPortFactory.Create(cpConfig);
            }
            catch (Exception ex)
            {
                // TODO: log ERROR
                //
                _log.Fatal(ex);
            }

            if (cp != null)
            {
                this.Add(cp);
            }
            return cp;
        }
        #endregion //

        #region Add
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
            //
            cp.Filters = this.Filters;
            cp.IdentityParsers = this.IdentityParsers;

            // register cp event
            //
            cp.Received += new EventHandler(cp_Received);
            cp.Determined += new EventHandler(cp_Determined);
            cp.Closed += new EventHandler(cp_Closed);

            StationCommuniPortBinder.Bind(cp, this.Soft.Hardware);
        }
        #endregion //Add

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cp_Received(object sender, EventArgs e)
        {
            ICommuniPort cp = (ICommuniPort)sender;
            byte[] bs = cp.Read();

            // TODO: cp.stations
            //
            StationCollection stations = new StationCollection();
            foreach (IStation st in this.Soft.Hardware.Stations)
            {
                if (st.CommuniPortConfig.IsMatch(cp))
                {
                    stations.Add(st);
                }
            }

            foreach (IStation st in stations)
            {
                foreach (IDevice d in st.Devices)
                {
                    if (bs.Length > 0)
                    {
                        //ITaskProcessor processor = d.Dpu.Processor;
                        //IUploadParseResult pr = processor.ProcessUpload(d, bs);
                        //bs = pr.Remain;
                        d.ProcessUpload(bs);
                    }
                }
            }

        }

        #region Remove
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public bool Remove(ICommuniPort cp)
        {
            if (cp == null)
            {
                throw new ArgumentNullException("cp");
            }

            // remove cp event handler
            //
            cp.Determined -= new EventHandler(cp_Determined);
            cp.Closed -= new EventHandler(cp_Closed);

            Hardware hd = this.Soft.Hardware;
            StationCommuniPortBinder.Unbind(cp, hd);

            return this.CommuniPorts.Remove(cp);
        }
        #endregion //

        #region cp_Closed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cp_Closed(object sender, EventArgs e)
        {
            ICommuniPort cp = sender as ICommuniPort;

            this.Remove(cp);
        }
        #endregion //cp_Closed

        #region cp_Determined
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
        #endregion //cp_Determined


        #endregion //Methods
    }

}
