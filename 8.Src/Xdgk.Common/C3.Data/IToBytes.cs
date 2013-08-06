
using System;
using System.Reflection;


namespace Xdgk.Common
{
    public interface IToBytes
    {
        byte[] ToBytes();
        int BytesCountOfEmpty { get; }
    }

}
