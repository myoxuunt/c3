using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCDeviceParameters : UserControl
    {
        public UCDeviceParameters()
        {
            InitializeComponent();
        }

        #region DeviceParameters
        /// <summary>
        /// 
        /// </summary>
        public DeviceParameterCollection DeviceParameters
        {
            get
            {
                if (_deviceParameters == null)
                {
                    _deviceParameters = new DeviceParameterCollection();
                }
                return _deviceParameters;
            }
            set
            {
                _deviceParameters = value;
                if (_deviceParameters != null)
                {
                    foreach (DeviceParameter p in _deviceParameters)
                    {
                        UCDeviceParameterItem uc = new UCDeviceParameterItem();
                        uc.DeviceParameter = p;

                        this.tableLayoutPanel1.RowCount += 1;
                        this.tableLayoutPanel1.Controls.Add(uc, 0, this.tableLayoutPanel1.RowCount - 1);
                    }
                }
            }
        } private DeviceParameterCollection _deviceParameters;
        #endregion //DeviceParameters

        public void ApplyNewValue()
        {
            foreach (Control c in this.tableLayoutPanel1.Controls)
            {
                if (c is UCDeviceParameterItem)
                {
                    UCDeviceParameterItem c2 = (UCDeviceParameterItem)c;
                    c2.ApplyNewValue();
                }
            }
        }

    }
}
