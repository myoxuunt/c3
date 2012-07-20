using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewerWrapper
    {
        public ViewerWrapper(Label titleLabel, Panel container)
        {
            this.Title = titleLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        public Label Title
        {
            get { return _title; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Title");
                }
                _title = value;
            }
        } private Label _title;

        #region Container
        /// <summary>
        /// 
        /// </summary>
        public Panel Container
        {
            get
            {
                return _container;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Container");
                }
                _container = value;
            }
        } private Panel _container;
        #endregion //Container

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

        #region
        public void View(IStation station)
        {
            if (station == null)
            {
                throw new ArgumentNullException("station");
            }
            // 
            //
            this.Title.Text = station.Name;

            this.ViewerManager.View( station );
        }

        public void View(IDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            this.Title.Text = device.GetType().Name;

            this.ViewerManager.View(device);
        }
        #endregion //

    }

    /// <summary>
    /// 
    /// </summary>
    public class ViewerBase
    {
        protected Panel _panel;
        protected ViewerBase(Panel panel)
        {
            _panel = panel;
        }

        protected void AddUCViewerToPanel(Control ctrl)
        {
            this._panel.Controls.Add(ctrl);
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
                _station = value;
            }
        } private IStation _station;
        #endregion //Station

        public UCStationViewer UCStationViewer
        {
            get
            {
                if (_ucStationViewer == null)
                {
                    _ucStationViewer = new UCStationViewer();
                    this.AddUCViewerToPanel(_ucStationViewer);
                }
                return _ucStationViewer;
            }
        } private UCStationViewer _ucStationViewer;
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
                _device = value;
            }
        } private IDevice _device;
        #endregion //Device

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

        private ViewerBase CreateViewer(Type type,Panel panel)
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
        }
    }
}
