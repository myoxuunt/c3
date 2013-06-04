using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;

namespace S
{
    public partial class FrmMain : Form
    {

        #region Members
        #endregion //Members

        #region FrmMain
        public FrmMain()
        {
            InitializeComponent();
        }
        #endregion //FrmMain

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
