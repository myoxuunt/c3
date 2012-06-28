using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class ReceivePartCollection : Collection<ReceivePart>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal IParseResult ToValues(byte[] bytes)
        {
            MultiErrorResult failpr = new MultiErrorResult();

            foreach (ReceivePart rp in this)
            {
                IParseResult pr = rp.ToValues(bytes);
                if (pr.IsSuccess)
                {
                    return pr;
                }
                else
                {
                    failpr.ParseResults.Add(pr);
                }
            }
            return failpr;
        }
    }
}
