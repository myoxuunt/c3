
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class OperaBase : IOpera
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public byte[] CreateSendBytes(IDevice device)
        {
            byte[] bytes = OnCreateSendBytes(device);

            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        abstract public byte[] OnCreateSendBytes(IDevice device);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="received"></param>
        /// <returns></returns>
        public IParseResult ParseReceivedBytes(IDevice device, byte[] received)
        {
            IParseResult pr = OnParseReceivedBytes(device, received);
            return pr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="received"></param>
        /// <returns></returns>
        abstract public IParseResult OnParseReceivedBytes(IDevice device, byte[] received);

#region IOpera ≥…‘±

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        } private string _name;

        public string Text
        {
            get
            {
                if (_text == null)
                {
                    _text = string.Empty;
                }
                return _text;
            }
            set
            {
                _text = value;
            }
        } private string _text; 

#endregion
    }

}
