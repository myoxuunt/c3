using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework; 
namespace Xdgk.Communi.BytesConverter.Test
{
    [TestFixture]
    public class XD1100AlaramConverterNO2Test
    {
        [Test]
        public void AlarmNO2Converter_vs_GRAlarmConverter()
        {
            impl(new byte[] { 0x01, 0x02});
            impl(new byte[] { 0xff, 0x00});
            impl(new byte[] { 0x00, 0xff});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        private void impl( byte[] bs )
        {
            byte[] alarm2byte = //new byte[] { 0xff, 0x00};
                bs;
            byte[] alarm4byte = new byte[] { 00, 00, alarm2byte[1], alarm2byte[0] };

            IList result1 = new GRAlarmDataConverter().ConvertToObject(alarm2byte) as IList;
            IList result2 = new XD1100AlarmNO2Converter().ConvertToObject(alarm4byte) as IList;

            Assert.AreEqual(result1.Count, result2.Count);

            for (int i = 0; i < result1.Count; i++)
            {

                object o1 = result1[i];
                object o2 = result2[i];

                Console.WriteLine(o1);
                Assert.AreEqual(o1, o2);
            }

        }
    }
}
