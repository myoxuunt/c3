using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using NLog;
using C3.Remote;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteServer
    {
        static private NLog.Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 
        /// </summary>
        public bool IsStarted
        {
            get { return _isStarted; }
        } private bool _isStarted;

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (!_isStarted)
            {
                //string filename = "Config\\Remote.xml";
                //RemotingConfiguration.Configure(filename, false);
                C3.Remote.RemoteObject.Executeing += new ExecuteEventHandler(RemoteObject_Executeing);

                IDictionary tcpProperties = new Hashtable();
                tcpProperties["name"] = "RemoteTest";
                tcpProperties["port"] = 9000;

                BinaryClientFormatterSinkProvider tcpClientSinkProvider =
                    new BinaryClientFormatterSinkProvider();

                BinaryServerFormatterSinkProvider tcpServerSinkProvider =
                    new BinaryServerFormatterSinkProvider();

                tcpServerSinkProvider.TypeFilterLevel =
                    System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

                TcpChannel tcpChannel = new TcpChannel(
                    tcpProperties,
                    tcpClientSinkProvider,
                    tcpServerSinkProvider);

                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(Remote.RemoteObject),
                    "RO",
                     WellKnownObjectMode.Singleton);

                _isStarted = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RemoteObject_Executeing(object sender, ExecuteEventArgs e)
        {
            string s = string.Format("{0}: {1}, ", "stationName",e.Parameter.StationName);
            s+= string.Format("{0}: {1}, ", "address",e.Parameter.DeviceAddress );
            s+= string.Format("{0}: {1}, ", "executename",e.Parameter.ExecuteName );
            s+= string.Format("{0}: {1}, ", "count",e.Parameter.HashTable.Count );
            log.Info("Executing");

            e.CallbackWrapper.Callback("status");
            log.Info(s);
            Result r = new Result();
            r.ResultEnum = ResultEnum.Fail;
            r.FailMessage = "fial message.";
            e.Result = r;
        }
    }
}
