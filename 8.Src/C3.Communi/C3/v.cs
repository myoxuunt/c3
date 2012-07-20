using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public interface IView
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ViewerWrapper
    {
        public ViewerWrapper()
        {
        }

        #region UCViewerWrapper
        /// <summary>
        /// 
        /// </summary>
        public UCViewerWrapper UCViewerWrapper
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

        #region ViewerManager
        /// <summary>
        /// 
        /// </summary>
        public ViewerManager ViewerManager
        {
            get
            {
                if (_viewerManager == null)
                {
                    _viewerManager = new ViewerManager(this.UCViewerWrapper.ViewContainer );
                }
                return _viewerManager;
            }
            set
            {
                _viewerManager = value;
            }
        } private ViewerManager _viewerManager;
        #endregion //ViewerManager

        #region View
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        public void View(IStation station)
        {
            if (station == null)
            {
                throw new ArgumentNullException("station");
            }
            // 
            //
            this.UCViewerWrapper.Title = station.Name;

            this.ViewerManager.View( station );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public void View(IDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            this.UCViewerWrapper.Title = device.GetType().Name;

            this.ViewerManager.View(device);
        }
        #endregion //View

    }

    /// <summary>
    /// 
    /// </summary>
    public class ViewerBase
    {
        #region View
        /// <summary>
        /// 
        /// </summary>
        public IView View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
            }
        } private IView _view;
        #endregion //View

        protected Control _ctrl;
        protected Panel _panel;
        protected ViewerBase(Panel panel)
        {
            _panel = panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        protected void AddUCViewerToPanel(Control ctrl)
        {
            Console.WriteLine("add ucview: " + ctrl.GetType ().Name );
            ctrl.Dock = DockStyle.Fill;
            this._panel.Controls.Add(ctrl);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowMeOnly()
        {
            foreach (Control item in this._panel.Controls)
            {
                item.Visible = false;
            }
            _ctrl.Visible = true;
        }
    }

    public class StationViewer : ViewerBase
    {
        public StationViewer(Panel panel)
            : base(panel)
        {
        }
        #region Station
        /// <summary>
        /// 
        /// </summary>
        public IStation Station
        {
            get
            {
                return _station;
            }
            set
            {
                if (_station != value)
                {
                    _station = value;
                    this.UCStationViewer.Station = _station;
                }
            }
        } private IStation _station;
        #endregion //Station

        public UCStationViewer UCStationViewer
        {
            get
            {
                if (_ctrl == null)
                {
                    _ctrl = new UCStationViewer();
                    this.AddUCViewerToPanel(_ctrl);
                }
                return _ctrl as UCStationViewer;
            }
        } 
        //private UCStationViewer _ucStationViewer;
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceViewer : ViewerBase
    {
        public DeviceViewer(Panel panel)
            : base(panel)

        {
        }

        #region Device
        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                if (_device != value)
                {
                    _device = value;
                    this.UCDeviceViewer.Device = _device;
                }
            }
        } private IDevice _device;
        #endregion //Device


        #region UCDeviceViewer
        /// <summary>
        /// 
        /// </summary>
        public UCDeviceViewer UCDeviceViewer
        {
            get
            {
                if (_ctrl == null)
                {
                    _ctrl = new UCDeviceViewer();
                    this.AddUCViewerToPanel(_ctrl);
                }
                return _ctrl as UCDeviceViewer;
            }
        }
        // private UCDeviceViewer _uCDeviceViewer;
        #endregion //UCDeviceViewer
        // properties
        //
        // 1. device
        // 2. device data displayer
        // 3. task viewer
    }

    //public class DeviceDataDisplayer
    //{
    //    // properties
    //    //
    //    // 1. IDeviceData 
    //    // 2. device

    //}


    /// <summary>
    /// 
    /// </summary>
    public class ViewerCollection : Xdgk.Common.Collection<ViewerBase>
    {
        public ViewerBase Find(Type type, Panel panel)
        {
            ViewerBase result = null;
            foreach (ViewerBase item in this)
            {
                if (item.GetType() == type)
                {
                    result = item;
                    break;
                }
            }
            if (result == null)
            {
                result = CreateViewer(type, panel);
                this.Add(result);
            }
            return result;
        }

        private ViewerBase CreateViewer(Type type, Panel panel)
        {
            ViewerBase v = null;
            if (type == typeof(StationViewer))
            {
                v = new StationViewer(panel);
            }
            else if (type == typeof(DeviceViewer))
            {
                v = new DeviceViewer(panel);
            }
            else
            {
                throw new ArgumentException(string.Format("cannot create view by '{0}'", type.Name));
            }
            return v;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ViewerManager
    {
        private Panel _panel;

        public ViewerManager(Panel panel)
        {
            _panel = panel;
        }

        #region Viewers
        /// <summary>
        /// 
        /// </summary>
        public ViewerCollection Viewers
        {
            get
            {
                if (_viewers == null)
                {
                    _viewers = new ViewerCollection();
                }
                return _viewers;
            }
            set
            {
                _viewers = value;
            }
        } private ViewerCollection _viewers;
        #endregion //Viewers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        internal void View(IStation station)
        {
            StationViewer v = Viewers.Find(typeof(StationViewer), _panel) as StationViewer;
            if (v == null)
            {
                throw new InvalidOperationException("not find StationViewer");
            }
            v.Station = station;
            v.ShowMeOnly();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        internal void View(IDevice device)
        {
            DeviceViewer v = Viewers.Find(typeof(DeviceViewer),_panel) as DeviceViewer;
            if (v == null)
            {
                throw new InvalidOperationException("not fine DeviceViewer");
            }

            v.Device = device;
            v.ShowMeOnly();
        }
    }
}
