
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceDataCollection : Xdgk.Common.Collection<IDeviceData>
    {
        static private readonly int DEFAULT_CAPABILITY = 1000;
        static private readonly int MIN_CAPABILITY = 10;

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
    }

}
