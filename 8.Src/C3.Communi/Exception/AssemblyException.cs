using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class AssemblyException : Exception 
    {
        public AssemblyException(string msg)
            : base(msg)
        {

        }

        public AssemblyException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigException : System.Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ConfigException(string message)
            : base( message )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerEx"></param>
        public ConfigException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class C3Exception : Exception
    {
        public C3Exception(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public C3Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
