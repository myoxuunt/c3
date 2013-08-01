using System;
using Xdgk.Common;
using System.Windows.Forms;

namespace C3.Communi
{
    public interface IUIEntryFactory
    {
        void Create(ISelectedHardwareItem selectedHardwareItemProvider, ToolStripMenuItem parentMenuItem);
    }

}
