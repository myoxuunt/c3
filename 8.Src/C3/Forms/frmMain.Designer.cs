namespace C3
{
    partial class FrmMain
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
            this.mnuAddin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssListenPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.sc1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.sc1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuStation,
            this.mnuDevice,
            this.mnuSetting,
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
            this.mnuAddin,
            this.toolStripSeparator1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(57, 20);
            this.mnuFile.Text = "文件(&F)";
            // 
            // mnuAddin
            // 
            this.mnuAddin.Name = "mnuAddin";
            this.mnuAddin.Size = new System.Drawing.Size(125, 22);
            this.mnuAddin.Text = "插件(&A)...";
            this.mnuAddin.Click += new System.EventHandler(this.mnuM_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(125, 22);
            this.mnuExit.Text = "退出(&X)";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
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
            this.mnuToolbar.Click += new System.EventHandler(this.mnuToolbar_Click);
            // 
            // mnuStatusbar
            // 
            this.mnuStatusbar.Name = "mnuStatusbar";
            this.mnuStatusbar.Size = new System.Drawing.Size(124, 22);
            this.mnuStatusbar.Text = "状态栏(&S)";
            this.mnuStatusbar.Click += new System.EventHandler(this.mnuStatusbar_Click);
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
            this.mnuStation.DropDownOpening += new System.EventHandler(this.mnuStation_DropDownOpening);
            // 
            // mnuStationAdd
            // 
            this.mnuStationAdd.Name = "mnuStationAdd";
            this.mnuStationAdd.Size = new System.Drawing.Size(152, 22);
            this.mnuStationAdd.Text = "添加(&A)...";
            this.mnuStationAdd.Click += new System.EventHandler(this.mnuStationAdd_Click);
            // 
            // mnuStationEdit
            // 
            this.mnuStationEdit.Name = "mnuStationEdit";
            this.mnuStationEdit.Size = new System.Drawing.Size(152, 22);
            this.mnuStationEdit.Text = "修改(&E)...";
            this.mnuStationEdit.Click += new System.EventHandler(this.mnuStationEdit_Click);
            // 
            // mnuStationDelete
            // 
            this.mnuStationDelete.Name = "mnuStationDelete";
            this.mnuStationDelete.Size = new System.Drawing.Size(152, 22);
            this.mnuStationDelete.Text = "删除(&D)...";
            this.mnuStationDelete.Click += new System.EventHandler(this.mnuStationDelete_Click);
            // 
            // mnuDevice
            // 
            this.mnuDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeviceAdd,
            this.mnuDeviceEdit,
            this.mnuDeviceDelete,
            this.mnuCommuniDetail});
            this.mnuDevice.Name = "mnuDevice";
            this.mnuDevice.Size = new System.Drawing.Size(58, 20);
            this.mnuDevice.Text = "设备(&D)";
            this.mnuDevice.DropDownOpening += new System.EventHandler(this.mnuDevice_DropDownOpening);
            // 
            // mnuDeviceAdd
            // 
            this.mnuDeviceAdd.Name = "mnuDeviceAdd";
            this.mnuDeviceAdd.Size = new System.Drawing.Size(152, 22);
            this.mnuDeviceAdd.Text = "添加(&A)...";
            this.mnuDeviceAdd.Click += new System.EventHandler(this.mnuDeviceAdd_Click);
            // 
            // mnuDeviceEdit
            // 
            this.mnuDeviceEdit.Name = "mnuDeviceEdit";
            this.mnuDeviceEdit.Size = new System.Drawing.Size(152, 22);
            this.mnuDeviceEdit.Text = "修改(&E)...";
            this.mnuDeviceEdit.Click += new System.EventHandler(this.mnuDeviceEdit_Click);
            // 
            // mnuDeviceDelete
            // 
            this.mnuDeviceDelete.Name = "mnuDeviceDelete";
            this.mnuDeviceDelete.Size = new System.Drawing.Size(152, 22);
            this.mnuDeviceDelete.Text = "删除(&D)...";
            this.mnuDeviceDelete.Click += new System.EventHandler(this.mnuDeviceDelete_Click);
            // 
            // mnuCommuniDetail
            // 
            this.mnuCommuniDetail.Name = "mnuCommuniDetail";
            this.mnuCommuniDetail.Size = new System.Drawing.Size(152, 22);
            this.mnuCommuniDetail.Text = "通讯记录(&L)...";
            this.mnuCommuniDetail.Click += new System.EventHandler(this.mnuCommuniDetail_Click);
            // 
            // mnuSetting
            // 
            this.mnuSetting.Name = "mnuSetting";
            this.mnuSetting.Size = new System.Drawing.Size(57, 20);
            this.mnuSetting.Text = "设置(&S)";
            this.mnuSetting.DropDownOpening += new System.EventHandler(this.mnuSetting_DropDownOpening);
            this.mnuSetting.Click += new System.EventHandler(this.mnuSetting_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuTest});
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
            // mnuTest
            // 
            this.mnuTest.Name = "mnuTest";
            this.mnuTest.Size = new System.Drawing.Size(113, 22);
            this.mnuTest.Text = "Test";
            this.mnuTest.Visible = false;
            this.mnuTest.Click += new System.EventHandler(this.mnuTest_Click);
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
            this.tssListenPort.Size = new System.Drawing.Size(63, 17);
            this.tssListenPort.Text = "[ListenPort]";
            // 
            // sc1
            // 
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc1.Location = new System.Drawing.Point(0, 49);
            this.sc1.Name = "sc1";
            this.sc1.Size = new System.Drawing.Size(777, 463);
            this.sc1.SplitterDistance = 259;
            this.sc1.TabIndex = 9;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(777, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 534);
            this.Controls.Add(this.sc1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.sc1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
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
        private System.Windows.Forms.ToolStripMenuItem mnuAddin;
        private System.Windows.Forms.ToolStripMenuItem mnuTest;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting;
    }
}

