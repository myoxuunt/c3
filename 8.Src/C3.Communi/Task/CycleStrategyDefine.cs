
using System;
using Xdgk.Common;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>

    public class CycleStrategyDefine : StrategyDefine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cycle"></param>
        public CycleStrategyDefine(TimeSpan cycle)
        {
            this.Cycle = cycle;
        }

        #region Cycle
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Cycle
        {
            get { return _cycle; }
            set { _cycle = value; }
        } private TimeSpan _cycle;
        #endregion //Cycle

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Strategy Create()
        {
            return new CycleStrategy(this.Cycle);
        }
    }

}
