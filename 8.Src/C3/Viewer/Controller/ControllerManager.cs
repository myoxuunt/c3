using System;
using System.Diagnostics;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class ControllerManager
    {
        #region UCViewerWrapper
        /// <summary>
        /// 
        /// </summary>
        public UCViewerWrapper UcViewerWrapper
        {
            get
            {
                if (_ucViewerWrapper == null)
                {
                    _ucViewerWrapper = new UCViewerWrapper();
                    _ucViewerWrapper.Dock = DockStyle.Fill;
                }
                return _ucViewerWrapper;
            }
            set
            {
                _ucViewerWrapper = value;
            }
        } private UCViewerWrapper _ucViewerWrapper;
        #endregion //UCViewerWrapper

        #region Act
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Act(Model model)
        {
            Controller c = Controllers.Find(model.ControllerType);

            Debug.Assert(c != null);

            c.Model = model;
            this.UcViewerWrapper.Title = model.Title;
            Controllers.OnlyShow(c);

        }
        #endregion //Act

        #region Controllers
        /// <summary>
        /// 
        /// </summary>
        public ControllerCollection Controllers
        {
            get
            {
                if (_controllers == null)
                {
                    _controllers = new ControllerCollection();

                    StationView sv = new StationView(this.UcViewerWrapper.ViewContainer);
                    StationController sc = new StationController(sv);
                    _controllers.Add(sc);

                    _controllers.Add(
                        new DeviceController(
                            new DeviceView(this.UcViewerWrapper.ViewContainer)
                            ));
                }
                return _controllers;
            }
            set
            {
                _controllers = value;
            }
        } private ControllerCollection _controllers;
        #endregion //Controllers

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="station"></param>
        //internal void View(IStation station)
        //{
        //    //StationController v = Controllers.Find(typeof(StationController), _panel) as StationController;
        //    //if (v == null)
        //    //{
        //    //    throw new InvalidOperationException("not find StationViewer");
        //    //}
        //    //v.Station = station;
        //    ////v.ShowMeOnly();
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="device"></param>
        //internal void View(IDevice device)
        //{
        //    //DeviceController v = Controllers.Find(typeof(DeviceController), _panel) as DeviceController;
        //    //if (v == null)
        //    //{
        //    //    throw new InvalidOperationException("not fine DeviceViewer");
        //    //}

        //    //v.Device = device;
        //    ////v.ShowMeOnly();
        //}
    }
}
