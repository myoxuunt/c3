using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace Xdgk.GR.Data
{
    public class XD100TemperatureControlMode
    {
        #region XD100TemperatureControlMode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public XD100TemperatureControlMode(string name, XD100TemperatureControlModeEnum mode)
        {
            this.Name = name;
            //this.Value = value;
            this._mode = mode;
        }
        #endregion //XD100TemperatureControlMode

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } private string _name;
        #endregion //Name

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get { return (int)this.Mode; }
        }
        #endregion //Value

        #region Mode
        /// <summary>
        /// 
        /// </summary>
        public XD100TemperatureControlModeEnum Mode
        {
            get { return this._mode; }
        } private XD100TemperatureControlModeEnum _mode;
        #endregion //Mode

    }

}
