using System;
using System.Threading;
using System.Diagnostics;
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

        public event CommuniPortEventHandler ClosedCommuniPort;

        public event CommuniPortEventHandler ReceivedCommuniPort;
        #endregion //Events

        #region CommuniPortManager
        /// <summary>
        /// 
        /// </summary>
        public CommuniPortManager()
        {
            CommuniPortFactory.Default.CommuniPortCreated += new CommuniPortCreatedEventHandler(Default_CommuniPortCreated);
        }
        #endregion //CommuniPortManager

        private void Default_CommuniPortCreated(object sender, CommuniPortCreatedEventArgs e)
        {
            if (e.Success)
            {
                ICommuniPort cp = e.CommuniPort;
                this.Add(cp);
            }

            this.CreateCommuniPortResults.Add(new CreateCommuniPortResult(
                e.Success, e.CommuniPortConfig, "", e.Exception));

            _log.Info(string.Format("CreateCommuniPort '{0}' is '{1}'", e.CommuniPortConfig, e.Success));
        }

        #region Properties
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
                    //_filters = new FilterCollection();
                    _filters = FilterFactory.CreateFromConfigFile();
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

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
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

            //StationCommuniPortBinder.Bind(cp, this.Soft.Hardware);

            //
            //
            if (AddedCommuniPort != null)
            {
                AddedCommuniPort(this, new CommuniPortEventArgs(cp));
            }
        }
        #endregion //Add

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

            //Hardware hd = this.Soft.Hardware;
            //StationCommuniPortBinder.Unbind(cp, hd);

            bool r = this.CommuniPorts.Remove(cp);

            //
            //
            if (RemovedCommuniPort != null)
            {
                RemovedCommuniPort(this, new CommuniPortEventArgs(cp));
            }
            return r;
        }
        #endregion //

        #region ProcessReceived
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cp_Received(object sender, EventArgs e)
        {
            if (ReceivedCommuniPort != null)
            {
                ReceivedCommuniPort(this, new CommuniPortEventArgs((ICommuniPort)sender));
            }

            /*
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
                        //IUploadParseResult parseResult = processor.ProcessUpload(d, bs);
                        //bs = parseResult.Remain;
                        d.ProcessUpload(bs);
                    }
                }
            }
*/
        }
        #endregion //ProcessReceived

        #region cp_Closed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cp_Closed(object sender, EventArgs e)
        {
            if (this.ClosedCommuniPort != null)
            {
                this.ClosedCommuniPort(this, new CommuniPortEventArgs((ICommuniPort)sender));
            }

            ICommuniPort cp = sender as ICommuniPort;
            this.Remove(cp);
        }
        #endregion //cp_Closed

        #region ProcessDetermined
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cp_Determined(object sender, EventArgs e)
        {

            if (this.DeterminedCommuniPort != null)
            {
                DeterminedCommuniPort(this, new CommuniPortEventArgs((ICommuniPort)sender));
            }

            /*
            ICommuniPort cp = sender as ICommuniPort;

            Hardware hd = this.Soft.Hardware;

            StationCommuniPortBinder.Bind(cp, hd);
             */
        }
        #endregion //ProcessDetermined

        #region GetCommuniPort
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpCfg"></param>
        /// <returns></returns>
        public ICommuniPort GetOrCreateCommuniPort(ICommuniPortConfig cpCfg)
        {
            if (cpCfg == null)
            {
                throw new ArgumentNullException("cpCfg");
            }

            ICommuniPort r = null;

            foreach (ICommuniPort cp in this.CommuniPorts)
            {
                if (cpCfg.IsMatch(cp))
                {
                    r = cp;
                    break;
                }
            }

            if (r == null)
            {
                if (cpCfg.CanCreate)
                {
                    CommuniPortFactory f = CommuniPortFactory.Default;
                    bool added = f.Add(cpCfg);
                    _log.Info("add " + cpCfg + " to communiport factory: " + added);
                }
            }

            return r;
        }
        #endregion //GetCommuniPort
        #endregion //Methods

        #region CreateCommuniPortResults
        /// <summary>
        /// 
        /// </summary>
        public CreateCommuniPortResultCollection CreateCommuniPortResults
        {
            get
            {
                if (_createCommuniPortResults == null)
                {
                    _createCommuniPortResults = new CreateCommuniPortResultCollection();
                }
                return _createCommuniPortResults;
            }
        } private CreateCommuniPortResultCollection _createCommuniPortResults;
        #endregion //CreateCommuniPortResults

        //private void CreateWithThread(ICommuniPortConfig cpCfg)
        //{
        //    CreateCommuniPortResult createResult = null;
        //    CommuniPortFactory f = CommuniPortFactory.Default;
        //    f.Start ();
        //    if (f.Success)
        //    {
        //        this.Add(f.ResultCommuniPort);
        //        createResult = new CreateCommuniPortResult(true, cpCfg,
        //            "create success", null);
        //    }
        //    else
        //    {
        //        createResult = new CreateCommuniPortResult(true, cpCfg,
        //            "create fail", f.Exception);
        //    }
        //        this.CreateCommuniPortResults.Add(createResult);
        //    //Thread t = new Thread(f.Start());
        //    //try
        //    //{
        //    //    t.Start();
        //    //}
        //    //catch 

        //}
    }
}
