
using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class ViewerBase
    {
#region View
        /// <summary>
        /// 
        /// </summary>
        public IView View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
            }
        } private IView _view;
#endregion //View

        protected Control _ctrl;
        protected Panel _panel;
        protected ViewerBase(Panel panel)
        {
            _panel = panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        protected void AddUCViewerToPanel(Control ctrl)
        {
            Console.WriteLine("add ucview: " + ctrl.GetType ().Name );
            ctrl.Dock = DockStyle.Fill;
            this._panel.Controls.Add(ctrl);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowMeOnly()
        {
            foreach (Control item in this._panel.Controls)
            {
                item.Visible = false;
            }
            _ctrl.Visible = true;
        }
    }

}
