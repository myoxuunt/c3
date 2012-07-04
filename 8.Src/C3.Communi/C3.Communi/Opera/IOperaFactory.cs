using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Xdgk.Common;
using Xdgk.Communi.Interface;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOperaFactory
    {
        IOpera Create(string operaName);
    }
}