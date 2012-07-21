using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class UCDeviceViewer : UserControl
    {
        public UCDeviceViewer()
        {
            InitializeComponent();
        }

        #region Device
        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                if (_device != value)
                {
                    _device = value;
                    this.richTextBox1.Text = DateTime.Now.ToString() + _device.ToString();
                }
            }
        } private IDevice _device;
        #endregion //Device
    }
}
