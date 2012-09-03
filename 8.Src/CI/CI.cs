using System;
using System.Collections.Generic;
using System.Text;

namespace CI
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
        _controllerStatus ControllerStatus { get; set; }
        bool CanExecute();
        bool Execute(ParameterBag pb);
        event EventHandler Executing;
        event EventHandler Executed;
        //void ReadMode();
        //void WriteMode();
    }
}
