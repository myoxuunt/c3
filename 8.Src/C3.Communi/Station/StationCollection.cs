
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class StationCollection : Xdgk.Common.Collection<IStation>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="this1"></param>
        protected override void InsertItem(int index, IStation item)
        {
            if (this.CheckExist(item.Name))
            {
                throw new ArgumentException("Exist Station " + item.Name);
            }
            base.InsertItem(index, item);
            item.Stations = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IStation Find(Guid guid)
        {
            IStation r = null;
            foreach (IStation station in this)
            {
                IStationSource stationSource = station.StationSource;
                if (stationSource.Guid == guid)
                {
                    r = station;
                    break;
                }
            }

            return r;
        }

        #region CheckExist
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public bool CheckExist(string stationName)
        {
            return ExistName(stationName, null);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public bool ExistName(string name, IStation ignore)
        {
            name = name.Trim();
            foreach (IStation st in this)
            {
                if (string.Compare(st.Name, name, true) == 0 &&
                        st != ignore)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        internal IDevice FindDevice(int deviceID)
        {
            IDevice r = null;
            foreach (IStation st in this)
            {
                r = st.Devices.Find(deviceID);
                if (r != null)
                {
                    break;
                }
            }
            return r;
        }
    }

}
