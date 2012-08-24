
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class GroupController : IController
    {
        #region IController ≥…‘±

        public IModel Model
        {
            get
            {
                return _group;
            }
            set
            {
                _group = (IGroup)value;
                foreach (IController ctrl in this.Controllers)
                {
                    ((GroupViewer)this.Viewer).AddViewer(ctrl.Viewer);
                }
                //UpdateViewer();
            }
        } private IGroup _group;

        public IGroup Group
        {
            get { return this.Model as IGroup; }
            set { this.Model = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public ControllerCollection Controllers
        {
            get
            {
                if (_controllers == null)
                {
                    _controllers = new ControllerCollection();
                    foreach (IParameter p in this._group.Parameters)
                    {
                        IController c = ControllerFactory.Create(p);
                        _controllers.Add(c);
                    }
                }
                return _controllers;
            }
        } private ControllerCollection _controllers;

        /// <summary>
        /// 
        /// </summary>
        public IViewer Viewer
        {
            get
            {
                if (_groupViewer == null)
                {
                    _groupViewer = new GroupViewer();
                    _groupViewer.Controller = this;
                }
                return _groupViewer;
            }
            set
            {
                throw new NotSupportedException();
            }
        } private GroupViewer _groupViewer;

        public void UpdateModel()
        {
            //throw new NotImplementedException();
            foreach (IController item in this.Controllers)
            {
                item.UpdateModel();
            }
        }

        public void UpdateViewer()
        {
            foreach (IController item in this.Controllers)
            {
                item.UpdateViewer();
            }
        }

        public bool Verify()
        {
            bool r = true;
            foreach (IController item in this.Controllers)
            {
                if (!item.Verify())
                {
                    r = false;
                    break;
                }
            }
            return r;
        }

        #endregion
    }

}
