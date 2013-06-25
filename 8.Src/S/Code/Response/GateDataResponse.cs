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
    /// <summary>
    /// 
    /// </summary>
    public class GateDataResponse : NameCountResponseBase
    {
        public GateDataResponse(string gateName, VGate100Data[] datas, byte datasCount)
            : base(0, 0x85, gateName, datas, datasCount)
        {
        }
    }
}
