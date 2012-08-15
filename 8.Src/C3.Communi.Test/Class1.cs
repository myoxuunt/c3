using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Xml.Serialization ;

namespace C3.Communi.Test
{
    [XmlInclude(typeof(NullCommuniPortConfig))]
    [XmlInclude(typeof(SerialCommuniPortConfig))]
    [XmlInclude(typeof(RemoteIPAddressConfig))]
    [XmlInclude(typeof(RemotePortConfig))]
    public class Wrap
    {
        public object val = "wrap object";
        public object val2 = NullCommuniPortConfig.Default;
        public object val3 = new SerialCommuniPortConfig();
        public object val4 = new RemoteIPAddressConfig();
        public object val5 = new RemotePortConfig();

    }

    [TestFixture ]
    public class CommuniPortConfigSerialTest
    {

        [Test]
        public void t2()
        {
            doit2(NullCommuniPortConfig.Default);
            doit2(new RemoteIPAddressConfig());
            doit2(new SerialCommuniPortConfig ());
            doit2(new RemotePortConfig());
            doit2(NullCommuniPortConfig.Default);

        }

        void doit2(ICommuniPortConfig v)
        {
            string s = CommuniPortConfigSerializer.Serialize(v);
            Console.WriteLine(s);
            ICommuniPortConfig v2 = CommuniPortConfigSerializer.Deserialize(s);
            Console.WriteLine(v2);
        }

        [Test]
        public void t()
        {
            //doit(NullCommuniPortConfig.Default);
            Console.WriteLine();
            //doit(new SerialCommuniPortConfig(
            //    new SerialPortSetting("c1", 9600, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.One)
            //    ));

            Console.WriteLine();
            doit(new Wrap());

            Console.WriteLine();
            //RemoteIPAddressConfig ipcfg = (RemoteIPAddressConfig) doit(
            //    new RemoteIPAddressConfig(System.Net.IPAddress.Parse("1.2.3.4")));
            //Assert.AreEqual(ipcfg.RemoteIPAddress, System.Net.IPAddress.Parse("1.2.3.4"));

            Console.WriteLine();
            //doit(new RemotePortConfig());

            Console.WriteLine();
            Console.WriteLine();
            
        }
            XmlSerializer er = new XmlSerializer(
                //value.GetType ()
                //, 
                typeof(Wrap)
                //new Type[] { 
                //    typeof(SerialCommuniPortConfig),
                //    typeof(NullCommuniPortConfig ),
                //    typeof(RemoteIPAddressConfig ),
                //    typeof(RemotePortConfig )
                );

        object doit(object value)
        {
            //Wrap  value = new Wrap();
            //value.val = valueSrc;




            StringWriter sw = new StringWriter();
            er.Serialize(sw, value);


            Console.WriteLine(sw.ToString());

            StringReader sr = new StringReader(sw.ToString());
            //XmlSerializer er2 = new XmlSerializer(value.GetType ());
            object obj = er.Deserialize(sr);
            Console.WriteLine(obj);
            return obj;
        }
    }
}
