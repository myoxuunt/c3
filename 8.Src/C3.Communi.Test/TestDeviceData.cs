using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NUnit.Framework;

namespace C3.Communi.Test
{
    [TestFixture]
    public class TestDeviceData : DeviceDataBase
    {
        public override string ToString()
        {
            return base.ToString();
        }

        [Test]
        public void T()
        {
            Unit u = Unit.FindByName(Unit.Cm);
            TestDeviceData dat = new TestDeviceData();
            PropertyInfo[] pis = dat.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object[] atts = pi.GetCustomAttributes(typeof(DeviceDataItemAttribute), false);
                if (atts.Length > 0)
                {
                    //Console.WriteLine(atts[0].ToString());
                    DeviceDataItemAttribute att = (DeviceDataItemAttribute)atts[0];
                    object value = pi.GetValue(dat, null);
                    Console.WriteLine("{0} {1} {2} {3}", att.Name, att.OrderNumber, value, att.Unit.Text);

                }
            }
        }

        #region DoubleValue
        /// <summary>
        /// 
        /// </summary>

        [DeviceDataItem("DV", 0, Unit.Cm)]
        public double DoubleValue
        {
            get
            {
                //new DeviceDataItemAttribute("", 0, Unit.Cm);
                return _doubleValue;
            }
            set
            {
                _doubleValue = value;
            }
        } private double _doubleValue = 1.23d;
        #endregion //DoubleValue

        #region FloatValue
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("intvalue", 4, Unit.M3PerHour)]
        public float FloatValue
        {
            get
            {
                return _floatValue;
            }
            set
            {
                _floatValue = value;
            }
        } private float _floatValue = 12.567f;
        #endregion //FloatValue

        #region IntValue
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("intvalue", 1, Unit.M3PerHour)]
        public int IntValue
        {
            get
            {
                return _intValue;
            }
            set
            {
                _intValue = value;
            }
        } private int _intValue = 0987;

        #region S
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("stringvalue", 9999, Unit.M3PerHour)]
        public string S
        {
            get
            {
                if (_s == null)
                {
                    _s = string.Empty;
                }
                return _s;
            }
            set
            {
                _s = value;
            }
        } private string _s = "stringvlu";
        #endregion //S
        #endregion //IntValue
    }

}
