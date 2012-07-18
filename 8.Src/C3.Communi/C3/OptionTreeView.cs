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
            this.Click += new EventHandler(OptionTreeView_Click);
            this.AfterSelect += new TreeViewEventHandler(OptionTreeView_AfterSelect);

            BytesConverterCollection bytesConverters = this._soft.BytesConverterManager.BytesConverterCollection;
            //BytesConverterView.UCBytesConverterViewer  c1 = new C3.BytesConverterView.UCBytesConverterViewer(
            //this.Nodes.Add(new BytesConverterOptionTreeNode(bytesConverters));
            //this.Nodes.Add(new OptionTreeNode ());

            CRCerCollection crcers = this._soft.CRCerManager.CRCers;
            Control c = new UCCrcViewer(crcers);
            OptionTreeNode op = new OptionTreeNode("ooo", c);
            this.Nodes.Add(op);
        }

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

            _displayArea.Label.Text = optionNode.GetType().Name;
            c.Visible = true;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class OptionTreeNode : TreeNode
    {
        public OptionTreeNode( string text , Control control)
        {
            this.Text = text;
            this.Control = control;
        }

        /// <summary>
        /// 
        /// </summary>
        virtual public Control Control 
        {
            get
            {
                return _control;
            }
            set
            {
                _control = value;
            }
        } private Control _control;
    }

    /*
    /// <summary>
    /// 
    /// </summary>
    public class BytesConverterOptionTreeNode : OptionTreeNode
    {
        private BytesConverterCollection _bytesConverters;

        public BytesConverterOptionTreeNode(BytesConverterCollection bytesConverters)
        {
            this.Text = "BCOption";
            this._bytesConverters = bytesConverters;
        }

        public override Control Control
        {
            get
            {
                if (_control == null)
                {
                    _control = new C3.BytesConverterView.UCBytesConverterViewer();
                    _control.BytesConverters = _bytesConverters;
                }
                return _control;
            }
        } private C3.BytesConverterView.UCBytesConverterViewer _control;
    }
     */

    /// <summary>
    /// 
    /// </summary>
    public class DisplayArea
    {
        public DisplayArea(Label label, Panel panel)
        {
            this.Label = label;
            this.Panel = panel;
        }
        /// <summary>
        /// 
        /// </summary>
        public Label Label
        {
            get { return _label; }
            set { _label = value; }
        } private Label _label;


        /// <summary>
        /// 
        /// </summary>
        public Panel Panel
        {
            get { return _panel; }
            set { _panel = value; }
        } private Panel _panel;


    }
}
