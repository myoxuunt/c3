using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCDeviceParameterItem : UserControl
    {
        public UCDeviceParameterItem()
        {
            InitializeComponent();
        }

        #region Parameter
        /// <summary>
        /// 
        /// </summary>
        public Parameter Parameter
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
                    this.label1.Text = _deviceParameter.Name + ":";
                    this.label2.Text = _deviceParameter.Unit.Text;
                    this.textBox1.Text = _deviceParameter.Value.ToString();
                }
            }
        } private Parameter _deviceParameter;
        #endregion //Parameter

        /// <summary>
        /// 
        /// </summary>
        internal void ApplyNewValue()
        {
            //if (this.Parameter.ValueType.IsEnum)
            //{
            //    object obj = Enum.Parse(this.Parameter.ValueType, this.textBox1.Text);
            //    this.Parameter.Value = obj;
            //}
            //else
            //{
            //    this.Parameter.Value = Convert.ChangeType(this.textBox1.Text, this.Parameter.ValueType);
            //}

            this.Parameter.SetValue(this.textBox1.Text);
        }
    }
}
