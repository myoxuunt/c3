using System;
using Xdgk.Common;

public class TypeBase
{

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="description"></param>
    /// <param name="type"></param>
    internal TypeBase(string name, string text, string description, Type type)
    {
        this.Name = name;
        this.Text = text;
        this.Description = description;
        this.Type = type;
    }
    #endregion //Constructor

    #region Text
    /// <summary>
    /// 
    /// </summary>
    public string Text
    {
        get
        {
            if (_text == null)
            {
                _text = this.Name;
            }
            return _text;
        }
        set
        {
            _text = value;
        }
    } private string _text;
    #endregion //Text

    #region Description
    /// <summary>
    /// 
    /// </summary>
    public string Description
    {
        get
        {
            if (_description == null)
            {
                _description = string.Empty;
            }
            return _description;
        }
        set
        {
            _description = value;
        }
    } private string _description;
    #endregion //Description

    #region Type
    /// <summary>
    /// 
    /// </summary>
    public Type Type
    {
        get
        {
            return _type;
        }
        private set
        {
            if (value == null)
            {
                throw new ArgumentNullException("Type");
            }
            _type = value;
        }
    } private Type _type;
    #endregion //Type

    #region Name
    /// <summary>
    /// 
    /// </summary>
    public string Name
    {
        get
        {
            if (_name == null)
            {
                _name = string.Empty;
            }
            return _name;
        }
        private
        set
        {
            _name = value;
        }
    } private string _name;
    #endregion //Name

    #region ToString
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return this.Text;
    }
    #endregion //ToString
}

