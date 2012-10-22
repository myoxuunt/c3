using C3.Communi;

namespace XGDPU
{
    public class XGDeviceFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public XGDeviceFactory(IDPU dpu)
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
            XGDeviceSource source = (XGDeviceSource)deviceSource;
            XGDevice d = new XGDevice();
            d.Address = source.Address;
            d.Name = source.DeviceName;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            d.Pickers = Dpu.OperaFactory.CreatePickers(this.Dpu.DeviceType.Type.Name);
            return d;
        }
    }
}