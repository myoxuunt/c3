using System;
using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ITaskFactory
    {
        TaskCollection Create(IDevice device);
    }
}
