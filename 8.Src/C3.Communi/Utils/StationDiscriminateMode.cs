using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    internal class StationDiscriminateMode
    {
        /// <summary>
        /// 
        /// </summary>
        public class Item
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dm"></param>
            /// <param name="text"></param>
            public Item(DiscriminateMode dm, string text, Type type)
            {
                this._discriminateMode = dm;
                this._text = text;
                this._type = type;
            }

            #region Type
            /// <summary>
            /// 
            /// </summary>
            public Type Type
            {
                get
                {
                    return _type;
                }
            } private Type _type;
            #endregion //Type


            /// <summary>
            /// 
            /// </summary>
            public DiscriminateMode DiscriminateMode
            {
                get { return _discriminateMode; }
            } private DiscriminateMode _discriminateMode;

            /// <summary>
            /// 
            /// </summary>
            public string Text
            {
                get { return _text; }
            } private string _text;
        }

        /// <summary>
        /// 
        /// </summary>
        static public Item[] Items
        {
            get { return s_items; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communiPortType"></param>
        /// <returns></returns>
        static public DiscriminateMode Find(Type communiPortType)
        {
            foreach (Item item in Items)
            {
                if (item.Type == communiPortType)
                {
                    return item.DiscriminateMode;
                }
            }
            throw new InvalidOperationException(
                    string.Format("not find type '{0}'", communiPortType)
                    );
        }

        /// <summary>
        /// 
        /// </summary>
        static private Item[] s_items = new Item[] 
        {
            new Item (
                DiscriminateMode.ByIPAddress,  
                strings.ByIP,           
                typeof(RemoteIPAddressConfig)),

            new Item (
                DiscriminateMode.ByLocalPort,  
                strings.ByLocalPort,    
                typeof(LocalPortConfig)),

            new Item (
                DiscriminateMode.ByRemotePort, 
                strings.ByRemotePort,   
                typeof(RemotePortConfig)),

            new Item (
                DiscriminateMode.ByRemoteIPAddressAndPort, 
                strings.ByRemoteIPAndPort, 
                typeof (RemoteIPAddressAndPortConfig )),
        };
    }

}
