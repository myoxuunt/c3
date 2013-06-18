using System;
using System.Threading;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortFactory
    {

        private const int SLEEP_TIME = 500;

        /// <summary>
        /// 
        /// </summary>
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
        private bool _startFlag = true;

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
            if (_startFlag == true)
            {
                if (_thread == null)
                {
                    this._startFlag = true;

                    _thread = new Thread(new ThreadStart(Target));
                    _thread.Start();
                }
            }
            else
            {
                throw new InvalidOperationException(
                    "cannot start communiport factory while start flag is false");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            // TODO: b == false ?? when exit app by notify icon exit menu
            //
            //bool b = (this._thread != null &&
            //       this._thread.ThreadState == ThreadState.Running);

            this._startFlag = false;
        }

        /// <summary>
        /// 
        /// </summary>
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
            while (this._startFlag)
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
                        if (success)
                        {
                            this._cpCfgs.Remove(cfg);
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
    }
}
