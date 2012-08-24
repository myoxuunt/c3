using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi ;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionTreeView : TreeView 
    {

        private Soft _soft;
        private DisplayArea _displayArea;
        private OptionTreeNode _bcNode, _crcNode;

        /// <summary>
        /// 
        /// </summary>
        public OptionTreeView(Soft soft, DisplayArea displayArea)
        {
            this._soft = soft;
            this._displayArea = displayArea;
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.ShowLines = false;
            this.Click += new EventHandler(OptionTreeView_Click);
            this.AfterSelect += new TreeViewEventHandler(OptionTreeView_AfterSelect);

            this.Nodes.Add(GetBytesConverterNode());
            this.Nodes.Add(GetCrcNode());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private OptionTreeNode GetCrcNode()
        {
            if (_crcNode == null)
            {
                CRCerCollection crcers = C3App.App.Soft.CRCerManager.CRCers;
                Control c = new UCCrcViewer(crcers);
                _crcNode  = new OptionTreeNode(Strings.Crcer , c);
            }
            return _crcNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private OptionTreeNode GetBytesConverterNode()
        {

            if (_bcNode == null)
            {
                BytesConverterCollection bcs = C3App.App.Soft.BytesConverterManager.BytesConverters;
                UCBytesConverterViewer c1 = new UCBytesConverterViewer(bcs);
                _bcNode = new OptionTreeNode(Strings.BytesConverter, c1);
            }
            return _bcNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OptionTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.OptionTreeView_Click(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OptionTreeView_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
            TreeNode node = this.SelectedNode;
            if (node != null)
            {
                OptionTreeNode optionNode = node as OptionTreeNode;
                if (optionNode != null)
                {
                    ClickOptionTreeNode(optionNode);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionNode"></param>
        private void ClickOptionTreeNode(OptionTreeNode optionNode)
        {
            Control c = optionNode.Control;
            c.Dock = DockStyle.Fill;

            if (!_displayArea.Panel.Contains(c))
            {
                _displayArea.Panel.Controls.Add(c);
            }

            foreach (Control item in _displayArea.Panel.Controls)
            {
                item.Visible = false;
            }

            _displayArea.Label.Text = optionNode.Text;
            c.Visible = true;
        }

    }
}
