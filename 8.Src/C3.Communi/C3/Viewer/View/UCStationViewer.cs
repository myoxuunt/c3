using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C3.Communi ;

namespace C3
{
    public partial class UCStationViewer : UserControl
    {
        public UCStationViewer()
        {
            InitializeComponent();
        }

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

        private void RefreshStation()
        {
            this.richTextBox1.Text += Station.Name;
        } 
    }
}
