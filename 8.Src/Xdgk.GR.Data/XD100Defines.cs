namespace Xdgk.GR.Data
{
    public class XD100Defines
    {
        /// <summary>
        ///  
        /// </summary>
        static public TemperatureControlModeCollection TemperatureControlModeCollection
        {
            get
            {
                if (_temperatureControlModeCollection == null)
                {
                    _temperatureControlModeCollection = new TemperatureControlModeCollection();

                    XD100TemperatureControlMode m = null;
                    m = new XD100TemperatureControlMode(
                            //CZGR1.XD100.XD100Strings.OTGT2ControlLine, 
                            "温度控制曲线",
                            XD100TemperatureControlModeEnum.OT_GT2);
                    _temperatureControlModeCollection.Add(m);

                    m = new XD100TemperatureControlMode(
                            //CZGR1.XD100.XD100Strings.TimeControlLine, 
                            "分时控制曲线",
                            XD100TemperatureControlModeEnum.Time_GT2);
                    _temperatureControlModeCollection.Add(m);
                }
                return _temperatureControlModeCollection;
            }
        } static private TemperatureControlModeCollection _temperatureControlModeCollection;


        /// <summary>
        /// 
        /// </summary>
        static public ValveTypeCollection ValveTypeCollection
        {
            get
            {
                if (_valveTypeCollection == null)
                {
                    _valveTypeCollection = new ValveTypeCollection();
                    ValveType v = null;
                    v = new ValveType(
                            //CZGR1.XD100.XD100Strings.AnalogValve, 
                            "模拟阀",
                            ValveTypeEnum.AnalogValve);
                    _valveTypeCollection.Add(v);

                    v = new ValveType(
                            //CZGR1.XD100.XD100Strings.TrinityValve, 
                            "三位阀",
                            ValveTypeEnum.TrinityValve);
                    _valveTypeCollection.Add(v);
                }
                return _valveTypeCollection;
            }
        } static private ValveTypeCollection _valveTypeCollection;

    }

}
