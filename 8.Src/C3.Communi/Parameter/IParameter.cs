using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{

    public interface IParameter : IOrderNumber 
    {
        ParameterGroup Group { get; set; }
        //int OrderNumber { get; set; }
        string Name { get; set; }
        string Text { get; set; }
        object Value { get; set; }
        void SetValue(string stringValue);
        Type ValueType { get; set; }
        string Description { get; set; }
        Unit Unit { get; set; }
        // TODO: 2012-08-03
        //
        // ParameterOption Option{get;set;}

        IParameterUI ParameterUI { get; set; }
    }
}
