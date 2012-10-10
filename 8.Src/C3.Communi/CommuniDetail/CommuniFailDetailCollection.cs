using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniDetailCollection : Xdgk.Common.Collection<CommuniDetail>
    {
        private static int MAXCOUNT = 1000;
        public CommuniDetailCollection()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="this1"></param>
        protected override void InsertItem(int index, CommuniDetail item)
        {        
            base.InsertItem(index, item);
            if (this.Count >= MAXCOUNT)
            {
                // 2010-09-09
                //
                //this.RemoveAt(this.Count - 1);
                this.RemoveAt(0);
            }
        }
    }
}
