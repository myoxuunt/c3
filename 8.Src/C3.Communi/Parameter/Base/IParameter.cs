
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public interface IParameter : IOrderNumber, IModel
    {
        string Name { get; set; }
        string Text { get; set; }
        object Value { get; set; }
        //void SetValue(string stringValue);
        Type ValueType { get; set; }
        string Description { get; set; }
        Unit Unit { get; set; }
        // TODO: 2012-08-03
        //
        // ParameterOption Option{get;set;}

    }

}
