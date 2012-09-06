using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFluxProvider
    {
        double InstantFlux { get; }
        double Sum { get; }
    }
}
