using System.Reflection;
using Xdgk.Common;

namespace Xdgk.Common
{
    public class AttributePropertyInfoPair : IOrderNumber
    {

        #region AttributePropertyInfoPair
        /// <summary>
        /// 
        /// </summary>
        /// <param name="att"></param>
        /// <param name="pi"></param>
        public AttributePropertyInfoPair(DataItemAttribute att, PropertyInfo pi)
        {
            this.Attribute = att;
            this.PropertyInfo = pi;
        }
        #endregion //AttributePropertyInfoPair

        #region Attribute
        /// <summary>
        /// 
        /// </summary>
        public DataItemAttribute Attribute
        {
            get
            {
                return _attribute;
            }
            set
            {
                _attribute = value;
            }
        } private DataItemAttribute _attribute;
        #endregion //Attribute

        #region PropertyInfo
        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo PropertyInfo
        {
            get
            {
                return _propertyInfo;
            }
            set
            {
                _propertyInfo = value;
            }
        } private PropertyInfo _propertyInfo;
        #endregion //PropertyInfo

        #region OrderNumber
        /// <summary>
        /// 
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return this.Attribute.OrderNumber;
            }
            set
            {
                this.Attribute.OrderNumber = value;
            }
        }

        #endregion //OrderNumber
    }

}
