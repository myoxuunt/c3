
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi ;


namespace C3
{
    public class DisplayArea
    {
        public DisplayArea(Label label, Panel panel)
        {
            this.Label = label;
            this.Panel = panel;
        }
        /// <summary>
        /// 
        /// </summary>
        public Label Label
        {
            get { return _label; }
            set { _label = value; }
        } private Label _label;


        /// <summary>
        /// 
        /// </summary>
        public Panel Panel
        {
            get { return _panel; }
            set { _panel = value; }
        } private Panel _panel;


    }

}
