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
            IParseResult pr = null;
            if (this.Count == 0)
            {
                pr = new HasNotReceivedPartError();
            }
            else if (this.Count == 1)
            {
                ReceivePart rp = this[0];
                pr = rp.ToValues(bytes);
            }
            else
            {
                pr = ParseMutilReceivePart(bytes);
            }
            return pr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private IParseResult ParseMutilReceivePart(byte[] bytes)
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
                    failpr.Errors.Add(pr);
                }
            }
            return failpr;
        }
    }
}
