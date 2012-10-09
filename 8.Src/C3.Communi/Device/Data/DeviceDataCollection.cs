using System;
using System.Threading;
using Xdgk.Common;
//using C3.Data;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler LastDataChanged;

        #region OnLastDataChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void OnLastDataChanged(object obj)
        {
            EventArgs e = (EventArgs)obj;
            this.OnLastDataChanged(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnLastDataChanged(EventArgs e)
        {
            if (this.LastDataChanged != null)
            {
                this.LastDataChanged(this, e);
            }
        }
        #endregion //OnLastDataChanged

        #region OnLastDataChangedCallback
        // <summary>
        /// 
        /// </summary>
        private SendOrPostCallback OnLastDataChangedCallback
        {
            get
            {
                if (_onLastDataChangedCallback == null)
                {
                    _onLastDataChangedCallback = new SendOrPostCallback(OnLastDataChanged);
                }
                return _onLastDataChangedCallback;
            }
        } private SendOrPostCallback _onLastDataChangedCallback;
        #endregion //OnLastDataChangedCallback

        #region Last
        /// <summary>
        /// 
        /// </summary>
        public IData Last
        {
            get
            {
                IData last = null;
                if (this.Datas.Count > 0)
                {
                    last = this.Datas[this.Datas.Count - 1];
                }
                return last;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Last");
                }
                this.Datas.Add(value);

                if (Soft.IsUseUISynchronizationContext)
                {
                    Soft.Post(this.OnLastDataChangedCallback, EventArgs.Empty);
                }
                else
                {
                    OnLastDataChanged(EventArgs.Empty);
                }
            }
        }
        #endregion //Last

        #region Datas
        /// <summary>
        /// 
        /// </summary>
        public DataCollection Datas
        {
            get
            {
                if (_datas == null)
                {
                    _datas = new DataCollection();
                }
                return _datas;
            }
            set
            {
                _datas = value;
            }
        } private DataCollection _datas;
        #endregion //Datas


    }

}
