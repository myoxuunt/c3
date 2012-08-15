
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public interface IParameterViewer : IViewer
    {
        Control Control { get; set; }
    }

}
