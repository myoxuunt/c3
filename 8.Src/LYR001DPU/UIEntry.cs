using System;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;


namespace LYR001DPU
{
    public class UIEntry : IUIEntryFactory
    {
        private ToolStripMenuItem _otProviderSetting;
        private ToolStripMenuItem _mnuTemperatureLine;

        private ISelectedHardwareItem _selectedHardwareItem;

        private const string 
            MENU_OT = "�����¶�(&T)...",
            MENU_GRSETTING = "��������(&S)...";

        private const string
            MENU_OT_NAME = "mnuOtStandardSetting",
            MENU_GRSETTING_NAME = "mnuGRSetting";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentMenuItem"></param>
        public void Create(ISelectedHardwareItem sel, ToolStripMenuItem parentMenuItem)
        {
            _selectedHardwareItem = sel;
            parentMenuItem.DropDownOpening += new EventHandler(parentMenuItem_DropDownOpening);

            if (!parentMenuItem.DropDownItems.ContainsKey(MENU_OT_NAME))
            {
                _otProviderSetting = new ToolStripMenuItem(MENU_OT);
                _otProviderSetting.Name = MENU_OT_NAME;
                _otProviderSetting.Click += new EventHandler(_otProviderSetting_Click);
                parentMenuItem.DropDownItems.Add(_otProviderSetting);
            }


            _mnuTemperatureLine = new ToolStripMenuItem(MENU_GRSETTING);
            _mnuTemperatureLine.Click += new EventHandler(_mnuTemperatureLine_Click);
            parentMenuItem.DropDownItems.Add(_mnuTemperatureLine);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _otProviderSetting_Click(object sender, EventArgs e)
        {
            //frmOutsideStandard f = new frmOutsideStandard();
            //f.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mnuTemperatureLine_Click(object sender, EventArgs e)
        {
            if (this._selectedHardwareItem.SelectedHardwareItem is LYR001Device)
            {
                LYR001Device d = this._selectedHardwareItem.SelectedHardwareItem as LYR001Device;
                string stationName = d.Station.Name;

                int deviceID = GuidHelper.ConvertToInt32(d.Guid);
                LocalController c = new LocalController();
                frmXD100ModbusTemperatureControl f =
                    new frmXD100ModbusTemperatureControl(stationName, deviceID, c);
                f.ShowDialog();
            }
            else
            {
                NUnit.UiKit.UserMessage.Display("selecte LYR001 device first");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void parentMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            LYR001Device d = _selectedHardwareItem.SelectedHardwareItem as LYR001Device;
            this._mnuTemperatureLine.Visible = d != null;
        }
    }

}
