using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common ;

namespace S
{
    public class LogItem
    {
        public LogItem(DateTime dt , string content)
        {
            DT = dt;
            Content = content;
        }

        #region DT
        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get
            {
                return _dT;
            }
            set
            {
                _dT = value;
            }
        } private DateTime _dT;
        #endregion //DT

        #region Content
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            get
            {
                if (_content == null)
                {
                    _content = string.Empty;
                }
                return _content;
            }
            set
            {
                _content = value;
            }
        } private string _content;
        #endregion //Content

        public override string ToString()
        {
            return DT.ToString() + Environment.NewLine + Content;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class LogItemCollection : Collection<LogItem>
    {
        /// <summary>
        /// 
        /// </summary>
        private static int MAXCOUNT = 1000;

        public event EventHandler Added;

        /// <summary>
        /// 
        /// </summary>
        internal LogItemCollection ()
        {
        }

        new public void Add(LogItem li)
        {
            base.Add(li);
            if (Added != null)
            {
                Added(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="this1"></param>
        protected override void InsertItem(int index, LogItem  item)
        {        
            base.InsertItem(index, item);
            if (this.Count >= MAXCOUNT)
            {
                this.RemoveAt(0);
            }
        }

        public override string ToString()
        {
            StringBuilder  sb = new StringBuilder ();
            foreach ( LogItem item in this )
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}
