using System;
using System.Reflection;

namespace Xdgk.Common
{
    public class HeatData : DataBase
    {
        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时流量", 10, Unit.M3PerHour, "f2")]
        public double InstantFlux
        {
            get
            {
                return _instantFlux;
            }
            set
            {
                _instantFlux = value;
            }
        } private double _instantFlux;
        #endregion //InstantFlux

        #region Sum
        /// <summary>
        /// 
        /// </summary>
        [DataItem("累计流量", 20, Unit.M3, "f0")]
        public double Sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
            }
        } private double _sum;
        #endregion //Sum

        #region InstantHeat
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时热量", 30, Unit.GJPerHour, "f2")]
        public double InstantHeat
        {
            get
            {
                return _instantHeat;
            }
            set
            {
                _instantHeat = value;
            }
        } private double _instantHeat;
        #endregion //InstantHeat

        #region SumHeat
        /// <summary>
        /// 
        /// </summary>
        [DataItem("累计热量", 40, Unit.GJ, "f0")]
        public double SumHeat
        {
            get
            {
                return _sumHeat;
            }
            set
            {
                _sumHeat = value;
            }
        } private double _sumHeat;
        #endregion //SumHeat

        #region GT
        /// <summary>
        /// 
        /// </summary>
        [DataItem("供温", 50, Unit.Centidegree, "f2")]
        public double GT
        {
            get
            {
                return _gT;
            }
            set
            {
                _gT = value;
            }
        } private double _gT;
        #endregion //GT

        #region BT
        /// <summary>
        /// 
        /// </summary>
        [DataItem("回温", 60, Unit.Centidegree, "f2")]
        public double BT
        {
            get
            {
                return _bT;
            }
            set
            {
                _bT = value;
            }
        } private double _bT;
        #endregion //BT
    }

}
