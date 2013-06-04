using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{
    public class SApp : AppBase
    {

        /// <summary>
        /// 
        /// </summary>
        public override Form MainForm
        {
            get
            {
                if (_mainForm == null)
                {
                    _mainForm = new FrmMain();
                }
                return _mainForm;
            }
        } private FrmMain _mainForm;

        /// <summary>
        /// 
        /// </summary>
        static public SApp App
        {
            get
            {
                if (AppBase.DefaultInstance == null)
                {
                    AppBase.DefaultInstance = new SApp();
                }
                return AppBase.DefaultInstance as SApp;
            }
        }
    }

    public class Soft
    {
        public SocketListenerManager SocketListenerManager
        {
            get
            {
                if (_socketListenerManager == null)
                {
                    _socketListenerManager = new SocketListenerManager();
                    string path = PathUtils.SocketListenerConfigFileName;
                    XmlSocketListenBuilder builder = new XmlSocketListenBuilder(path);
                    builder.Build(_socketListenerManager);
                }
                return _socketListenerManager;
            }
        } private SocketListenerManager _socketListenerManager;


        #region ISoft ≥…‘±

        #region CommuniPortManager
        public CommuniPortManager CommuniPortManager
        {
            get
            {
                if (_communiPortManager == null)
                {
                    _communiPortManager = new CommuniPortManager(this);
                }
                return _communiPortManager;
            }
        } private CommuniPortManager _communiPortManager;
        #endregion //CommuniPortManager

        #region ErrorManager
        public ErrorManager ErrorManager
        {
            get
            {
                if (_errorManager == null)
                {
                    _errorManager = new ErrorManager(this);
                }
                return _errorManager;
            }
        } private ErrorManager _errorManager;
        #endregion //

        #endregion
    }
}
