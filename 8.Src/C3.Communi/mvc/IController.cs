
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public interface IController
    {
        IModel Model { get; set; }
        IViewer Viewer { get; set; }

        void UpdateModel();
        void UpdateViewer();
        bool Verify();
    }

}
