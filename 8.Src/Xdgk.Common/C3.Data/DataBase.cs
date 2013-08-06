using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;

namespace Xdgk.Common
{
    internal class HideDataAttributeNameManager
    {
        internal class TypeNames
        {
            /// <summary>
            /// 
            /// </summary>
            internal TypeNames(string type)
            {
                _type = type;
            }

            /// <summary>
            /// 
            /// </summary>
            internal string Type
            {
                get { return _type; }
            } private string _type;

            /// <summary>
            /// 
            /// </summary>
            internal List<string> Names
            {
                get
                {
                    if (_names == null)
                    {
                        _names = new List<string>();
                    }
                    return _names;
                }
            } private List<string> _names;
        }


        private List<TypeNames> _list = new List<TypeNames>();

        static internal HideDataAttributeNameManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new HideDataAttributeNameManager();

                    // create 
                    //
                    string fileName = System.Windows.Forms.Application.StartupPath + "\\Config\\HideData.xml";
                    if (!File.Exists(fileName))
                    {
                        goto EXIT;
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);
                    XmlNodeList typeNodes = doc.SelectNodes("hideData/type");
                    if (typeNodes == null)
                    {
                        goto EXIT; 
                    }

                    foreach (XmlNode typeNode in typeNodes)
                    {
                        XmlAttribute typeNameAtt = typeNode.Attributes["name"];
                        if (typeNameAtt != null)
                        {
                            string typeName = typeNameAtt.Value;
                            TypeNames type_names = new TypeNames(typeName);

                            XmlNodeList attributeNodes = typeNode.SelectNodes("attribute");
                            foreach (XmlNode attNode in attributeNodes)
                            {
                                XmlAttribute xmlAttName = attNode.Attributes["name"];
                                if (xmlAttName != null)
                                {
                                    string name = xmlAttName.Value;
                                    type_names.Names.Add(name);
                                }
                            }
                            _default._list.Add(type_names);
                        }
                    }
                }

            EXIT:
                return _default;
            }
        } static private HideDataAttributeNameManager _default;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="attributeNameForHide"></param>
        /// <returns></returns>
        internal bool Contains(string type, string name)
        {
            type = type.Trim();

            foreach (TypeNames item in _list)
            {
                if (item.Type == type)
                {
                    bool r = item.Names.Contains(name);
                    if (r)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    abstract public class DataBase : IData
    {

        #region DataBase
        /// <summary>
        /// 
        /// </summary>
        protected DataBase()
        {
            this.DT = DateTime.Now;
        }
        #endregion //DataBase

        #region DT
        /// <summary>
        /// 
        /// </summary>
        /// 
        [DataItem("Ê±¼ä", -1, Unit.None, "G")]
        public DateTime DT
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        } private DateTime _dt;
        #endregion //DT

        #region GetDeviceDataItemAttributes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        virtual public AttributePropertyInfoPairCollection GetDeviceDataItemAttributes()
        {
            AttributePropertyInfoPairCollection result = new AttributePropertyInfoPairCollection();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            foreach (PropertyInfo pi in propertyInfos)
            {
                object[] atts = pi.GetCustomAttributes(typeof(DataItemAttribute), false);
                if (atts.Length > 0)
                {
                    DataItemAttribute att = (DataItemAttribute)atts[0];

                    AttributePropertyInfoPair pair = new AttributePropertyInfoPair(
                        att, pi);

                    result.Add(pair);
                }
            }

            // sort
            //
            result.Sort();

            return result;
        }
        #endregion //GetDeviceDataItemAttributes

        #region GetReportItems
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        virtual public ReportItemCollection GetReportItems()
        {
            ReportItemCollection reportItems = new ReportItemCollection();

            AttributePropertyInfoPairCollection attPropertyInfos = this.GetDeviceDataItemAttributes();
            foreach (AttributePropertyInfoPair item in attPropertyInfos)
            {
                DataItemAttribute att = item.Attribute;
                PropertyInfo pi = item.PropertyInfo;
                object value = pi.GetValue(this, null);

                //if (!this.HideDataAttributeNames.Contains(att.Name))
                if (!HideDataAttributeNameManager.Default.Contains(this.GetType().Name, att.Name))
                {
                    ReportItem reportItem = new ReportItem(att.Name, value, att.Unit, att.Format);
                    reportItems.Add(reportItem);
                }
            }

            return reportItems;
        }
        #endregion //GetReportItems

        #region IsValid
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
        {
            get { return true; }
        }
        #endregion //IsValid

        #region GetValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetValue(string propertyName)
        {
            return ReflectionHelper.GetPropertyValue(this, propertyName);
        }
        #endregion //GetValue

        #region HasPropertyName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool HasPropertyName(string propertyName)
        {
            return ReflectionHelper.HasProperty(this, propertyName);
        }
        #endregion //HasPropertyName

        //#region HideDataAttributeNames
        ///// <summary>
        ///// 
        ///// </summary>
        //public List<string> HideDataAttributeNames
        //{
        //    get
        //    {
        //        if (_hideDataAttributeNames == null)
        //        {
        //            _hideDataAttributeNames = new List<string>();
        //        }
        //        return _hideDataAttributeNames;
        //    }
        //} private List<string> _hideDataAttributeNames;
        //#endregion //HideDataAttributeNames
    }
}
