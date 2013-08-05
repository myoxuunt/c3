using System;
using Xdgk .Common ;

namespace Xdgk.GR.Common
{
    /// <summary>
    /// 
    /// </summary>
    public enum ManualAutomaticEnum
    {
        [EnumText("手动")]
        Manual = 0,

        [EnumText("自动")]
        Automatic = 1,
    }

    public class ManualAutomatic
    {

        static public ManualAutomatic Manual = new ManualAutomatic(ManualAutomaticEnum.Manual,
            EnumTextAttributeHelper.GetEnumTextAttributeValue(ManualAutomaticEnum.Manual));

        static public ManualAutomatic Automatic = new ManualAutomatic(ManualAutomaticEnum.Automatic,
            EnumTextAttributeHelper.GetEnumTextAttributeValue(ManualAutomaticEnum.Automatic));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ma"></param>
        /// <param name="text"></param>
        private ManualAutomatic( ManualAutomaticEnum ma, string text)
        {
            _manualAutomaticEnum = ma;
            _text = text;
        }

        public ManualAutomaticEnum ManualAutoMaticEnum
        {
            get { return _manualAutomaticEnum; }
        } private ManualAutomaticEnum _manualAutomaticEnum;

        public string Text
        {
            get { return _text; }
        } private string _text;

        public override string ToString()
        {
            return this.Text;
        }
    }


    public class PumpStatus
    {

        #region Members
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
        static public PumpStatus Fault = new PumpStatus(PumpStatusEnum.Fault, "故障");
        #endregion //Members

        #region Find
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
        #endregion //Find

        #region PumpStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private PumpStatus(PumpStatusEnum value, string text)
        {
            this.PumpStatusEnum = value;
            this.Text = text;
        }
        #endregion //PumpStatus

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

        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
        #endregion //ToString
    }


    /// <summary>
    /// 
    /// </summary>
    public class PumpData
    {
        public PumpData(string pumpName, int runningFrequency)
        {
            this.PumpName = pumpName;
            this.RunningFrequency = runningFrequency;
        }
        #region PumpName
        /// <summary>
        /// 
        /// </summary>
        public string PumpName
        {
            get
            {
                if (_pumpName == null)
                {
                    _pumpName = string.Empty;
                }
                return _pumpName;
            }
            set
            {
                _pumpName = value;
            }
        } private string _pumpName;
        #endregion //PumpName

        #region PumpStatus
        /// <summary>
        /// 
        /// </summary>
        public PumpStatus PumpStatus
        {
            get
            {
                return this.RunningFrequency > 0 ? PumpStatus.Run : PumpStatus.Stop;
            }
        } 
        #endregion //PumpStatus

        #region RunningFrequency
        /// <summary>
        /// 
        /// </summary>
        public int RunningFrequency
        {
            get
            {
                return _runningFrequency;
            }
            set
            {
                _runningFrequency = value;
            }
        } private int _runningFrequency;
        #endregion //RunningFrequency

        public override string ToString()
        {
            return string.Format("{0}: {1}Hz", this.PumpName, this.RunningFrequency);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PumpDataCollection : Xdgk.Common.Collection<PumpData>
    {
        public override string ToString()
        {
            string s = string.Empty;
            for (int i = 0; i < this.Count; i++)
            {
                s += this[i].ToString() + ((i == Count - 1) ? "" : ", ");
            }
            return s; 
        }
    }

}
