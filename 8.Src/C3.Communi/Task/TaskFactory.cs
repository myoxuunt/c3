using System;
using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ITaskFactory
    {
        IDPU Dpu { get; set; }
        TaskCollection Create(IDevice device);
    }
}
