
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class CommuniPortConfigViewer : IViewer
    {

        #region IViewer ≥…‘±

        public Control UC
        {
            get { return _uc; }
        } private Control _uc = new UCCommuniPortConfigUI();

        /// <summary>
        /// 
        /// </summary>
        public IController Controller
        {
            get
            {
                return _c;
            }
            set
            {
                _c = (CommuniPortConfigController)value;
            }
        } private CommuniPortConfigController _c;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Verify()
        {
            return ((UCCommuniPortConfigUI)_uc).Verify();
        }
    }

}
