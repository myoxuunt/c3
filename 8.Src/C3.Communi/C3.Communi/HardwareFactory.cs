using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class Hardware
    {
        public StationCollection Stations
        {
            get
            {
                if (_stations == null)
                {
                    _stations = new StationCollection();
                }
                return _stations;
            }
            set 
            { 
                _stations = value;
            }
        } private StationCollection _stations;
    }

    public class HardwareFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public ISPUCollection ISPUs
        {
            get { return _ispus; }
            set { _ispus = value; }
        } private ISPUCollection _ispus;

        #region DPUs
        /// <summary>
        /// 
        /// </summary>
        public DPUCollection DPUs
        {
            get
            {
                if (_dPUs == null)
                {
                    _dPUs = new DPUCollection();
                }
                return _dPUs;
            }
            set
            {
                _dPUs = value;
            }
        } private DPUCollection _dPUs;
        #endregion //DPUs

        #region SourceConfigs
        /// <summary>
        /// 
        /// </summary>
        public SourceConfigCollection SourceConfigs
        {
            get
            {
                if (_sourceConfigs == null)
                {
                    _sourceConfigs = new SourceConfigCollection();
                }
                return _sourceConfigs;
            }
            set
            {
                _sourceConfigs = value;
            }
        } private SourceConfigCollection _sourceConfigs;
        #endregion //SourceConfigs

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Hardware Create()
        {
            Hardware hd = new Hardware();
            CreateStations(hd);
            CreateDevices(hd);
            return hd;
        }

        private void CreateDevices(Hardware hd)
        {
            // device
            //
            foreach (IDPU dpu in this.DPUs)
            {
                IDeviceSourceProvider deviceSourceProvider = dpu.DeviceSourceProvider;
                deviceSourceProvider.SourceConfigs = this.SourceConfigs;
                IDeviceSource[] deviceSources = deviceSourceProvider.GetDeviceSources();
                foreach (IDeviceSource deviceSource in deviceSources)
                {
                    IDeviceFactory factory = dpu.DeviceFactory;
                    IDevice device = factory.Create(deviceSource);
                    device.DeviceSource = deviceSource;

                    // TODO: find station by device
                    //
                    Guid stationGuid = deviceSource.StationGuid;
                    IStation station = hd.Stations.Find(stationGuid);
                    if (station == null)
                    {
                        throw new Exception(
                            string.Format("not find station by guid '{0}'", stationGuid)
                        );
                    }
                    //IStation st = null ;
                    //device.Station = st;
                    //st.Devices.Add(device);
                    station.Devices.Add(device);
                }
            }
        }

        private void CreateStations(Hardware hd)
        {
            // 
            foreach ( ISPU spu in ISPUs )
            {
                spu.StationSourceProvider.SourceConfigs = this.SourceConfigs;
                IStationSource[] stationSources = spu.StationSourceProvider.GetStationSources();

                foreach ( IStationSource stationSource in  stationSources )
                {
                    IStationFactory factory = spu.StationFactory;
                    IStation station = factory.Create(stationSource);
                    station.StationSource = stationSource;
                    hd.Stations.Add(station);
                }
            }
        }
    }
}
