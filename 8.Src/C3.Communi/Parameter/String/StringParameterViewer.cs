
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class StringParameterViewer : IViewer
    {
        private UCStringParameterUI _uc = new UCStringParameterUI();

        #region IViewer 成员
        /// <summary>
        /// 
        /// </summary>
        public IController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                _controller = value;
            }
        } private IController _controller;

        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get { return this._uc.ParameterName; }
            set { this._uc.ParameterName = value; }
        }

        public string Value
        {
            get { return this._uc.Value; }
            set { this._uc.Value = value; }
        }

        public string Unit
        {
            get { return this._uc.Unit; }
            set { this._uc.Unit = value; }
        }

        #endregion

        #region IViewer 成员

        public Control UC
        {
            get
            {
                return _uc;
            }
        }

        #endregion
    }

}
