using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
    public class ParameterUI : ParameterUIBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnSetParameter(IParameter parameter)
        {
            if (parameter.ValueType.IsEnum)
            {
                UCComboBoxParameterUI c = new UCComboBoxParameterUI();
                c.Parameter = parameter;
                this.Control = c;
            }
            else
            {
                UCParameterUI paramCtrl = new UCParameterUI();
                paramCtrl.Parameter = parameter;
                this.Control = paramCtrl;
            }
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

}
