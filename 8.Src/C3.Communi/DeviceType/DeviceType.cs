using System;
using Xdgk.Common;

namespace C3.Communi
{


    /// <summary>
    /// 
    /// </summary>
    public class DeviceType : TypeBase 
    {
        #region DeviceType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        internal DeviceType(string name, Type type)
            : this(name, null, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal DeviceType(string name, string text, Type type)
            : this(name, text, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal DeviceType(string name, string text, string description, Type type)
            : base(name, text, description, type)
        {
        }
        #endregion //DeviceType


        #region Kind
        /// <summary>
        /// 
        /// </summary>
        private DeviceKind Kind
        {
            get
            {
                //if (_kind == null)
                //{
                //    _kind = new DeviceKind();
                //}
                return _kind;
            }
            set
            {
                _kind = value;
            }
        } private DeviceKind _kind;
        #endregion //Kind

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDevice Create(IDPU dpu)
        {
            if (dpu == null)
            {
                throw new ArgumentNullException("dpu");
            }
            IDevice d = (IDevice)Activator.CreateInstance(this.Type);
            d.Dpu = dpu;
            d.DeviceType = this;

            return d;
        }
    }
}
