namespace C3
{
    partial class frmStationType
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
            this.lstStationType = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(52, 214);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(140, 214);
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lstStationType
            // 
            this.lstStationType.FormattingEnabled = true;
            this.lstStationType.ItemHeight = 12;
            this.lstStationType.Location = new System.Drawing.Point(12, 12);
            this.lstStationType.Name = "lstStationType";
            this.lstStationType.Size = new System.Drawing.Size(200, 196);
            this.lstStationType.TabIndex = 19;
            // 
            // frmStationType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 251);
            this.Controls.Add(this.lstStationType);
            this.Name = "frmStationType";
            this.Text = "#frmStationType#";
            this.Load += new System.EventHandler(this.frmStationType_Load);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.lstStationType, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstStationType;
    }
}