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
    public partial class frmCPInfo : Form
    {
        public frmCPInfo()
        {
            InitializeComponent();
            this.txtCPCreateLog.ReadOnly = true;
        }

        private void frmCPCreateLog_Load(object sender, EventArgs e)
        {
            RefreshConnection();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshConnection();
        }

        private void RefreshConnection()
        {
            this.txtCPCreateLog.Clear();
            foreach (CPCreateLog i in CommuniPortFactory.Default.CPCreateLogs)
            {
                string text = i.DT.ToString() + " " + i.Log + Environment.NewLine;
                this.txtCPCreateLog.AppendText(text);
            }
            this.txtCPCreateLog.ScrollToCaret();


            this.listView1.Items.Clear();
            CommuniPortCollection cps = SoftManager.GetSoft().CommuniPortManager.CommuniPorts;
            foreach (ICommuniPort cp in cps)
            {
                ListViewItem lvi = new ListViewItem(cp.ToString());

                string[] subItems = new string[] { GetStationNames(cp), cp.CreateDateTime.ToString() };

                lvi.SubItems.AddRange(subItems);

                listView1.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        private string GetStationNames(ICommuniPort cp)
        {
            string r = string.Empty;

            StationCollection stations = SoftManager.GetSoft().Hardware.Stations;
            foreach (IStation st in stations)
            {
                if (st.CommuniPort == cp)
                {
                    if (r.Length > 0)
                    {
                        r += ",";
                    }
                    r += st.Name;
                }
            }

            return r;
        }
    }
}
