
using System;

namespace C3.Communi
{
    public class CommuniPortConfigController : IController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public CommuniPortConfigController(CommuniPortConfigParameter p)
        {
            this.Model = p;
        }

        #region IController ≥…‘±

        public IModel Model
        {
            get
            {
                return this._communiPortConfigParameter;
            }
            set
            {
                this._communiPortConfigParameter = (CommuniPortConfigParameter)value;
            }
        }
        private CommuniPortConfigParameter _communiPortConfigParameter;

        public IViewer Viewer
        {
            get
            {
                if (_v == null)
                {
                    _v = new CommuniPortConfigViewer();
                    _v.Controller = this;
                }
                return _v;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
        private CommuniPortConfigViewer _v;

        public void UpdateModel()
        {
            //throw new NotImplementedException();
            //this.CommuniPortConfigParameter .CommuniPortConfig = this.CommuniPortConfigUI .
            UCCommuniPortConfigUI ui = (UCCommuniPortConfigUI)_v.UC;
            CommuniPortConfigParameter p = (CommuniPortConfigParameter)this.Model;
            p.CommuniPortConfig = ui.CommuniPortConfig;
        }

        private CommuniPortConfigParameter CommuniPortConfigParameter
        {
            get { return (CommuniPortConfigParameter)this.Model; }
        }
        /// <summary>
        /// 
        /// </summary>
        private UCCommuniPortConfigUI CommuniPortConfigUI
        {
            get
            {
                return (UCCommuniPortConfigUI)this._v.UC;
            }
        }

        public void UpdateViewer()
        {
            UCCommuniPortConfigUI ui = (UCCommuniPortConfigUI)_v.UC;
            CommuniPortConfigParameter p = (CommuniPortConfigParameter)this.Model;
            ui.CommuniPortConfig = p.CommuniPortConfig;
        }

        public bool Verify()
        {
            //throw new NotImplementedException();
            return this._v.Verify();
        }

        #endregion
    }

}
