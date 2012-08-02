using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class Opera : OperaBase
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Opera(string deviceType, string name)
        {
            this._deviceType = deviceType;
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public Opera(string deviceType, string name, string text, string args)
        {
            this._deviceType = deviceType;
            this.Name = name;
            if (text == null || text.Length == 0)
            {
                this.Text = name;
            }
            else
            {
                this.Text = text;
            }
            _args = args;
        }
        #endregion //

        #region Args
        /// <summary>
        /// 
        /// </summary>
        public string Args
        {
            get { return _args; }
        }  private string _args;
        #endregion //Args

        #region DeviceType
        /// <summary>
        /// 
        /// </summary>
        public string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        } private string _deviceType;
        #endregion //DeviceType

        #region ReceiveParts
        /// <summary>
        /// 
        /// </summary>
        public ReceivePartCollection ReceiveParts
        {
            get 
            { 
                if( _receivePartCollection == null )
                    _receivePartCollection = new ReceivePartCollection();
                return _receivePartCollection;
            }
            set
            {
                this._receivePartCollection = value;
            }
        } private ReceivePartCollection _receivePartCollection;
        #endregion //ReceiveParts

        #region SendPart
        /// <summary>
        /// 
        /// </summary>
        public SendPart SendPart
        {
            get { return _sendPart; }
            set { _sendPart = value; }
        } private SendPart _sendPart;
        #endregion //

        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return this.Name;
            if (Args != null)
            {
                string s = string.Format(this.Text, CreateArgs(Args));
                return s;
            }
            return Text;
        }
        #endregion //ToString

        #region CreateArgs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object[] CreateArgs(string args)
        {
            List<object> list = new List<object>();
            string[] ss = args.Split(',');
            foreach (string name in ss)
            {
                if (name.Trim().Length == 0)
                    continue;

                DataField df = this.SendPart.DataFieldManager[name];
                if (df == null)
                {
                    // TODO:
                    //
                    //string exmsg = string.Format("not find datafield '{0}'", name);
                    //throw new ConfigException(name);
                }
                object obj = df.Value;
                list.Add(obj);
            }
            return list.ToArray();
        }
        #endregion //CreateArgs

        #region OnCreateSendBytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public override byte[] OnCreateSendBytes(IDevice device)
        {
            //TODO: address string 
            //
            this.SendPart["Address"] = device.Address;
            byte[] bytes = this.SendPart.ToBytes();
            return bytes;
        }
        #endregion //OnCreateSendBytes

        #region OnParseReceivedBytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="received"></param>
        /// <returns></returns>
        public override IParseResult OnParseReceivedBytes(IDevice device, byte[] received)
        {
            string addressFieldName = "Address";

            IParseResult pr = this.ReceiveParts.ToValues(received);


            if (pr.IsSuccess)
            {
                // match address
                //
                object addressObject = pr.Results[addressFieldName];
                if (addressObject != null)
                {
                    Int64 address = Convert.ToInt64(addressObject);
                    if (address != device.Address)
                    {
                        pr = new AddressErrorResult(device.Address, address);
                    }
                }
            }

            return pr;
        }
        #endregion //OnParseReceivedBytes
    }
}
