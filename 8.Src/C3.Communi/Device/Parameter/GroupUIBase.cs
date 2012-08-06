
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    abstract public class GroupUIBase : IGroupUI
    {
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
        }private Control _control;

        public IGroup Group
        {
            get
            {
                return _group;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Group");
                }

                if (_group != value)
                {
                    _group = value;
                    OnGroupChanged();
                }
            }
        } private IGroup _group;

        /// <summary>
        /// 
        /// </summary>
        abstract protected void OnGroupChanged();

        public void ApplyNewValue()
        {
            OnApplyNewValue();
        }

        /// <summary>
        /// 
        /// </summary>
        abstract protected void OnApplyNewValue();



#region IGroupUI ≥…‘±


        public ParameterUICollection ParameterUIs
        {
            get
            {
                return _parameterUIs;
            }
            set
            {
                _parameterUIs = value;
            }
        } private ParameterUICollection _parameterUIs; 

#endregion
    }

}
