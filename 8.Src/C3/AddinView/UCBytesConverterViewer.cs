using System;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class UCBytesConverterViewer : UserControl
    {
        private Dictionary<string, Type> _bytesConverterDict;
        /// <summary>
        /// 
        /// </summary>
        public UCBytesConverterViewer(Dictionary<string, Type> bcs)
        {
            InitializeComponent();

            //this.BytesConverters = bcs;
            _bytesConverterDict = bcs;
            RefreshBytesConverters();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public BytesConverterCollection BytesConverters
        //{
        //    get { return _bytesConverters; }
        //    set 
        //    {
        //        if (_bytesConverters != value)
        //        {
        //            _bytesConverters = value;
        //            if (_bytesConverters != null)
        //            {
        //                RefreshBytesConverters();            
        //            }
        //        }
        //    }
        //} private BytesConverterCollection _bytesConverters;

        /// <summary>
        /// 
        /// </summary>
        private void RefreshBytesConverters()
        {
            this.listView1.Items.Clear();
            string[] typeNames = new string[this._bytesConverterDict.Count];

            this._bytesConverterDict.Keys.CopyTo(typeNames, 0);
            Array.Sort(typeNames);
            foreach (string typeName in typeNames)
            {
                ListViewItem lvi = CreateListViewItem(typeName);
                this.listView1.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(string name)
        {
            string[] items = new string[] { name };
            ListViewItem lvi = new ListViewItem(items);
            return lvi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        private string GetAssemblyInfo(IBytesConverter bc)
        {
            return bc.GetType().Assembly.FullName;
        }
    }
}
