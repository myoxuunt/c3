using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Xdgk.Common
{
    public class NotifyIconManager
    {
        #region NotifyIconManager
        /// <summary>
        /// 
        /// </summary>
        public NotifyIconManager()
            : this(false)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public NotifyIconManager(bool enabled)
        {
            this.EnabledNotifyIcon = enabled;
        }
        #endregion //NotifyIconManager

        #region EnabledNotifyIcon
        /// <summary>
        /// 
        /// </summary>
        public bool EnabledNotifyIcon
        {
            get
            {
                return _enabledNotifyIcon;
            }
            set
            {
                _enabledNotifyIcon = value;
            }
        } private bool _enabledNotifyIcon;
        #endregion //EnabledNotifyIcon

        #region NotifyIcon
        /// <summary>
        /// 
        /// </summary>
        public NotifyIcon NotifyIcon
        {
            get
            {
                if (_enabledNotifyIcon && _notifyIcon == null)
                {
                    _notifyIcon = new NotifyIcon();
                    _notifyIcon.Text = GetNotifyIconText();
                    _notifyIcon.DoubleClick += new EventHandler(_notifyIcon_DoubleClick);
                    _notifyIcon.ContextMenu = GetContextMenu();
                    _notifyIcon.Icon = GetIcon();
                }
                return _notifyIcon;
            }
        }private NotifyIcon _notifyIcon;
        #endregion //NotifyIcon


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetNotifyIconText()
        {
            string s = ConfigurationManager.AppSettings["AppName"];
            return s; 
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ContextMenu GetContextMenu()
        {
            if (_contextMenu == null)
            {
                _contextMenu = new ContextMenu();
                _contextMenu.Name = "NotifyIconContextMenu";
            }
            return _contextMenu;
        }private ContextMenu _contextMenu;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (NotifyIconDoubleClick != null)
            {
                NotifyIconDoubleClick(this, e);
            }
        }
        public event EventHandler NotifyIconDoubleClick;

        #region GetIcon
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private System.Drawing.Icon GetIcon()
        {
            Icon icon = null;

            string fileName = System.IO.Path.Combine(
                Application.StartupPath,
                this.IconPath);
            try
            {
                icon = new Icon(fileName);
            }
            catch (Exception ex)
            {
                // TODO:
                //
                Console.WriteLine(ex.Message);
            }

            if (icon == null)
            {
                icon = Resources.Resource.DefaultNotifyIcon;
            }
            return icon;
        }
        #endregion //GetIcon

        #region IconPath
        /// <summary>
        /// 
        /// </summary>
        public string IconPath
        {
            get
            {
                if (_iconPath == null)
                {
                    string iconPath = ConfigurationManager.AppSettings["NotifyIconPath"];
                    _iconPath = iconPath;
                    if (_iconPath == null)
                    {
                        _iconPath = string.Empty;
                    }
                }
                return _iconPath;
            }
            set
            {
                _iconPath = value;
            }
        } private string _iconPath;
        #endregion //IconPath

        #region Start
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (this._enabledNotifyIcon)
            {
                this.NotifyIcon.Visible = true;
            }
        }
        #endregion //Start

        #region Stop
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (this._enabledNotifyIcon)
            {
                this.NotifyIcon.Visible = false;
            }
        }
        #endregion //Stop
    }

}
