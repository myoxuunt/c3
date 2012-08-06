using System;
using System.Drawing ;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    public interface IParameterUI
    {
        Control Control { get; set; }
        IParameter Parameter { get; set; }
        void ApplyNewValue();
        Size Size { get; }
    }

}
