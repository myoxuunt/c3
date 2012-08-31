using System;
using C3.Data;

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
        // TODO: 2012-08-03 parameter option
        //
        // ParameterOption Option{get;set;}

    }

}
