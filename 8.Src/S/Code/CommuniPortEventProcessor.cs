using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{
    internal class CommuniPortEventProcessor
    {
        private CommuniPortManager _cpManager;
        private ClientManager _clientManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpManager"></param>
        internal CommuniPortEventProcessor(CommuniPortManager cpManager)
        {
            Debug.Assert(cpManager != null);
            this._cpManager = cpManager;


            _cpManager.AddedCommuniPort += new CommuniPortEventHandler(_cpManager_AddedCommuniPort);
            _cpManager.ClosedCommuniPort += new CommuniPortEventHandler(_cpManager_ClosedCommuniPort);
            _cpManager.DeterminedCommuniPort += new CommuniPortEventHandler(_cpManager_DeterminedCommuniPort);
            _cpManager.ReceivedCommuniPort += new CommuniPortEventHandler(_cpManager_ReceivedCommuniPort);
            _cpManager.RemovedCommuniPort += new CommuniPortEventHandler(_cpManager_RemovedCommuniPort);
        }

        void _cpManager_RemovedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            this._clientManager.RemoveByCommuniPort(e.CommuniPort);
        }

        void _cpManager_ReceivedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _cpManager_DeterminedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _cpManager_ClosedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _cpManager_AddedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            Client c = new Client(e.CommuniPort);
            this._clientManager.Add(c);
        }
    }

}
