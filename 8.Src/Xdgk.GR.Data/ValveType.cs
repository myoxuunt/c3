namespace Xdgk.GR.Data
{
    public class ValveType
    {
        #region ValveType
        /// <summary>
        /// 
        /// </summary>
        public ValveType(string name, ValveTypeEnum type)
        {
            this.Name = name;
            this._valveTypeEnum = type;
        }
        #endregion //ValveType

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } private string _name;
        #endregion //Name

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get { return (int)this._valveTypeEnum; }
        }
        #endregion //Value

        #region ValveTypeEnum
        /// <summary>
        /// 
        /// </summary>
        public ValveTypeEnum ValveTypeEnum
        {
            get { return _valveTypeEnum; }
        } private ValveTypeEnum _valveTypeEnum;
        #endregion //ValveTypeEnum
    }

}
