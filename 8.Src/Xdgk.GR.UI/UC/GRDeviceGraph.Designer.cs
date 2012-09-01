namespace CZGR
{
    partial class GRDeviceGraph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GRDeviceGraph));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPressCurve = new System.Windows.Forms.Button();
            this.btnTempCurve = new System.Windows.Forms.Button();
            this.btnGRAlarmData = new System.Windows.Forms.Button();
            this.btnGRData = new System.Windows.Forms.Button();
            this.btnReadRealData = new System.Windows.Forms.Button();
            this.lblMark = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblStationName = new System.Windows.Forms.Label();
            this.lblRMValue = new System.Windows.Forms.Label();
            this.lblRMText = new System.Windows.Forms.Label();
            this.lblCMValue = new System.Windows.Forms.Label();
            this.lblCMText = new System.Windows.Forms.Label();
            this.lblWLValue = new System.Windows.Forms.Label();
            this.lblWLText = new System.Windows.Forms.Label();
            this.lblBP2Value = new System.Windows.Forms.Label();
            this.lblBP2Text = new System.Windows.Forms.Label();
            this.lblBT2Value = new System.Windows.Forms.Label();
            this.lblBT2Text = new System.Windows.Forms.Label();
            this.lblGP2Value = new System.Windows.Forms.Label();
            this.lblGP2Text = new System.Windows.Forms.Label();
            this.lblGT2Value = new System.Windows.Forms.Label();
            this.lblGT2Text = new System.Windows.Forms.Label();
            this.lblBP1Value = new System.Windows.Forms.Label();
            this.lblBP1Text = new System.Windows.Forms.Label();
            this.lblBT1Value = new System.Windows.Forms.Label();
            this.lblBT1Text = new System.Windows.Forms.Label();
            this.lblGP1Value = new System.Windows.Forms.Label();
            this.lblGP1Text = new System.Windows.Forms.Label();
            this.lblGT1Value = new System.Windows.Forms.Label();
            this.lblGT1Text = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.btnPressCurve);
            this.panel1.Controls.Add(this.btnTempCurve);
            this.panel1.Controls.Add(this.btnGRAlarmData);
            this.panel1.Controls.Add(this.btnGRData);
            this.panel1.Controls.Add(this.btnReadRealData);
            this.panel1.Controls.Add(this.lblMark);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.lblStationName);
            this.panel1.Controls.Add(this.lblRMValue);
            this.panel1.Controls.Add(this.lblRMText);
            this.panel1.Controls.Add(this.lblCMValue);
            this.panel1.Controls.Add(this.lblCMText);
            this.panel1.Controls.Add(this.lblWLValue);
            this.panel1.Controls.Add(this.lblWLText);
            this.panel1.Controls.Add(this.lblBP2Value);
            this.panel1.Controls.Add(this.lblBP2Text);
            this.panel1.Controls.Add(this.lblBT2Value);
            this.panel1.Controls.Add(this.lblBT2Text);
            this.panel1.Controls.Add(this.lblGP2Value);
            this.panel1.Controls.Add(this.lblGP2Text);
            this.panel1.Controls.Add(this.lblGT2Value);
            this.panel1.Controls.Add(this.lblGT2Text);
            this.panel1.Controls.Add(this.lblBP1Value);
            this.panel1.Controls.Add(this.lblBP1Text);
            this.panel1.Controls.Add(this.lblBT1Value);
            this.panel1.Controls.Add(this.lblBT1Text);
            this.panel1.Controls.Add(this.lblGP1Value);
            this.panel1.Controls.Add(this.lblGP1Text);
            this.panel1.Controls.Add(this.lblGT1Value);
            this.panel1.Controls.Add(this.lblGT1Text);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1600, 800);
            this.panel1.TabIndex = 0;
            // 
            // btnPressCurve
            // 
            this.btnPressCurve.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnPressCurve.Location = new System.Drawing.Point(233, 630);
            this.btnPressCurve.Name = "btnPressCurve";
            this.btnPressCurve.Size = new System.Drawing.Size(93, 23);
            this.btnPressCurve.TabIndex = 59;
            this.btnPressCurve.Text = "压力曲线(&P)";
            this.btnPressCurve.UseVisualStyleBackColor = false;
            this.btnPressCurve.Click += new System.EventHandler(this.btnPressCurve_Click);
            // 
            // btnTempCurve
            // 
            this.btnTempCurve.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnTempCurve.Location = new System.Drawing.Point(233, 601);
            this.btnTempCurve.Name = "btnTempCurve";
            this.btnTempCurve.Size = new System.Drawing.Size(93, 23);
            this.btnTempCurve.TabIndex = 58;
            this.btnTempCurve.Text = "温度曲线(&T)";
            this.btnTempCurve.UseVisualStyleBackColor = false;
            this.btnTempCurve.Click += new System.EventHandler(this.btnTempCurve_Click);
            // 
            // btnGRAlarmData
            // 
            this.btnGRAlarmData.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnGRAlarmData.Location = new System.Drawing.Point(134, 630);
            this.btnGRAlarmData.Name = "btnGRAlarmData";
            this.btnGRAlarmData.Size = new System.Drawing.Size(93, 23);
            this.btnGRAlarmData.TabIndex = 57;
            this.btnGRAlarmData.Text = "报警数据(&P)";
            this.btnGRAlarmData.UseVisualStyleBackColor = false;
            this.btnGRAlarmData.Click += new System.EventHandler(this.btnGRAlarmData_Click);
            // 
            // btnGRData
            // 
            this.btnGRData.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnGRData.Location = new System.Drawing.Point(134, 601);
            this.btnGRData.Name = "btnGRData";
            this.btnGRData.Size = new System.Drawing.Size(93, 23);
            this.btnGRData.TabIndex = 56;
            this.btnGRData.Text = "供热数据(&G)";
            this.btnGRData.UseVisualStyleBackColor = false;
            this.btnGRData.Click += new System.EventHandler(this.btnGRData_Click);
            // 
            // btnReadRealData
            // 
            this.btnReadRealData.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnReadRealData.Location = new System.Drawing.Point(35, 601);
            this.btnReadRealData.Name = "btnReadRealData";
            this.btnReadRealData.Size = new System.Drawing.Size(93, 23);
            this.btnReadRealData.TabIndex = 55;
            this.btnReadRealData.Text = "读取数据(&R)";
            this.btnReadRealData.UseVisualStyleBackColor = false;
            this.btnReadRealData.Click += new System.EventHandler(this.btnReadRealData_Click);
            // 
            // lblMark
            // 
            this.lblMark.AutoSize = true;
            this.lblMark.BackColor = System.Drawing.Color.Transparent;
            this.lblMark.Location = new System.Drawing.Point(1547, 777);
            this.lblMark.Name = "lblMark";
            this.lblMark.Size = new System.Drawing.Size(29, 12);
            this.lblMark.TabIndex = 54;
            this.lblMark.Text = "Mark";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTime.Location = new System.Drawing.Point(859, 43);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(17, 12);
            this.lblDateTime.TabIndex = 53;
            this.lblDateTime.Text = "dt";
            // 
            // lblStationName
            // 
            this.lblStationName.AutoSize = true;
            this.lblStationName.BackColor = System.Drawing.Color.Transparent;
            this.lblStationName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStationName.Location = new System.Drawing.Point(857, 11);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(130, 19);
            this.lblStationName.TabIndex = 52;
            this.lblStationName.Text = "stationname";
            // 
            // lblRMValue
            // 
            this.lblRMValue.AutoSize = true;
            this.lblRMValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblRMValue.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRMValue.ForeColor = System.Drawing.Color.Red;
            this.lblRMValue.Location = new System.Drawing.Point(930, 654);
            this.lblRMValue.Name = "lblRMValue";
            this.lblRMValue.Size = new System.Drawing.Size(86, 19);
            this.lblRMValue.TabIndex = 51;
            this.lblRMValue.Text = "rmvalue";
            // 
            // lblRMText
            // 
            this.lblRMText.AutoSize = true;
            this.lblRMText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblRMText.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRMText.ForeColor = System.Drawing.Color.Red;
            this.lblRMText.Location = new System.Drawing.Point(849, 654);
            this.lblRMText.Name = "lblRMText";
            this.lblRMText.Size = new System.Drawing.Size(75, 19);
            this.lblRMText.TabIndex = 50;
            this.lblRMText.Text = "rmtext";
            this.lblRMText.Visible = false;
            // 
            // lblCMValue
            // 
            this.lblCMValue.AutoSize = true;
            this.lblCMValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblCMValue.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCMValue.ForeColor = System.Drawing.Color.Red;
            this.lblCMValue.Location = new System.Drawing.Point(930, 635);
            this.lblCMValue.Name = "lblCMValue";
            this.lblCMValue.Size = new System.Drawing.Size(86, 19);
            this.lblCMValue.TabIndex = 49;
            this.lblCMValue.Text = "cmvalue";
            // 
            // lblCMText
            // 
            this.lblCMText.AutoSize = true;
            this.lblCMText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblCMText.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCMText.ForeColor = System.Drawing.Color.Red;
            this.lblCMText.Location = new System.Drawing.Point(849, 635);
            this.lblCMText.Name = "lblCMText";
            this.lblCMText.Size = new System.Drawing.Size(75, 19);
            this.lblCMText.TabIndex = 48;
            this.lblCMText.Text = "cmtext";
            this.lblCMText.Visible = false;
            // 
            // lblWLValue
            // 
            this.lblWLValue.AutoSize = true;
            this.lblWLValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblWLValue.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWLValue.ForeColor = System.Drawing.Color.Red;
            this.lblWLValue.Location = new System.Drawing.Point(1323, 687);
            this.lblWLValue.Name = "lblWLValue";
            this.lblWLValue.Size = new System.Drawing.Size(86, 19);
            this.lblWLValue.TabIndex = 47;
            this.lblWLValue.Text = "wlvalue";
            // 
            // lblWLText
            // 
            this.lblWLText.AutoSize = true;
            this.lblWLText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblWLText.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWLText.ForeColor = System.Drawing.Color.Red;
            this.lblWLText.Location = new System.Drawing.Point(1323, 660);
            this.lblWLText.Name = "lblWLText";
            this.lblWLText.Size = new System.Drawing.Size(75, 19);
            this.lblWLText.TabIndex = 46;
            this.lblWLText.Text = "wltext";
            // 
            // lblBP2Value
            // 
            this.lblBP2Value.AutoSize = true;
            this.lblBP2Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBP2Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBP2Value.ForeColor = System.Drawing.Color.Red;
            this.lblBP2Value.Location = new System.Drawing.Point(930, 392);
            this.lblBP2Value.Name = "lblBP2Value";
            this.lblBP2Value.Size = new System.Drawing.Size(97, 19);
            this.lblBP2Value.TabIndex = 45;
            this.lblBP2Value.Text = "bp2value";
            // 
            // lblBP2Text
            // 
            this.lblBP2Text.AutoSize = true;
            this.lblBP2Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBP2Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBP2Text.ForeColor = System.Drawing.Color.Red;
            this.lblBP2Text.Location = new System.Drawing.Point(1022, 89);
            this.lblBP2Text.Name = "lblBP2Text";
            this.lblBP2Text.Size = new System.Drawing.Size(86, 19);
            this.lblBP2Text.TabIndex = 44;
            this.lblBP2Text.Text = "bp2text";
            // 
            // lblBT2Value
            // 
            this.lblBT2Value.AutoSize = true;
            this.lblBT2Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBT2Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBT2Value.ForeColor = System.Drawing.Color.Red;
            this.lblBT2Value.Location = new System.Drawing.Point(930, 373);
            this.lblBT2Value.Name = "lblBT2Value";
            this.lblBT2Value.Size = new System.Drawing.Size(97, 19);
            this.lblBT2Value.TabIndex = 43;
            this.lblBT2Value.Text = "bt2value";
            // 
            // lblBT2Text
            // 
            this.lblBT2Text.AutoSize = true;
            this.lblBT2Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBT2Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBT2Text.ForeColor = System.Drawing.Color.Red;
            this.lblBT2Text.Location = new System.Drawing.Point(930, 89);
            this.lblBT2Text.Name = "lblBT2Text";
            this.lblBT2Text.Size = new System.Drawing.Size(86, 19);
            this.lblBT2Text.TabIndex = 42;
            this.lblBT2Text.Text = "bt2text";
            // 
            // lblGP2Value
            // 
            this.lblGP2Value.AutoSize = true;
            this.lblGP2Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGP2Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGP2Value.ForeColor = System.Drawing.Color.Red;
            this.lblGP2Value.Location = new System.Drawing.Point(930, 154);
            this.lblGP2Value.Name = "lblGP2Value";
            this.lblGP2Value.Size = new System.Drawing.Size(97, 19);
            this.lblGP2Value.TabIndex = 41;
            this.lblGP2Value.Text = "gp2value";
            // 
            // lblGP2Text
            // 
            this.lblGP2Text.AutoSize = true;
            this.lblGP2Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGP2Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGP2Text.ForeColor = System.Drawing.Color.Red;
            this.lblGP2Text.Location = new System.Drawing.Point(1022, 70);
            this.lblGP2Text.Name = "lblGP2Text";
            this.lblGP2Text.Size = new System.Drawing.Size(86, 19);
            this.lblGP2Text.TabIndex = 40;
            this.lblGP2Text.Text = "gp2text";
            // 
            // lblGT2Value
            // 
            this.lblGT2Value.AutoSize = true;
            this.lblGT2Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGT2Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGT2Value.ForeColor = System.Drawing.Color.Red;
            this.lblGT2Value.Location = new System.Drawing.Point(930, 135);
            this.lblGT2Value.Name = "lblGT2Value";
            this.lblGT2Value.Size = new System.Drawing.Size(97, 19);
            this.lblGT2Value.TabIndex = 39;
            this.lblGT2Value.Text = "gt2value";
            // 
            // lblGT2Text
            // 
            this.lblGT2Text.AutoSize = true;
            this.lblGT2Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGT2Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGT2Text.ForeColor = System.Drawing.Color.Red;
            this.lblGT2Text.Location = new System.Drawing.Point(930, 70);
            this.lblGT2Text.Name = "lblGT2Text";
            this.lblGT2Text.Size = new System.Drawing.Size(86, 19);
            this.lblGT2Text.TabIndex = 38;
            this.lblGT2Text.Text = "gt2text";
            // 
            // lblBP1Value
            // 
            this.lblBP1Value.AutoSize = true;
            this.lblBP1Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBP1Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBP1Value.ForeColor = System.Drawing.Color.Red;
            this.lblBP1Value.Location = new System.Drawing.Point(50, 392);
            this.lblBP1Value.Name = "lblBP1Value";
            this.lblBP1Value.Size = new System.Drawing.Size(97, 19);
            this.lblBP1Value.TabIndex = 37;
            this.lblBP1Value.Text = "bp1value";
            // 
            // lblBP1Text
            // 
            this.lblBP1Text.AutoSize = true;
            this.lblBP1Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBP1Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBP1Text.ForeColor = System.Drawing.Color.Red;
            this.lblBP1Text.Location = new System.Drawing.Point(116, 55);
            this.lblBP1Text.Name = "lblBP1Text";
            this.lblBP1Text.Size = new System.Drawing.Size(86, 19);
            this.lblBP1Text.TabIndex = 36;
            this.lblBP1Text.Text = "bp1text";
            // 
            // lblBT1Value
            // 
            this.lblBT1Value.AutoSize = true;
            this.lblBT1Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBT1Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBT1Value.ForeColor = System.Drawing.Color.Red;
            this.lblBT1Value.Location = new System.Drawing.Point(50, 373);
            this.lblBT1Value.Name = "lblBT1Value";
            this.lblBT1Value.Size = new System.Drawing.Size(97, 19);
            this.lblBT1Value.TabIndex = 35;
            this.lblBT1Value.Text = "bt1value";
            // 
            // lblBT1Text
            // 
            this.lblBT1Text.AutoSize = true;
            this.lblBT1Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblBT1Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBT1Text.ForeColor = System.Drawing.Color.Red;
            this.lblBT1Text.Location = new System.Drawing.Point(24, 55);
            this.lblBT1Text.Name = "lblBT1Text";
            this.lblBT1Text.Size = new System.Drawing.Size(86, 19);
            this.lblBT1Text.TabIndex = 34;
            this.lblBT1Text.Text = "bt1text";
            // 
            // lblGP1Value
            // 
            this.lblGP1Value.AutoSize = true;
            this.lblGP1Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGP1Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGP1Value.ForeColor = System.Drawing.Color.Red;
            this.lblGP1Value.Location = new System.Drawing.Point(50, 154);
            this.lblGP1Value.Name = "lblGP1Value";
            this.lblGP1Value.Size = new System.Drawing.Size(97, 19);
            this.lblGP1Value.TabIndex = 33;
            this.lblGP1Value.Text = "gp1value";
            // 
            // lblGP1Text
            // 
            this.lblGP1Text.AutoSize = true;
            this.lblGP1Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGP1Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGP1Text.ForeColor = System.Drawing.Color.Red;
            this.lblGP1Text.Location = new System.Drawing.Point(116, 36);
            this.lblGP1Text.Name = "lblGP1Text";
            this.lblGP1Text.Size = new System.Drawing.Size(86, 19);
            this.lblGP1Text.TabIndex = 32;
            this.lblGP1Text.Text = "gp1text";
            // 
            // lblGT1Value
            // 
            this.lblGT1Value.AutoSize = true;
            this.lblGT1Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGT1Value.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGT1Value.ForeColor = System.Drawing.Color.Red;
            this.lblGT1Value.Location = new System.Drawing.Point(50, 135);
            this.lblGT1Value.Name = "lblGT1Value";
            this.lblGT1Value.Size = new System.Drawing.Size(97, 19);
            this.lblGT1Value.TabIndex = 31;
            this.lblGT1Value.Text = "gt1value";
            // 
            // lblGT1Text
            // 
            this.lblGT1Text.AutoSize = true;
            this.lblGT1Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblGT1Text.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGT1Text.ForeColor = System.Drawing.Color.Red;
            this.lblGT1Text.Location = new System.Drawing.Point(24, 36);
            this.lblGT1Text.Name = "lblGT1Text";
            this.lblGT1Text.Size = new System.Drawing.Size(86, 19);
            this.lblGT1Text.TabIndex = 30;
            this.lblGT1Text.Text = "gt1text";
            // 
            // GRDeviceGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(213)))), ((int)(((byte)(253)))));
            this.Controls.Add(this.panel1);
            this.Name = "GRDeviceGraph";
            this.Size = new System.Drawing.Size(1600, 800);
            this.Load += new System.EventHandler(this.GRDeviceGraph_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPressCurve;
        private System.Windows.Forms.Button btnTempCurve;
        private System.Windows.Forms.Button btnGRAlarmData;
        private System.Windows.Forms.Button btnGRData;
        private System.Windows.Forms.Button btnReadRealData;
        private System.Windows.Forms.Label lblMark;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblStationName;
        private System.Windows.Forms.Label lblRMValue;
        private System.Windows.Forms.Label lblRMText;
        private System.Windows.Forms.Label lblCMValue;
        private System.Windows.Forms.Label lblCMText;
        private System.Windows.Forms.Label lblWLValue;
        private System.Windows.Forms.Label lblWLText;
        private System.Windows.Forms.Label lblBP2Value;
        private System.Windows.Forms.Label lblBP2Text;
        private System.Windows.Forms.Label lblBT2Value;
        private System.Windows.Forms.Label lblBT2Text;
        private System.Windows.Forms.Label lblGP2Value;
        private System.Windows.Forms.Label lblGP2Text;
        private System.Windows.Forms.Label lblGT2Value;
        private System.Windows.Forms.Label lblGT2Text;
        private System.Windows.Forms.Label lblBP1Value;
        private System.Windows.Forms.Label lblBP1Text;
        private System.Windows.Forms.Label lblBT1Value;
        private System.Windows.Forms.Label lblBT1Text;
        private System.Windows.Forms.Label lblGP1Value;
        private System.Windows.Forms.Label lblGP1Text;
        private System.Windows.Forms.Label lblGT1Value;
        private System.Windows.Forms.Label lblGT1Text;

    }
}
