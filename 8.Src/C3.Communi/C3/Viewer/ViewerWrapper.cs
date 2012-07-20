
using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
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

}
