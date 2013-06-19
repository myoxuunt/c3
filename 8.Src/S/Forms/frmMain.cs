using System;
using System.Text;
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
        private Client _selectedClient = null;

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
            c.LogItems.Added -= new EventHandler(LogItems_Added);

            TreeNode node = c.Tag as TreeNode;
            if (node != null)
            {
                this.treeView1.Nodes.Remove(node);
            }

            if (c == _selectedClient)
            {
                this.txtClientInfo.Clear();
                this.richTextBox1.Clear();
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
            //this.Close();
            App.Exit(0);
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
            this.Text = Config.Default.AppName;

            this.toolStrip1.Visible = false;
            this.mnuView.Visible = false;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_selectedClient != null)
            {
                _selectedClient.LogItems.Added -= new EventHandler(LogItems_Added);
            }

            TreeNode node = e.Node;
            Client c = node.Tag as Client;

            RefreshClient(c);


            c.LogItems.Added += new EventHandler(LogItems_Added);
            _selectedClient = c;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LogItems_Added(object sender, EventArgs e)
        {
            LogItemCollection logItems = sender as LogItemCollection;
            if (logItems.Count > 0)
            {
                LogItem li = logItems[logItems.Count - 1];
                string s = li.ToString() + Environment.NewLine;
                SApp.UISynchronizationContext.Post(this.UpdateTextCallback, s);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private SendOrPostCallback UpdateTextCallback
        {
            get
            {
                if (_updateTextCallback == null)
                {
                    _updateTextCallback = new SendOrPostCallback(UpdateTarget);
                }
                return _updateTextCallback;
            }
        } private SendOrPostCallback _updateTextCallback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void UpdateTarget(object state)
        {
            if (this.richTextBox1.Text.Length > 65535)
            {
                this.richTextBox1.Clear();
            }

            this.richTextBox1.AppendText((string)state);
            this.richTextBox1.Select(this.richTextBox1.Text.Length, 0);
            this.richTextBox1.ScrollToCaret();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private string GetClientInfo(Client c)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("连接信息: " + c.CommuniPort.ToString());
            sb.AppendLine("连接时间: " + c.CommuniPort.CreateDateTime);

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            NUnit.UiKit.UserMessage.DisplayInfo(
                string.Format("{0}\r\n\r\n版本: {1}", 
                Config.Default.AppName, 
                Config.Default.Version)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                Client c = this.treeView1.SelectedNode.Tag as Client;
                RefreshClient(c);
            }
        }

        private void RefreshClient(Client c)
        {
            this.txtClientInfo.Text = GetClientInfo(c);
            this.richTextBox1.Focus();
            this.richTextBox1.Text = c.LogItems.ToString();
            this.richTextBox1.Select(this.richTextBox1.Text.Length, 0);
            this.richTextBox1.ScrollToCaret();
        }

        private void mnuClearLogs_Click(object sender, EventArgs e)
        {
            if (this._selectedClient != null)
            {
                this._selectedClient.LogItems.Clear();
                RefreshClient(_selectedClient);
            }
        }

        private void mnuTest_Click(object sender, EventArgs e)
        {
            //throw new Exception("test exception");
        }
    }
}
