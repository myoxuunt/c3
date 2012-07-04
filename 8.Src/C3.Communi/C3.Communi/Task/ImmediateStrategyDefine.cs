
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class ImmediateStrategyDefine : StrategyDefine
    {
        public override Strategy Create()
        {
            return new ImmediateStrategy();
        }
    }

}
