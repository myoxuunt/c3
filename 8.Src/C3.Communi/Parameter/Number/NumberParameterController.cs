
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class NumberParameterController : IController
    {
        public NumberParameterController(NumberParameter p)
        {
            this.Model = p;
        }

        #region IController 成员

        public IModel Model
        {
            get
            {
                return _n;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Model");
                }
                _n = (NumberParameter)value;
            }
        }
        private NumberParameter _n;

        public IViewer Viewer
        {
            get
            {
                if (_v == null)
                {
                    _v = new NumberParameterViewer(this);
                }
                return _v;
            }
            set
            {
                throw new NotSupportedException();
            }
        } private NumberParameterViewer _v;

        public void UpdateModel()
        {
            this._n.Value = this._v.Value;
        }

        public void UpdateViewer()
        {
            this._v.ParameterName = _n.Text + ":";
            this._v.ValueType = _n.ValueType;
            this._v.Value = _n.Value;
            this._v.Unit = _n.Unit.Text;
        }

        public bool Verify()
        {
            return this._v.Verify();
        }

        #endregion
    }

    public class EnumParameterController : IController
    {

        public EnumParameterController(EnumParameter m)
        {
            if (m == null)
            {
                throw new ArgumentNullException("m");
            }
            _m = m;
        }
        #region IController 成员

        public IModel Model
        {
            get
            {
                return _m;
            }
            set
            {
                _m = (EnumParameter)value;
            }
        } private EnumParameter _m;

        public IViewer Viewer
        {
            get
            {
                if (_v == null)
                {
                    _v = new EnumParameterViewer(this);
                }
                return _v;
            }
            set
            {
                throw new NotSupportedException();
            }
        } private EnumParameterViewer _v;

        public void UpdateModel()
        {
            this._m.Value = _v.EnumValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateViewer()
        {
            this._v.ParameterName = _m.Text + ":";
            this._v.ValueType = _m.ValueType;
            this._v.EnumValue = _m.Value;
            this._v.Unit = _m.Unit.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            return true;
        }

        #endregion
    }
}
