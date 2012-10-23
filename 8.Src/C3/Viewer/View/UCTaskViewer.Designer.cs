namespace C3
{
    partial class UCTaskViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chOperaName = new System.Windows.Forms.ColumnHeader();
            this.chLastExecute = new System.Windows.Forms.ColumnHeader();
            this.chStrategy = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.cmsTaskItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRunTask = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaskDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTaskItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chOperaName,
            this.chLastExecute,
            this.chStrategy,
            this.chStatus});
            this.listView1.ContextMenuStrip = this.cmsTaskItem;
            this.listView1.Location = new System.Drawing.Point(61, 29);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(397, 126);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
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
            // cmsTaskItem
            // 
            this.cmsTaskItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunTask,
            this.mnuTaskDetail});
            this.cmsTaskItem.Name = "cmsTaskItem";
            this.cmsTaskItem.Size = new System.Drawing.Size(114, 48);
            this.cmsTaskItem.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTaskItem_Opening);
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
            // UCTaskViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "UCTaskViewer";
            this.Size = new System.Drawing.Size(512, 251);
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.cmsTaskItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chOperaName;
        private System.Windows.Forms.ColumnHeader chLastExecute;
        private System.Windows.Forms.ColumnHeader chStrategy;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ContextMenuStrip cmsTaskItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRunTask;
        private System.Windows.Forms.ToolStripMenuItem mnuTaskDetail;
    }
}