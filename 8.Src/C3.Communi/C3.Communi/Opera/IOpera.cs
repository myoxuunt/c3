using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public interface IOpera
    {
        byte[] CreateSend(IDevice device);

        IParseResult Parse(IDevice device, byte[] received);
    }

}
