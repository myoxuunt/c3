using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class Task : TaskBase
    {
        public Task(IDevice device, IOpera opera, Strategy strategy, TimeSpan timeout)
            : this(device, opera, strategy, timeout, 1)
        {

        }

        public Task(IDevice device, IOpera opera, Strategy strategy, TimeSpan timeout, int retryTimes)
        {
            this.Device = device;
            this.Opera = opera;
            this.Strategy = strategy;
            this.Timeout = timeout;
            this.Retry = new Retry(retryTimes);
        }
    }
}
