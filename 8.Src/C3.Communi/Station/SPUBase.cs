using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    abstract public class SPUBase : ISPU 
    {

        #region Name
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
        #endregion //Name

        #region StationType
        /// <summary>
        /// 
        /// </summary>
        public StationType StationType
        {
            get
            {
                return _stationType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("StationType");
                }
                _stationType = value;
            }
        } private StationType _stationType;
        #endregion //StationType

        #region StationFactory
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
        #endregion //StationFactory


        #region StationPersister
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
        #endregion //StationPersister

        #region StationSourceProvider
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
        #endregion //StationSourceProvider

        #region Description
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Empty;
                }
                return _description; 
            }
            set { _description = value; }
        } private string _description;
        #endregion //Description


        #region StationUI
        /// <summary>
        /// 
        /// </summary>
        public IStationUI StationUI
        {
            get
            {
                return _stationUI;
            }
            set
            {
                _stationUI = value;
            }
        } private IStationUI _stationUI;

        #endregion //StationUI
    }
}
