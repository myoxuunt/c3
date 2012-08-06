
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    abstract public class ParameterUIBase : IParameterUI
    {
        /// <summary>
        /// 
        /// </summary>
        virtual public Control Control
        {
            get
            {
                return _control;
            }
            set
            {
                _control = value;
            }
        } private Control _control;

        /// <summary>
        /// 
        /// </summary>
        virtual public IParameter Parameter
        {
            get
            {
                return _parameter ;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Parameter");
                }
                if (_parameter != value)
                {
                    _parameter = value;
                    OnSetParameter(_parameter);
                }
            }
        } private IParameter _parameter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        abstract protected void OnSetParameter(IParameter parameter);

        /// <summary>
        /// 
        /// </summary>
        public void ApplyNewValue()
        {
            OnApplyNewValue();
        }

        /// <summary>
        /// 
        /// </summary>
        abstract protected void OnApplyNewValue();

        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Size Size
        {
            get { return this.Control.Size; }
        }
    }

}
