
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

        #region IController ≥…‘±

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
            //throw new NotImplementedException();
            this._v.ParameterName = _n.Name;
            this._v.ValueType = _n.ValueType;
            this._v.Value = _n.Value;
            this._v.Unit = _n.Unit.Text;
        }

        public bool Verify()
        {
            //throw new NotImplementedException();
            return this._v.Verify();
        }

        #endregion
    }

}
