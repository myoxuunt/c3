
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
using C3.Data;


namespace XD1100DPU
{
    internal class XD1100DeviceSource : DeviceSourceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        internal XD1100DeviceSource(DataRow row)
        {
            this.DataRow = row;
        }

        #region DataRow
        /// <summary>
        /// 
        /// </summary>
        public DataRow DataRow
        {
            get
            {
                return _dataRow;
            }
            set
            {
                _dataRow = value;
                this.Address = Convert.ToUInt64(_dataRow["DeviceAddress"]);

                this.DevcieTypeName = _dataRow["DeviceType"].ToString().Trim();

                this.Guid = GuidHelper.Create(
                        Convert.ToInt32(_dataRow["DeviceID"])
                        );

                this.StationGuid = GuidHelper.Create(
                        Convert.ToInt32(_dataRow["StationID"])
                        );

                string ex = _dataRow["DeviceExtend"].ToString();
                StringStringDictionary extend = StringStringDictionaryConverter.Parse(ex);

                foreach (string obj in extend.Keys)
                {
                    if (StringHelper.Equal(obj.ToString(), "htmMode"))
                    {
                        string k = extend[obj];
                        this.HtmModeValue = (ModeValue)Enum.Parse(typeof(ModeValue), k);
                    }
                }
            }
        } private DataRow _dataRow;

        #endregion //DataRow

        #region HtmModeValue
        /// <summary>
        /// 
        /// </summary>
        public ModeValue HtmModeValue
        {
            get
            {
                return _htmModeValue;
            }
            set
            {
                _htmModeValue = value;
            }
        } private ModeValue _htmModeValue;
        #endregion //HtmModeValue

    }

}
