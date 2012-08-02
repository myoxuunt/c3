
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class IdentityParserCollection : Collection<IIdentityParser>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityBytes"></param>
        /// <returns></returns>
        public bool Parse(byte[] identityBytes, out string identity)
        {
            bool b = false;
            identity = null;

            foreach (IIdentityParser parser in this)
            {
                b = parser.Parse(identityBytes, out identity);
                if (b)
                {
                    break;
                }
            }
            return b;
        }
    }

}
