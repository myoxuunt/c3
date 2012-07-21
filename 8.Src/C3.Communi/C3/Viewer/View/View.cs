using System;
using System.Windows.Forms;

namespace C3
{

    abstract public class View
    {

        #region View
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        protected View(Control parentControl)
        {
            this._parentControl = parentControl;
        }
        #endregion //View

        #region ParentControl
        /// <summary>
        /// 
        /// </summary>
        public Control ParentControl
        {
            get { return _parentControl; }
        } private Control _parentControl;
        #endregion //ParentControl

        #region ViewControl
        /// <summary>
        /// 
        /// </summary>
        abstract public Control ViewControl { get; set; } 
        #endregion //ViewControl
    }

}
