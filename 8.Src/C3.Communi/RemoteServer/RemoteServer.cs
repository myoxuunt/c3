using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using Xdgk.Common;
using NLog;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteServer
    {
        static private NLog.Logger log = LogManager.GetCurrentClassLogger();

        #region IsStarted
        /// <summary>
        ///
        /// 
        /// </summary>
        public bool IsStarted
        {
            get { return _isStarted; }
        } private bool _isStarted;
        #endregion //IsStarted

        #region Start
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (!_isStarted)
            {
                Xdgk.Common.RemoteObject.Executeing += new ExecuteEventHandler(RemoteObject_Executeing);

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
                    typeof(Xdgk.Common.RemoteObject),
                    "RO",
                     WellKnownObjectMode.Singleton);

                _isStarted = true;
            }
        }
        #endregion //Start

        #region RemoteObject_Executeing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RemoteObject_Executeing(object sender, ExecuteEventArgs e)
        {
            ExecuteResult r = null;
            int id = e.ExecuteArgs.DeviceID;
            IDevice device = SoftManager.GetSoft().Hardware.FindDevice(id);

            if (StringHelper.Equal(e.ExecuteArgs.ExecuteName, DefineExecuteNames.IsReady))
            {
                if (device == null)
                {
                    string s = string.Format(RemoteStrings.NotFindDeviceByID, id);
                    r = ExecuteResult.CreateFailExecuteResult(s);
                }
                else
                {
                    if (device.Station.CommuniPort != null &&
                        device.Station.CommuniPort.IsOpened)
                    {
                        r = ExecuteResult.CreateSuccessExecuteResult();
                    }
                    else
                    {
                        r = ExecuteResult.CreateFailExecuteResult(RemoteStrings.NotConnection);
                    }
                }
            }
            else
            {
                TaskExecutor te = new TaskExecutor();
                r = te.Execute(device, e.ExecuteArgs.ExecuteName, e.ExecuteArgs.KeyValues);
                log.Info("task execute : {0}, {1}", r.IsSuccess, r.FailMessage);
                if (r.IsSuccess)
                {
                    this.Add(e, te);
                }
            }
            e.Result = r;
        }
        #endregion //RemoteObject_Executeing

        /// <summary>
        /// 
        /// </summary>
        Hashtable _eeArgs_te_hash = new Hashtable();

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eeArgs"></param>
        /// <param name="te"></param>
        private void Add(ExecuteEventArgs eeArgs, TaskExecutor te)
        {
            te.Ended += new EventHandler(te_Ended);
            _eeArgs_te_hash.Add(eeArgs, te);
            
        }
        #endregion //Add

        #region Get
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private ExecuteEventArgs Get(TaskExecutor exe)
        {
            ExecuteEventArgs r = null;
            foreach (object obj in _eeArgs_te_hash.Keys)
            {
                TaskExecutor te = (TaskExecutor)_eeArgs_te_hash[obj];
                if (te == exe)
                {
                    r = (ExecuteEventArgs)obj;
                    break;
                }
            }
            return r;
        }
        #endregion //Get

        #region te_Ended
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void te_Ended(object sender, EventArgs e)
        {
            TaskExecutor exe = (TaskExecutor)sender;
            ExecuteEventArgs eeArgs = Get(exe);

            ResultArgs args = new ResultArgs();
            args.ExecuteArgs = eeArgs.ExecuteArgs;
            args.IsComplete= true;
            args.IsSuccess = exe.Task.LastParseResult.IsSuccess;
            args.Message = exe.Task.LastParseResult.ToString();
            if (args.IsSuccess)
            {
                args.KeyValues = exe.Task.LastParseResult.Results;
            }

            eeArgs.CallbackWrapper.Callback(args);
        }
        #endregion //te_Ended

    }
}
