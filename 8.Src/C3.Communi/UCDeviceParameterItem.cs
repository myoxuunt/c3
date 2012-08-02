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

        #region DeviceParameter
        /// <summary>
        /// 
        /// </summary>
        public DeviceParameter DeviceParameter
        {
            get
            {
                if (_deviceParameter == null)
                {
                    _deviceParameter = new DeviceParameter();
                }
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
        } private DeviceParameter _deviceParameter;
        #endregion //DeviceParameter

        /// <summary>
        /// 
        /// </summary>
        internal void ApplyNewValue()
        {
            if (this.DeviceParameter.ValueType.IsEnum)
            {
                object obj = Enum.Parse(this.DeviceParameter.ValueType, this.textBox1.Text);
                this.DeviceParameter.Value = obj;
            }
            else
            {
                this.DeviceParameter.Value = Convert.ChangeType(this.textBox1.Text, this.DeviceParameter.ValueType);
            }
        }
    }
}
