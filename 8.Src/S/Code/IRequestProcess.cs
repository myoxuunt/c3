
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using C3.Communi;
using VGate100Common;
using VPump100Common;


namespace S
{
    internal interface IRequestProcess
    {
        bool Process(Client client, byte[] received);
    }

}
