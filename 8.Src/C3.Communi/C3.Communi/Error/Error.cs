using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class ErrorManager
    {
        #region Constructor
        public ErrorManager(Soft soft)
        {
            this.Soft = soft;
        }
        #endregion //Constructor

        #region Soft
        /// <summary>
        /// 
        /// </summary>
        public Soft Soft
        {
            get
            {
                if (_soft == null)
                {
                    _soft = new Soft();
                }
                return _soft;
            }
            set
            {
                _soft = value;
            }
        } private Soft _soft;
        #endregion //Soft

        public void Process(Exception ex, string msg)
        {
            this.Process(ex, msg, false);
        }

        public void Process(Exception ex)
        {
            this.Process(ex, string.Empty, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="exit"></param>
        public void Process(Exception ex, string msg, bool exit)
        {
            
        }
    }
}
