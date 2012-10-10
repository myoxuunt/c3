
using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    abstract public class Controller
    {
        protected Controller(View v)
        {
            this.View = v;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public IView View
        //{
        //    get
        //    {
        //        return _view;
        //    }
        //    set
        //    {
        //        _view = value;
        //    }
        //} private IView _view;
        /*
        protected Control _ctrl;
        protected Panel _panel;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        protected Controller(Panel panel)
        {
            _panel = panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        protected void AddUCViewerToPanel(Control ctrl)
        {
            Console.WriteLine("add ucview: " + ctrl.GetType().Name);
            ctrl.Dock = DockStyle.Fill;
            this._panel.Controls.Add(ctrl);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowMeOnly()
        {
            foreach (Control this1 in this._panel.Controls)
            {
                this1.Visible = false;
            }
            _ctrl.Visible = true;
        }
         */

        #region Model
        /// <summary>
        /// 
        /// </summary>
        public Model Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnSetModel();
            }
        }private Model _model;
        #endregion //Model

        abstract protected void OnSetModel();

        #region View
        /// <summary>
        /// 
        /// </summary>
        public View View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
            }
        } private View _view;
        #endregion //View


    }

}
