namespace C3
{
    partial class UCDeviceViewer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvTask = new System.Windows.Forms.ListView();
            this.chOperaName = new System.Windows.Forms.ColumnHeader();
            this.chLastExecute = new System.Windows.Forms.ColumnHeader();
            this.chStrategy = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.cmnuTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRunTask = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaskDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvDeviceDataLast = new System.Windows.Forms.ListView();
            this.chNO = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.chUnit = new System.Windows.Forms.ColumnHeader();
            this.mnuCustomTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTask.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvTask
            // 
            this.lvTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chOperaName,
            this.chLastExecute,
            this.chStrategy,
            this.chStatus});
            this.lvTask.ContextMenuStrip = this.cmnuTask;
            this.lvTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTask.FullRowSelect = true;
            this.lvTask.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTask.Location = new System.Drawing.Point(0, 0);
            this.lvTask.Name = "lvTask";
            this.lvTask.Size = new System.Drawing.Size(387, 199);
            this.lvTask.TabIndex = 1;
            this.lvTask.UseCompatibleStateImageBehavior = false;
            this.lvTask.View = System.Windows.Forms.View.Details;
            // 
            // chOperaName
            // 
            this.chOperaName.Text = "OperaName";
            this.chOperaName.Width = 78;
            // 
            // chLastExecute
            // 
            this.chLastExecute.Text = "LastExecute";
            this.chLastExecute.Width = 96;
            // 
            // chStrategy
            // 
            this.chStrategy.Text = "Strategy";
            this.chStrategy.Width = 100;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // cmnuTask
            // 
            this.cmnuTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunTask,
            this.mnuTaskDetail,
            this.mnuCustomTask});
            this.cmnuTask.Name = "cmnuTask";
            this.cmnuTask.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmnuTask.Size = new System.Drawing.Size(153, 92);
            // 
            // mnuRunTask
            // 
            this.mnuRunTask.Name = "mnuRunTask";
            this.mnuRunTask.Size = new System.Drawing.Size(152, 22);
            this.mnuRunTask.Text = "执行(&I)";
            this.mnuRunTask.Click += new System.EventHandler(this.mnuRunTask_Click);
            // 
            // mnuTaskDetail
            // 
            this.mnuTaskDetail.Name = "mnuTaskDetail";
            this.mnuTaskDetail.Size = new System.Drawing.Size(152, 22);
            this.mnuTaskDetail.Text = "详细(&D)";
            this.mnuTaskDetail.Click += new System.EventHandler(this.mnuTaskDetail_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvDeviceDataLast);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvTask);
            this.splitContainer1.Size = new System.Drawing.Size(387, 376);
            this.splitContainer1.SplitterDistance = 173;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvDeviceDataLast
            // 
            this.lvDeviceDataLast.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNO,
            this.chName,
            this.chValue,
            this.chUnit});
            this.lvDeviceDataLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDeviceDataLast.FullRowSelect = true;
            this.lvDeviceDataLast.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDeviceDataLast.Location = new System.Drawing.Point(0, 0);
            this.lvDeviceDataLast.Name = "lvDeviceDataLast";
            this.lvDeviceDataLast.Size = new System.Drawing.Size(387, 173);
            this.lvDeviceDataLast.TabIndex = 1;
            this.lvDeviceDataLast.UseCompatibleStateImageBehavior = false;
            this.lvDeviceDataLast.View = System.Windows.Forms.View.Details;
            // 
            // chNO
            // 
            this.chNO.Text = "NO";
            // 
            // chName
            // 
            this.chName.Text = "Name";
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            // 
            // chUnit
            // 
            this.chUnit.Text = "Unit";
            // 
            // mnuCustomTask
            // 
            this.mnuCustomTask.Name = "mnuCustomTask";
            this.mnuCustomTask.Size = new System.Drawing.Size(152, 22);
            this.mnuCustomTask.Text = "CustomTask";
            this.mnuCustomTask.Click += new System.EventHandler(this.mnuCustomTask_Click);
            // 
            // UCDeviceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCDeviceViewer";
            this.Size = new System.Drawing.Size(387, 376);
            this.cmnuTask.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvTask;
        private System.Windows.Forms.ColumnHeader chOperaName;
        private System.Windows.Forms.ColumnHeader chLastExecute;
        private System.Windows.Forms.ColumnHeader chStrategy;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvDeviceDataLast;
        private System.Windows.Forms.ColumnHeader chNO;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chUnit;
        private System.Windows.Forms.ContextMenuStrip cmnuTask;
        private System.Windows.Forms.ToolStripMenuItem mnuRunTask;
        private System.Windows.Forms.ToolStripMenuItem mnuTaskDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomTask;
    }
}
