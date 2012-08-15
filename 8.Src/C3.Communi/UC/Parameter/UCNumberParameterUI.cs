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
        public UCNumberParameterUI()
        {
            InitializeComponent();
        }


        public string ParameterName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

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

        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            get { return this.lblUnit.Text; }
            set { this.lblUnit.Text = value; }
        }

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
    }
}
