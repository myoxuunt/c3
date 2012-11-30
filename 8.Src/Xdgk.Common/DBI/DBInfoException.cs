using System;

namespace Xdgk.Common
{
    public class DBInfoException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DBInfoException(string message)
            : base(message)
        {
        }
    }

}
