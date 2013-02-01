using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    public interface IFilter
    {
        byte[] Filtrate(byte[] source);
    }
}
