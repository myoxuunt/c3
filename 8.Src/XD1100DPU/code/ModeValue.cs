
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
using C3.Data;


namespace XD1100DPU
{
    public enum ModeValue
    {
        [EnumText("DirectText")]
            Direct,
            [EnumText("InDirectText")]
                Indirect,
            //[EnumText("MixedText")]
            Mixed,
    }

}
