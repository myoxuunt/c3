
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
using C3.Data;


namespace XD1100DPU
{
    public class UIEntry : IUIEntry
    {
        private ToolStripMenuItem _otProviderSetting;
        private ToolStripMenuItem _mnuTemperatureLine;

        private ISelectedHardwareItem _selectedHardwareItem;

#region IUIEntry 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentMenuItem"></param>
        public void Set(ISelectedHardwareItem sel, ToolStripMenuItem parentMenuItem)
        {
            _selectedHardwareItem = sel;
            parentMenuItem.DropDownOpening += new EventHandler(parentMenuItem_DropDownOpening);

            ToolStripMenuItem _otProviderSetting = new ToolStripMenuItem("室外温度(&T)...");
            _otProviderSetting.Click += new EventHandler(_otProviderSetting_Click);
            parentMenuItem.DropDownItems.Add(_otProviderSetting);


            _mnuTemperatureLine = new ToolStripMenuItem("tempLine(&L)...");
            _mnuTemperatureLine.Click += new EventHandler(_mnuTemperatureLine_Click);
            parentMenuItem.DropDownItems.Add(_mnuTemperatureLine);

        }

        void _otProviderSetting_Click(object sender, EventArgs e)
        {
            frmOutsideStandard f = new frmOutsideStandard();
            f.ShowDialog();
        }

        void _mnuTemperatureLine_Click(object sender, EventArgs e)
        {
            NUnit.UiKit.UserMessage.Display("temp line");
        }

        void parentMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            XD1100Device d = _selectedHardwareItem.SelectedHardwareItem as XD1100Device;
            this._mnuTemperatureLine.Visible = d != null;
        }
#endregion
    }

}
