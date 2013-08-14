
using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;


namespace C3.Communi
{
    public class ComplexOpera : OperaBase
    {
        private int _currentIndex = 0;

        public ComplexOpera(string deviceType, string name, OperaCollection childOperas)
        {
            if (string.IsNullOrEmpty(deviceType))
            {
                throw new ArgumentException("deviceType is null or empty");
            }

            if (childOperas == null || childOperas.Count == 0)
            {
                throw new ArgumentException("childOperas is null or empty");
            }

            this._deviceType = deviceType;
            this.Name = name;
            this._childOperas = childOperas;
        }

        /// <summary>
        /// 
        /// </summary>
        public OperaCollection ChildOperas
        {
            get
            {
                if (_childOperas == null)
                {
                    _childOperas = new OperaCollection();
                }
                return _childOperas;
            }
        }
        private OperaCollection _childOperas = null;

        #region DeviceType
        /// <summary>
        /// 
        /// </summary>
        public string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        } private string _deviceType;
        #endregion //DeviceType

        public override IOpera Current
        {
            get
            {
                if (_currentIndex >= 0 && _currentIndex < _childOperas.Count)
                {
                    return _childOperas[_currentIndex];
                }
                else
                {
                    throw new InvalidOperationException("current index out of range");
                    //return null;
                }
            }
        }

        public override bool IsComplex()
        {
            return true;
        }

        public override bool NextChildOpera()
        {
            //_currentIndex++;
            //return _currentIndex < _childOperas.Count;

            if (_currentIndex < _childOperas.Count - 1)
            {
                _currentIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ResetChildOpera()
        {
            _currentIndex = 0;
        }

        public override byte[] OnCreateSendBytes(IDevice device)
        {
            return this.Current.CreateSendBytes(device);
        }

        public override IParseResult OnParseReceivedBytes(IDevice device, byte[] received)
        {
            return this.Current.ParseReceivedBytes(device, received);
        }

        public override bool HasNextChildOpera()
        {
            return this._currentIndex < this.ChildOperas.Count - 1;
        }

        public override string Text
        {
            get
            {
                return string.Format("{0}.{1}", base.Text, this.Current.Text);
            }
            set
            {
                base.Text = value;
            }
        }
    }

}
