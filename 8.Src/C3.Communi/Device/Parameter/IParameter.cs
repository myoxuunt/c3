using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    public interface IParameter : IOrderNumber 
    {
        ParameterGroup Group { get; set; }
        //int OrderNumber { get; set; }
        string Name { get; set; }
        string Text { get; set; }
        object Value { get; set; }
        void SetValue(string stringValue);
        Type ValueType { get; set; }
        string Description { get; set; }
        Unit Unit { get; set; }
        // TODO: 2012-08-03
        //
        // ParameterOption Option{get;set;}

        IParameterUI ParameterUI { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IParameterUI
    {
        Control Control { get; set; }
        IParameter Parameter { get; set; }
        void ApplyNewValue();
    }

    /// <summary>
    /// 
    /// </summary>
    public class ParameterUICollection : Collection<IParameterUI>
    {
    }

    /// <summary>
    /// 
    /// </summary>
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
    }

    /// <summary>
    /// 
    /// </summary>
    public class ParameterUI : ParameterUIBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnSetParameter(IParameter parameter)
        {
            UCParameterUI paramCtrl = new UCParameterUI();
            paramCtrl.Parameter = parameter;
            this.Control = paramCtrl;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplyNewValue()
        {
            UCParameterUI paramCtrl = this.Control as UCParameterUI;
            paramCtrl.ApplyNewValue();
        }
    }
}
