namespace Xdgk.GR.UI
{
    partial class frmOTGT2Value
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
            this.lblTime = new System.Windows.Forms.Label();
            this.numGT2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numOT = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numGT2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOT)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(29, 66);
            this.okButton.TabIndex = 4;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(107, 66);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(10, 14);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(59, 12);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "室外温度:";
            // 
            // numGT2
            // 
            this.numGT2.Location = new System.Drawing.Point(75, 39);
            this.numGT2.Name = "numGT2";
            this.numGT2.Size = new System.Drawing.Size(104, 21);
            this.numGT2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "二次供温:";
            // 
            // numOT
            // 
            this.numOT.Location = new System.Drawing.Point(75, 12);
            this.numOT.Name = "numOT";
            this.numOT.Size = new System.Drawing.Size(104, 21);
            this.numOT.TabIndex = 1;
            // 
            // frmOTGT2Value
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 101);
            this.Controls.Add(this.numOT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numGT2);
            this.Controls.Add(this.lblTime);
            this.Name = "frmOTGT2Value";
            this.Text = "二次供温设定";
            this.Load += new System.EventHandler(this.frmOTGT2Value_Load);
            this.Controls.SetChildIndex(this.lblTime, 0);
            this.Controls.SetChildIndex(this.numGT2, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.numOT, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numGT2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.NumericUpDown numGT2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numOT;
    }
}