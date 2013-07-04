namespace C3
{
    partial class frmCPCreateLog
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
            this.txtCPCreateLog = new System.Windows.Forms.RichTextBox();
            this.tbnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCPCreateLog
            // 
            this.txtCPCreateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCPCreateLog.Location = new System.Drawing.Point(12, 12);
            this.txtCPCreateLog.Name = "txtCPCreateLog";
            this.txtCPCreateLog.Size = new System.Drawing.Size(499, 312);
            this.txtCPCreateLog.TabIndex = 0;
            this.txtCPCreateLog.Text = "";
            this.txtCPCreateLog.WordWrap = false;
            // 
            // tbnOK
            // 
            this.tbnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbnOK.Location = new System.Drawing.Point(436, 334);
            this.tbnOK.Name = "tbnOK";
            this.tbnOK.Size = new System.Drawing.Size(75, 23);
            this.tbnOK.TabIndex = 1;
            this.tbnOK.Text = "关闭";
            this.tbnOK.UseVisualStyleBackColor = true;
            this.tbnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmCPCreateLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 369);
            this.Controls.Add(this.tbnOK);
            this.Controls.Add(this.txtCPCreateLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCPCreateLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "连接记录";
            this.Load += new System.EventHandler(this.frmCPCreateLog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtCPCreateLog;
        private System.Windows.Forms.Button tbnOK;
    }
}