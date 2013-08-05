
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;


namespace LYR001DPU
{
    internal class LYR001StatusData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cm1"></param>
        /// <param name="cm2"></param>
        /// <param name="cm3"></param>
        /// <param name="rm1"></param>
        /// <param name="rm2"></param>
        /// <param name="rm3"></param>
        /// <param name="manOrAuto"></param>
        /// <param name="alarmList"></param>
        public LYR001StatusData(PumpStatus cm1,
            PumpStatus cm2,
            PumpStatus cm3,
            PumpStatus rm1,
            PumpStatus rm2,
            ManualAutomatic manOrAuto,
            List<string> alarmList
            )
        {
            _cM1 = cm1;
            _cM2 = cm2;
            _cM3 = cm3;

            _rM1 = rm1;
            _rM2 = rm2;

            _manualAutomatic = manOrAuto;
            _alarmList = alarmList;

        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get { return _dt; }
        } private DateTime _dt = DateTime.Now;

        #region CM1
        /// <summary>
        /// 
        /// </summary>
        public PumpStatus CM1
        {
            get
            {
                return _cM1;
            }
        } private PumpStatus _cM1;
        #endregion //CM1

        #region CM2
        /// <summary>
        /// 
        /// </summary>
        public PumpStatus CM2
        {
            get
            {
                return _cM2;
            }
        } private PumpStatus _cM2;
        #endregion //CM2

        #region CM3
        /// <summary>
        /// 
        /// </summary>
        public PumpStatus CM3
        {
            get
            {
                return _cM3;
            }
        } private PumpStatus _cM3;
        #endregion //CM3

        #region RM1
        /// <summary>
        /// 
        /// </summary>
        public PumpStatus RM1
        {
            get
            {
                return _rM1;
            }
        } private PumpStatus _rM1;
        #endregion //RM1

        #region RM2
        /// <summary>
        /// 
        /// </summary>
        public PumpStatus RM2
        {
            get
            {
                return _rM2;
            }
        } private PumpStatus _rM2;
        #endregion //RM2


        /// <summary>
        /// 
        /// </summary>
        public ManualAutomatic ManualAutomatic
        {
            get { return _manualAutomatic; }
        } private ManualAutomatic _manualAutomatic;

        /// <summary>
        /// 
        /// </summary>
        public List<string> AlarmList
        {
            get { return _alarmList; }
        } private List<string> _alarmList;
    }


}
