
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using VGate100Common;
using VPump100Common;


namespace S
{
    public class FailResponse : BaseOpera
    {
        public FailResponse(byte address, byte cmdType, ResponseStatusEnum status)
            : base(address, cmdType)
        {
            this.Status = status;
        }

        public ResponseStatusEnum Status
        {
            get { return (ResponseStatusEnum)base.CommandNO; }
            set { base.CommandNO = (byte)value; }
        }

        public override byte[] OnToBytes()
        {
            return new byte[0];
        }
    }

    public class GateFailResponse : FailResponse
    {
        public GateFailResponse(ResponseStatusEnum status)
            : base(0, 0x85, status)
        {

        }
    }

    public class PumpFailResponse : FailResponse
    {
        public PumpFailResponse(ResponseStatusEnum status)
            : base(0, 0x86, status)
        {

        }
    }

}
