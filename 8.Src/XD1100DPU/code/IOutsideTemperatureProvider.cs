
using C3.Communi;

namespace XD1100DPU
{
    internal interface IOutsideTemperatureProvider
    {
        float GetStandardOutsideTemperature(IDevice device);
    }
}
