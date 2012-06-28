
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IIdentityParser
    {
        bool Parse(byte[] identityBytes, out string identity);
    }

}
