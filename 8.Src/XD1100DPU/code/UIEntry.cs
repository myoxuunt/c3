using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
//using C3.Data;
using Xdgk.Common;
using Xdgk.GR.Common;


namespace XD1100DPU
{
    public class UIEntry : IUIEntry
    {
        private ToolStripMenuItem _otProviderSetting;
        private ToolStripMenuItem _mnuTemperatureLine;

        private ISelectedHardwareItem _selectedHardwareItem;

        private const string 
            MENU_OT = "室外温度(&T)...",
            MENU_GRSETTING = "供温设置(&S)...";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentMenuItem"></param>
        public void Set(ISelectedHardwareItem sel, ToolStripMenuItem parentMenuItem)
        {
            _selectedHardwareItem = sel;
            parentMenuItem.DropDownOpening += new EventHandler(parentMenuItem_DropDownOpening);

            _otProviderSetting = new ToolStripMenuItem(MENU_OT);
            _otProviderSetting.Click += new EventHandler(_otProviderSetting_Click);
            parentMenuItem.DropDownItems.Add(_otProviderSetting);


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
            frmOutsideStandard f = new frmOutsideStandard();
            f.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mnuTemperatureLine_Click(object sender, EventArgs e)
        {
            if (this._selectedHardwareItem.SelectedHardwareItem is XD1100Device)
            {
                XD1100Device d = this._selectedHardwareItem.SelectedHardwareItem as XD1100Device;
                string stationName = d.Station.Name;

                int deviceID = GuidHelper.ConvertToInt32(d.Guid);
                LocalController c = new LocalController();
                frmXD100ModbusTemperatureControl f =
                    new frmXD100ModbusTemperatureControl(stationName, deviceID, c);
                f.ShowDialog();
            }
            else
            {
                NUnit.UiKit.UserMessage.Display("selecte xd1100 device first");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void parentMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            XD1100Device d = _selectedHardwareItem.SelectedHardwareItem as XD1100Device;
            this._mnuTemperatureLine.Visible = d != null;
        }
    }

}
