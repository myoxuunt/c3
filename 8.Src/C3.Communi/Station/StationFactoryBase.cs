using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    abstract public class StationFactoryBase : IStationFactory
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spu"></param>
        protected StationFactoryBase(ISPU spu)
        {
            this.Spu = spu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationSource"></param>
        /// <returns></returns>
        public IStation Create(IStationSource stationSource)
        {
            return OnCreate(stationSource);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationSource"></param>
        /// <returns></returns>
        abstract protected IStation OnCreate(IStationSource stationSource);


        #region Spu
        /// <summary>
        /// 
        /// </summary>
        public ISPU Spu
        {
            get
            {
                return _spu;
            }
            set
            {
                if(value == null )
                {
                    throw new ArgumentNullException("Spu");
                }
                _spu = value;
            }
        } private ISPU _spu;

        #endregion
    }
}
