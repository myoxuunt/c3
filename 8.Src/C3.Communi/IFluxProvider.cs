using System;
using Xdgk.Common;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGT1Provider
    {
        DateTime GT1DataDT { get; }
        double GT1 { get; }
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IFluxProvider
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime FluxDataDT { get; }

        /// <summary>
        /// 
        /// </summary>
        double InstantFlux { get; }

        /// <summary>
        /// 
        /// </summary>
        double Sum { get; }

        /// <summary>
        /// 
        /// </summary>
        FluxPlace FluxPlace { get; }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum FluxPlace
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumText("一次侧")]
        FirstSide = 0,

        /// <summary>
        /// 
        /// </summary>
        [EnumText("二次侧")]
        SecondSide = 1,

        /// <summary>
        /// 
        /// </summary>
        [EnumText("补水")]
        RecruitSide = 2,

        /// <summary>
        /// 
        /// </summary>
        [EnumText("未知")]
        Unknown = 3,
    }
}
