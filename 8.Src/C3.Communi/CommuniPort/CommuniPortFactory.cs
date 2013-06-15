using System;
using System.Threading;

namespace C3.Communi
{
    public delegate void CommuniPortCreatedEventHandler(object sender, CommuniPortCreatedEventArgs e);


    public class CommuniPortCreatedEventArgs : EventArgs
    {
        public CommuniPortCreatedEventArgs(ICommuniPortConfig cfg, bool success, ICommuniPort cp, Exception ex)
        {
            this._communiPortConfig = cfg;
            this._success = success;
            this._communiPort = cp;
            this._exception = ex;
        }

        public ICommuniPortConfig CommuniPortConfig
        {
            get { return _communiPortConfig; }
        } private ICommuniPortConfig _communiPortConfig;

        public bool Success
        {
            get { return _success; }
        } private bool _success;

        public ICommuniPort CommuniPort
        {
            get { return _communiPort; }
        } private ICommuniPort _communiPort;

        public Exception Exception
        {
            get { return _exception; }
        } private Exception _exception;
    }

    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortFactory
    {
        static public CommuniPortFactory Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new CommuniPortFactory();
                    _default.Start();
                }
                return _default;
            }
        } static private CommuniPortFactory _default;
        public event CommuniPortCreatedEventHandler CommuniPortCreated;

        private CommuniPortConfigCollection _cpCfgs = new CommuniPortConfigCollection();
        private Thread _thread = null;

        private CommuniPortFactory()
        {
            //this._cpCfg = cpCfg;
        }

        public bool Add(ICommuniPortConfig cfg)
        {
            if (!_cpCfgs.Contains(cfg))
            {
                this._cpCfgs.Add(cfg);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (_thread == null)
            {
                _thread = new Thread(new ThreadStart(Target));
                _thread.Start();
            }
        }

        public bool IsStarted
        {
            get
            {
                return this._thread != null &&
                _thread.ThreadState == ThreadState.Running;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void Target()
        {
            while (true)
            {
                for (int i = _cpCfgs.Count - 1; i >= 0; i--)
                {
                    ICommuniPortConfig cfg = _cpCfgs[i];


                    if (cfg.CanCreate)
                    {                    
                        ICommuniPort cp = null;
                        bool success = false;
                        Exception ex = null;

                        try
                        {
                            cp = cfg.Create();
                            success = true;
                        }
                        catch (Exception e)
                        {
                            ex= e;
                            success = false;
                        }

                        if (CommuniPortCreated != null)
                        {
                            CommuniPortCreated ( this, 
                                new CommuniPortCreatedEventArgs (
                                    cfg, success, cp, ex));
                        }
                    }
                }
                Thread.Sleep(SLEEP_TIME);
            }
        }

        private const int SLEEP_TIME = 100;


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="cpConfig"></param>
        ///// <returns></returns>
        //public ICommuniPort Create(ICommuniPortConfig cpConfig)
        //{
        //    if (cpConfig == null)
        //    {
        //        throw new ArgumentNullException("cpConfig");
        //    }

        //    if (!cpConfig.CanCreate)
        //    {
        //        string s = string.Format(
        //            "cannot create form communiPortConfig: '{0}'",
        //            cpConfig.ToString());
        //        throw new InvalidOperationException(s);
        //    }

        //    ICommuniPort cp = cpConfig.Create();
        //    return cp;

        //}
    }
}
