
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

}
