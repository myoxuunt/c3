using System;
using System.Collections.Generic;
using System.Text;
using C3.Data;

namespace Xdgk.GR.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class FlowmeterData : DeviceDataBase
    {
        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("瞬时", 10, Unit.M3PerSecond, "f2")]
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
        [DeviceDataItem("累计", 20, Unit.M3, "f0")]
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
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PumpStatusEnum
    {
        Stop = 0,
        Run = 1,
    }

    public class PumpStatus
    {
        /// <summary>
        /// 
        /// </summary>
        static public PumpStatus Run = new PumpStatus(PumpStatusEnum.Run, "运行");
        /// <summary>
        /// 
        /// </summary>
        static public PumpStatus Stop = new PumpStatus(PumpStatusEnum.Stop, "停止");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public PumpStatus Find(PumpStatusEnum value)
        {
            if (value == PumpStatusEnum.Run)
            {
                return Run;
            }
            else if (value == PumpStatusEnum.Stop)
            {
                return Stop;
            }
            else
            {
                throw new ArgumentException(value.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private PumpStatus(PumpStatusEnum value, string text)
        {
            this.PumpStatusEnum = value;
            this.Text = text;
        }

        #region PumpStatusEnum
        /// <summary>
        /// 
        /// </summary>
        public PumpStatusEnum PumpStatusEnum
        {
            get
            {
                return _pumpStatusEnum;
            }
            private set
            {
                _pumpStatusEnum = value;
            }
        } private PumpStatusEnum _pumpStatusEnum;
        #endregion //PumpStatusEnum

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    _text = string.Empty;
                }
                return _text;
            }
            private set
            {
                _text = value;
            }
        } private string _text;
        #endregion //Text

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
