
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;


namespace C3.Communi
{
    public class UnitType
    {
        private UnitType(string text)
        {
            if (text == null)
            {
                _text = string.Empty;
            }

            this._text = text;
        }

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
                    _text = string.Empty;
                }
                return _text;
            }
        } private string _text;
#endregion //Text


        // TODO: 2012-07-23 unit type text
        //
        static public readonly UnitType
            None = new UnitType(""),
                 Length = new UnitType("Length"),
                 Press = new UnitType("Press"),
                 Temperature = new UnitType("Temperature"),
                 Flow = new UnitType("Flow"),
                 Volume = new UnitType("Volume"),
                 Time = new UnitType("Time");
    }

}
