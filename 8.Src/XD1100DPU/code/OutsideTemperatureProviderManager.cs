
using C3.Communi;

namespace XD1100DPU
{
    internal class OutsideTemperatureProviderManager
    {
        private OutsideTemperatureProviderManager()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static public IOutsideTemperatureProvider Provider
        {
            get
            {
                if (_p == null)
                {
                    _p = new FixedOTProvider();
                }
                return _p; 
            }
            set
            {
                _p = value;
            }
        } static private IOutsideTemperatureProvider _p;

        #region IOutsideTemperatureProvider ≥…‘±
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        static public float GetStandardOutsideTemperature(IDevice device)
        {
            return Provider.GetStandardOutsideTemperature(device);
        }

        #endregion
    }

}
