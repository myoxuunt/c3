using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C3
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            C3.Communi.TestDeviceData.T();
            return;
            C3App.App.Run();

        }
    }
}
