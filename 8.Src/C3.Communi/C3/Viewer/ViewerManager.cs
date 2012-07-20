
using System;
using System.Windows.Forms;
using C3.Communi;


namespace C3
{
    public class ViewerManager
    {
        private Panel _panel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
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
            DeviceViewer v = Viewers.Find(typeof(DeviceViewer), _panel) as DeviceViewer;
            if (v == null)
            {
                throw new InvalidOperationException("not fine DeviceViewer");
            }

            v.Device = device;
            v.ShowMeOnly();
        }
    }

}
