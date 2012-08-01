﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi;
using C3.Resources;

namespace C3
{
    public partial class frmCommuniDetails : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private class ViewOption
        {
            private bool _enabled;
            private bool _passValue;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="enabled"></param>
            /// <param name="passValue"></param>
            private ViewOption ( bool enabled, bool passValue )
            {
                _enabled = enabled;
                _passValue = passValue;
            }

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

            /// <summary>
            /// 
            /// </summary>
            static public ViewOption 
                All = new ViewOption(false, false),
                OnlySuccess = new ViewOption(true, true),
                OnlyFail = new ViewOption(true, false);
        }

        /// <summary>
        /// 
        /// </summary>
        private string _splitString = C3.Resources.CommuniDetailResource.SplitString;

        /// <summary>
        /// 
        /// </summary>
        private IDevice _device;
        /// <summary>
        /// 
        /// </summary>
        private CommuniDetailCollection  _communiDetailQueue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="communiDetailQueue"></param>
        public frmCommuniDetails(IDevice device,
            CommuniDetailCollection  communiDetailQueue)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        private void SetFormText(IDevice device)
        {
            this.Text = string.Format(
                "{0}-{1}",
                device.Station.Name,
                device.Text
                );
        }

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

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            this.richTextBox1.Clear();

            StringBuilder sb = new StringBuilder();
            foreach (CommuniDetail item in this._communiDetailQueue)
            {
                if (this.SelectedViewOption.IsPass(item.IsSuccess))
                {
                    sb.AppendLine(CommuniDetailResource.SendDateTime + _splitString + item.SendDateTime.ToString());
                    sb.AppendLine(CommuniDetailResource.Opera + _splitString + item.OperaText);
                    sb.AppendLine(CommuniDetailResource.Result + _splitString + item.ParseResult);
                    sb.AppendLine(CommuniDetailResource.Sended + _splitString + GetBytesString(item.Send));
                    sb.AppendLine(CommuniDetailResource.Received + _splitString + GetBytesString(item.Received));
                    sb.AppendLine();
                }
            }
            this.richTextBox1.AppendText(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        private string GetBytesString(byte[] bs)
        {
            if (bs == null || bs.Length == 0)
            {
                return null;
            }

            string s = string.Format("[{0:000}] ", bs.Length) + BitConverter.ToString(bs);
            return s;
        }

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
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void frmCommuniDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
