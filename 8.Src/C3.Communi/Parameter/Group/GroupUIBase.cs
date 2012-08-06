using System;
using System.Windows.Forms;

namespace C3.Communi
{
    abstract public class GroupUIBase : IGroupUI
    {
        #region GroupUIBase
        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        protected GroupUIBase(IGroup group)
        {
            this.Group = group;
        }
        #endregion //GroupUIBase

        #region Control
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
        }private Control _control;
        #endregion //Control

        #region Group
        /// <summary>
        /// 
        /// </summary>
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
                    OnSetGroup();
                }
            }
        } private IGroup _group;
        #endregion //Group

        #region OnGroupChanged
        /// <summary>
        /// 
        /// </summary>
        abstract protected void OnSetGroup();
        #endregion //OnGroupChanged

        #region ApplyNewValue
        public void ApplyNewValue()
        {
            OnApplyNewValue();
        }
        #endregion //ApplyNewValue

        #region OnApplyNewValue
        /// <summary>
        /// 
        /// </summary>
        abstract protected void OnApplyNewValue();
        #endregion //OnApplyNewValue

        #region ParameterUIs
        /// <summary>
        /// 
        /// </summary>
        public ParameterUICollection ParameterUIs
        {
            // TODO: 2012-08-06 not implement
            // 
            //
            get
            {
                return _parameterUIs;
            }
            set
            {
                _parameterUIs = value;
            }
        } private ParameterUICollection _parameterUIs;
        #endregion //ParameterUIs

    }

}
