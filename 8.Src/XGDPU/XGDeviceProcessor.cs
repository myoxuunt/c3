
using System;
using System.Data;
using System.Collections.Generic;
using C3.Communi;
using Xdgk.Common;


namespace XGDPU
{
    internal class XGDeviceProcessor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="bs"></param>
        /// <returns></returns>
        //public override IUploadParseResult OnProcessUpload(IDevice device, byte[] bs)
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            XGDevice xg = (XGDevice)device;
            DateTime dt = (DateTime)pr.Results["DT"];
            string cardSN = pr.Results["cardSN"].ToString();

            //xg.DeviceDataManager.Last = new XGData(dt, cardSN);
            SaveToDBAndUpdateLast(xg, dt, cardSN);

            IOpera op = device.Dpu.OperaFactory.Create(device.GetType().Name,
                XGOperaNames.RemoveUpload);

            TimeSpan timeout = TimeSpan.FromMilliseconds(device.Station.CommuniPortConfig.TimeoutMilliSecond);
            Task task = new Task(device, op, Strategy.CreateImmediateStrategy(), timeout);
            device.TaskManager.Tasks.Enqueue(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                XGDevice xgdevice = (XGDevice)task.Device;

                switch (opera)
                {
                    case XGOperaNames.ReadCount:
                        ProcessXGReadRecordCountResult(xgdevice, pr);
                        break;

                    case XGOperaNames.ReadRecord:
                        ProcessXGReadRecordResult(xgdevice, pr);
                        break;

                    case XGOperaNames.RecordUpload:
                        ProcessXGUploadRecordResult(xgdevice, pr);
                        break;

                    case XGOperaNames.ClearRecord:
                        ProcessXGClearRecordResult(pr);
                        break;

                    case XGOperaNames.RemoveUpload:
                        ProcessXGRemoveUploadResult(pr);
                        break;

                    case XGOperaNames.ReadXGDate:
                        ProcessXGReadDateResult(xgdevice, pr);
                        break;

                    case XGOperaNames.ReadXGTime:
                        ProcessXGReadTimeResult(xgdevice, pr);
                        break;

                    case XGOperaNames.WriteXGDate:
                    case XGOperaNames.WriteXGTime:
                        // do nothing
                        //
                        break;

                    default:
                        {
                            string errmsg = string.Format("{0} {1}",
                                    xgdevice.DeviceType.Text,
                                    opera);
                            throw new NotSupportedException(errmsg);
                        }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgdevice"></param>
        /// <param name="pr"></param>
        private void ProcessXGReadTimeResult(XGDevice xgdevice, IParseResult pr)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgdevice"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGReadDateResult(XGDevice xgdevice, IParseResult pr)
        {
            DateTime dt = (DateTime)pr.Results["DT"];
            string cardSn = pr.Results["cardsn"].ToString();

            dt = SaveToDBAndUpdateLast(xgdevice, dt, cardSn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgdevice"></param>
        /// <param name="dt"></param>
        /// <param name="cardSn"></param>
        /// <returns></returns>
        private static DateTime SaveToDBAndUpdateLast(XGDevice xgdevice, DateTime dt, string cardSn)
        {
            XGData data = new XGData(dt, cardSn);
            xgdevice.DeviceDataManager.Last = data;
            DBI.Instance.InsertXGData(xgdevice, data);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseResult"></param>
        private void ProcessXGRemoveUploadResult(IParseResult pr)
        {
            // do nothing
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseResult"></param>
        private void ProcessXGClearRecordResult(IParseResult pr)
        {
            // do nothing
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parseResult"></param>
        private void ProcessXGUploadRecordResult(XGDevice xgdevice, IParseResult pr)
        {
            ProcessXGDeviceRecordHelp(xgdevice, pr, false);

            IOperaFactory operaFactory = xgdevice.Dpu.OperaFactory;
            IOpera op = operaFactory.Create(xgdevice.GetType().Name, XGOperaNames.RemoveUpload);
            //ITaskFactory taskFactory = xgdevice.Dpu.TaskFactory;

            TimeSpan tsTimeout = TimeSpan.FromMilliseconds(xgdevice.Station.CommuniPortConfig.TimeoutMilliSecond);
            Task task = new Task(xgdevice, op, new ImmediateStrategy(), tsTimeout);

            xgdevice.TaskManager.Tasks.Enqueue(task);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGReadRecordResult(XGDevice xgdevice, IParseResult pr)
        {
            ProcessXGDeviceRecordHelp(xgdevice, pr, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xgdevice"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGDeviceRecordHelp(XGDevice xgdevice, IParseResult parseResult, bool isFD)
        {
            // 1. uploadrecord
            // 2. readrecord 
            //    2.1 return record               
            //    2.2 return record count if record idx out of range
            /*
               if (parseResult.IsSuccess)
               {
               if (StringHelper.Equal(parseResult.ReceivePartName, "recordcount"))
               {
               string errmsg = string.Format("record index '{0}' out of range",
               parseResult.NameObjects.GetObject("recordcount"));
            //CZGRApp.Default.MainForm.UpdateXGDeviceState(xgdevice, errmsg);   
            log.Warning(errmsg);

            }
            else
            {
            // upload record or readrecord
            //
            DBXGDevice dbxgdevice = xgdevice.DBXGDevice;
            DateTime dt = Convert.ToDateTime(parseResult.NameObjects.GetObject("DT"));
            string cardsn = parseResult.NameObjects.GetObject("cardsn").ToString();
            DBXGData dbxgdata = new DBXGData(dbxgdevice, DBCard.FindCard(cardsn), dt);
            dbxgdata.Create();

            // update xgdatalistview
            //
            CZGRApp.Default.MainForm.UpdateXGData(xgdevice, parseResult, dbxgdata, isFD);
            }
            }
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="parseResult"></param>
        private void ProcessXGReadRecordCountResult(XGDevice xgdevice, IParseResult pr)
        {
            IOperaFactory operaFactory = xgdevice.Dpu.OperaFactory;
            TimeSpan timeout = TimeSpan.FromMilliseconds(xgdevice.Station.CommuniPortConfig.TimeoutMilliSecond);
            int count = Convert.ToInt32(pr.Results["recordcount"]);
            if (count > 0)
            {
                for (int i = 1; i < count + 1; i++)
                {
                    Opera op = (Opera)operaFactory.Create(xgdevice.GetType().Name, XGOperaNames.ReadRecord);
                    op.SendPart["recordidx"] = i;

                    Task task = new Task(xgdevice, op, new ImmediateStrategy(), timeout);
                    xgdevice.TaskManager.Tasks.Enqueue(task);
                }

                Opera clearOP = (Opera)operaFactory.Create(xgdevice.GetType().Name, XGOperaNames.ClearRecord);

                Task clearTask = new Task(xgdevice, clearOP, new ImmediateStrategy(), timeout);
                xgdevice.TaskManager.Tasks.Enqueue(clearTask);

            }
        }
    }

}
