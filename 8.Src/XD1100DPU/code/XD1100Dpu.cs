using System;
using C3.Communi;
using NLog;
using Xdgk.GR.Common;

namespace XD1100DPU
{
    public class XD1100Dpu : DPUBase
    {
        static private Logger _log = LogManager.GetCurrentClassLogger();

        public XD1100Dpu()
        {
            this.Name = "XD1100Dpu";
            this.DeviceFactory = new XD1100DeviceFactory (this);
            this.DevicePersister = new XD1100DevicePersister ();
            this.DeviceSourceProvider = new XD1100DeviceSourceProvider ();
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                    "XD1100Device",
                    typeof(XD1100Device));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new XD1100DeviceProcessor();

            string path = PathUtils.GetAssemblyDirectory(typeof(XD1100Device).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
            this.UIEntry = new UIEntry();

            // TODO: init outside temperature provider manager
            //
            SoftManager.GetSoft().HardwareCreated += new EventHandler(XD1100Dpu_HardwareCreated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void XD1100Dpu_HardwareCreated(object sender, EventArgs e)
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
