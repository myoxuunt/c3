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
}
