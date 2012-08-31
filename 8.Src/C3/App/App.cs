using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class C3App : Xdgk.Common.AppBase
    {
        /// <summary>
        /// 
        /// </summary>
        private C3App()
        {
            this.IsProcessUnhandleException = !false;
            //Soft.UISynchronizationContext = new System.Windows.Forms.WindowsFormsSynchronizationContext();
        }

        /// <summary>
        /// 
        /// </summary>
        static public C3App App
        {
            get
            {
                if (Xdgk.Common.AppBase.DefaultInstance== null)
                {
                    Xdgk.Common.AppBase.DefaultInstance = new C3App();
                }
                return Xdgk.Common.AppBase.DefaultInstance as C3App;
            }
        } 


        /// <summary>
        /// 
        /// </summary>
        public override System.Windows.Forms.Form MainForm
        {
            get
            {
                if (_mainForm == null)
                {
                    _mainForm = new FrmMain();
                }
                return _mainForm;
            }
        } private System.Windows.Forms.Form _mainForm;

        /// <summary>
        /// 
        /// </summary>
        public Soft Soft
        {
            get
            {
                return SoftManager.GetSoft();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Config Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new Config();
                    _config.AppName = System.Configuration.ConfigurationManager.AppSettings["AppName"];
                }
                return _config;
            }
        } private Config _config;
    }
}
