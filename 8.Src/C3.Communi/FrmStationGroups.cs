
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class FrmStationGroups : FrmGroups
    {
        #region StationType
        /// <summary>
        /// 
        /// </summary>
        public StationType StationType
        {
            get
            {
                return _stationType;
            }
            set
            {
                _stationType = value;
            }
        } private StationType _stationType;
        #endregion //StationType

        #region Stations
        /// <summary>
        /// 
        /// </summary>
        public StationCollection Stations
        {
            get
            {
                return _stations;
            }
            set
            {
                _stations = value;
            }
        } private StationCollection _stations;
        #endregion //Stations

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

        /// <summary>
        /// 
        /// </summary>
        protected override void Fill()
        {
            this.Text = string.Format("{0} - {1}",
                    ADEStatusText.GetText(this.AdeStatus),
                    this.StationType.Text);

            base.Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool Verify()
        {
            bool exist = this.Stations.ExistName(this.Station.Name, this.Station);
            if (exist)
            {
                NUnit.UiKit.UserMessage.DisplayFailure("Exist name");
            }
            return !exist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmStationGroups_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmStationGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(394, 475);
            this.Name = "FrmStationGroups";
            this.Load += new System.EventHandler(this.FrmStationGroups_Load_1);
            this.ResumeLayout(false);

        }

        private void FrmStationGroups_Load_1(object sender, EventArgs e)
        {

        }
    }

}
