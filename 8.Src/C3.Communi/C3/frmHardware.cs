using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class frmHardware : Form
    {
        object _value;
        public frmHardware(object value)
        {
            InitializeComponent();
            _value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        private Hardware Hardware
        {
            get
            {
                return C3App.App.Soft.Hardware;
            }
        }

        private void frmHardware_Load(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = _value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object obj = this.propertyGrid1.SelectedGridItem.Value;
            frmHardware f = new frmHardware(obj);
            f.ShowDialog();
        }
    }
}
