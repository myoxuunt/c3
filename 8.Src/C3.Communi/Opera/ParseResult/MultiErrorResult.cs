
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class MultiErrorResult : ParseResultBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ParseResultCollection Errors
        {
            get 
            {
                if (_errors == null)
                {
                    _errors = new ParseResultCollection();
                }
                return _errors;
            }
            set 
            {
                _errors = value;
            }
        } private ParseResultCollection _errors;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string r = string.Empty;
            foreach (IParseResult pr in Errors)
            {
                string s = string.Format("'{0}'", pr.ToString());
                if (r.Length > 0)
                {
                    r += ", ";
                }
                r += s;
            }
            return r;
        }
    }

}
