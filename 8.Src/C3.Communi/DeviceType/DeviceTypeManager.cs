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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public DeviceType AddDeviceType(string name, string text, Type type)
        {
            return new DeviceType(name, text, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        //static public void Add(DeviceType deviceType)
        //{
            //DeviceTypes.Add(deviceType);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        static public void Remove(DeviceType deviceType)
        {
            DeviceTypes.Remove(deviceType);
        }

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
                //if (StringHelper.Equal(item.Name, typeName))
                if (StringHelper.Equal(item.Type.Name , typeName))
                {
                    r = item;
                    break;
                }
            }
            //if (r == null)
            //{
            //    r = new DeviceType(typeName);
            //    DeviceTypes.Add(r);
            //}
            return r;
        }
        #endregion //GetDeviceType
    }

    public class StationTypeManager
    {
        #region 
        /// <summary>
        /// 
        /// </summary>
        private StationTypeManager()
        {

        }
        #endregion //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public  StationType AddStationType (string name, string text, Type type)
        {
            StationType t = new StationType(name, text, type);
            StationTypes.Add(t);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        //static public void Add(DeviceType deviceType)
        //{
            //DeviceTypes.Add(deviceType);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        static public void Remove(StationType stationType)
        {
            StationTypes.Remove(stationType);
        }

        #region DeviceTypes
        /// <summary>
        /// 
        /// </summary>
        static public StationTypeCollection StationTypes
        {
            get
            {
                if (_stationTypes == null)
                {
                    _stationTypes = new StationTypeCollection();
                }
                return _stationTypes;
            }
        } static private StationTypeCollection _stationTypes;
        #endregion //DeviceTypes

        #region GetDeviceType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        static public StationType GetStationType(string typeName)
        {
            StationType r = null;
            foreach (StationType item in StationTypes)
            {
                //if (StringHelper.Equal(item.Name, typeName))
                if (StringHelper.Equal(item.Type.Name, typeName))
                {
                    r = item;
                    break;
                }
            }

            //if (r == null)
            //{
            //    r = new DeviceType(typeName);
            //    DeviceTypes.Add(r);
            //}
            return r;
        }
        #endregion //
    }



}
