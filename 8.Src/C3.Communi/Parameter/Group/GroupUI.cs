using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
    public class GroupUI : GroupUIBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        public GroupUI(IGroup group)
            : base(group)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplyNewValue()
        {
            IGroup group = this.Group;
            foreach (IParameter item in this.Group.Parameters)
            {
                item.ParameterUI.ApplyNewValue();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnSetGroup()
        {
            UCGroupUI ui = new UCGroupUI();
            ui.Group = this.Group;
            this.Control = ui;
        }
    }

}
