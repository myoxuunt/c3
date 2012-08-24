using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCStringParameterUI : UserControl
    {
        public UCStringParameterUI()
        {
            InitializeComponent();
        }
        public string ParameterName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        public string Value
        {
            get { return this.txtValue.Text; }
            set { this.txtValue.Text = value; }
        }

        public string Unit
        {
            get { return this.lblUnit.Text; }
            set { this.lblUnit.Text = value; }
        }

        private void UCParameterUI_Load(object sender, EventArgs e)
        {
        }
    }
}
