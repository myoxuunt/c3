using System;
using System.Threading;
using Xdgk.Common;

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
        public IDeviceData Last
        {
            get
            {
                IDeviceData last = null;
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
        public DeviceDataCollection Datas
        {
            get
            {
                if (_datas == null)
                {
                    _datas = new DeviceDataCollection();
                }
                return _datas;
            }
            set
            {
                _datas = value;
            }
        } private DeviceDataCollection _datas;
        #endregion //Datas

        #region DeviceDataCollection
        /// <summary>
        /// 
        /// </summary>
        public class DeviceDataCollection : Xdgk.Common.Collection<IDeviceData>
        {
            static private readonly int DEFAULT_CAPABILITY = 1000;
            static private readonly int MIN_CAPABILITY = 10;

            #region Add
            /// <summary>
            /// 
            /// </summary>
            /// <param name="d"></param>
            internal new void Add(IDeviceData deviceData)
            {
                base.Add(deviceData);
            }
            #endregion //Add

            #region Insert
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <param name="item"></param>
            internal new void Insert(int index, IDeviceData item)
            {
                base.Insert(index, item);
            }
            #endregion //Insert

            #region Capability
            /// <summary>
            /// 
            /// </summary>
            public int Capability
            {
                get { return _capability; }
                set
                {
                    if (value < MIN_CAPABILITY)
                    {
                        value = MIN_CAPABILITY;
                    }
                    _capability = value;
                }
            } private int _capability = DEFAULT_CAPABILITY;
            #endregion //Capability

            #region InsertItem
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <param name="item"></param>
            protected override void InsertItem(int index, IDeviceData item)
            {
                base.InsertItem(index, item);
                if (this.Count > this.Capability)
                {
                    // TODO:
                    //
                    this.RemoveAt(0);
                }
            }
            #endregion //InsertItem
        }
        #endregion //DeviceDataCollection

    }

}
