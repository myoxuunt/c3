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

        public object Value
        {
            get { return this.txtValue.Text; }
            set { this.txtValue.Text = value.ToString (); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        } private Type _valueType;

        public string Unit
        {
            get { return this.lblUnit.Text; }
            set { this.lblUnit.Text = value; }
        }
    }
}
