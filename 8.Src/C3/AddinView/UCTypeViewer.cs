using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3
{
    public partial class UCTypeViewer : UserControl
    {
        private IEnumerable _enumerable;
        public UCTypeViewer(IEnumerable enumerable)
        {
            InitializeComponent();
            this._enumerable = enumerable;
            RefreshEnumerable();
        }


        /// <summary>
        /// 
        /// </summary>
        private void RefreshEnumerable()
        {
            this.listView1.Items.Clear();
            IEnumerator t = _enumerable.GetEnumerator();
            while (t.MoveNext())
            {
                object obj = t.Current;

                ListViewItem lvi = CreateListViewItem(obj);
                this.listView1.Items.Add(lvi);
            }

            //foreach (IBytesConverter bc in this.BytesConverters)
            //{
            //    ListViewItem lvi = CreateListViewItem(bc);
            //    this.listView1.Items.Add(lvi);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(object value)
        {
            string[] items = new string[] { value.GetType ().FullName };
            ListViewItem lvi = new ListViewItem(items);
            return lvi;
        }
    }
}
