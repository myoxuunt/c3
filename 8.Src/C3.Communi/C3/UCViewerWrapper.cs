using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3
{
    public partial class UCViewerWrapper : UserControl
    {
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

        public string Title
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }
    }
}
