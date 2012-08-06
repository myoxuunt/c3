using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
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
                if (_groupUI == null)
                {
                    _groupUI = new GroupUI();
                }
                return _groupUI;
            }
            set
            {
                _groupUI = value;
            }
        } private IGroupUI _groupUI;
        #endregion //GroupUI

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    _text = this.Name;
                }
                return _text;
            }
            set
            {
                _text = value;
            }
        } private string _text;
        #endregion //Text

        #region Parameters
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
        #endregion //Parameters
    }
}
