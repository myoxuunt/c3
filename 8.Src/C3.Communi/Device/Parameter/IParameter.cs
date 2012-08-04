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
    abstract public class ParameterUI : IParameterUI
    {
        /// <summary>
        /// 
        /// </summary>
        public Control Control
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
        public IParameter Parameter
        {
            get
            {
                return _parameter ;
            }
            set
            {
                _parameter = value;
            }
        } private IParameter _parameter;

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
}
