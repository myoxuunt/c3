using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    public interface IGroup : IOrderNumber
    {
        string Name { get; set; }
        IGroupUI GroupUI { get; set; }
        ParameterCollection Parameters { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ParameterGroup : IGroup 
    {
        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region OrderNumber
        /// <summary>
        /// 
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                _orderNumber = value;
            }
        } private int _orderNumber;
        #endregion //OrderNumber

        #region GroupUI
        /// <summary>
        /// 
        /// </summary>
        public IGroupUI GroupUI
        {
            get
            {
                //if (_groupUI == null)
                //{
                //    _groupUI = new IGroupUI();
                //}
                return _groupUI;
            }
            set
            {
                _groupUI = value;
            }
        } private IGroupUI _groupUI;
        #endregion //GroupUI




        /// <summary>
        /// 
        /// </summary>
        public ParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new ParameterCollection();
                }
                return _parameters;
            }
        } private ParameterCollection _parameters;
    }

    public interface IGroupUI
    {
        Control Control { get; set; }
        IGroup Group{ get; set; }
        ParameterUICollection ParameterUIs { get; set; }
        void ApplyNewValue();
    }

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
                _group = value;
            }
        } private IGroup _group;

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

    /// <summary>
    /// 
    /// </summary>
    public class GroupUI : GroupUIBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplyNewValue()
        {
            IGroup group = this.Group;
            foreach (IParameter item in this.Group.Parameters)
            {
                item.ParameterUI.ApplyNewValue();
            }
        }
    }

}
