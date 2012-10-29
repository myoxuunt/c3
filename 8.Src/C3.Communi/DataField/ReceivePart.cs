using System;
namespace C3.Communi
{

    /// <summary>
    /// 代表从设备返回的数据的定义以及相关处理
    /// </summary>
    public class ReceivePart : PartBase 
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ReceivePart(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "must >= 0 ");
            this.DataFieldManager.Length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        public ReceivePart(string name, int length)
            : this( length )
        {
            this.Name = name;
        }
        #endregion //

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } private string _name = string.Empty;


        //#region DatafieldManager
        ///// <summary>
        ///// 
        ///// </summary>
        //public DatafieldManager DataFieldManager
        //{
        //    get
        //    {
        //        if (this._DatafieldManager == null)
        //            this._DatafieldManager = new DatafieldManager();
        //        return this._DatafieldManager;
        //    }
        //} private DatafieldManager _DatafieldManager;
        //#endregion //

        #region ToValues
        /// <summary>
        /// 根据DataFieldManager的定义，将bytes转换为相应的值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public IParseResult ToValues(byte[] bytes)
        {
            if (bytes == null)
            {
                //    return new LengthErrorResult(this.Name, this.DataFieldManager.Length, 0);
                bytes = new byte[0];
            }

            if (bytes.Length < this.DataFieldManager.Length)
                return new LengthErrorResult(this.Name, this.DataFieldManager.Length, bytes.Length);

            return this.DataFieldManager.ToValues(this.Name, bytes);
        }
        #endregion //ToValues

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="df"></param>
        public void Add(DataField df)
        {
            if (!df.IsBytesVolatile)
            {
                throw new ArgumentException("dataField.IsBytesVolatile must be true");
            }
            this.DataFieldManager.DataFields.Add(df);
        }
        #endregion //Add

        //#region Remove
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="df"></param>
        //public void Remove(DataField df)
        //{
        //    this.DataFieldManager.DataFields.Remove(df);
        //}
        //#endregion //Remove
    }
}
