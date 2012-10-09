
namespace Xdgk.GR.Data
{
    public class OTControlLineDefines
    {
        public const int OTMin = -50;
        public const int OTMax = 50;
        public const int GT2Min = 0;
        public const int GT2Max = 99;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ot"></param>
        /// <returns></returns>
        static public bool CheckOTValue(int ot)
        {
            return (ot >= OTMin) && (ot <= OTMax);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gt2"></param>
        /// <returns></returns>
        static public bool CheckGT2Value(int gt2)
        {
            return (gt2 >= GT2Min) && (gt2 <= GT2Max);
        }
    }

}
