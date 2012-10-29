namespace C3
{
    partial class frmCustomTask
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSend = new System.Windows.Forms.Label();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(258, 152);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(346, 152);
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(118, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 21);
            this.txtName.TabIndex = 19;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 21);
            this.lblName.TabIndex = 20;
            this.lblName.Text = "Name:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSend
            // 
            this.lblSend.Location = new System.Drawing.Point(12, 36);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(100, 21);
            this.lblSend.TabIndex = 22;
            this.lblSend.Text = "Send:";
            this.lblSend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(118, 36);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(300, 104);
            this.txtSend.TabIndex = 21;
            // 
            // frmCustomTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 188);
            this.Controls.Add(this.lblSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Name = "frmCustomTask";
            this.Text = "frmCustomTask";
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.txtSend, 0);
            this.Controls.SetChildIndex(this.lblSend, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.TextBox txtSend;
    }
}