namespace Xdgk.GR.Common
{
    partial class frmXD100ModbusTemperatureControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbValveType = new System.Windows.Forms.ComboBox();
            this.cmbControlMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStationName = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.ucotControlLine1 = new Xdgk.GR.Common.UCOTControlLine();
            this.ucTimeControlLine21 = new Xdgk.GR.Common.UCTimeControlLine2();
            this.ucValveOpenDegree1 = new Xdgk.GR.Common.UCValveOpenDegree();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(672, 11);
            this.okButton.TabIndex = 16;
            this.okButton.Visible = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(672, 38);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "控制方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "阀门类型：";
            // 
            // cmbValveType
            // 
            this.cmbValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValveType.FormattingEnabled = true;
            this.cmbValveType.Location = new System.Drawing.Point(87, 67);
            this.cmbValveType.Name = "cmbValveType";
            this.cmbValveType.Size = new System.Drawing.Size(137, 20);
            this.cmbValveType.TabIndex = 5;
            // 
            // cmbControlMode
            // 
            this.cmbControlMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControlMode.FormattingEnabled = true;
            this.cmbControlMode.Location = new System.Drawing.Point(87, 42);
            this.cmbControlMode.Name = "cmbControlMode";
            this.cmbControlMode.Size = new System.Drawing.Size(137, 20);
            this.cmbControlMode.TabIndex = 3;
            this.cmbControlMode.SelectedIndexChanged += new System.EventHandler(this.cmbControlMode_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "站名：";
            // 
            // txtStationName
            // 
            this.txtStationName.BackColor = System.Drawing.Color.White;
            this.txtStationName.Location = new System.Drawing.Point(87, 15);
            this.txtStationName.Name = "txtStationName";
            this.txtStationName.ReadOnly = true;
            this.txtStationName.Size = new System.Drawing.Size(137, 21);
            this.txtStationName.TabIndex = 1;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(750, 13);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 14;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(750, 40);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 15;
            this.btnWrite.Text = "设置\r\n";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.AllowDrop = true;
            this.statusBar1.Location = new System.Drawing.Point(0, 504);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(837, 22);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 13;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 837;
            // 
            // ucotControlLine1
            // 
            this.ucotControlLine1.Location = new System.Drawing.Point(317, 42);
            this.ucotControlLine1.Name = "ucotControlLine1";
            this.ucotControlLine1.OTControlLine = null;
            this.ucotControlLine1.Size = new System.Drawing.Size(305, 77);
            this.ucotControlLine1.TabIndex = 19;
            // 
            // ucTimeControlLine21
            // 
            this.ucTimeControlLine21.GTBase2 = 0F;
            this.ucTimeControlLine21.Location = new System.Drawing.Point(11, 120);
            this.ucTimeControlLine21.Name = "ucTimeControlLine21";
            this.ucTimeControlLine21.Size = new System.Drawing.Size(814, 378);
            this.ucTimeControlLine21.TabIndex = 18;
            this.ucTimeControlLine21.TimeControlLine = null;
            // 
            // ucValveOpenDegree1
            // 
            this.ucValveOpenDegree1.Location = new System.Drawing.Point(317, 4);
            this.ucValveOpenDegree1.Name = "ucValveOpenDegree1";
            this.ucValveOpenDegree1.Size = new System.Drawing.Size(258, 32);
            this.ucValveOpenDegree1.TabIndex = 20;
            // 
            // frmXD100ModbusTemperatureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 526);
            this.Controls.Add(this.ucValveOpenDegree1);
            this.Controls.Add(this.ucotControlLine1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtStationName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbControlMode);
            this.Controls.Add(this.cmbValveType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucTimeControlLine21);
            this.Name = "frmXD100ModbusTemperatureControl";
            this.Text = "Modbus供温设置";
            this.Load += new System.EventHandler(this.frmXD100TemperatureControl_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmXD100TemperatureControl_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmXD100TemperatureControl_FormClosing);
            this.Controls.SetChildIndex(this.ucTimeControlLine21, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbValveType, 0);
            this.Controls.SetChildIndex(this.cmbControlMode, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtStationName, 0);
            this.Controls.SetChildIndex(this.btnRead, 0);
            this.Controls.SetChildIndex(this.btnWrite, 0);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.Controls.SetChildIndex(this.ucotControlLine1, 0);
            this.Controls.SetChildIndex(this.ucValveOpenDegree1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbValveType;
        private System.Windows.Forms.ComboBox cmbControlMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStationName;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private Xdgk.GR.Common.UCTimeControlLine2 ucTimeControlLine21;
        private Xdgk.GR.Common.UCOTControlLine ucotControlLine1;
        private Xdgk.GR.Common.UCValveOpenDegree ucValveOpenDegree1;
    }
}
