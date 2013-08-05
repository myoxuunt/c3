using System;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;


namespace XD1100DPU
{
    public class UIEntry : IUIEntryFactory
    {
        private ToolStripMenuItem _otProviderSetting;
        private ToolStripMenuItem _mnuTemperatureLine;

        private const string 
            MENU_OT = "室外温度(&T)...",
            MENU_GRSETTING = "供温设置(&S)...";

        private const string
            MENU_OT_NAME = "mnuOtStandardSetting",
            MENU_GRSETTING_NAME = "mnuGRSetting";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentMenuItem"></param>
        public void Create(/*ISelectedHardwareItem sel,*/ ToolStripMenuItem parentMenuItem)
        {
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
            frmOutsideStandard f = new frmOutsideStandard();
            if (f.ShowDialog() == DialogResult.OK)
            {
                IOutside outSide = f.SelectedOutSide;
                if (outSide != null)
                {
                    OutsideTemperatureProviderManager.Provider = new DeviceOTProvider(outSide);

                    int deviceID = GuidHelper.ConvertToInt32(((IDevice)outSide).Guid);
                    DBI.Instance.SetOutsideTemperatureProviderDevice(deviceID);
                }
                else
                {
                    OutsideTemperatureProviderManager.Provider = null;
                    DBI.Instance.ClearOutsideTemperatureProviderDevice();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mnuTemperatureLine_Click(object sender, EventArgs e)
        {
            XD1100Device selectedXd1100 = SoftManager.GetSoft().SelectedHardwareItem as XD1100Device;
            if (selectedXd1100 != null)
            {
                string stationName = selectedXd1100.Station.Name;

                int deviceID = GuidHelper.ConvertToInt32(selectedXd1100.Guid);
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
            XD1100Device d = SoftManager.GetSoft().SelectedHardwareItem as XD1100Device;
            this._mnuTemperatureLine.Visible = d != null;
        }
    }

}
