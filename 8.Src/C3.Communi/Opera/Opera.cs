using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public class OperaWithChild : OperaBase
    {
        private int _currentIndex = 0;

        public OperaWithChild(string deviceType, string name, OperaCollection childOperas)
        {
            if (string.IsNullOrEmpty(deviceType))
            {
                throw new ArgumentException("deviceType is null or empty");
            }

            if (childOperas == null || childOperas.Count == 0)
            {
                throw new ArgumentException("childOperas is null or empty");
            }

            this._deviceType = deviceType;
            this.Name = name;
            this._childOperas = childOperas;
        }

        /// <summary>
        /// 
        /// </summary>
        public OperaCollection ChildOperas
        {
            get
            {
                if (_childOperas == null)
                {
                    _childOperas = new OperaCollection();
                }
                return _childOperas;
            }
        }
        private OperaCollection _childOperas = null;

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

        public override IOpera Current
        {
            get
            {
                if (_currentIndex >= 0 && _currentIndex < _childOperas.Count)
                {
                    return _childOperas[_currentIndex];
                }
                else
                {
                    throw new InvalidOperationException("current index out of range");
                }
            }
        }

        public override bool HasChildOpera()
        {
            return true;
        }

        public override bool NextChildOpera()
        {
            _currentIndex++;
            return _currentIndex < _childOperas.Count;
        }

        public override void ResetChildOpera()
        {
            _currentIndex = 0;
        }

        public override byte[] OnCreateSendBytes(IDevice device)
        {
            return this.Current.CreateSendBytes(device);
        }

        public override IParseResult OnParseReceivedBytes(IDevice device, byte[] received)
        {
            return this.Current.ParseReceivedBytes(device, received);
        }
    }




    /// <summary>
    /// 
    /// </summary>
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

        private const string ADDRESS = "Address";

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
            if (this.SendPart.IsNeedAddress)
            {
                if (this.SendPart.DataFieldManager[ADDRESS] != null)
                {
                    this.SendPart[ADDRESS] = device.Address;
                }
            }

            DataFieldValueProvider valueProvider = new DataFieldValueProvider(device);
            byte[] bytes = this.SendPart.ToBytes(valueProvider);

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
            IParseResult pr = this.ReceiveParts.ToValues(received);

            if (pr.IsSuccess)
            {
                // has address data field
                //
                KeyValue addressKV = pr.Results.Find(ADDRESS);
                bool hasAddress = addressKV != null;
                if (hasAddress)
                {
                    // match address
                    //
                    object addressObject = pr.Results[ADDRESS];
                    if (addressObject != null)
                    {
                        UInt64 address = Convert.ToUInt64(addressObject);
                        if (address != device.Address)
                        {
                            pr = new AddressErrorResult(device.Address, address);
                        }
                    }
                }
            }

            return pr;
        }
        #endregion //OnParseReceivedBytes

        ///// <summary>
        ///// 
        ///// </summary>
        //public DataFieldValueProvider DataFieldValueProvider
        //{
        //    get 
        //    {
        //        if (_dataFieldValueProvider == null)
        //        {
        //            _dataFieldValueProvider = new DataFieldValueProvider();
        //        }
        //        return _dataFieldValueProvider;
        //    }
        //} private DataFieldValueProvider _dataFieldValueProvider;

        public override IOpera Current
        {
            get { return this; }
        }

        public override bool HasChildOpera()
        {
            return false;
        }

        public override bool NextChildOpera()
        {
            return false;
        }

        public override void ResetChildOpera()
        {
        }
    }
}
