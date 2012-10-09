namespace XD1100DPU
{
    abstract public class HeatTransferMode
    {
        static private HeatTransferMode
            _direct = new HtmDirect(),
                    _indirect = new HtmIndirect(),
                    _mixed = new HtmMixed();

        /// <summary>
        /// 
        /// </summary>
        static private HeatTransferMode[] a = new HeatTransferMode[]
        {
            _direct, _indirect , _mixed 
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public HeatTransferMode Parse(int value)
        {
            ModeValue mv = (ModeValue)value;
            return Parse(mv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modeValue"></param>
        /// <returns></returns>
        static public HeatTransferMode Parse(ModeValue modeValue)
        {
            HeatTransferMode r = null;
            foreach (HeatTransferMode item in a)
            {
                if (modeValue == item.ModeValue)
                {
                    r = item;
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        abstract public ModeValue ModeValue{get;}


    }

}
