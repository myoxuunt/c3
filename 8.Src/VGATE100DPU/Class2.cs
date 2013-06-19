using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using C3.Communi.SimpleDPU;

namespace VGATE100DPU
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
        public void InsertVGate100Data(int deviceID, VGate100Data data)
        {
            string s = " insert into tblGateData(deviceid, DT, BeforeWL, BehindWL, Height, instantFlux, TotalAmount, RemainAmount) " +
                       " values(@deviceID, @dt, @beforeWL, @behindWL, @height, @instantFlux, @totalAmount, @remainAmount)";

            ListDictionary list = new ListDictionary();
            list.Add("DeviceID", deviceID);
            list.Add("Dt", data.DT);
            list.Add("BeforeWL", data.BeforeWL);
            list.Add("BehindWL", data.BehindWL);
            list.Add("Height", data.Height);
            list.Add("InstantFlux", data.InstantFlux);
            list.Add("TotalAmount", data.TotalAmount);
            list.Add("RemainAmount", data.RemainAmount);

            ExecuteScalar(s, list);
        }

        public DateTime GetVGateLastDateTime(int deviceID)
        {
            string s = "select Max(DT) from tblGateData where DeviceID = @deviceID";
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
        /// <param name="pr"></param>
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
                    else if (StringHelper.Equal(pr.Name, "noNameOrDatas"))
                    {
                        byte status = Convert.ToByte(pr.Results["status"]);
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
