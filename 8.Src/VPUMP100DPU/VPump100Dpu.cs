using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using C3.Communi.SimpleDPU;
using VPump100Common;

namespace VPUMP100DPU
{

    internal class DBI : DBIBase
    {
        /// <summary>
        /// 
        /// </summary>
        static internal DBI Instance
        {
            get
            {
                if (_instance == null)
                {

                    SourceConfig sc = SourceConfigManager.SourceConfigs.Find("Connection");
                    if (sc != null)
                    {
                        _instance = new DBI(sc.Value);
                    }
                    else
                    {
                        throw new ConfigException("connection");
                    }
                }
                return _instance;
            }
        } static private DBI _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        private DBI(string s)
            : base(s)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="d"></param>
        public void InsertVPump100Data(int deviceID, VPump100Data data)
        {
            string s = " insert into tblGateData(deviceid, DT, instantFlux, efficiency, TotalAmount, RemainAmount, pumpStatus, forceStatus, vibrateStatus, powerStatus) " +
                       " values(@deviceID, @dt, @instantFlux, @efficiency, @totalAmount, @remainAmount, @pumpStatus, @forceStatus, @vibrateStatus, @powerStatus)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("Dt", data.DT);
            list.Add("InstantFlux", data.InstantFlux);
            list.Add("Efficiency", data.Efficiency);
            list.Add("TotalAmount", data.TotalAmount);
            list.Add("RemainAmount", data.RemainAmount);
            list.Add("pumpStatus", data.PumpStatus);
            list.Add("forceStatus", data.ForceStartStatus);
            list.Add("vibrateStatus", data.VibrateStatus);
            list.Add("powerStatus", data.PowerStatus);

            ExecuteScalar(s, list);
        }

        public DateTime GetVPumpLastDateTime(int deviceID)
        {
            string s = "select Max(DT) from tblPumpData where DeviceID = @deviceID";
            ListDictionary list = new ListDictionary();
            list.Add("deviceID", deviceID);

            object obj = ExecuteScalar(s, list);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDateTime(obj);
            }
            else
            {
                return DateTime.Now.Date;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VPump100Dpu : DPUBase
    {
        public VPump100Dpu()
        {
            this.Name = "VPump100Dpu";
            this.DeviceFactory = new VPump100Factory(this);
            this.DevicePersister = new VPump100Persister(DBI.Instance);
            this.DeviceSourceProvider = //new VPump100SourceProvider();
                new SimpleDeviceSourceProvider(DBI.Instance, typeof(VPump100));
            this.DeviceType = DeviceTypeManager.AddDeviceType(
                "VPump100",
                typeof(VPump100));
            this.DeviceUI = new DeviceUI(this);
            this.Processor = new VPump100Processor();

            string path = PathUtils.GetAssemblyDirectory(typeof(VPump100).Assembly);
            this.TaskFactory = new XmlTaskFactory(this, path);
            this.OperaFactory = new XmlOperaFactory(path);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class VPump100Processor : TaskProcessorBase
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
                        string name = Convert.ToString(pr.Results["name"]);
                        byte status = Convert.ToByte(pr.Results["status"]);
                        byte recordCount = Convert.ToByte(pr.Results["count"]);
                        byte[] recordsBytes = (byte[])pr.Results["datas"];

                        Debug.Assert(recordCount <= 5);

                        if (status == 0)
                        {
                            VPump100Data[] datas = ProcessRecord(recordsBytes, recordCount);
                            foreach (VPump100Data d in datas)
                            {
                                task.Device.DeviceDataManager.Last = d;

                                int id = GuidHelper.ConvertToInt32(task.Device.Guid);
                                DBI.Instance.InsertVPump100Data(id, d);
                            }
                        }
                        else
                        {

                        }
                    }
                    else if (StringHelper.Equal(pr.Name, "noNameOrDatas"))
                    {
                        byte status = Convert.ToByte(pr.Results["status"]);
                        ResponseStatusEnum repStatus = (ResponseStatusEnum)status;
                        string text = EnumTextAttribute.GetEnumTextAttributeValue(repStatus);
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
        private VPump100Data[] ProcessRecord(byte[] recordsBytes, byte recordCount)
        {
            List<VPump100Data > list = new List<VPump100Data> ();
            for (int i = 0; i < recordCount; i++)
            {
                VPump100Data data = VPump100Data.ToVPump100Data(
                    recordsBytes, 
                    i * VPump100Data.BytesCountOfVPumpData);
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
    public class VPump100Persister : SimpleDevicePersister
    {
        public VPump100Persister(DBIBase dbi)
            : base(dbi)
        {
        }
    }

    public class VPump100Factory : DeviceFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dpu"></param>
        public VPump100Factory(IDPU dpu)
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
            VPump100 d = new VPump100();
            //d.DeviceSource = deviceSource;
            //SetDeviceProperties(d, deviceSource);
            base.SetDeviceProperties(d, deviceSource);
            return d;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DeviceKind("PumpDevice")]
    internal class VPump100 : DeviceBase
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
                return DBI.Instance.GetVPumpLastDateTime(deviceID);
            }
            else
            {
                string msg = "not find lazy name: " + name;
                throw new InvalidOperationException(msg);
            }
        }
    }
}
