using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceTypeManager
    {
        #region DeviceTypeManager
        /// <summary>
        /// 
        /// </summary>
        private DeviceTypeManager()
        {

        }
        #endregion //DeviceTypeManager

        #region DeviceTypes
        /// <summary>
        /// 
        /// </summary>
        static public DeviceTypeCollection DeviceTypes
        {
            get
            {
                if (_deviceTypes == null)
                {
                    _deviceTypes = new DeviceTypeCollection();
                }
                return _deviceTypes;
            }
        } static private DeviceTypeCollection _deviceTypes;
        #endregion //DeviceTypes

        #region GetDeviceType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        static public DeviceType GetDeviceType(string typeName)
        {
            DeviceType r = null;
            foreach (DeviceType item in DeviceTypes)
            {
                if (StringHelper.Equal(item.Name, typeName))
                {
                    r = item;
                    break;
                }
            }

            if (r == null)
            {
                r = new DeviceType(typeName);
                DeviceTypes.Add(r);
            }
            return r;
        }
        #endregion //GetDeviceType
    }

}
