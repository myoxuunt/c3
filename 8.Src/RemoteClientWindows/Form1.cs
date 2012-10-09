using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.GR.Common;
using Xdgk.Common;
using RemoteClient;

namespace RemoteClientWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            RemoteController c = new RemoteController();
            frmXD100ModbusTemperatureControl f =
                new frmXD100ModbusTemperatureControl("test station name",2, c);
            f.ShowDialog();
        }
    }
}
