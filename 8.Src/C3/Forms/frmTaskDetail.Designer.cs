namespace C3
{
    partial class frmTaskDetail
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
            this.lblStation = new System.Windows.Forms.Label();
            this.txtStation = new System.Windows.Forms.TextBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.lblTask = new System.Windows.Forms.Label();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.lblLastExecute = new System.Windows.Forms.Label();
            this.txtLastExecute = new System.Windows.Forms.TextBox();
            this.lblCycle = new System.Windows.Forms.Label();
            this.dtpTaskCycle = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(108, 160);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(196, 160);
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblStation
            // 
            this.lblStation.Location = new System.Drawing.Point(12, 9);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(100, 21);
            this.lblStation.TabIndex = 19;
            this.lblStation.Text = "站点:";
            this.lblStation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStation
            // 
            this.txtStation.Location = new System.Drawing.Point(118, 9);
            this.txtStation.Name = "txtStation";
            this.txtStation.ReadOnly = true;
            this.txtStation.Size = new System.Drawing.Size(150, 21);
            this.txtStation.TabIndex = 20;
            // 
            // lblDevice
            // 
            this.lblDevice.Location = new System.Drawing.Point(12, 36);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(100, 21);
            this.lblDevice.TabIndex = 21;
            this.lblDevice.Text = "设备:";
            this.lblDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(118, 36);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.ReadOnly = true;
            this.txtDevice.Size = new System.Drawing.Size(150, 21);
            this.txtDevice.TabIndex = 22;
            // 
            // lblTask
            // 
            this.lblTask.Location = new System.Drawing.Point(12, 63);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(100, 21);
            this.lblTask.TabIndex = 23;
            this.lblTask.Text = "任务:";
            this.lblTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTask
            // 
            this.txtTask.Location = new System.Drawing.Point(118, 63);
            this.txtTask.Name = "txtTask";
            this.txtTask.ReadOnly = true;
            this.txtTask.Size = new System.Drawing.Size(150, 21);
            this.txtTask.TabIndex = 24;
            // 
            // lblLastExecute
            // 
            this.lblLastExecute.Location = new System.Drawing.Point(12, 90);
            this.lblLastExecute.Name = "lblLastExecute";
            this.lblLastExecute.Size = new System.Drawing.Size(100, 21);
            this.lblLastExecute.TabIndex = 25;
            this.lblLastExecute.Text = "最后执行:";
            this.lblLastExecute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLastExecute
            // 
            this.txtLastExecute.Location = new System.Drawing.Point(118, 90);
            this.txtLastExecute.Name = "txtLastExecute";
            this.txtLastExecute.ReadOnly = true;
            this.txtLastExecute.Size = new System.Drawing.Size(150, 21);
            this.txtLastExecute.TabIndex = 26;
            // 
            // lblCycle
            // 
            this.lblCycle.Location = new System.Drawing.Point(12, 117);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(100, 21);
            this.lblCycle.TabIndex = 27;
            this.lblCycle.Text = "周期:";
            this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpTaskCycle
            // 
            this.dtpTaskCycle.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTaskCycle.Location = new System.Drawing.Point(118, 117);
            this.dtpTaskCycle.Name = "dtpTaskCycle";
            this.dtpTaskCycle.ShowUpDown = true;
            this.dtpTaskCycle.Size = new System.Drawing.Size(150, 21);
            this.dtpTaskCycle.TabIndex = 28;
            // 
            // frmTaskDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 198);
            this.Controls.Add(this.dtpTaskCycle);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.lblLastExecute);
            this.Controls.Add(this.txtLastExecute);
            this.Controls.Add(this.lblTask);
            this.Controls.Add(this.txtTask);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.txtDevice);
            this.Controls.Add(this.lblStation);
            this.Controls.Add(this.txtStation);
            this.Name = "frmTaskDetail";
            this.Text = "任务";
            this.Load += new System.EventHandler(this.frmTaskDetail_Load);
            this.Controls.SetChildIndex(this.txtStation, 0);
            this.Controls.SetChildIndex(this.lblStation, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.txtDevice, 0);
            this.Controls.SetChildIndex(this.lblDevice, 0);
            this.Controls.SetChildIndex(this.txtTask, 0);
            this.Controls.SetChildIndex(this.lblTask, 0);
            this.Controls.SetChildIndex(this.txtLastExecute, 0);
            this.Controls.SetChildIndex(this.lblLastExecute, 0);
            this.Controls.SetChildIndex(this.lblCycle, 0);
            this.Controls.SetChildIndex(this.dtpTaskCycle, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.TextBox txtStation;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.TextBox txtTask;
        private System.Windows.Forms.Label lblLastExecute;
        private System.Windows.Forms.TextBox txtLastExecute;
        private System.Windows.Forms.Label lblCycle;
        private System.Windows.Forms.DateTimePicker dtpTaskCycle;
    }
}