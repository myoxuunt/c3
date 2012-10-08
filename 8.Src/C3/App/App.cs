using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class C3App : Xdgk.Common.AppBase
    {

        #region C3App
        /// <summary>
        /// 
        /// </summary>
        private C3App()
        {
            this.IsProcessUnhandleException = true;
            //Soft.UISynchronizationContext = new System.Windows.Forms.WindowsFormsSynchronizationContext();

            object obj = this.Soft;
        }
        #endregion //C3App

        #region App
        /// <summary>
        /// 
        /// </summary>
        static public C3App App
        {
            get
            {
                if (Xdgk.Common.AppBase.DefaultInstance == null)
                {
                    C3App app = new C3App();
                    //app.MainForm = new FrmMain();

                    Xdgk.Common.AppBase.DefaultInstance = app;
                    app.Soft.Prepare();
                    app.Soft.Start();

                }
                return Xdgk.Common.AppBase.DefaultInstance as C3App;
            }
        }
        #endregion //App

        #region MainForm
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
        #endregion //MainForm

        #region Soft
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
        #endregion //Soft

        #region Config
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
        #endregion //Config
    }
}
