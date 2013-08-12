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
        public Task(IDevice device, IOpera opera, Strategy strategy)
            : this(device, opera, strategy, 1)
        {

        }

        public Task(IDevice device, IOpera opera, Strategy strategy, int retryTimes)
        {
            this.Device = device;
            this.Opera = opera;
            this.Strategy = strategy;
            this.Retry = new Retry(retryTimes);
        }
    }
}
