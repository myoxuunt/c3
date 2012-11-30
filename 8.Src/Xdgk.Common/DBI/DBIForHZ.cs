namespace Xdgk.Common
{
    public class DBIForHZ : DBIBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        public DBIForHZ(string connString) : base ( connString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        virtual public object InsertFlowmeterData(int deviceID, FlowmeterData data)
        {
            string s = string.Format(
                    "insert into tblFluxData(deviceid, DT, instantFlux, sum) values({0}, '{1}', {2}, {3})",
                    deviceID, data.DT, data.InstantFlux, data.Sum);

            return ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="data"></param>
        virtual public object InsertHeatData(int deviceID, HeatData data)
        {
            string s = 
                "insert into tblHeatData(deviceid, dt, instantFlux, sum, IH, SH, GT, BT) " +
                "values(@deviceid, @dt, @instantFlux, @sum, @IH, @SH, @GT, @BT)" ;

            KeyValueCollection kvs = new KeyValueCollection();

            kvs.Add("deviceid", deviceID);
            kvs.Add("dt", data.DT);
            kvs.Add("instantFlux", data.InstantFlux);
            kvs.Add("sum", data.Sum);
            kvs.Add("IH", data.InstantHeat);
            kvs.Add("SH", data.SumHeat);
            kvs.Add("GT", data.GT);
            kvs.Add("BT", data.BT);

            return this.ExecuteScalar(s, kvs);
        }
    }

}
