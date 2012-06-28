using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public interface IParseResult
    {
        bool IsSuccess { get; set; }

        string Name { get; set; }

        KeyValueCollection Results { get; set; }

        byte[] ParseBytes { get; set; }
    }

    public class ParseResultCollection : Collection<IParseResult>
    {
    }
}
