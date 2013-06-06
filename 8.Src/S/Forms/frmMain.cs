using System;
using System.Threading;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;

namespace S
{
    public partial class FrmMain : Form
    {

        #region Members
        #endregion //Members

        #region FrmMain
        /// <summary>
        /// 
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            Init();
        }
        #endregion //FrmMain

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            // register events
            //

            App.ClientManager.Added += new ClientEventHandler(ClientManager_Added);
            App.ClientManager.Removed += new ClientEventHandler(ClientManager_Removed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientManager_Removed(object sender, ClientEventArgs e)
        {
            SApp.Post(new SendOrPostCallback(RemoveClientNode), e.Client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void RemoveClientNode(object obj)
        {
            Client c = obj as Client;
            TreeNode node = c.Tag as TreeNode;
            if (node != null)
            {
                this.treeView1.Nodes.Remove(node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientManager_Added(object sender, ClientEventArgs e)
        {
            SApp.Post(new SendOrPostCallback(AddClientNode), e.Client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void AddClientNode(object obj)
        {
            Client c = obj as Client;
            string s = c.CommuniPort.ToString();
            TreeNode node = new TreeNode(s);
            node.Tag = c;
            c.Tag = node;
            this.treeView1.Nodes.Add(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private SApp App
        {
            get
            {
                return SApp.App;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //
            //
            string s = string.Empty;
            foreach (SocketListener item in this.App.SocketListenerManager.SocketListeners)
            {
                s += item.LocalEndpoint.ToString() + ", ";
            }
            if (s.Length > 2)
            {
                s = s.Remove(s.Length - 2);
            }

            this.tssListenPort.Text = string.Format("{0}: {1}", Strings.Listening, s);
        }


    }
}
