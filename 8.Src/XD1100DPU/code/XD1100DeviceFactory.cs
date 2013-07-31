
using C3.Communi;
using Xdgk.GR.Common;

namespace XD1100DPU
{
    public class XD1100DeviceFactory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public XD1100DeviceFactory(IDPU dpu)
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
            XD1100DeviceSource source = (XD1100DeviceSource)deviceSource;
            XD1100Device d = new XD1100Device();
            d.Address = source.Address;
            d.Name = source.DeviceName;
            d.DeviceSource = source;
            d.DeviceType = this.Dpu.DeviceType;
            d.Dpu = this.Dpu;
            d.Guid = source.Guid;
            d.StationGuid = source.StationGuid;
            d.HtmMode = HeatTransferMode.Parse(source.HtmModeValue);
            d.Pickers = Dpu.OperaFactory.CreatePickers(this.Dpu.DeviceType.Type.Name);

            //d.DeviceDataManager.Last = new XD1100Data();
            return d;
        }
    }

}
