
using System;
using Xdgk.Common;

namespace C3.Communi
{
    ///
    public class SuccessResult : ParseResultBase 
    {
        /// <summary>
        /// 
        /// </summary>
        public SuccessResult(string name)
        {
            this.Name = name;
            this.IsSuccess = true;
        }

        public override string ToString()
        {
            return strings.SuccessResult;
        }
    }
}
