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
    public partial class UCTimeControlLine : UserControl
    {
        public UCTimeControlLine()
        {
            InitializeComponent();

            this.SetZedControlStyle(this.zedTimeControlLine);
            //this.SetZedStyle(this.zedTimeControlLine.GraphPane);
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
            zedControl.GraphPane.Title.Text = XD100Strings.TimeControlLine;
            // 
            zedControl.IsShowContextMenu = false;

            //
            zedControl.IsEnableHZoom = false;
            zedControl.IsEnableVZoom = false;
            zedControl.IsEnableZoom = false;

            //
            zedControl.IsShowPointValues = true;
            zedControl.GraphPane.Legend.IsVisible = false;

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

            // X
            //
            gp.XAxis.Title.FontSpec.Size = fontSize;
            gp.XAxis.MinorGrid.IsVisible = false;
            gp.XAxis.Title.Text = XD100Strings.Time;
            gp.XAxis.Scale.MajorStep = 2;
            gp.XAxis.Scale.MinorStep = 1;
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 24;
            gp.XAxis.Scale.Format = "G";
            SetMajorTicStyle(gp.XAxis.MajorTic);
            SetMinorTicStyle(gp.XAxis.MinorTic);

            // Y
            //
            gp.YAxis.Title.FontSpec.Size = fontSize;
            gp.YAxis.MajorGrid.IsVisible = false;
            gp.YAxis.Title.Text = XD100Strings.GT2;
            gp.YAxis.Scale.Min = 0;
            gp.YAxis.Scale.Max = 100;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCTimeControlLine_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 获取或设置二次供温基准
        /// </summary>
        public int GTBase2
        {
            get { return _gtBase2; }
            set 
            {
                _gtBase2 = value;
                this.BindZed();
            }
        } private int _gtBase2;

        /// <summary>
        /// 获取或设置时间温度控制曲线
        /// </summary>
        public KeyValuePair<int, int>[] TimeControlLine
        {
            get { return _timeControlLine; }
            set 
            {
                if (_timeControlLine != value)
                {
                    if (value != null)
                    {
                        _timeControlLine = value;
                        this.BindDataGridView();
                        this.BindZed();
                    }
                }
            }
        } private KeyValuePair<int, int>[] _timeControlLine;

        /// <summary>
        /// 
        /// </summary>
        private void BindDataGridView()
        {
            this.dgvTimeControlLine.DataSource = this.TimeControlLine;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDataGridViewColumns()
        {
            DataGridViewColumn timeColumn = CreateDataGridViewColumn(
                "time", XD100Strings.TimeHeaderText, "Key", typeof(int), 90);

            DataGridViewColumn valueColumn = CreateDataGridViewColumn(
                "value", XD100Strings.AdjustHeaderText, "Value", typeof(int), 110);

            this.dgvTimeControlLine.Columns.Add(timeColumn);
            this.dgvTimeControlLine.Columns.Add(valueColumn);
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindZed()
        {
            if (this.TimeControlLine != null)
            {
                GraphPane myPane = this.zedTimeControlLine.GraphPane;
                myPane.CurveList.Clear();
                // Add a forward step type curve
                //
                double[] x = new double[this.TimeControlLine.Length];
                double[] y = new double[this.TimeControlLine.Length];

                int i = 0;
                for (; i < TimeControlLine.Length; i++)
                {
                    x[i] = TimeControlLine[i].Key;
                    y[i] = TimeControlLine[i].Value + this.GTBase2;
                }
                //x[i] = TimeControlLine[0].Key + 24;
                //y[i] = TimeControlLine[i-1].Value;

                LineItem curve = myPane.AddCurve("Forward Step", x, y, Color.Green, SymbolType.Circle);
                curve.Symbol.Fill = new Fill(Color.White);
                curve.Symbol.Size = 5;
                curve.Line.StepType = StepType.ForwardStep;

                myPane.AxisChange();
                this.zedTimeControlLine.Invalidate();
            }
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
            //DataGridViewColumnCollection cc = this.dgvTimeControlLine.Columns;
            //Console.WriteLine(cc[0].Width);
            //Console.WriteLine(cc[1].Width);
            DataGridViewRow currentRow = this.dgvTimeControlLine.CurrentRow;
            if (currentRow == null)
            {
                NUnit.UiKit.UserMessage.DisplayInfo(XD100Strings.FirstSelectItem);
                return;
            }

            if (currentRow.DataBoundItem != null)
            {
                int time = Convert.ToInt32(currentRow.Cells[0].Value);
                int value = Convert.ToInt32(currentRow.Cells[1].Value);

                frmTimeValue f = new frmTimeValue(time, value, this.GTBase2);
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    //currentRow.Cells[0].Value = f.Time;
                    //currentRow.Cells[1].Value = f.Adjust;

                    KeyValuePair<int, int> kv = new KeyValuePair<int, int>(f.Time, f.Adjust);
                    int index = GetTimeIndex(f.Time);
                    this.TimeControlLine[index] = kv;
                    this.dgvTimeControlLine.Refresh();
                    this.BindZed();
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetTimeIndex(int time)
        {
            for (int i = 0; i < this.TimeControlLine.Length; i++)
            {
                KeyValuePair<int, int> kv = this.TimeControlLine[i];
                if (kv.Key == time)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
