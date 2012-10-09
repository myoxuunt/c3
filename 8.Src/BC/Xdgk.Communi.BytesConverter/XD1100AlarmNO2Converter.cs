using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace Xdgk.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class XD1100AlarmNO2Converter : BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            throw new NotImplementedException(
                "not implement XD1100PumpStateAndAlarmConverter.ConvertToBytes()"
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            /*
             * 
            10017	位	为1时一次供低压报警
            10018	位	为1时二次供高压报警
            10019	位	为1时二次回高压报警
            10020	位	为1时二次回低压报警
            10021	位	为1时一次供低温报警
            10022	位	为1时二次供高温报警
            10023	位	为1时水位计水位高报警
            10024	位	为1时水位计水位低报警
            10025	位	为1时循环泵1故障
            10026	位	为1时循环泵2故障
            10027	位	为1时循环泵3故障
            10028	位	为1时补水泵1故障
            10029	位	为1时补水泵2故障
            10030	位	为1时水位开关低报警
            10031	位	为1时水位开关高报警
            10032	位	为1时掉电故障
             * 
             */

            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (bytes.Length < 4)
            {
                return new ArgumentException("bytes.Length must >= 4");
            }

            byte[] alarmBytes = new byte[2];
            Array.Copy(bytes, 2, alarmBytes, 0, 2);

            Array.Reverse(alarmBytes);

            GRAlarmDataConverter gralarmDC = new GRAlarmDataConverter();
            object result = gralarmDC.ConvertToObject(alarmBytes);
            return result;
        }
    }
}
