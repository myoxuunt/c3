
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class CommuniPortConfigParameter : ParameterBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderNumber"></param>
        public CommuniPortConfigParameter(string name, ICommuniPortConfig value, int orderNumber)
            : base(name, typeof(ICommuniPortConfig), value, orderNumber)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPortConfig CommuniPortConfig
        {
            get { return (ICommuniPortConfig)this.Value; }
            set { this.Value = value; }
        }
    }

}
