using System;
using C3.Communi;

namespace C3
{
    public class DeviceMode : Model
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public DeviceMode(IDevice device)
        {
            this._device = device;
            this.ControllerType = typeof(DeviceController);
        }

        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get { return _device; }
        } private IDevice _device;


        /// <summary>
        /// 
        /// </summary>
        public override string Title
        {
            get
            {
                //return "stationname(P)" + this.Device.Name + ":" + this.Device.Text;
                return string.Format("{0}:{1}", this.Device.Station.Name, this.Device.Dpu.DeviceType.Text);
            }
        }
    }

}
