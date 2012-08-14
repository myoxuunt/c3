
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    public interface IGroup : IOrderNumber
    {
        string Name { get; set; }
        string Text { get; set; }
        //IGroupUI GroupUI { get; set; }
        ParameterCollection Parameters { get; }
    }

}
