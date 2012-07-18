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
        {
            this.Device = device;
            this.Opera = opera;
            this.Stragegy = strategy;
            this.Timeout = timeout;
        }
    }
}
