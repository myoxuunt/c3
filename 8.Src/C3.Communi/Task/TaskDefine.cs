using System;

namespace C3.Communi
{
    public class TaskDefine
    {

        //#region DefaultTaskTimeout
        ///// <summary>
        ///// 获取或设置默认任务超时时间
        ///// </summary>
        //static public TimeSpan DefaultTaskTimeout
        //{
        //    get { return _defaultTaskTimeout; }
        //    set { _defaultTaskTimeout = value; }
        //} static private TimeSpan _defaultTaskTimeout = TimeSpan.FromSeconds(10);
        //#endregion //DefaultTaskTimeout

        #region TaskDefine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="operaName"></param>
        /// <param name="strategyDefine"></param>
        /// <param name="taskTimeout"></param>
        public TaskDefine(string deviceType, string operaName, StrategyDefine strategyDefine, int retryTimes)
                //TimeSpan taskTimeout)
        {
            if (strategyDefine == null)
            {
                throw new ArgumentNullException("strategyDefine");
            }

            this.DeviceType = deviceType;
            this.OperaName = operaName;
            this.StrategyDefine = strategyDefine;
            this.RetryTimes = retryTimes;
            //this.TimeOut = taskTimeout;
        }
        #endregion //TaskDefine

        #region DeviceType
        /// <summary>
        /// 
        /// </summary>
        public string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        } private string _deviceType;
        #endregion //DeviceType

        #region OperaName
        /// <summary>
        /// 
        /// </summary>
        public string OperaName
        {
            get { return _operaName; }
            set { _operaName = value; }
        } private string _operaName;
        #endregion //OperaName

        //#region TimeOut
        ///// <summary>
        ///// 
        ///// </summary>
        //public TimeSpan TimeOut
        //{
        //    get { return _timeOut; }
        //    set { _timeOut = value; }
        //} private TimeSpan _timeOut;
        //#endregion //TimeOut

        #region StrategyDefine
        /// <summary>
        /// 
        /// </summary>
        public StrategyDefine StrategyDefine
        {
            get { return _strategyDefine; }
            set { _strategyDefine = value; }
        } private StrategyDefine _strategyDefine;
        #endregion //StrategyDefine

        public int RetryTimes
        {
            get { return _retryTimes; }
            set
            {
                if (value < Retry.RETRY_MIN)
                {
                    _retryTimes = Retry.RETRY_MIN;
                }
                else if (value > Retry.RETRY_MAX)
                {
                    _retryTimes = Retry.RETRY_MAX;
                }
                else
                {
                    _retryTimes = value;
                }
            }
        } private int _retryTimes = 1;

        #region Create
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationCollection"></param>
        /// <returns></returns>
        //public TaskCollection Create(StationCollection stationCollection)
        public ITask Create()
        {
               if (stationCollection == null)
               {
               throw new ArgumentNullException("stationCollection");
               }

               TaskCollection taskCollection = new TaskCollection();
               foreach( Station st in stationCollection )
               {
               foreach (Device device in st.Devices)
               {
               if (StringHelper.Equal(device.DeviceDefine.DeviceType,
               this.DeviceType))
               {
               Opera opera = device.DeviceDefine.CreateOpera(this.OperaName);
               Strategy strategy = this.StrategyDefine.Create();
               Task task = new Task(device, opera, strategy, TimeOut);
               taskCollection.Add(task);
               }
               }
               }
               return taskCollection;
            return null;
        }
    */
        #endregion //Create
    }

}
