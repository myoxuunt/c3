using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    abstract public class SPUBase : ISPU 
    {
        /// <summary>
        /// 
        /// </summary>
        public Type StationType
        {
            get
            {
                return _stationType;
            }
            set
            {
                _stationType = value;
            }
        } private Type _stationType;

        /// <summary>
        /// 
        /// </summary>
        public IStationFactory StationFactory
        {
            get
            {
                return _stationFactory;
            }
            set
            {
                _stationFactory = value;
            }
        } private IStationFactory _stationFactory;


        /// <summary>
        /// 
        /// </summary>
        public IStationPersister StationPersister
        {
            get
            {
                return _stationPersister;
            }
            set
            {
                _stationPersister = value;
            }
        } private IStationPersister _stationPersister;

        /// <summary>
        /// 
        /// </summary>
        public IStationSourceProvider StationSourceProvider
        {
            get
            {
                return _stationSourceProvider;
            }
            set
            {
                _stationSourceProvider = value;
            }
        } private IStationSourceProvider _stationSourceProvider;

    }
}
