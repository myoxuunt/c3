
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class GroupViewer : IViewer
    {

        #region IViewer 成员

        public IController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                this._controller = (GroupController)value;
            }
        } private GroupController _controller;

        #endregion

        #region IViewer 成员

        public Control UC
        {
            get { return _uc; }
        }
        private UCGroupUI _uc = new UCGroupUI();


        public void AddViewer(IViewer viewer)
        {
            this._uc.AddControl(viewer.UC);
        }
        #endregion
    }

}
