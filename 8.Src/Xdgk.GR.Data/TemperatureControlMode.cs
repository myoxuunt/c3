
using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;


namespace Xdgk.GR.Data
{
    public class TemperatureControlMode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        //public TemperatureControlMode(string name, int value)
        public TemperatureControlMode(string name, TemperatureControlModeEnum mode)
        {
            this.Name = name;
            //this.Value = value;
            this._mode = mode;
        }

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

        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get { return (int)this.Mode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public TemperatureControlModeEnum Mode
        {
            get { return this._mode; }
        } private TemperatureControlModeEnum _mode;


    }

}
