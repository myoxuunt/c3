using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCParameterUI : UserControl
    {
        public UCParameterUI()
        {
            InitializeComponent();
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
                _deviceParameter = value;
                if (_deviceParameter != null)
                {
                    this.label1.Text = _deviceParameter.Text + ":";
                    this.label2.Text = _deviceParameter.Unit.Text;
                    this.textBox1.Text = _deviceParameter.Value.ToString();
                }
            }
        } private IParameter _deviceParameter;
        #endregion //Parameter

        /// <summary>
        /// 
        /// </summary>
        internal void ApplyNewValue()
        {
            this.Parameter.SetValue(this.textBox1.Text);
        }
    }
}
