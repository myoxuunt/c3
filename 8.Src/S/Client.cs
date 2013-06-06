using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{
    internal class RPProcessor
    {
        static private ReceivePart RP

        {
            get
            {
                if (_rp == null)
                {
                    string xmlPath = Application.StartupPath + @"\Config\DeviceDefine.xml";
                    _rp = ReceivePartFacotry.Create(xmlPath, "vFlux", "receive");
                }
                return _rp;
            }
        }
        static ReceivePart _rp;

        /// <summary>
        /// 
        /// </summary>
        private RPProcessor()
        {

        }

        static public void Process(Client client, byte[] bs)
        {
            IParseResult pr = RP.ToValues(bs);
            if (pr.IsSuccess)
            {
                string name = Convert.ToString(pr.Results["name"]);
                name = name.Trim();

                //long ticks = Convert.ToInt64(pr.Results["dt"]);
                //byte[] bs2 = BitConverter.GetBytes(ticks);
                DateTime dt = (DateTime)pr.Results["dt"];
                //Console.WriteLine(BitConverter.ToString(bs2));
                Console.WriteLine(name + " : " + dt);

            }
            else
            {

            }

            Console.WriteLine(BitConverter.ToString(bs));
        }
    }
    public class Client : ITag 
    {
        // TODO: unregister received event
        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="communiPort"></param>
        public Client(ICommuniPort communiPort)
        {
            this.CommuniPort = communiPort;
            this.CommuniPort.Received += new EventHandler(CommuniPort_Received);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CommuniPort_Received(object sender, EventArgs e)
        {
            ICommuniPort cp = (ICommuniPort )sender ;
            byte[] bs = cp.Read();

            // TODO: 
            //

            RPProcessor.Process(this, bs);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get { return _communiPort; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CommuniPort");
                }
                _communiPort = value;
            }
        } private ICommuniPort _communiPort;

        #region ITag ≥…‘±

        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        } private object _tag;

        #endregion
    }

}
