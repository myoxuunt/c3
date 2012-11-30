using System;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExecuteController : IDisposable 
    {
        event EventHandler ResultEvent;

        ExecuteResult Doit(ExecuteArgs args);
        ResultArgs ResultArgs{ get; }
    }

}
