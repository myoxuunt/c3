//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xdgk.Common;
//using System.Diagnostics;

//namespace C3.Communi
//{
//    public class GroupManager
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        private GroupManager()
//        {

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        static public GroupCollection Groups
//        {
//            get
//            {
//                if (_groups == null)
//                {
//                    _groups = new GroupCollection();
//                }
//                return _groups;
//            }
//        } static private GroupCollection _groups;

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        static Group GetGroup(string name)
//        {
//            Group r = null;
//            foreach (Group item in Groups)
//            {
//                if (StringHelper.Equal(item.Name, name))
//                {
//                    r = item;
//                    break;
//                }
//            }
//            return r;
//        }
//    }

//}
