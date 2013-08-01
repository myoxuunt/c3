
using C3.Communi;
using Xdgk.GR.Common;

namespace LYR001DPU
{
    public class LYR001DeviceFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public LYR001DeviceFactory(IDPU dpu)
            : base(dpu)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceSource"></param>
        /// <returns></returns>
        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            LYR001DeviceSource source = (LYR001DeviceSource)deviceSource;
            LYR001Device d = new LYR001Device();
            d.Address = source.Address;
            d.Name = source.DeviceName;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            d.HtmMode = HeatTransferMode.Parse(source.HtmModeValue);
            d.Pickers = Dpu.OperaFactory.CreatePickers(this.Dpu.DeviceType.Type.Name);

            //d.DeviceDataManager.Last = new LYR001Data();
            return d;
        }
    }

}
