using System;
using System.Collections.Generic;
using System.Text;
using C3.Data;
namespace Xdgk.GR.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class FlowmeterData : IDeviceData
    {
        #region Sum
        /// <summary>
        /// 
        /// </summary>
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

        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
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


        #region IDeviceData 成员

        public DateTime DT
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        } DateTime _dt = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReportItemCollection GetReportItems()
        {
            ReportItemCollection ris = new ReportItemCollection();
            ris.Add(new ReportItem("时间", this.DT, Unit.FindByName(Unit.None)));
            ris.Add(new ReportItem("瞬时", this.InstantFlux, Unit.FindByName(Unit.M3PerSecond)));
            ris.Add(new ReportItem("累计", this.Sum, Unit.FindByName(Unit.M3)));
            return ris;
        }

        #endregion
    }
}
