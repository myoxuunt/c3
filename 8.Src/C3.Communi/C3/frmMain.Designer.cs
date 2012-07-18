namespace C3
{
    partial class frmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddinManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaskManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuM = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatusbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStationAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStationEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStationDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeviceAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeviceEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeviceDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCommuniDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaskView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssListenPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.sc1 = new System.Windows.Forms.SplitContainer();
            this.sc2 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.sc1.Panel2.SuspendLayout();
            this.sc1.SuspendLayout();
            this.sc2.Panel1.SuspendLayout();
            this.sc2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuStation,
            this.mnuDevice,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(777, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.mnuExit,
            this.mnuAddinManager,
            this.mnuTaskManage,
            this.mnuM});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(57, 20);
            this.mnuFile.Text = "文件(&F)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(136, 22);
            this.mnuExit.Text = "退出";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuAddinManager
            // 
            this.mnuAddinManager.Name = "mnuAddinManager";
            this.mnuAddinManager.Size = new System.Drawing.Size(136, 22);
            this.mnuAddinManager.Text = "插件管理(P)";
            // 
            // mnuTaskManage
            // 
            this.mnuTaskManage.Name = "mnuTaskManage";
            this.mnuTaskManage.Size = new System.Drawing.Size(136, 22);
            this.mnuTaskManage.Text = "任务管理(&T)";
            // 
            // mnuM
            // 
            this.mnuM.Name = "mnuM";
            this.mnuM.Size = new System.Drawing.Size(136, 22);
            this.mnuM.Text = "M";
            this.mnuM.Click += new System.EventHandler(this.mnuM_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolbar,
            this.mnuStatusbar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(57, 20);
            this.mnuView.Text = "视图(&V)";
            // 
            // mnuToolbar
            // 
            this.mnuToolbar.Name = "mnuToolbar";
            this.mnuToolbar.Size = new System.Drawing.Size(124, 22);
            this.mnuToolbar.Text = "工具栏(&T)";
            // 
            // mnuStatusbar
            // 
            this.mnuStatusbar.Name = "mnuStatusbar";
            this.mnuStatusbar.Size = new System.Drawing.Size(124, 22);
            this.mnuStatusbar.Text = "状态栏(&S)";
            // 
            // mnuStation
            // 
            this.mnuStation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStationAdd,
            this.mnuStationEdit,
            this.mnuStationDelete});
            this.mnuStation.Name = "mnuStation";
            this.mnuStation.Size = new System.Drawing.Size(57, 20);
            this.mnuStation.Text = "站点(&S)";
            // 
            // mnuStationAdd
            // 
            this.mnuStationAdd.Name = "mnuStationAdd";
            this.mnuStationAdd.Size = new System.Drawing.Size(113, 22);
            this.mnuStationAdd.Text = "添加(&A)";
            // 
            // mnuStationEdit
            // 
            this.mnuStationEdit.Name = "mnuStationEdit";
            this.mnuStationEdit.Size = new System.Drawing.Size(113, 22);
            this.mnuStationEdit.Text = "修改(&E)";
            // 
            // mnuStationDelete
            // 
            this.mnuStationDelete.Name = "mnuStationDelete";
            this.mnuStationDelete.Size = new System.Drawing.Size(113, 22);
            this.mnuStationDelete.Text = "删除(&D)";
            // 
            // mnuDevice
            // 
            this.mnuDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeviceAdd,
            this.mnuDeviceEdit,
            this.mnuDeviceDelete,
            this.mnuCommuniDetail,
            this.mnuTaskView});
            this.mnuDevice.Name = "mnuDevice";
            this.mnuDevice.Size = new System.Drawing.Size(58, 20);
            this.mnuDevice.Text = "设备(&D)";
            // 
            // mnuDeviceAdd
            // 
            this.mnuDeviceAdd.Name = "mnuDeviceAdd";
            this.mnuDeviceAdd.Size = new System.Drawing.Size(164, 22);
            this.mnuDeviceAdd.Text = "添加(&A)";
            // 
            // mnuDeviceEdit
            // 
            this.mnuDeviceEdit.Name = "mnuDeviceEdit";
            this.mnuDeviceEdit.Size = new System.Drawing.Size(164, 22);
            this.mnuDeviceEdit.Text = "修改(&E)";
            // 
            // mnuDeviceDelete
            // 
            this.mnuDeviceDelete.Name = "mnuDeviceDelete";
            this.mnuDeviceDelete.Size = new System.Drawing.Size(164, 22);
            this.mnuDeviceDelete.Text = "删除(&D)";
            // 
            // mnuCommuniDetail
            // 
            this.mnuCommuniDetail.Name = "mnuCommuniDetail";
            this.mnuCommuniDetail.Size = new System.Drawing.Size(164, 22);
            this.mnuCommuniDetail.Text = "mnuCommuniDetail";
            this.mnuCommuniDetail.Click += new System.EventHandler(this.mnuCommuniDetail_Click);
            // 
            // mnuTaskView
            // 
            this.mnuTaskView.Name = "mnuTaskView";
            this.mnuTaskView.Size = new System.Drawing.Size(164, 22);
            this.mnuTaskView.Text = "TaskView";
            this.mnuTaskView.Click += new System.EventHandler(this.mnuTaskView_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(58, 20);
            this.mnuHelp.Text = "帮助(&H)";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(113, 22);
            this.mnuAbout.Text = "关于(&A)";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssListenPort});
            this.statusStrip1.Location = new System.Drawing.Point(0, 512);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(777, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssListenPort
            // 
            this.tssListenPort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssListenPort.Name = "tssListenPort";
            this.tssListenPort.Size = new System.Drawing.Size(109, 17);
            this.tssListenPort.Text = "toolStripStatusLabel1";
            // 
            // sc1
            // 
            this.sc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc1.Location = new System.Drawing.Point(0, 49);
            this.sc1.Name = "sc1";
            // 
            // sc1.Panel2
            // 
            this.sc1.Panel2.Controls.Add(this.sc2);
            this.sc1.Size = new System.Drawing.Size(777, 463);
            this.sc1.SplitterDistance = 259;
            this.sc1.TabIndex = 9;
            // 
            // sc2
            // 
            this.sc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sc2.Location = new System.Drawing.Point(0, 0);
            this.sc2.Name = "sc2";
            this.sc2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc2.Panel1
            // 
            this.sc2.Panel1.Controls.Add(this.richTextBox1);
            this.sc2.Size = new System.Drawing.Size(514, 463);
            this.sc2.SplitterDistance = 234;
            this.sc2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(777, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(514, 234);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 534);
            this.Controls.Add(this.sc1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.sc1.Panel2.ResumeLayout(false);
            this.sc1.ResumeLayout(false);
            this.sc2.Panel1.ResumeLayout(false);
            this.sc2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuAddinManager;
        private System.Windows.Forms.ToolStripMenuItem mnuTaskManage;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuToolbar;
        private System.Windows.Forms.ToolStripMenuItem mnuStatusbar;
        private System.Windows.Forms.ToolStripMenuItem mnuStation;
        private System.Windows.Forms.ToolStripMenuItem mnuStationAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuStationEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuStationDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuDevice;
        private System.Windows.Forms.ToolStripMenuItem mnuDeviceAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuDeviceEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDeviceDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssListenPort;
        private System.Windows.Forms.SplitContainer sc1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuCommuniDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuM;
        private System.Windows.Forms.ToolStripMenuItem mnuTaskView;
        private System.Windows.Forms.SplitContainer sc2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

