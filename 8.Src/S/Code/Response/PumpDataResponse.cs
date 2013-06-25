
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
    public class PumpDataResponse : NameCountResponseBase 
    {
        public PumpDataResponse(string pumpName, VPump100Data [] datas, byte datasCount)
            : base(0, 0x86, pumpName, datas, datasCount)
        {
        }
    }

}
