
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceDefineCollection : Xdgk.Common.Collection<DeviceDefine>
    {
        // TODO: 2010-09-10
        //
        // cannot add same name devicedefine
        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, DeviceDefine item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (Exist(item.DeviceType))
            {
                string s = string.Format("DeivceType '{0}' exist", item.DeviceType );
                ConfigException ex = new ConfigException(s);
                throw ex;
            }
            base.InsertItem(index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public bool Exist(string deviceType)
        {
            foreach (DeviceDefine item in this)
            {
                if (StringHelper.Equal(item.DeviceType, deviceType))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public string GetDeviceText(string deviceType)
        {
            foreach (DeviceDefine dd in this)
            {
                if (StringHelper.Equal(dd.DeviceType, deviceType, false, true))
                {
                    return dd.Text;
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public DeviceDefine FindDeviceDefine(string deviceType)
        {
            foreach (DeviceDefine dd in this)
            {
                if (StringHelper.Equal(dd.DeviceType, deviceType))
                {
                    return dd;
                }
            }
            return null;
        }
    }

}
