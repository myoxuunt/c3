
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class NumberParameterViewer : IViewer
    {
        public NumberParameterViewer(NumberParameterController c)
        {
            this.Controller = c;
        }

        #region IViewer ≥…‘±

        public Control UC
        {
            get { return _uc; }
        } private UCNumberParameterUI _uc = new UCNumberParameterUI();

        public IController Controller
        {
            get
            {
                return _c;
            }
            set
            {
                _c = (NumberParameterController)value;
            }
        }
        private NumberParameterController _c;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get { return this._uc.ParameterName; }
            set { this._uc.ParameterName = value; }
        }

        public Type ValueType
        {
            get { return this._uc.ValueType; }
            set { this._uc.ValueType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get { return this._uc.Value; }
            set { this._uc.Value = value; }
        }

        public string Unit
        {
            get { return this._uc.Unit; }
            set { this._uc.Unit = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Verify()
        {
            return this._uc.Verify();
        }
    }

}
