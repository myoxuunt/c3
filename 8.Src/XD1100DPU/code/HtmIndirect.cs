
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
//using C3.Data;
using Xdgk.Common;


namespace XD1100DPU
{
    internal class HtmIndirect: HeatTransferMode
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Indirect ; }
        }
    }

}
