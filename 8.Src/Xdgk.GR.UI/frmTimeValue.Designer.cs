namespace Xdgk.GR.UI
{
    partial class frmTimeValue
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
            this.lblTime = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.numAdjust = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjust)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(19, 60);
            this.okButton.TabIndex = 4;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(97, 60);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "修正值:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(12, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 12);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "时间:";
            this.lblTime.Click += new System.EventHandler(this.lblTime_Click);
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTime.Location = new System.Drawing.Point(65, 6);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(104, 21);
            this.txtTime.TabIndex = 1;
            // 
            // numAdjust
            // 
            this.numAdjust.Location = new System.Drawing.Point(65, 33);
            this.numAdjust.Name = "numAdjust";
            this.numAdjust.Size = new System.Drawing.Size(104, 21);
            this.numAdjust.TabIndex = 3;
            // 
            // frmTimeValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 94);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.numAdjust);
            this.Controls.Add(this.label1);
            this.Name = "frmTimeValue";
            this.Text = "修正值设定";
            this.Load += new System.EventHandler(this.frmTimeValue_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.numAdjust, 0);
            this.Controls.SetChildIndex(this.lblTime, 0);
            this.Controls.SetChildIndex(this.txtTime, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numAdjust)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.NumericUpDown numAdjust;
    }
}