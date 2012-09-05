using System;
using System.Diagnostics;
using Xdgk.Common;

namespace C3.Communi
{
    public class LocalController : IExecuteController
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ResultEvent;

        /// <summary>
        /// 
        /// </summary>
        private ExecuteArgs _executeArgs;
        #endregion //

        #region Doit
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
                    string s = string.Format(RemoteStrings.NotFindDeviceByID, deviceID);
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
                Debug.Assert(device != null);
                TaskExecutor te = new TaskExecutor();
                te.Ended += new EventHandler(te_Ended);
                r = te.Execute(device, args.ExecuteName, args.KeyValues);
            }
            return r;
        }
        #endregion //Doit

        #region te_Ended
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void te_Ended(object sender, EventArgs e)
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
        #endregion //te_Ended

        #region ResultArgs
        /// <summary>
        /// 
        /// </summary>
        public ResultArgs ResultArgs
        {
            get { return _resultArgs; }
        } private ResultArgs _resultArgs;
        #endregion //ResultArgs

        #region Dispose
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // nothing
            //
        }
        #endregion //Dispose

    }

}
