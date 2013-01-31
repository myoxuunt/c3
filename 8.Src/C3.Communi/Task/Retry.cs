using System;

namespace C3.Communi
{
    public class Retry
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        public const int RETRY_MIN = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int RETRY_MAX = 5;
        #endregion //Members

        #region Retry
        /// <summary>
        /// 
        /// </summary>
        public Retry()
            : this(1)
        {

        }
        #endregion //Retry

        #region Retry
        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        public Retry(int times)
        {
            this.Times = times;
        }
        #endregion //Retry

        #region Times
        /// <summary>
        /// 
        /// </summary>
        public int Times
        {
            get
            {
                return _times;
            }
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(
                            "Times",
                            value,
                            string.Format("max must in [{0}, {1}]", RETRY_MIN, RETRY_MAX));
                }
                _times = value;
            }
        } private int _times = 1;
        #endregion //Times

        #region Current
        /// <summary>
        /// 
        /// </summary>
        public int Current
        {
            get
            {
                return _current;
            }
        } private int _current;
        #endregion //Current

        #region IncreaseCurrent
        /// <summary>
        /// 
        /// </summary>
        public void IncreaseCurrent()
        {
            _current++;
        }
        #endregion //IncreaseCurrent

        #region Reset
        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            this._current = 0;
        }
        #endregion //Reset

        #region CanTry
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanTry()
        {
            return _current < this._times;
        }
        #endregion //CanTry
    }

}
