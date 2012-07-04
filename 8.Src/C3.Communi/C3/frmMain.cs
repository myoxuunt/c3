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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private C3App App
        {
            get
            {
                return C3App.App;
            }
        }

        private Soft Soft
        {
            get { return this.App.Soft; }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Soft soft = SoftManager.GetSoft();
            object o2 = soft.Hardware;
            int n = soft.Hardware.Stations.Count;

            n= soft.SocketListenerManager.SocketListeners.Count;

            this.Text = n.ToString();

            // spu
            //
            this.ucSpus1.SPUs = App.Soft.SPUs;
            this.ucSpus1.RefreshSPUs();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmHardware f = new frmHardware(C3App.App.Soft.Hardware.Stations[0]);
            f.ShowDialog();
        }

    }
}
