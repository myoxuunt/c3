using System.Windows.Forms;


namespace C3
{
    public class AddinTreeNode : TreeNode
    {
        public AddinTreeNode( string text , Control control)
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
