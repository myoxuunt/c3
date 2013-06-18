﻿using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class AppBase
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        private bool _enabledNotifyIcon = false;

        /// <summary>
        /// 
        /// </summary>
        private bool _isSuredToQuit = false;
        #endregion //Members

        #region App
        /// <summary>
        /// 
        /// </summary>
        protected AppBase()
            : this(false)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected AppBase(bool enabledNotifyIcon)
        {
            this._enabledNotifyIcon = enabledNotifyIcon;

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            // set default values
            //
            this.IsProcessUnhandleException = true;
            this.IsSingleApplication = true;

            // events handle
            //
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        #endregion //App

        #region Application_ApplicationExit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_ApplicationExit(object sender, EventArgs e)
        {
            OnApplicationExit();
        }
        #endregion //Application_ApplicationExit

        #region OnApplicationExit
        /// <summary>
        /// 
        /// </summary>
        virtual protected void OnApplicationExit()
        {
        }
        #endregion //OnApplicationExit

        #region MainForm
        /// <summary>
        /// 
        /// </summary>
        abstract public Form MainForm
        {
            get;
            //get { return _mainForm; }
            //set
            //{
            //    _mainForm = value;
            //}
        }//  private Form _mainForm;
        #endregion //MainForm

        #region IsProcessUnhandleException
        /// <summary>
        /// 
        /// </summary>
        public bool IsProcessUnhandleException
        {
            get
            {
                return _isProcessUnhandleException;
            }
            set
            {
                if( _isProcessUnhandleException != value )
                {
                    _isProcessUnhandleException = value;
                    if (_isProcessUnhandleException == true)
                    {
                        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                    }
                    else
                    {
                        Application.ThreadException -= new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                        AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                    }
                }
            }
        } private bool _isProcessUnhandleException;
        #endregion //

        #region CurrentDomain_UnhandledException
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            ProcessUnhandleException(ex);
        }
        #endregion //CurrentDomain_UnhandledException

        #region Application_ThreadException
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            ProcessUnhandleException(ex);
        }
        #endregion //Application_ThreadException

        #region ProcessUnhandleException
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        virtual protected void ProcessUnhandleException(Exception ex)
        {
            // message exception
            //
            NUnit.UiKit.UserMessage.DisplayFailure(ex.Message);

            // log exception
            //
            ExceptionLogger.Save(ex);

            // exit app
            //
            this.Exit(0xFF);
        }
        #endregion //ProcessUnhandleException

        #region IsSingleApplication
        /// <summary>
        /// 
        /// </summary>
        public bool IsSingleApplication
        {
            get
            {
                return _isSingleApplication;
            }
            set
            {
                _isSingleApplication = value;
            }
        } private bool _isSingleApplication;
        #endregion //

        #region Run
        /// <summary>
        /// 
        /// </summary>
        virtual public void Run()
        {
            if (IsSingleApplication && Xdgk.Common.Diagnostics.HasPreInstance())
            {
                ShowRunningMessage();
                return;
            }
            else
            {
                if (NotifyIconManager.EnabledNotifyIcon)
                {
                    NotifyIconManager.Start();
                    this.MainForm.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
                }

                Application.Run(MainForm);
            }
        }
        #endregion //Run

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isSuredToQuit)
            {
                e.Cancel = true;
                this.MainForm.Hide();
            }
        }

        #region ShowRunningMessage
        /// <summary>
        /// 
        /// </summary>
        virtual protected void ShowRunningMessage()
        {
            NUnit.UiKit.UserMessage.DisplayInfo(this.RunningMessage);
        }
        #endregion //ShowRunningMessage

        #region RunningMessage

        /// <summary>
        /// 
        /// </summary>
        public string RunningMessage
        {
            get
            {
                if (_runningMessage == null)
                {
                    _runningMessage = string.Empty;
                }
                return _runningMessage;
            }
            set { _runningMessage = value; }
        } private string _runningMessage = "Running...";
        #endregion //RunningMessage

        // TODO: logger
        // 
        // TODO: Config
        //


        #region Default
        /// <summary>
        /// 
        /// </summary>
        static protected AppBase DefaultInstance
        {
            get 
            {
                return _defaultInstance;
            }
            set
            {
                _defaultInstance = value;
            }
        } static private AppBase _defaultInstance;
        #endregion //Default

        #region IsDisposeForm
        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposeForm
        {
            get { return _isDisposeForm; }
            set { _isDisposeForm = value; }
        } private bool _isDisposeForm = true;
        #endregion //IsDisposeForm

        #region Exit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exitCode"></param>
        virtual public void Exit(int exitCode)
        {
            this._isSuredToQuit = true;

            //OnApplicationExit();
            DisposeMainFormOrNot();
            NotifyIconManager.Stop();
            //Environment.Exit(exitCode);
            this.MainForm.Close();
        }
        #endregion //Exit

        #region DisposeMainFormOrNot
        /// <summary>
        /// 
        /// </summary>
        private void DisposeMainFormOrNot()
        {
            if (IsDisposeForm)
            {
                if (MainForm != null)
                {
                    this.MainForm.Dispose();
                }
            }
        }
        #endregion //DisposeMainFormOrNot

        #region NotifyIconManager
        /// <summary>
        /// 
        /// </summary>
        public NotifyIconManager NotifyIconManager
        {
            get
            {
                if (_notifyIconManager == null)
                {
                    _notifyIconManager = new NotifyIconManager(_enabledNotifyIcon);
                    _notifyIconManager.NotifyIconDoubleClick += new EventHandler(_notifyIconManager_NotifyIconDoubleClick);
                    ContextMenu contextMenu = _notifyIconManager.GetContextMenu();

                    MenuItem displayMi = new MenuItem("显示(&D)", OnDisplayMenuItemClick);
                    contextMenu.MenuItems.Add(displayMi);

                    MenuItem spMi = new MenuItem("-");
                    contextMenu.MenuItems.Add(spMi);

                    MenuItem exitMi = new MenuItem("退出(&X)", OnExitMenuItemClick);
                    contextMenu.MenuItems.Add(exitMi);
                }
                return _notifyIconManager;
            }
            set
            {
                _notifyIconManager = value;
            }
        }private NotifyIconManager _notifyIconManager;
        #endregion //NotifyIconManager

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _notifyIconManager_NotifyIconDoubleClick(object sender, EventArgs e)
        {
            ShowAndActivateMainForm();
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisplayMenuItemClick(object sender, EventArgs e)
        {
            ShowAndActivateMainForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowAndActivateMainForm()
        {
            this.MainForm.Show();
            this.MainForm.Activate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExitMenuItemClick(object sender, EventArgs e)
        {
            //this.Exit(0);
            //this._isSuredToQuit = true;
            //this.MainForm.Close();
            this.Exit(0);
        }

    }
}
