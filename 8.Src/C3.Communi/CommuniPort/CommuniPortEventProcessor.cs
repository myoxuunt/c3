
using System;
using System.Diagnostics;
using NLog;


namespace C3.Communi
{
    public class CommuniPortEventProcessor
    {
        private CommuniPortManager _cpManager;

        public CommuniPortEventProcessor(Soft soft, CommuniPortManager cpManager)
        {
            Debug.Assert(soft != null);
            this._soft = soft;

            Debug.Assert(cpManager != null);
            this._cpManager = cpManager;

            _cpManager.AddedCommuniPort += new CommuniPortEventHandler(_cpManager_AddedCommuniPort);
            _cpManager.ClosedCommuniPort += new CommuniPortEventHandler(_cpManager_ClosedCommuniPort);
            _cpManager.DeterminedCommuniPort += new CommuniPortEventHandler(_cpManager_DeterminedCommuniPort);
            _cpManager.ReceivedCommuniPort += new CommuniPortEventHandler(_cpManager_ReceivedCommuniPort);
            _cpManager.RemovedCommuniPort += new CommuniPortEventHandler(_cpManager_RemovedCommuniPort);
        }

        private Hardware GetHardware()
        {
            return this.Soft.Hardware;
        }

        void _cpManager_RemovedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            ICommuniPort cp = e.CommuniPort;
            StationCommuniPortBinder.Unbind(cp, GetHardware());
        }

        void _cpManager_ReceivedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            ICommuniPort cp = e.CommuniPort;
            ProcessReceived(cp);
        }

        void _cpManager_DeterminedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            ProcessDetermined(e.CommuniPort);
        }

        void _cpManager_ClosedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            // do nothing
            //
        }

        void _cpManager_AddedCommuniPort(object sender, CommuniPortEventArgs e)
        {
            StationCommuniPortBinder.Bind(e.CommuniPort, this.GetHardware());
        }

        /// <summary>
        /// 
        /// </summary>
        public Soft Soft
        {
            get { return _soft; }
        } private Soft _soft;

        #region ProcessReceived
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessReceived(ICommuniPort cp)
        {
            //ICommuniPort cp = (ICommuniPort)sender;
            byte[] bs = cp.Read();

            // TODO: cp.stations
            //
            StationCollection stations = new StationCollection();
            foreach (IStation st in this.Soft.Hardware.Stations)
            {
                if (st.CommuniPortConfig.IsMatch(cp))
                {
                    stations.Add(st);
                }
            }

            foreach (IStation st in stations)
            {
                foreach (IDevice d in st.Devices)
                {
                    if (bs.Length > 0)
                    {
                        //ITaskProcessor processor = d.Dpu.Processor;
                        //IUploadParseResult parseResult = processor.ProcessUpload(d, bs);
                        //bs = parseResult.Remain;
                        d.ProcessUpload(bs);
                    }
                }
            }

        }
        #endregion //ProcessReceived

        #region ProcessDetermined
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProcessDetermined(ICommuniPort cp )
        {
            Hardware hd = this.GetHardware();
            StationCommuniPortBinder.Bind(cp, hd);
        }
        #endregion //ProcessDetermined
    }

}
