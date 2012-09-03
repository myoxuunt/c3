using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ControllerIn
{

    public enum _controllerStatus
    {
        None,
        Doing,
    }
    
    public class ParameterBag
    {
        #region DeviceID
        /// <summary>
        /// 
        /// </summary>
        public int DeviceID
        {
            get
            {
                return _deviceID;
            }
            set
            {
                _deviceID = value;
            }
        } private int _deviceID;
        #endregion //DeviceID

        #region OperaName
        /// <summary>
        /// 
        /// </summary>
        public string OperaName
        {
            get
            {
                if (_operaName == null)
                {
                    _operaName = string.Empty;
                }
                return _operaName;
            }
            set
            {
                _operaName = value;
            }
        } private string _operaName;
        #endregion //OperaName

        //#region KeyValues
        ///// <summary>
        ///// 
        ///// </summary>
        //public KeyValueCollection KeyValues
        //{
        //    get
        //    {
        //        if (_keyValues == null)
        //        {
        //            _keyValues = new KeyValueCollection();
        //        }
        //        return _keyValues;
        //    }
        //    set
        //    {
        //        _keyValues = value;
        //    }
        //} private KeyValueCollection _keyValues;
        //#endregion //KeyValues
    }

    /// <summary>
    /// 
    /// </summary>
    public interface _1100ControllerInterface
    {
        String StationName { get; set; }
        int DeviceID { get; set; }
        string OperaName { get; set; }
        Hashtable Parameters { get; }

        void Doit();

        event EventHandler Notify;
        object NotifyObject { get; }
    }
}
