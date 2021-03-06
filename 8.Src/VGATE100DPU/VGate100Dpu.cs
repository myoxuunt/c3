﻿using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using C3.Communi.SimpleDPU;
using VGate100Common;

namespace VGATE100DPU
{
    /// <summary>
    /// 
    /// </summary>
    public class VGate100Dpu : DPUBase
    {
        public VGate100Dpu()
        {
            this.Name = "VGate100Dpu";
            this.DeviceFactory = new VGate100Factory(this);
            this.DevicePersister = new VGate100Persister(DBI.Instance);
            this.DeviceSourceProvider = //new VGate100SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(VGate100));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "VGate100",
                typeof(VGate100));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new VGate100Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(VGate100).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class VGate100Processor : TaskProcessorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parseResult"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, "read"))
                {
                    if (StringHelper.Equal(pr.Name, string.Empty))
                    {
                        string stationName = Convert.ToString(pr.Results["name"]);
                        if (StringHelper.Equal(task.Device.Station.Name, stationName))
                        {
                            byte status = Convert.ToByte(pr.Results["status"]);
                            byte recordCount = Convert.ToByte(pr.Results["count"]);
                            byte[] recordsBytes = (byte[])pr.Results["datas"];

                            Debug.Assert(recordCount <= 5);

                            if (status == 0)
                            {
                                VGate100Data[] datas = ProcessRecord(recordsBytes, recordCount);
                                foreach (VGate100Data d in datas)
                                {
                                    task.Device.DeviceDataManager.Last = d;

                                    int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                                    DBI.Instance.InsertVGate100Data(id, d);
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            pr.Tag = string.Format("名称不匹配, 期望'{0}', 实际'{1}'",
                                task.Device.Station.Name, stationName);
                        }
                    }
                    else if (StringHelper.Equal(pr.Name, "noNameOrDatas"))
                    {
                        byte status = Convert.ToByte(pr.Results["status"]);
                        ResponseStatusEnum repStatus = (ResponseStatusEnum)status;
                        string text = EnumTextAttributeHelper.GetEnumTextAttributeValue(repStatus);
                        pr.Tag = text;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordsBytes"></param>
        /// <param name="recordCount"></param>
        private VGate100Data[] ProcessRecord(byte[] recordsBytes, byte recordCount)
        {
            List<VGate100Data > list = new List<VGate100Data> ();
            for (int i = 0; i < recordCount; i++)
            {
                VGate100Data data = VGate100Data.ToVGate100Data(
                    recordsBytes, 
                    i * VGate100Data.BytesCountOfVGateData);
                list.Add(data);
            }
            return list.ToArray();
        }

        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            // nothing
            //
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class VGate100Persister : SimpleDevicePersister
    {
        public VGate100Persister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    public class VGate100Factory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public VGate100Factory(IDPU dpu)
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
            VGate100 d = new VGate100();
            //d.DeviceSource = deviceSource;
            //SetDeviceProperties(d, deviceSource);
            base.SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DeviceKind("FluxDevice")]
    internal class VGate100 : DeviceBase
    {
        public override object GetLazyDataFieldValue(string name)
        {
            if (StringHelper.Equal(name, "name"))
            {
                return this.Station.Name;
            }
            else if (StringHelper.Equal(name, "dt"))
            {
                //return DateTime.Now;
                int deviceID = GuidHelper.ConvertToInt32(this.Guid);
                return DBI.Instance.GetVGateLastDateTime(deviceID);
            }
            else
            {
                string msg = "not find lazy name: " + name;
                throw new InvalidOperationException(msg);
            }
        }
    }
}
