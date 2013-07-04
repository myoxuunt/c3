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
    public partial class frmCPCreateLog : Form
    {
        public frmCPCreateLog()
        {
            InitializeComponent();
        }

        private void frmCPCreateLog_Load(object sender, EventArgs e)
        {
            this.richTextBox1.ReadOnly = true;
            foreach (CPCreateLog i in CommuniPortFactory.Default.CPCreateLogs)
            {
                string text = i.DT.ToString() + " " + i.Log + Environment.NewLine;
                this.richTextBox1.AppendText(text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
