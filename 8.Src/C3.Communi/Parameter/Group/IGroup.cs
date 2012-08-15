
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public interface IGroup : IOrderNumber, IModel
    {
        string Name { get; set; }
        string Text { get; set; }
        ParameterCollection Parameters { get; }
    }

}
