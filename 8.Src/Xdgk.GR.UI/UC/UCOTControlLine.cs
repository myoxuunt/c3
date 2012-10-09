using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
namespace Xdgk.GR.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCOTControlLine : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UCOTControlLine()
        {
            InitializeComponent();
            this.SetZedControlStyle(this.zedTimeControlLine);
            this.dgvTimeControlLine.AutoGenerateColumns = false;
            this.dgvTimeControlLine.ReadOnly = true;
            AddDataGridViewColumns();
 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zedControl"></param>
        private void SetZedControlStyle(ZedGraphControl zedControl)
        {
            zedControl.GraphPane.Title.Text = XD100Strings.OTGT2ControlLine;
            // 
            zedControl.IsShowContextMenu = false;

            //
            zedControl.IsEnableHZoom = false;
            zedControl.IsEnableVZoom = false;
            zedControl.IsEnableZoom = false;

            //
            zedControl.IsShowPointValues = true;

            GraphPane gp = zedControl.GraphPane;
            SetZedStyle(gp);

        }

        /// <summary>
        /// 
        /// </summary>
        private void SetZedStyle(ZedGraph.GraphPane gp)
        {
            int fontSize = 12;
            gp.IsFontsScaled = false;
            gp.Legend.IsVisible = false;

            // X
            //
            gp.XAxis.Title.FontSpec.Size = fontSize;
            gp.XAxis.MinorGrid.IsVisible = false;
            gp.XAxis.Title.Text = XD100Strings.OT;
            gp.XAxis.Scale.MajorStep = 10;
            gp.XAxis.Scale.MinorStep = 5;
            gp.XAxis.Scale.Min = Xdgk.GR.Common.OTControlLineDefines.OTMin;
            gp.XAxis.Scale.Max = Xdgk.GR.Common.OTControlLineDefines.OTMax;
            gp.XAxis.Scale.Format = "G";
            SetMajorTicStyle(gp.XAxis.MajorTic);
            SetMinorTicStyle(gp.XAxis.MinorTic);

            // Y
            //
            gp.YAxis.Title.FontSpec.Size = fontSize;
            gp.YAxis.MajorGrid.IsVisible = false;
            gp.YAxis.Title.Text = XD100Strings.GT2;
            gp.YAxis.Scale.Min = Xdgk.GR.Common.OTControlLineDefines.GT2Min;
            gp.YAxis.Scale.Max = Xdgk.GR.Common.OTControlLineDefines.GT2Max;
            gp.YAxis.Scale.MajorStep = 10;
            gp.YAxis.Scale.MinorStep = 5;
            gp.YAxis.Scale.Format = "G";
            SetMajorTicStyle(gp.YAxis.MajorTic);
            SetMinorTicStyle(gp.YAxis.MinorTic);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="majorTic"></param>
        private void SetMajorTicStyle(ZedGraph.MajorTic majorTic)
        {
            majorTic.IsAllTics = false;
            majorTic.IsBetweenLabels = false;
            majorTic.IsCrossInside = false;
            majorTic.IsCrossOutside = false;
            majorTic.IsInside = true;
            majorTic.IsOpposite = false;
            majorTic.IsOutside = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMinorTicStyle(ZedGraph.MinorTic minorTic)
        {
            minorTic.IsAllTics = false;
            minorTic.IsCrossInside = false;
            minorTic.IsCrossOutside = false;
            minorTic.IsInside = true;
            minorTic.IsOpposite = false;
            minorTic.IsOutside = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyValuePair<int, int>[] OTControlLine
        {
            get { return _otControlLine; }
            set //{ _otControlLine = value; }
            {
                if (_otControlLine != value)
                {
                    if (value != null)
                    {
                        _otControlLine = value;
                        this.BindDataGridView();
                        this.BindZed();
                    }
                }
            }
        } private KeyValuePair<int, int>[] _otControlLine;

        /// <summary>
        /// 
        /// </summary>
        private void BindDataGridView()
        {
            this.dgvTimeControlLine.DataSource = this.OTControlLine;
        }



        /// <summary>
        /// 
        /// </summary>
        private void BindZed()
        {
            if (this.OTControlLine != null)
            {
                GraphPane myPane = this.zedTimeControlLine.GraphPane;
                myPane.CurveList.Clear();
                // Add a forward step type curve
                //
                double[] x = new double[this.OTControlLine.Length];
                double[] y = new double[this.OTControlLine.Length];

                int i = 0;
                for (; i < OTControlLine.Length; i++)
                {
                    x[i] = OTControlLine[i].Key;
                    y[i] = OTControlLine[i].Value;
                }
                //x[i] = TimeControlLine[0].Key + 24;
                //y[i] = TimeControlLine[i-1].Value;

                LineItem curve = myPane.AddCurve("ot-gt2 control line", x, y, Color.Green, SymbolType.Circle);
                curve.Symbol.Fill = new Fill(Color.White);
                curve.Symbol.Size = 5;
                curve.Line.StepType = StepType.NonStep;

                myPane.AxisChange();
                this.zedTimeControlLine.Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDataGridViewColumns()
        {
            DataGridViewColumn timeColumn = CreateDataGridViewColumn(
                "ot", XD100Strings.OTHeaderText, "Key", typeof(int), 90);

            DataGridViewColumn valueColumn = CreateDataGridViewColumn(
                "value", XD100Strings.GT2, "Value", typeof(int), 110);

            this.dgvTimeControlLine.Columns.Add(timeColumn);
            this.dgvTimeControlLine.Columns.Add(valueColumn);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private DataGridViewColumn CreateDataGridViewColumn(string name, string headerText, 
            string dataPropertyName, Type valueType, int width)
        {
            DataGridViewColumn c = new DataGridViewTextBoxColumn();
            c.Name = name;
            c.HeaderText = headerText;
            c.DataPropertyName = dataPropertyName;
            c.ValueType = valueType;
            c.Width = width;
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = this.dgvTimeControlLine.CurrentRow;
            if (currentRow == null)
            {
                NUnit.UiKit.UserMessage.DisplayInfo(XD100Strings.FirstSelectItem);
                return;
            }

            int index = currentRow.Index;
            if (currentRow.DataBoundItem != null)
            {
                KeyValuePair<int, int> bindKv = (KeyValuePair<int, int>)currentRow.DataBoundItem;
                int ot = Convert.ToInt32(currentRow.Cells[0].Value);
                int gt2 = Convert.ToInt32(currentRow.Cells[1].Value);

                int otPrevious = GetOTPrevious(index);
                int otNext = GetOTNext(index);

                //frmTimeValue f = new frmTimeValue(time, value, this.GTBase2);
                frmOTGT2Value f = new frmOTGT2Value(ot, gt2, otPrevious, otNext);
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    //currentRow.Cells[0].Value = f.Time;
                    //currentRow.Cells[1].Value = f.Adjust;

                    KeyValuePair<int, int> kv = new KeyValuePair<int, int>(f.OT, f.GT2);
                    this.OTControlLine[index] = kv;
                    this.dgvTimeControlLine.Refresh();
                    this.BindZed();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetOTPrevious(int index)
        {
            int n = index - 1;
            if (n >= 0)
            {
                return Convert.ToInt32(this.dgvTimeControlLine.Rows[n].Cells[0].Value);
            }
            else
            {
                return Xdgk.GR.Common.OTControlLineDefines.OTMin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetOTNext(int index)
        {
            int n = index + 1;
            if (n >= this.dgvTimeControlLine.Rows.Count)
            {
                return Xdgk.GR.Common.OTControlLineDefines.OTMax;
            }
            else
            {
                return Convert.ToInt32(this.dgvTimeControlLine.Rows[n].Cells[0].Value);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCOTControlLine_Load(object sender, EventArgs e)
        {

        }
    }
}
