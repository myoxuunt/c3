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
    public partial class UCCrcViewer : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        private CRCerCollection _crcers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crcers"></param>
        public UCCrcViewer(CRCerCollection crcers)
        {
            InitializeComponent();
            _crcers = crcers;
            RefreshCrcers();
        }

        private void RefreshCrcers()
        {
            foreach (ICRCer item in this._crcers)
            {
                ListViewItem lvi = CreateListViewItem(item);
                this.listView1.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this1"></param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(ICRCer item)
        {
            string assemblyInfo = GetAssemblyInfo(item);
            //string[] items = new string[] { this1.GetType ().Name , "-", assemblyInfo };
            string[] items = new string[] { item.GetType().FullName };
            ListViewItem lvi = new ListViewItem(items );
            return lvi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this1"></param>
        /// <returns></returns>
        private string GetAssemblyInfo(ICRCer item)
        {
            return item.GetType().Assembly.FullName;
        }

        
    }
}
