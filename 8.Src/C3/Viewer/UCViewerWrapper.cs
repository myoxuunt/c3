using System.Windows.Forms;

namespace C3
{
    public partial class UCViewerWrapper : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UCViewerWrapper()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public Panel ViewContainer
        {
            get { return this.panel1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }
    }
}
