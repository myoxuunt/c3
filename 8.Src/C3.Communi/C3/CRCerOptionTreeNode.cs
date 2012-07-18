//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;
//using C3.Communi;
//using Xdgk.Communi;
//namespace C3
//{
//    public class CRCerOptionTreeNode : OptionTreeNode
//    {
//        private CRCerCollection _crcers;
//        public CRCerOptionTreeNode(CRCerCollection crcers)
//        {
//            this.Text = "crcers";
//            _crcers = crcers;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public override Control Control
//        {
//            get
//            {
//                if (_control == null)
//                {
//                    _control = new UCCrcViewer(_crcers);
//                }
//                return _control;
//            }
//        }
//        private Control _control;
//    }

//}
