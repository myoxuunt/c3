using System.Windows.Forms;
using C3.Communi ;

namespace C3
{
    public partial class UCStationViewer : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UCStationViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public IStation Station
        {
            get { return _station; }
            set 
            {
                if (_station !=value)
                {
                    _station = value;
                    RefreshStation();
                }
            }
        }private IStation _station;

        /// <summary>
        /// 
        /// </summary>
        private void RefreshStation()
        {
        } 
    }
}
