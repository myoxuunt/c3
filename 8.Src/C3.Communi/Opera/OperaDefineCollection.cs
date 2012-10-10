
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class OperaDefineCollection : Xdgk.Common.Collection<OperaDefine>
    {
        // TODO: 2010-09-10
        //
        // cannot add same name devicedefine
        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="this1"></param>
        protected override void InsertItem(int index, OperaDefine item)
        {
            if (item == null)
                throw new ArgumentNullException("this1");

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
        private bool Exist(string deviceType)
        {
            foreach (OperaDefine item in this)
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
            foreach (OperaDefine dd in this)
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
        private OperaDefine FindDeviceDefine(string deviceType)
        {
            foreach (OperaDefine dd in this)
            {
                if (StringHelper.Equal(dd.DeviceType, deviceType))
                {
                    return dd;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operaName"></param>
        /// <returns></returns>
        public IOpera Create(string deviceType, string operaName)
        {
            OperaDefine dd = FindDeviceDefine(deviceType);
            if (dd == null)
            {
                string s = string.Format("not find device type '{0}' from opera define collection",deviceType );
                throw new InvalidOperationException(s);
            }

            IOpera opera = dd.CreateOpera(operaName);
            return opera;
        }
    }

}
