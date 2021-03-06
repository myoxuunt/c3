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

        #region IViewer ��Ա

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

    public class EnumParameterViewer : IViewer
    {

        public EnumParameterViewer(EnumParameterController c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("c");
            }
            _c = c;
        }
        #region IViewer ��Ա

        public Control UC
        {
            get { return _uc; }
        } private UCEnumParameterUI _uc = new UCEnumParameterUI();

        public IController Controller
        {
            get
            {
                return _c;
            }
            set
            {
                throw new NotImplementedException();
            }
        } private EnumParameterController _c;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Type ValueType
        {
            get { return _uc.ValueType; }
            set { _uc.ValueType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object EnumValue
        {
            get { return _uc.EnumValue; }
            set { _uc.EnumValue = value; }
        }

        public string ParameterName
        {
            get { return _uc.ParameterName; }
            set { _uc.ParameterName = value; }
        }

        public string Unit
        {
            get { return _uc.Unit; }
            set { _uc.Unit = value; }
        }
    }

}
