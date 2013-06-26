
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using C3.Communi;
using VGate100Common;
using VPump100Common;


namespace S
{
    internal class RequestProcessManager
    {
        static public RequestProcessManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new RequestProcessManager();
                }
                return _default;
            }
        } static private RequestProcessManager _default;

        private RequestProcessManager()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IRequestProcess[] GetRequestProcesses()
        {
            if (_processes == null)
            {
                _processes = new IRequestProcess[] { 
                    new VGate100RequestProcess (),
                        new VPump100RequestProcess (),
                };
            }
            return _processes;
        } private IRequestProcess[] _processes = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="received"></param>
        public void Process(Client client, byte[] received)
        {
            bool success = false;
            foreach (IRequestProcess item in this.GetRequestProcesses())
            {
                success = item.Process(client, received);
                if (success)
                {
                    break;
                }
            }

            if (!success)
            {
                string s = string.Format(Strings.InvalidRequest, BitConverter.ToString(received));
                client.LogItems.Add(new LogItem(DateTime.Now, s));
            }
        }
    }

}
