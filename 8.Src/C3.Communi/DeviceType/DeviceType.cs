using System;

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
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal DeviceType(string text, Type type)
            : base(text, type)
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
