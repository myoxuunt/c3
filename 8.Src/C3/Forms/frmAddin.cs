using System;
using System.Windows.Forms;
using C3.Communi ;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmAddin : NUnit.UiKit.FixedDialogBase
    {

        #region frmAddin
        /// <summary>
        /// 
        /// </summary>
        public frmAddin()
        {
            InitializeComponent();
            Init();
        }
        #endregion //frmAddin

        #region Init
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.splitContainer1.Panel1.Controls.Add(this.OptionTreeView);
        }
        #endregion //Init

        #region Soft
        /// <summary>
        /// 
        /// </summary>
        private Soft Soft
        {
            get { return C3App.App.Soft; }
        }
        #endregion //Soft

        #region AddinTreeView
        /// <summary>
        /// 
        /// </summary>
        private AddinTreeView OptionTreeView
        {
            get 
            {
                if (_optionTreeView==null)
                {
                    DisplayArea displayArea = new DisplayArea(this.label1, this.panel1);
                    _optionTreeView = new AddinTreeView(this.Soft, displayArea);
                    _optionTreeView.Dock = DockStyle.Fill;
                }
                return _optionTreeView;
            }
        } private AddinTreeView _optionTreeView;
        #endregion //AddinTreeView

        #region frmM_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmM_Load(object sender, EventArgs e)
        {
        }
        #endregion //frmM_Load


    }
}
