using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class HardwareFactory
    {

        #region ISPUs
        /// <summary>
        /// 
        /// </summary>
        public SPUCollection SPUs
        {
            get
            {
                if (_ispus == null)
                {
                    _ispus = CreateIspus();
                }
                return _ispus;
            }
            set { _ispus = value; }
        } private SPUCollection _ispus;
        #endregion //ISPUs

        #region CreateIspus
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SPUCollection CreateIspus()
        {
            SPUCollection spus = new SPUCollection();
            object[] objects = this.SPUAssemblyInfos.CreateInstanceWithInterface(typeof(ISPU));
            foreach (object obj in objects)
            {
                spus.Add((ISPU)obj);
            }
            return spus;
        }
        #endregion //CreateIspus

        #region SPUAssemblyInfos
        private AssemblyInfoCollection SPUAssemblyInfos
        {
            get
            {
                if (_spuAssemblyInfos == null)
                {
                    string path = PathUtils.SPUConfigFileName;
                    _spuAssemblyInfos = AssemblyInfoFactory.CreateFromXml(path);
                }
                return _spuAssemblyInfos;
            }
        } private AssemblyInfoCollection _spuAssemblyInfos;
        #endregion //SPUAssemblyInfos

        #region DPUAssemblyInfos
        private AssemblyInfoCollection DPUAssemblyInfos
        {
            get
            {
                if (_dpuAssemblyInfos == null)
                {
                    string path = PathUtils.DPUConfigFileName;
                    _dpuAssemblyInfos = AssemblyInfoFactory.CreateFromXml(path);
                }
                return _dpuAssemblyInfos;
            }
        } private AssemblyInfoCollection _dpuAssemblyInfos;
        #endregion //DPUAssemblyInfos

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
                    _dPUs = CreateDpus();
                }
                return _dPUs;
            }
            set
            {
                _dPUs = value;
            }
        }private DPUCollection _dPUs;
        #endregion //DPUs

        #region CreateDpus
        private DPUCollection CreateDpus()
        {
            DPUCollection dpus = new DPUCollection();
            object[] objects = DPUAssemblyInfos.CreateInstanceWithInterface(typeof(IDPU));
            foreach (object obj in objects)
            {
                dpus.Add((IDPU)obj);
            }
            return dpus;
        }
        #endregion //CreateDpus

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

        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Hardware Create()
        {
            VerifySPUs();
            VerifyDPUs();

            Hardware hd = new Hardware();
            CreateStations(hd);
            CreateDevices(hd);
            return hd;
        }
        #endregion //Create

        #region CreateDevices
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hd"></param>
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
                    device.Station = station;
                }
            }
        }
        #endregion //CreateDevices

        #region CreateStations
        private void CreateStations(Hardware hd)
        {
            // 
            foreach (ISPU spu in SPUs)
            {
                IStationSourceProvider sourceProvider = spu.StationSourceProvider;
                if (sourceProvider == null)
                {
                }

                sourceProvider.SourceConfigs = this.SourceConfigs;
                IStationSource[] stationSources = sourceProvider.GetStationSources();

                foreach (IStationSource stationSource in stationSources)
                {
                    IStationFactory factory = spu.StationFactory;
                    IStation station = factory.Create(stationSource);
                    station.StationSource = stationSource;
                    hd.Stations.Add(station);
                }
            }
        }
        #endregion //CreateStations

        #region VerifyDPUs
        /// <summary>
        /// 
        /// </summary>
        private void VerifyDPUs()
        {
            foreach (IDPU dpu in this.DPUs)
            {
                VerifyDPU(dpu);
            }
        }
        #endregion //VerifyDPUs

        #region VerifyDPU
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        private void VerifyDPU(IDPU dpu)
        {
            if (dpu == null)
            {
                throw new ArgumentNullException("dpu");
            }

            VerifyNotNull (dpu.DeviceFactory ,"dpu.DeviceFactory");
            VerifyNotNull(dpu.DevicePersister, "dpu.DevicePersister");
            VerifyNotNull(dpu.DeviceSourceProvider, "dpu.DeviceSourceProvider");
            VerifyNotNull(dpu.DeviceType, "dpu.DeviceType");
            VerifyNotNull(dpu.Processor, "dpu.Processor");
        }
        #endregion //VerifyDPU

        #region VerifySPUs
        /// <summary>
        /// 
        /// </summary>
        private void VerifySPUs()
        {
            foreach (ISPU spu in this.SPUs)
            {
                VerifySPU(spu);
            }
        }
        #endregion //VerifySPUs

        #region VerifySPU
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spu"></param>
        private void VerifySPU(ISPU spu)
        {
            if (spu == null)
            {
                throw new ArgumentNullException("spu");
            }

            VerifyNotNull(spu.StationFactory, "spu.StationFactory");
            VerifyNotNull(spu.StationPersister, "spu.StationPersister");
            VerifyNotNull(spu.StationSourceProvider, "spu.StationSourceProvider");
            VerifyNotNull(spu.StationType, "spu.StationType");
        }
        #endregion //VerifySPU

        #region VerifyNotNull
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objName"></param>
        private void VerifyNotNull(object obj, string objName)
        {
            if (obj == null)
            {
                throw new InvalidOperationException(
                    string.Format("'{0}' is null", objName)
                    );
            }
        }
        #endregion //VerifyNotNull

    }
}
