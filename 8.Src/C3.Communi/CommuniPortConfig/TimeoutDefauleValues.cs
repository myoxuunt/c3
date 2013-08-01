
using System;
using System.IO.Ports;
using Xdgk.Common;


namespace C3.Communi
{
    internal static class TimeoutDefauleValues
    {
        public const uint
            MinTimeoutMillsSencond = 100,
            MaxTimeoutMillsSecond = 60 * 1000,
            DefaultTimeoutMillsSecond = 10 * 1000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutValue"></param>
        static public void Verify(uint timeoutValue)
        {
            if (timeoutValue < MinTimeoutMillsSencond || timeoutValue > MaxTimeoutMillsSecond)
            {
                string s = string.Format(
                        "Timeout value out of range, timeout value must between [{0}, {1}] millsSencond, current is {2}",
                        MinTimeoutMillsSencond,
                        MaxTimeoutMillsSecond,
                        timeoutValue);
                throw new ArgumentOutOfRangeException(s);
            }
        }
    }

}
