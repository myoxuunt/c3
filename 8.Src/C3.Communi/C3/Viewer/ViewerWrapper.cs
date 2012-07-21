using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    //public class ViewerWrapper
    //{
    //    #region Constructor
    //    public ViewerWrapper()
    //    {
    //    }
    //    #endregion //Constructor

    //    #region UCViewerWrapper
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public UCViewerWrapper UCViewerWrapper
    //    {
    //        get
    //        {
    //            if (_ucViewerWrapper == null)
    //            {
    //                _ucViewerWrapper = new UCViewerWrapper();
    //                _ucViewerWrapper.Dock = DockStyle.Fill;
    //            }
    //            return _ucViewerWrapper;
    //        }
    //        set
    //        {
    //            _ucViewerWrapper = value;
    //        }
    //    } private UCViewerWrapper _ucViewerWrapper;
    //    #endregion //UCViewerWrapper

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public ControllerManager ControllerManager
    //    {
    //        get
    //        {
    //            if (_controllerManager == null)
    //            {
    //                //_viewerManager = new ControllerManager(this.UCViewerWrapper.ViewContainer);
    //            }
    //            return _controllerManager;
    //        }
    //        set
    //        {
    //            _controllerManager = value;
    //        }
    //    } private ControllerManager _controllerManager;

    //    #region View
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="station"></param>
    //    public void View(IStation station)
    //    {
    //        if (station == null)
    //        {
    //            throw new ArgumentNullException("station");
    //        }
    //        // 
    //        //
    //        this.UCViewerWrapper.Title = station.Name;

    //        this.ControllerManager.Act(station);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="device"></param>
    //    public void View(IDevice device)
    //    {
    //        if (device == null)
    //        {
    //            throw new ArgumentNullException("device");
    //        }

    //        this.UCViewerWrapper.Title = device.GetType().Name;

    //        this.ControllerManager.View(device);
    //    }
    //    #endregion //View

    //}

}
