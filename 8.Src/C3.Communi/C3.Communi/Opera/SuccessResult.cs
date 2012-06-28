
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class SuccessResult : ParseResultBase 
    {
        /// <summary>
        /// 
        /// </summary>
        //public SuccessResult(string deviceType, string operaName)
        //: base(deviceType, operaName)
        public SuccessResult()
        {
            this.IsSuccess = true;
        }

        public override string ToString()
        {
            return strings.SuccessResult;
        }
    }

}
