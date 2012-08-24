using System.Windows.Forms;
using C3.Communi;
using Xdgk.Communi.Interface;

namespace C3
{
    public partial class UCBytesConverterViewer : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UCBytesConverterViewer(BytesConverterCollection bcs)
        {
            InitializeComponent();

            this.BytesConverters = bcs;
        }

        /// <summary>
        /// 
        /// </summary>
        public BytesConverterCollection BytesConverters
        {
            get { return _bytesConverters; }
            set 
            {
                if (_bytesConverters != value)
                {
                    _bytesConverters = value;
                    if (_bytesConverters != null)
                    {
                        RefreshBytesConverters();            
                    }
                }
            }
        } private BytesConverterCollection _bytesConverters;

        /// <summary>
        /// 
        /// </summary>
        private void RefreshBytesConverters()
        {
            this.listView1.Items.Clear();
            foreach (IBytesConverter bc in this.BytesConverters)
            {
                ListViewItem lvi = CreateListViewItem(bc);
                this.listView1.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(IBytesConverter bc)
        {
            string assemblyInfo = GetAssemblyInfo(bc);
            //string[] items = new string[] {bc.GetType().FullName , "-", assemblyInfo };
            string[] items = new string[] { bc.GetType().FullName };
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
