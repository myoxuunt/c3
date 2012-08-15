using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCNumberParameterUI : UserControl
    {

        #region UCNumberParameterUI
        /// <summary>
        /// 
        /// </summary>
        public UCNumberParameterUI()
        {
            InitializeComponent();
        }
        #endregion //UCNumberParameterUI

        #region ParameterName
        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }
        #endregion //ParameterName

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get
            {
                object obj = Convert.ChangeType(this.txtValue.Text.Trim(), this.ValueType);
                return obj;
            }
            set { this.txtValue.Text = value.ToString(); }
        }
        #endregion //Value

        #region ValueType
        /// <summary>
        /// 
        /// </summary>
        public Type ValueType
        {
            get { return _valueType; }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ValueType");
                }
                _valueType = value; 
            }
        } private Type _valueType;
        #endregion //ValueType

        #region Unit
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            get { return this.lblUnit.Text; }
            set { this.lblUnit.Text = value; }
        }
        #endregion //Unit

        #region Verify
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            bool r = false;
            string txt = this.txtValue.Text.Trim();
            if (txt.Length > 0)
            {
                try
                {
                    object obj = Convert.ChangeType(txt, this.ValueType);
                    r = true;
                }
                catch (Exception ex)
                {
                    NUnit.UiKit.UserMessage.DisplayFailure(ex.Message);
                }
            }
            return r;
        }
        #endregion //Verify
    }
}
