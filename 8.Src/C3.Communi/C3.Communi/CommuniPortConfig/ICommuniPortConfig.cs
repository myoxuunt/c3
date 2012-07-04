
using System;
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
    }


}
