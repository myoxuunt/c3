using System;
using System.Threading;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortFactory
    {
        private ICommuniPortConfig _cpCfg;
        public CommuniPortFactory(ICommuniPortConfig cpCfg)
        {
            this._cpCfg = cpCfg;
        }

        public void Doit()
        {
            Thread t = new Thread(target );
            t.Start();
        }

        void target()
        {
            try
            {
                _resultCommuniPort = this._cpCfg.Create();
                _success = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        public bool Success
        {
            get { return _success; }
        } private bool _success;

        public ICommuniPort ResultCommuniPort
        {
            get { return _resultCommuniPort; }
        } private ICommuniPort _resultCommuniPort;

        public Exception Exception
        {
            get { return _exception; }
        } private Exception _exception;
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
