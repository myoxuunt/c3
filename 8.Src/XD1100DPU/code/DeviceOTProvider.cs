
using System;
using C3.Communi;

namespace XD1100DPU
{
    internal class DeviceOTProvider : IOutsideTemperatureProvider
    {

        public DeviceOTProvider (IOutside outside)
        {
            this.Outside = outside;
        }

        /// <summary>
        /// 
        /// </summary>
        public IOutside Outside
        {
            get { return _outside; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Outside");
                }
                _outside = value;
            }
        } private IOutside _outside;

        #region IOutsideTemperatureProvider ≥…‘±
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public float GetStandardOutsideTemperature(IDevice device)
        {
            return this.Outside.OutsideTemperature;
        }
        #endregion
    }
}
