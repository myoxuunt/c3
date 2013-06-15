using System;
using System.IO.Ports;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ICommuniPortConfig
    {
        bool CanCreate { get; }
        ICommuniPort Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        bool IsMatch(ICommuniPort cp);

        /// <summary>
        /// 
        /// </summary>
        UInt32 TimeoutMilliSecond { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortConfigCollection : Collection<ICommuniPortConfig>
    {

    }

}
