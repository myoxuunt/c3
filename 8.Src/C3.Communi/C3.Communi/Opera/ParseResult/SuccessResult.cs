
using System;
using Xdgk.Common;

namespace C3.Communi
{
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

    public class MultiErrorResult : ParseResultBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ParseResultCollection ParseResults
        {
            get 
            {
                if (_parseResults == null)
                {
                    _parseResults = new ParseResultCollection();
                }
                return _parseResults;
            }
            set 
            {
                _parseResults = value;
            }
        } private ParseResultCollection _parseResults;
    }

}
