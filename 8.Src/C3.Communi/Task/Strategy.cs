using System;

namespace C3.Communi
{
    #region Strategy
    /// <summary>
    /// 任务策略，控制任务的执行方式
    /// </summary>
    public abstract class Strategy
    {
        #region CreateCycleStrategy
        /// <summary>
        /// 
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        static public CycleStrategy CreateCycleStrategy(int second)
        {
            return new CycleStrategy(TimeSpan.FromSeconds(second));
        }
        #endregion //CreateCycleStrategy


        #region CreateImmediateStrategy
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public ImmediateStrategy CreateImmediateStrategy()
        {
            return new ImmediateStrategy();
        }
        #endregion //CreateImmediateStrategy

        /// <summary>
        /// 
        /// </summary>
        protected ITask m_Owning = null;



        #region Owning
        /// <summary>
        /// 获取或设置和该策略所关联的Task
        /// </summary>
        public ITask Owning
        {
            get
            {
                return m_Owning;
            }
            set
            {
                m_Owning = value;
            }
        }
        #endregion //Owning


        #region NeedExecute
        /// <summary>
        /// 获取一个值，该值指示Task是否需要执行。
        /// </summary>
        /// <returns></returns>
        public bool NeedExecute()
        {
            return NeedExecute(DateTime.Now);
        }
        #endregion //NeedExecute

        /// <summary>
        /// 获取一个值，该值指示Task是否需要执行。
        /// </summary>
        /// <returns></returns>
        abstract public bool NeedExecute(DateTime dt);


        #region CanRemove
        /// <summary>
        /// 获取一个值，该值指示该Task是否可以被删除。
        /// </summary>
        /// <remarks>
        /// TaskManager会根据该标记自动删除Task。
        /// </remarks>
        abstract public bool CanRemove
        {
            get;
        }
        #endregion //CanRemove


        #region FirstExecute
        /// <summary>
        /// 获取一个值，该值指示该Task是否需要首先执行。
        /// </summary>
        abstract public bool FirstExecute
        {
            get;
        }
        #endregion //FirstExecute
    }

    #endregion //Strategy

    #region CycleStrateg
    /// <summary>
    /// 
    /// </summary>
    public class CycleStrategy : Strategy
    {
        static readonly TimeSpan DEFAULT_MIN_CYCLE = new TimeSpan(0, 0, 0, 0, 30);
        static readonly TimeSpan DEFAULT_MAX_CYCLE = new TimeSpan(1, 0, 0, 0, 0);

        static private TimeSpan s_MinCycle = DEFAULT_MIN_CYCLE;
        static private TimeSpan s_MaxCycle = DEFAULT_MAX_CYCLE;

        public CycleStrategy(TimeSpan timeSpan)
        {
            Cycle = timeSpan;
        }

        #region Cycle
        /// <summary>
        /// 获取或设置执行周期
        /// </summary>
        public TimeSpan Cycle
        {
            get { return _cycle; }
            set
            {
                //if (!(value >= s_MinCycle && value <= s_MaxCycle))
                if (!CheckCycleTimeSpan(value))
                {
                    string b = "cycle must between {1} to {2}";
                    string s = string.Format(b, value, s_MinCycle, s_MaxCycle);
                    throw new ArgumentOutOfRangeException("Cycle", value, s);
                }
                _cycle = value;
            }
        } private TimeSpan _cycle;
        #endregion //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        static public bool CheckCycleTimeSpan(TimeSpan value)
        {
            if (value >= s_MinCycle && value <= s_MaxCycle)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 获取一个值，该值指示Task是否需要执行。
        /// </summary>
        public override bool NeedExecute(DateTime dt)
        {
            TimeSpan ts = dt - m_Owning.LastExecute;
            if (ts >= _cycle || ts < TimeSpan.Zero)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanRemove
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool FirstExecute
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static public TimeSpan MinCycle
        {
            get { return s_MinCycle; }
            set
            {
                if (!(value >= CycleStrategy.DEFAULT_MIN_CYCLE &&
                    value <= CycleStrategy.DEFAULT_MAX_CYCLE &&
                    value < s_MaxCycle))
                {
                    throw new ArgumentOutOfRangeException(
                        "MinCycle",
                        value,
                        string.Format("must between {0} to {1}", DEFAULT_MIN_CYCLE, DEFAULT_MAX_CYCLE)
                        );
                }
                CycleStrategy.s_MinCycle = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static public TimeSpan MaxCycle
        {
            get { return s_MaxCycle; }
            set
            {
                if (!(value >= CycleStrategy.DEFAULT_MIN_CYCLE &&
                    value <= CycleStrategy.DEFAULT_MAX_CYCLE &&
                    value > s_MinCycle))
                {
                    throw new ArgumentOutOfRangeException(
                        "MaxCycle",
                        value,
                        string.Format("must between {0} to {1}", DEFAULT_MIN_CYCLE, DEFAULT_MAX_CYCLE)
                        );
                }
                s_MaxCycle = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return strings.Cycle;
        }
    }
    #endregion //

    #region ImmediateStrategy
    /// <summary>
    /// 立即执行且只执行一次的Task
    /// </summary>
    public class ImmediateStrategy : Strategy
    {
        public ImmediateStrategy()
        {
        }

        public override bool NeedExecute(System.DateTime dt)
        {
            return true;
        }

        public override bool CanRemove
        {
            get
            {
                return true;
            }
        }

        public override bool FirstExecute
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return strings.Immediate;
        }
    }

    #endregion //ImmediateTaskStrategy
}
