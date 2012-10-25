using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Xdgk.GR.Common
{
    public partial class UCValveOpenDegree : UserControl
    {
        public const int MIN = 0;
        public const int MAX = 100;

        /// <summary>
        /// 
        /// </summary>
        public UCValveOpenDegree()
        {
            InitializeComponent();
            //Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.numValveOpenDegree.Minimum = MIN;
            this.numValveOpenDegree.Maximum = MAX;
            this.ValveOpenDegree = MAX;
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue((int)0)]
        public int ValveOpenDegree
        {
            get { return (int)this.numValveOpenDegree.Value; }
            set 
            {
                if (value < MIN)
                {
                    value = MIN;
                }
                else if (value > MAX)
                {
                    value = MAX;
                }
                this.numValveOpenDegree.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCValveOpenDegree_Load(object sender, EventArgs e)
        {
        }
    }
}
