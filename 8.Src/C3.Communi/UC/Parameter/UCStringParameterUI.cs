using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCStringParameterUI : UserControl
    {
        public UCStringParameterUI()
        {
            InitializeComponent();
        }
        public string ParameterName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        public string Value
        {
            get { return this.txtValue.Text; }
            set { this.txtValue.Text = value; }
        }

        public string Unit
        {
            get { return this.lblUnit.Text; }
            set { this.lblUnit.Text = value; }
        }

        #region Parameter
        /// <summary>
        /// 
        /// </summary>
        public IParameter Parameter
        {
            get
            {
                //if (_deviceParameter == null)
                //{
                //_deviceParameter = new Parameter();
                //}
                return _deviceParameter;
            }
            set
            {
                Debug.Assert(value != null ,"value == null");
                Debug.Assert(value.ValueType == typeof(string), "value.valuetype not is string");

                _deviceParameter = value;
                if (_deviceParameter != null)
                {
                    this.lblName.Text = _deviceParameter.Text + ":";
                    this.lblUnit.Text = _deviceParameter.Unit.Text;
                    this.txtValue.Text = _deviceParameter.Value.ToString();
                }
            }
        } private IParameter _deviceParameter;
        #endregion //Parameter

        ///// <summary>
        ///// 
        ///// </summary>
        //internal void ApplyNewValue()
        //{
        //    this.Parameter.SetValue(this.txtValue.Text);
        //}

        private void UCParameterUI_Load(object sender, EventArgs e)
        {
            if (_deviceParameter != null)
            {
                this.lblName.Text = _deviceParameter.Text + ":";
                this.lblUnit.Text = _deviceParameter.Unit.Text;
                this.txtValue.Text = _deviceParameter.Value.ToString();
            }
        }
    }
}
