using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class Model
    {
        /// <summary>
        /// 
        /// </summary>
        public Type ControllerType
        {
            get { return _controllerType; }
            set { _controllerType = value; }
        } private Type _controllerType;

        /// <summary>
        /// 
        /// </summary>
        abstract public string Title
        {
            get;
        }

    }
}
