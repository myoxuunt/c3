using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public interface ICommuniPort
    {
        string ToXml();
    }

    public class CommuniPortCollection : Xdgk.Common.Collection<ICommuniPort>
    {

    }
}
