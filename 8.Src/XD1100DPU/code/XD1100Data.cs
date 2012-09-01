using System;
using C3.Communi;
using C3.Data;
using Xdgk.GR.Data;

namespace XD1100DPU
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    internal class XD1100Data : DeviceDataBase
    {
        private const string FloatFormat = "f2",
            IntFormat = "G";
        #region Members
        private float _GT1;
        private float _BT1;
        private float _GT2;
        private float _BT2;
        private float _OT;
        private float _GTBase2;
        private float _GP1;
        private float _BP1;
        private float _WL;
        private float _GP2;
        private float _BP2;
        private float _I1;
        private float _I2;
        private float _IR;
        private Int32 _S1;
        private Int32 _S2;
        private Int32 _SR;
        private int _OD;
        private float _PA2;
        private PumpStatus _CM1;
        private PumpStatus _CM2;
        private PumpStatus _CM3;
        private PumpStatus _RM1;
        private PumpStatus _RM2;
        #endregion //Members

        #region XD1100Data
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardSn"></param>
        public XD1100Data()
        {
        }
        #endregion //XD1100Data

        #region GT1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("一次供温", 1, Unit.Centidegree, FloatFormat)]
        public float GT1
        {
            get { return _GT1; }
            set { _GT1 = value; }
        }
        #endregion //GT1

        #region BT1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("一次回温", 2, Unit.Centidegree, FloatFormat)]
        public float BT1
        {
            get { return _BT1; }
            set { _BT1 = value; }
        }
        #endregion //BT1

        #region GT2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("二次供温", 3, Unit.Centidegree, FloatFormat)]
        public float GT2
        {
            get { return _GT2; }
            set { _GT2 = value; }
        }
        #endregion //GT2

        #region BT2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("二次回温", 4, Unit.Centidegree, FloatFormat)]
        public float BT2
        {
            get { return _BT2; }
            set { _BT2 = value; }
        }
        #endregion //BT2

        #region OT
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("室外温度", 5, Unit.Centidegree, FloatFormat)]
        public float OT
        {
            get { return _OT; }
            set { _OT = value; }
        }
        #endregion //OT

        #region GTBase2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("供温基准", 6, Unit.Centidegree, FloatFormat)]
        public float GTBase2
        {
            get { return _GTBase2; }
            set { _GTBase2 = value; }
        }
        #endregion //GTBase2

        #region GP1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("一次供压", 7, Unit.Mpa, FloatFormat)]
        public float GP1
        {
            get { return _GP1; }
            set { _GP1 = value; }
        }
        #endregion //GP1

        #region BP1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("一次回压", 8, Unit.Mpa, FloatFormat)]
        public float BP1
        {
            get { return _BP1; }
            set { _BP1 = value; }
        }
        #endregion //BP1

        #region WL
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("水箱水位", 9, Unit.M, FloatFormat)]
        public float WL
        {
            get { return _WL; }
            set { _WL = value; }
        }
        #endregion //WL

        #region GP2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("二次供压", 10, Unit.Mpa, FloatFormat)]
        public float GP2
        {
            get { return _GP2; }
            set { _GP2 = value; }
        }
        #endregion //GP2

        #region BP2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("二次回压", 11, Unit.Mpa, FloatFormat)]
        public float BP2
        {
            get { return _BP2; }
            set { _BP2 = value; }
        }
        #endregion //BP2

        #region I1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("一次瞬时", 12, Unit.M3PerSecond, FloatFormat)]
        public float I1
        {
            get { return _I1; }
            set { _I1 = value; }
        }
        #endregion //I1

        #region I2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("二次瞬时", 13, Unit.M3PerSecond, FloatFormat)]
        public float I2
        {
            get { return _I2; }
            set { _I2 = value; }
        }
        #endregion //I2

        #region IR
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("补水瞬时", 14, Unit.M3PerSecond, FloatFormat)]
        public float IR
        {
            get
            {
                if (_IR < 0)
                {
                    _IR = 0;
                }
                return _IR;
            }
            set { _IR = value; }
        }
        #endregion //IR

        #region S1
        [DeviceDataItem("一次累计", 15, Unit.M3, IntFormat)]
        /// <summary>
        /// 
        /// </summary>
        public Int32 S1
        {
            get { return _S1; }
            set { _S1 = value; }
        }
        #endregion //S1

        #region S2
        [DeviceDataItem("二次累计", 16, Unit.M3, IntFormat)]
        public Int32 S2
        {
            get { return _S2; }
            set { _S2 = value; }
        }
        #endregion //S1

        #region SR
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("补水累计", 17, Unit.M3, IntFormat)]
        public Int32 SR
        {
            get { return _SR; }
            set { _SR = value; }
        }
        #endregion //SR

        #region OD
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("阀门开度", 18, Unit.Percentage, IntFormat)]
        public int OD
        {
            get { return _OD; }
            set { _OD = value; }
        }
        #endregion //OD

        #region PA2
        /// <summary>
        /// 
        /// </summary>
        public float PA2
        {
            get { return _PA2; }
            set { _PA2 = value; }
        }
        #endregion //PA2

        #region CM1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("循环泵1", 19, Unit.None)]
        public PumpStatus CM1
        {
            get
            {
                if (_CM1 == null)
                {
                    _CM1 = PumpStatus.Stop;
                }
                return _CM1;
            }
            set { _CM1 = value; }
        }
        #endregion //CM1

        #region PumpStatus CM2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("循环泵2", 20, Unit.None)]
        public PumpStatus CM2
        {
            get
            {
                if (_CM2 == null)
                {
                    _CM2 = PumpStatus.Stop;
                }
                return _CM2;
            }
            set { _CM2 = value; }
        }
        #endregion //CM2

        #region CM3
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("循环泵3", 21, Unit.None)]
        public PumpStatus CM3
        {
            get
            {

                if (_CM3 == null)
                {
                    _CM3 = PumpStatus.Stop;
                }
                return _CM3;
            }
            set { _CM3 = value; }
        }
        #endregion //CM3

        #region RM1
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("补水泵1", 22, Unit.None)]
        public PumpStatus RM1
        {
            get
            {
                if (_RM1 == null)
                {
                    _RM1 = PumpStatus.Stop;
                }
                return _RM1;
            }
            set { _RM1 = value; }
        }
        #endregion //RM1

        #region RM2
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("补水泵2", 23, Unit.None)]
        public PumpStatus RM2
        {
            get
            {
                if (_RM2 == null)
                {
                    _RM2 = PumpStatus.Stop;
                }
                return _RM2;
            }
            set { _RM2 = value; }
        }
        #endregion //RM2
    }
}
