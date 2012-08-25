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
    public class AddinTreeView : TreeView 
    {


        private Soft _soft;
        private DisplayArea _displayArea;
        private AddinTreeNode _bcNode, _crcNode;

        #region AddinTreeView
        /// <summary>
        /// 
        /// </summary>
        public AddinTreeView(Soft soft, DisplayArea displayArea)
        {
            this._soft = soft;
            this._displayArea = displayArea;
            Init();
        }
        #endregion //AddinTreeView

        #region Init
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            //this.ShowLines = false;
            this.ShowRootLines = false;
            this.Click += new EventHandler(OptionTreeView_Click);
            this.AfterSelect += new TreeViewEventHandler(OptionTreeView_AfterSelect);

            this.Nodes.Add(GetBytesConverterNode());
            this.Nodes.Add(GetCrcNode());
            this.Nodes.Add(GetSPUNode());
            this.Nodes.Add(GetDpuNode());
        }
        #endregion //Init

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private TreeNode GetDpuNode()
        {
            if (_dpuNode==null)
            {
                _dpuNode = new AddinTreeNode(Strings.DeviceAddinName, new UCTypeViewer(_soft.DPUs));
            }
            return _dpuNode;
        } private AddinTreeNode _dpuNode;

        #region GetCrcNode
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AddinTreeNode GetCrcNode()
        {
            if (_crcNode == null)
            {
                CRCerCollection crcers = _soft.CRCerManager.CRCers;
                Control c = new UCCrcViewer(crcers);
                _crcNode  = new AddinTreeNode(Strings.Crcer , c);
            }
            return _crcNode;
        }
        #endregion //GetCrcNode

        #region GetBytesConverterNode
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AddinTreeNode GetBytesConverterNode()
        {
            if (_bcNode == null)
            {
                BytesConverterCollection bcs = _soft.BytesConverterManager.BytesConverters;
                UCBytesConverterViewer c1 = new UCBytesConverterViewer(bcs);
                _bcNode = new AddinTreeNode(Strings.BytesConverter, c1);
            }
            return _bcNode;
        }
        #endregion //GetBytesConverterNode

        private AddinTreeNode GetSPUNode()
        {
            if (_spuNode==null)
            {
                _spuNode = new AddinTreeNode(
                    Strings.StationAddinName , 
                    new UCTypeViewer(_soft.SPUs));
            }
            return _spuNode;
        } private AddinTreeNode _spuNode;
        #region OptionTreeView_AfterSelect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OptionTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.OptionTreeView_Click(null, null);
        }
        #endregion //OptionTreeView_AfterSelect

        #region OptionTreeView_Click
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
                AddinTreeNode optionNode = node as AddinTreeNode;
                if (optionNode != null)
                {
                    ClickOptionTreeNode(optionNode);
                }
            }
        }
        #endregion //OptionTreeView_Click

        #region ClickOptionTreeNode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionNode"></param>
        private void ClickOptionTreeNode(AddinTreeNode optionNode)
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
        #endregion //ClickOptionTreeNode

    }
}
