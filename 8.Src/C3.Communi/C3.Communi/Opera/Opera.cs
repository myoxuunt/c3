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
            this._name = name;
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
            this._name = name;
            if (text == null || text.Length == 0)
            {
                this._text = name;
            }
            else
            {
                this._text = text;
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

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        } private string _text;
        #endregion //Text

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } private string _name;
        #endregion //

        /// <summary>
        /// 
        /// </summary>
        public string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        } private string _deviceType;

        //#region ReceivePart
        ///// <summary>
        ///// 获取或设置第一个ReceivePart
        ///// </summary>
        //public ReceivePart ReceivePart
        //{
        //    get 
        //    { 
        //        //return _receivepart; 
        //        if (ReceiveParts.Count > 0)
        //            return _receivePartCollection[0];
        //        else
        //            return null;
        //    }
        //    set 
        //    { 
        //        //_receivepart = value; 
        //        if (value == null)
        //        {
        //            if (ReceiveParts.Count > 0)
        //            {
        //                ReceiveParts.RemoveAt(0);
        //            }
        //            else
        //            {
        //                // ignore
        //            }
        //        }
        //        else
        //        {
        //            if (ReceiveParts.Count > 0)
        //            {
        //                ReceiveParts[0] = value;
        //            }
        //            else
        //            {
        //                ReceiveParts.Add(value);
        //            }
        //        }
        //    }
        //}
        //#endregion //ReceivePart

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return this.Name;
            if (Args != null)
            {
                string s = string.Format(this._text, CreateArgs(Args));
                return s;
            }
            return _text;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="received"></param>
        /// <returns></returns>
        public override IParseResult OnParseReceivedBytes(IDevice device, byte[] received)
        {
            IParseResult pr = this.ReceiveParts.ToValues(received);
            return pr;

            /*
            // add send received log
            //
            AddCommuniDetail(this._lastSendBytes, bytes, pr);

            if (!pr.Success)
            {
                AddCommuniFailDetail(bytes, pr);
            }

            // match address
            //
            object addressObject = pr.NameObjects.GetObject("ADDRESS");
            if (addressObject != null)
            {
                
                //int address = (int)addressObject;
                //int address = Convert.ToInt32(addressObject);
                Int64 address = Convert.ToInt64(addressObject);
                if (address != this.Device.Address)
                {
                    //return new DataErrorResult( pr.ReceivePartName, pr
                    pr = new AddressErrorResult(pr.ReceivePartName, this.Device.Address, address);
                    AddCommuniFailDetail(bytes, pr);
                }
            }
            */

        }
    }
}
