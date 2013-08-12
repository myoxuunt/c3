using System;
using System.IO.Ports;
using Xdgk.Common;

namespace C3.Communi
{
    internal static class TimeoutDefauleValues
    {
        /// <summary>
        /// 
        /// </summary>
        internal const uint
            MinTimeoutMilliSencond = 100,
            MaxTimeoutMilliSecond = 60 * 1000,
            DefaultTimeoutMilliSecond = 10 * 1000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutValue"></param>
        static internal void Verify(uint timeoutValue)
        {
            if (timeoutValue < MinTimeoutMilliSencond || timeoutValue > MaxTimeoutMilliSecond)
            {
                string s = string.Format(
                        "Timeout value out of range, timeout value must between [{0}, {1}] millsSencond, current is {2}",
                        MinTimeoutMilliSencond,
                        MaxTimeoutMilliSecond,
                        timeoutValue);
                throw new ArgumentOutOfRangeException(s);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static internal uint FixTimeoutMilliSencond(uint value)
        {
            if (value > MaxTimeoutMilliSecond)
            {
                value = MaxTimeoutMilliSecond;
            }
            else if (value < MinTimeoutMilliSencond)
            {
                value = MinTimeoutMilliSencond;
            }
            return value;
        }
    }

}
