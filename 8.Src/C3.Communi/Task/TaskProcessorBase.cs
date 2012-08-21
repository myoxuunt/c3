
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class TaskProcessorBase : ITaskProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public void Process(ITask task, IParseResult pr)
        {
            OnProcess(task, pr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        abstract public void OnProcess(ITask task, IParseResult pr);

        //#region ProcessUpload
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="device"></param>
        ///// <param name="bs"></param>
        ///// <returns></returns>
        //public IUploadParseResult ProcessUpload(IDevice device, byte[] bs)
        //{
        //    return OnProcessUpload(device, bs);
        //}
        //#endregion //ProcessUpload

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="device"></param>
        ///// <param name="bs"></param>
        //abstract public  IUploadParseResult OnProcessUpload(IDevice device, byte[] bs);

        #region ITaskProcessor ≥…‘±


        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="pr"></param>
        public void ProcessUpload(IDevice device, IParseResult pr)
        {
            OnProcessUpload(device, pr);
        }

        abstract public void OnProcessUpload(IDevice device, IParseResult pr);

        #endregion
    }

}
