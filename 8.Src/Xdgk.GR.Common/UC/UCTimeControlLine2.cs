using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Xdgk.GR.Common
{
    public partial class UCTimeControlLine2 : UserControl
    {
        public UCTimeControlLine2()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        public float GTBase2
        {
            get { return (float)this.numGTBase2.Value; }
            set
            {
                // check value range
                //
                if (value < 0)
                {
                    value = 0;
                }
                if (value > 100)
                {
                    value = 100;
                }

                this.numGTBase2.Value = (decimal)value;
                this.ucTimeControlLine1.GTBase2 = (int)this.numGTBase2.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyValuePair<int, int>[] TimeControlLine
        {
            get { return this.ucTimeControlLine1.TimeControlLine; }
            set 
            {
                this.ucTimeControlLine1.TimeControlLine = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numGTBase2_ValueChanged(object sender, EventArgs e)
        {
            this.GTBase2 = (float)this.numGTBase2.Value;
        }
    }

}
