
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi ;


namespace C3
{
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

}
