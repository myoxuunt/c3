using System;
using System.IO.Ports;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class CommuniPortConfigBase : ICommuniPortConfig
    {

        abstract public bool CanCreate { get; }

        abstract public ICommuniPort Create();

        abstract public bool IsMatch(ICommuniPort cp);

        /// <summary>
        /// 
        /// </summary>
        public uint TimeoutMilliSecond
        {
            get
            {
                return _timeoutMilliSecond;
            }
            set
            {
                _timeoutMilliSecond = TimeoutDefauleValues.FixTimeoutMilliSencond(value);
            }
        } private uint _timeoutMilliSecond = TimeoutDefauleValues.DefaultTimeoutMilliSecond;
    }
}
