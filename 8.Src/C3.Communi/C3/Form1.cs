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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //foreach (object obj in System.Configuration.ConfigurationManager.AppSettings.Keys)
            //{
            //    string s = obj.ToString ();
            //    Console.WriteLine(s);
            //}

            Soft soft = SoftManager.GetSoft();
            object o2 = soft.Hardware;
            int n = soft.Hardware.Stations.Count;

            n= soft.SocketListenerManager.SocketListeners.Count;

            this.Text = n.ToString();
        }
    }
}
