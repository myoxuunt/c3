using System;
using C3.Communi;
using NLog;
using Xdgk.GR.Common;

namespace LYR001DPU
{
    public class LYR001Dpu : DPUBase
    {
        static private Logger _log = LogManager.GetCurrentClassLogger();

        public LYR001Dpu()
        {
            this.Name = "LYR001Dpu";
            this.DeviceFactory = new LYR001DeviceFactory (this);
            this.DevicePersister = new LYR001DevicePersister ();
            this.DeviceSourceProvider = new LYR001DeviceSourceProvider ();
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                    "LYR001Device",
                    typeof(LYR001Device));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new LYR001DeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(LYR001Device).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
            this.UIEntry = new UIEntry();

            // TODO: init outside temperature provider manager
            //
            SoftManager.GetSoft().HardwareCreated += new EventHandler(LYR001Dpu_HardwareCreated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LYR001Dpu_HardwareCreated(object sender, EventArgs e)
        {
            int id = DBI.Instance.GetOutsideTemperatureProviderDevice();
            if (id > 0)
            {
                Soft soft = sender as Soft;
                IDevice d = soft.Hardware.FindDevice(id);
                if (d != null)
                {
                    DeviceOTProvider provider = new DeviceOTProvider((IOutside)d);
                    OutsideTemperatureProviderManager.Provider = provider;
                    _log.Info("deviceOT provider is '{0}->{1}'", d.Station.Name, d.GetType().Name);
                }
            }
        }
    }

}
