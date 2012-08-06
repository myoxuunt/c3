using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public partial class FrmGroups :NUnit.UiKit.SettingsDialogBase 
    {
        public FrmGroups()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get { return _device; }
            set { _device = value; }
        } private IDevice _device;

        private Type _deviceType;
        private IStation _station;
        private ADEStatus _adeStatus;

        #region Groups
        /// <summary>
        /// 
        /// </summary>
        public GroupCollection Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ParameterGroups");
                }

                if (_groups != value)
                {
                    _groups = value;
                    //Fill();
                }
            }
        } private GroupCollection _groups;
        #endregion //Groups

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            foreach ( Group item in this.Groups )
            {
                TabPage tp = new TabPage(item.Text);
                tp.Controls.Add(item.GroupUI.Control);
                tabControl1.TabPages.Add(tp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmParameterGroups_Load(object sender, EventArgs e)
        {
            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        static public DialogResult Add(Type deviceType, IStation station, out IDevice newDevice)
        {
            newDevice = null;
            FrmGroups f = new FrmGroups();

            f._deviceType = deviceType;
            f._device = (IDevice)Activator.CreateInstance(f._deviceType);
            f._station = station;
            f._adeStatus = ADEStatus.Add;

            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                newDevice = f.Device;
            }
            return dr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        static public DialogResult Edit(IDevice device)
        {
            FrmGroups f = new FrmGroups();

            f._device = device;
            f._station = device.Station;
            f._deviceType = device.GetType();
            f._adeStatus = ADEStatus.Edit;

            f.Groups = device.Groups;

            DialogResult dr = f.ShowDialog();
            return dr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            foreach (IGroup item in this.Groups)
            {
                item.GroupUI.ApplyNewValue();
            }
        }
    }
}
