using System;

namespace C3.Communi
{
    public class StringParameterController : IController
    {
        public StringParameterController(StringParameter p)
        {
            this.Model = p;
        }

        #region IController ≥…‘±

        public IModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                //UpdateViewer();
            }
        } private IModel _model;

        public IViewer Viewer
        {
            get
            {
                if (_stringParameterViewer == null)
                {
                    _stringParameterViewer = new StringParameterViewer();
                    _stringParameterViewer.Controller = this;
                }

                return _stringParameterViewer;
            }
            set
            {
                throw new NotSupportedException();
            }
        } private StringParameterViewer _stringParameterViewer;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StringParameterViewer GetStringParameterViewer()
        {
            return this.Viewer as StringParameterViewer;
        }

        public void UpdateModel()
        {
            StringParameter p = (StringParameter)this.Model;
            StringParameterViewer v = this.GetStringParameterViewer();
            p.Value = v.Value;

        }

        public void UpdateViewer()
        {
            StringParameter p = (StringParameter)this.Model;
            StringParameterViewer v = this.GetStringParameterViewer();
            v.ParameterName = p.Text + ":";
            v.Value = p.Value.ToString();
            v.Unit = p.Unit.ToString();
        }

        public bool Verify()
        {
            return true;
        }

        #endregion
    }

}
