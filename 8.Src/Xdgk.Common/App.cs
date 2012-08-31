using System;
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

        #region App
        /// <summary>
        /// 
        /// </summary>
        protected AppBase()
        {
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
        } 
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
                Application.Run(MainForm);
            }
        }
        #endregion //Run

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
            OnApplicationExit();
            DisposeMainFormOrNot();
            Environment.Exit(exitCode);
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
    }
}
