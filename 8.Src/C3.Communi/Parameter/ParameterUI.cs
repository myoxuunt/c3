using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
    public class StringParameterUI : ParameterUIBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnSetParameter(IParameter parameter)
        {
            UCParameterUI paramCtrl = new UCParameterUI();
            paramCtrl.Parameter = parameter;
            this.Control = paramCtrl;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplyNewValue()
        {
            UCParameterUI paramCtrl = this.Control as UCParameterUI;
            paramCtrl.ApplyNewValue();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EnumParameterUI : ParameterUIBase
    {
        private UCComboBoxParameterUI ComboxParameterUI
        {
            get { return (UCComboBoxParameterUI)this.Control; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnSetParameter(IParameter parameter)
        {
            UCComboBoxParameterUI c = new UCComboBoxParameterUI();
            c.Parameter = parameter;

            this.Control = c;
        }

        protected override void OnApplyNewValue()
        {
            this.ComboxParameterUI.ApplyNewValue();
        }
    }

}
