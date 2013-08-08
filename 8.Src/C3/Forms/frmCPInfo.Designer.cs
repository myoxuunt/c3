namespace C3
{
    partial class frmCPInfo
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
            this.tbnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConnInfo = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chConnInfo = new System.Windows.Forms.ColumnHeader();
            this.chStations = new System.Windows.Forms.ColumnHeader();
            this.chCreateDT = new System.Windows.Forms.ColumnHeader();
            this.tpConnLog = new System.Windows.Forms.TabPage();
            this.txtCPCreateLog = new System.Windows.Forms.RichTextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpConnInfo.SuspendLayout();
            this.tpConnLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbnOK
            // 
            this.tbnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbnOK.Location = new System.Drawing.Point(507, 440);
            this.tbnOK.Name = "tbnOK";
            this.tbnOK.Size = new System.Drawing.Size(75, 23);
            this.tbnOK.TabIndex = 1;
            this.tbnOK.Text = "关闭";
            this.tbnOK.UseVisualStyleBackColor = true;
            this.tbnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpConnInfo);
            this.tabControl1.Controls.Add(this.tpConnLog);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(570, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // tpConnInfo
            // 
            this.tpConnInfo.Controls.Add(this.listView1);
            this.tpConnInfo.Location = new System.Drawing.Point(4, 21);
            this.tpConnInfo.Name = "tpConnInfo";
            this.tpConnInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnInfo.Size = new System.Drawing.Size(562, 397);
            this.tpConnInfo.TabIndex = 0;
            this.tpConnInfo.Text = "连接";
            this.tpConnInfo.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chConnInfo,
            this.chStations,
            this.chCreateDT});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(550, 385);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // chConnInfo
            // 
            this.chConnInfo.Text = "连接信息";
            this.chConnInfo.Width = 300;
            // 
            // chStations
            // 
            this.chStations.Text = "站点";
            this.chStations.Width = 120;
            // 
            // chCreateDT
            // 
            this.chCreateDT.Text = "连接时间";
            this.chCreateDT.Width = 120;
            // 
            // tpConnLog
            // 
            this.tpConnLog.Controls.Add(this.txtCPCreateLog);
            this.tpConnLog.Location = new System.Drawing.Point(4, 21);
            this.tpConnLog.Name = "tpConnLog";
            this.tpConnLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnLog.Size = new System.Drawing.Size(562, 397);
            this.tpConnLog.TabIndex = 1;
            this.tpConnLog.Text = "记录";
            this.tpConnLog.UseVisualStyleBackColor = true;
            // 
            // txtCPCreateLog
            // 
            this.txtCPCreateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCPCreateLog.Location = new System.Drawing.Point(6, 6);
            this.txtCPCreateLog.Name = "txtCPCreateLog";
            this.txtCPCreateLog.Size = new System.Drawing.Size(550, 385);
            this.txtCPCreateLog.TabIndex = 1;
            this.txtCPCreateLog.Text = "";
            this.txtCPCreateLog.WordWrap = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(12, 440);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmCPCreateLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 475);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tbnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCPCreateLog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "连接信息";
            this.Load += new System.EventHandler(this.frmCPCreateLog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpConnInfo.ResumeLayout(false);
            this.tpConnLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tbnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpConnInfo;
        private System.Windows.Forms.TabPage tpConnLog;
        private System.Windows.Forms.RichTextBox txtCPCreateLog;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chConnInfo;
        private System.Windows.Forms.ColumnHeader chStations;
        private System.Windows.Forms.ColumnHeader chCreateDT;
        private System.Windows.Forms.Button btnRefresh;
    }
}