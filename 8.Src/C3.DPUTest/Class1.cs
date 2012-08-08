using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using System.Windows.Forms;

namespace C3.DPUTest
{
    public class GuidFactory
    {
        public static Guid Create(int n)
        {
            byte[] bs = BitConverter.GetBytes(n);
            byte[] bs16 = new byte[16];

            for (int i = bs.Length - 1; i >= 0; i--)
            {
                bs16[16 - bs.Length + i] = bs[i];
            }
            Guid id = new Guid(bs16);
            return id;
        }
    }

    public class TDpu : DPUBase
    {
        public DeviceType GetDeviceType()
        {
            DeviceType r = DeviceTypeManager.GetDeviceType("TDevice");
            if (r == null)
            {
                //r = new DeviceType();
                //r.Name = "TDevice";
                //r.Type = typeof(TDevice);
                DeviceType t= DeviceTypeManager.AddDeviceType("TDevice", null, typeof(TDevice));
                //DeviceTypeManager.Add(t);
                r = t;
            }
            return r;

        }
        public TDpu()
        {
            this.DeviceType = //typeof(TDevice);
                GetDeviceType();
            this.DeviceFactory = new TDeviceFactory(this);
            this.DevicePersister = new TDevicePersister();
            this.DeviceSourceProvider = new TDeviceSourceProvider();
            this.Processor = new Processor();
        }
    }

    public class TDevice : DeviceBase
    {
            System.Windows.Forms.Timer _t = new Timer();
        public TDevice() //: base ("noname",  DeviceTypeManager.GetDeviceType ("TDeviceTypeString") ,123)
        {
            //this.DeviceType = 
            _t.Interval = 1000;
            _t.Tick += new EventHandler(_t_Tick);
            _t.Start();
            //this.Parameters = 
            CreateDeviceParameterCollection();
        }

        private void CreateDeviceParameterCollection()
        {
            //ParameterCollection p = this.Parameters;//new ParameterCollection();
            //p.Add(i);

            Group g = new Group();
            g.Name = "Name";
            g.Text = "Text";

            IParameter i = new Parameter("name", "value", Unit.FindByName(Unit.Cm), 0, "descrip");
            i.ParameterUI = new StringParameterUI();
            g.Parameters.Add ( i );

            i = new Parameter("ADE", Xdgk.Common.ADEStatus.Add, -1);
            i.ParameterUI = new EnumParameterUI();
            g.Parameters.Add(i);

            g.Parameters.Sort();

            this.Groups.Add(g);
            /*
            i.Name = "ppp";
            i.Value = 123;
            i.Description = "deeded";
            i.OrderNumber = 1;
            i.Unit = Unit.FindByName(Unit.M3PerHour);
            i.ValueType = typeof(int);
            p.Add(i);

            i = new Parameter();
            i.Name = "ppp2";
            i.Value = 123.345;
            i.Description = "deeded";
            i.OrderNumber = 2;
            i.Unit = Unit.FindByName(Unit.M3PerHour);
            i.ValueType = typeof(float);
            p.Add(i);

            i = new Parameter();
            i.Name = "ppp3";
            i.Value = Xdgk.Common.ADEStatus.Add;
            i.Description = "deeded";
            i.OrderNumber = 3;
            i.Unit = Unit.FindByName(Unit.M3PerHour);
            i.ValueType = typeof(Xdgk.Common.ADEStatus);
            p.Add(i);

            i = new Parameter();
            i.Name = "ppp3";
            i.Value = Xdgk.Common.ADEStatus.Edit;
            i.Description = "deeded";
            i.OrderNumber = 3;
            i.Unit = Unit.FindByName(Unit.M3PerHour);
            i.ValueType = typeof(Xdgk.Common.ADEStatus);
            p.Add(i);

            i = new Parameter();
            i.Name = "ppp3";
            i.Value = Xdgk.Common.ADEStatus.Add;
            i.Description = "deeded";
            i.OrderNumber = 3;
            i.Unit = Unit.FindByName(Unit.Hour);
            i.ValueType = typeof(Xdgk.Common.ADEStatus);
            p.Add(i); 
            */
            

            //return p;
            
        }

        void _t_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Test");
            this.DeviceDataManager.Last = new C3.Communi.Test.TestDeviceData();

        }

        public override string ToString()
        {
            //this.LastData = new C3.Communi.Test.TestDeviceData();
            return base.ToString();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class TDeviceFactory : DeviceFactoryBaseXmlTask
    {
        public TDeviceFactory(IDPU dpu)
            : base(dpu, PathUtils.GetAssemblyDirectory ( typeof(TDeviceFactory).Assembly))
        {
        }

        static private int n = 0;

        public override IDevice OnCreate(IDeviceSource deviceSource)
        {
            TDevice d = new TDevice();
            d.DeviceType = this.Dpu.DeviceType;
            d.Address = 1;
            d.Name = "D" + n++;
            d.Guid = deviceSource.Guid;
            d.StationGuid = deviceSource.StationGuid;
            //d.Tasks = 
            d.Dpu = this.Dpu;
            d.DeviceDataManager.Last = new C3.Communi.Test.TestDeviceData();
            return d;
        }
    }

    public class TDeviceSource : DeviceSourceBase
    {
    }

    public class TDeviceSourceProvider : DeviceSourceProviderBase
    {

        public override IDeviceSource[] OnGetDeviceSources()
        {
            TDeviceSource s = new TDeviceSource();
            s.Address = 123;
            s.DevcieTypeName = "Tdevice";
            s.Guid = GuidFactory.Create(01);
            s.StationGuid = GuidFactory.Create(11);

            return new IDeviceSource[] { s };

        }
    }

    public class TDevicePersister : DevicePersisterBase
    {

        public override void OnAdd(IDevice device)
        {
            //throw new NotImplementedException();
        }

        public override void OnUpdate(IDevice device)
        {
            throw new NotImplementedException();
        }

        public override void OnDelete(IDevice device)
        {
            throw new NotImplementedException();
        }
    }

    public class Processor : TaskProcessorBase
    {

        public override void OnProcess(ITask task, IParseResult pr)
        {
            string s = string.Format("{0} - {1} - {2}",
                DateTime.Now , task.Opera.Name , pr.ToString ());
            Console.WriteLine(s);           
        }
    }


}
