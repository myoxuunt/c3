using System;

namespace Xdgk.GR.Common
{
    public class PumpStatus
    {

        #region Members
        /// <summary>
        /// 
        /// </summary>
        static public PumpStatus Run = new PumpStatus(PumpStatusEnum.Run, "‘À––");
        /// <summary>
        /// 
        /// </summary>
        static public PumpStatus Stop = new PumpStatus(PumpStatusEnum.Stop, "Õ£÷π");
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

}
