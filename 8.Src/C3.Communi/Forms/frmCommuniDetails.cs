using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi;
using C3.Communi.Resources;
using Xdgk.Common;

namespace C3
{
    public partial class frmCommuniDetails : Form
    {

        #region ViewOption class
        /// <summary>
        /// 
        /// </summary>
        private class ViewOption
        {
            #region Members
            private bool _enabled;
            private bool _passValue;

            /// <summary>
            /// 
            /// </summary>
            static public ViewOption
                All = new ViewOption(false, false),
                OnlySuccess = new ViewOption(true, true),
                OnlyFail = new ViewOption(true, false);
            #endregion //Members

            #region ViewOption
            /// <summary>
            /// 
            /// </summary>
            /// <param name="enabled"></param>
            /// <param name="passValue"></param>
            private ViewOption(bool enabled, bool passValue)
            {
                _enabled = enabled;
                _passValue = passValue;
            }
            #endregion //ViewOption

            #region IsPass
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public bool IsPass(bool value)
            {
                if (_enabled)
                {
                    return value == _passValue;
                }
                else
                {
                    return true;
                }
            }
            #endregion //IsPass
        }
        #endregion //ViewOption class

        #region Members
        /// <summary>
        /// 
        /// </summary>
        private string _splitString = C3.Communi.Resources.CommuniDetailResource.SplitString;

        /// <summary>
        /// 
        /// </summary>
        private IDevice _device;

        /// <summary>
        /// 
        /// </summary>
        private CommuniDetailCollection _communiDetailQueue;
        #endregion //Members

        #region frmCommuniDetails
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="communiDetailQueue"></param>
        public frmCommuniDetails(IDevice device,
            CommuniDetailCollection communiDetailQueue)
        {
            InitializeComponent();

            if (communiDetailQueue == null)
                throw new ArgumentNullException("communiDetailQueue");

            if (device == null)
                throw new ArgumentNullException("device");

            this._device = device;
            this._communiDetailQueue = communiDetailQueue;

            //
            //
            SetFormText(device);

            FillComboBox();
            Fill();
        }
        #endregion //frmCommuniDetails

        #region SetFormText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        private void SetFormText(IDevice device)
        {
            this.Text = string.Format(
                "{0}:{1}",
                device.Station.Name,
                device.Text
                );
        }
        #endregion //SetFormText

        #region SelectedViewOption
        /// <summary>
        /// 
        /// </summary>
        private ViewOption SelectedViewOption
        {
            get
            {
                return (ViewOption)this.cmbView.SelectedValue;
            }
        }
        #endregion //SelectedViewOption

        #region ViewSource
        /// <summary>
        /// 
        /// </summary>
        private KeyValueCollection ViewSource
        {
            get
            {
                if (this._viewSource == null)
                {
                    _viewSource = new KeyValueCollection();
                    _viewSource.Add(new KeyValue(CommuniDetailResource.All, ViewOption.All));
                    _viewSource.Add(new KeyValue(CommuniDetailResource.OnlySuccess, ViewOption.OnlySuccess));
                    _viewSource.Add(new KeyValue(CommuniDetailResource.OnlyFail, ViewOption.OnlyFail));
                }
                return _viewSource;
            }
        } private KeyValueCollection _viewSource;
        #endregion //ViewSource

        #region FillComboBox
        /// <summary>
        /// 
        /// </summary>
        private void FillComboBox()
        {
            this.cmbView.Items.Clear();
            this.cmbView.DisplayMember = "Key";
            this.cmbView.ValueMember = "Value";
            this.cmbView.DataSource = ViewSource;

            this.cmbView.SelectedValue = ViewOption.All;
        }
        #endregion //FillComboBox

        #region Fill
        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            this.richTextBox1.Clear();

            StringBuilder sb = new StringBuilder();
            foreach (ICommuniDetail item in this._communiDetailQueue)
            {
                if (this.SelectedViewOption.IsPass(item.IsSuccess))
                {
                    sb.Append(item.ToString());
                    sb.AppendLine();
                }
            }
            this.richTextBox1.AppendText(sb.ToString());
            this.richTextBox1.ScrollToCaret();
        }
        #endregion //Fill

        #region btnCopyContext_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyContext_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Text != string.Empty)
            {
                Clipboard.SetText(this.richTextBox1.Text);
                NUnit.UiKit.UserMessage.DisplayInfo(strings.ContentCopied);
            }
        }
        #endregion //btnCopyContext_Click

        #region cmbView_SelectedIndexChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }
        #endregion //cmbView_SelectedIndexChanged

        #region btnClose_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion //btnClose_Click
    }
}
