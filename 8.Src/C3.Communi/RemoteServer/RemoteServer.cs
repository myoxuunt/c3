using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using NLog;
using Xdgk.Common;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RemoteObject_Executeing(object sender, ExecuteEventArgs e)
        {
            //string s = string.Format("{0}: {1}, ", "stationName",e.Parameter.StationName);
            //s+= string.Format("{0}: {1}, ", "address",e.Parameter.DeviceAddress );
            //s+= string.Format("{0}: {1}, ", "executename",e.Parameter.ExecuteName );
            //s+= string.Format("{0}: {1}, ", "count",e.Parameter.HashTable.Count );
            //log.Info("Executing");

            //ResultArgs args = new ResultArgs();
            //args.IsComplete = true;
            //args.IsSuccess = true;
            //args.Message = "message";
            ////e.Result 

            //e.CallbackWrapper.Callback(args);
            ////log.Info(s);
            //Result r = new Result();
            //r.ResultEnum = ResultEnum.Fail;
            //r.FailMessage = "fial message.";
            //e.Result = r;

            ExecuteResult r = null;
            int id = e.ExecuteArgs.DeviceID;
            IDevice device = SoftManager.GetSoft().Hardware.FindDevice(id);

            if (StringHelper.Equal(e.ExecuteArgs.ExecuteName, DefineExecuteNames.IsReady))
            {
                if (device == null)
                {
                    string s = string.Format("not find device by id '{0}'", id);
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
                        r = ExecuteResult.CreateFailExecuteResult("not connected");
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

        /// <summary>
        /// 
        /// </summary>
        Hashtable _eeArgs_te_hash = new Hashtable();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void te_Ended(object sender, EventArgs e)
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

    }

    public class LocalController : _1100ControllerInterface 
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ResultEvent;

        private ExecuteArgs _executeArgs;
        public ExecuteResult Doit(ExecuteArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            this._executeArgs = args;
            int deviceID = args.DeviceID;
            IDevice device = SoftManager.GetSoft().Hardware.FindDevice(deviceID);

            ExecuteResult r = null;
            if (StringHelper.Equal(args.ExecuteName, DefineExecuteNames.IsReady))
            {
                if (device == null)
                {
                    r = ExecuteResult.CreateFailExecuteResult("not find");
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
                        r = ExecuteResult.CreateFailExecuteResult("not connected");
                    }
                }
            }
            else
            {
                Debug.Assert(device != null);
                TaskExecutor te = new TaskExecutor();
                te.Ended += new EventHandler(te_Ended);
                r = te.Execute(device, args.ExecuteName, args.KeyValues);
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void te_Ended(object sender, EventArgs e)
        {

            TaskExecutor te = (TaskExecutor)sender;
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.ExecuteArgs = this._executeArgs;
            resultArgs.IsComplete = true;
            resultArgs.IsSuccess = te.Task.LastParseResult.IsSuccess;
            resultArgs.Message = te.Task.LastParseResult.ToString();
            resultArgs.KeyValues = te.Task.LastParseResult.Results;

            this._resultArgs = resultArgs;

            if (this.ResultEvent != null)
            {
                ResultEvent(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ResultArgs ResultArgs
        {
            get { return _resultArgs; }
        } private ResultArgs _resultArgs;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // nothing
            //
        }

    }
}
