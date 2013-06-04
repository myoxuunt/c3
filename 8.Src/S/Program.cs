using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SApp.App.Run();
        }
    }

    public class Client
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="communiPort"></param>
        public Client(ICommuniPort communiPort )
        {
            this.CommuniPort = communiPort;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get { return _communiPort; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CommuniPort");
                }
                _communiPort = value;
            }
        } private ICommuniPort _communiPort;
    }
}
