
using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi.Resources;


namespace C3.Communi
{
    public class SendFailCommuniDetail : CommuniDetailBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operaText"></param>
        /// <param name="sendDateTime"></param>
        public SendFailCommuniDetail(string operaText, DateTime sendDateTime,
                byte[] sendBytes)
            : base(operaText)
        {
            //this._operaText = operaText;
            this._sendDateTime = sendDateTime;
            this._send = sendBytes;
        }

#region Send
        /// <summary>
        /// 
        /// </summary>
        public byte[] Send
        {
            get
            {
                if (_send == null)
                {
                    _send = new byte[0];
                }
                return _send;
            }
        } private byte[] _send;
#endregion //Send

//#region OperaText
//        /// <summary>
//        /// 
//        /// </summary>
//        public string OperaText
//        {
//            get
//            {
//                if (_operaText == null)
//                {
//                    _operaText = string.Empty;
//                }
//                return _operaText;
//            }
//        } private string _operaText;
//#endregion //OperaText

#region SendDateTime
        /// <summary>
        /// 
        /// </summary>
        public DateTime SendDateTime
        {
            get { return _sendDateTime; }
        } private DateTime _sendDateTime;
#endregion //SendDateTime

        public override string ToString()
        {
            string _splitString = CommuniDetailResource.SplitString ;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(CommuniDetailResource.SendDateTime + _splitString + this.SendDateTime.ToString());
            sb.AppendLine(CommuniDetailResource.Opera + _splitString + this.OperaText);
            sb.AppendLine(CommuniDetailResource.Result + _splitString + CommuniDetailResource.SendFail);
            sb.AppendLine(CommuniDetailResource.Sended + _splitString + GetBytesString(this.Send));
            return sb.ToString();
        }
    }

}
