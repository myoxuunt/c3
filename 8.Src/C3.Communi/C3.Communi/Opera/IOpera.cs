using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public interface IOpera
    {
        byte[] CreateSendBytes(IDevice device);

        IParseResult ParseReceivedBytes(IDevice device, byte[] received);
    }

    abstract public class OperaBase : IOpera
    {
        public byte[] CreateSendBytes(IDevice device)
        {
            byte[] bytes = OnCreateSendBytes(device);
            
            return bytes;
        }

        abstract public byte[] OnCreateSendBytes(IDevice device);

        public IParseResult ParseReceivedBytes(IDevice device, byte[] received)
        {
            IParseResult pr = OnParseReceivedBytes(device, received);
            return pr;
        }

        abstract public IParseResult OnParseReceivedBytes(IDevice device, byte[] received);
    }

}
