using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using C3.Communi ;

namespace C3
{
    public partial class UCSpus : UserControl
    {
        public UCSpus()
        {
            InitializeComponent();
        }

        private void UCSpus_Load(object sender, EventArgs e)
        {

        }

        public SPUCollection SPUs
        {
            get { return _spus; }
            set { _spus = value; }
        }
        private SPUCollection _spus;

        /// <summary>
        /// 
        /// </summary>
        public void RefreshSPUs()
        {
            foreach (ISPU spu in SPUs)
            {
                string[] items = new string[] { spu.Name, spu.Description , spu.GetType ().Assembly.Location  };
                ListViewItem lvi = new ListViewItem(items );
                listView1.Items.Add(lvi);
            }
        }
    }
}
