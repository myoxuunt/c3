namespace Xdgk.GR.UI
{
    partial class UCValveOpenDegree
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
            this.lblValveOpenDegreeUnit = new System.Windows.Forms.Label();
            this.numValveOpenDegree = new System.Windows.Forms.NumericUpDown();
            this.lblValveOpenDegree = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numValveOpenDegree)).BeginInit();
            this.SuspendLayout();
            // 
            // lblValveOpenDegreeUnit
            // 
            this.lblValveOpenDegreeUnit.AutoSize = true;
            this.lblValveOpenDegreeUnit.Location = new System.Drawing.Point(219, 5);
            this.lblValveOpenDegreeUnit.Name = "lblValveOpenDegreeUnit";
            this.lblValveOpenDegreeUnit.Size = new System.Drawing.Size(23, 12);
            this.lblValveOpenDegreeUnit.TabIndex = 21;
            this.lblValveOpenDegreeUnit.Text = "(%)";
            // 
            // numValveOpenDegree
            // 
            this.numValveOpenDegree.Location = new System.Drawing.Point(76, 3);
            this.numValveOpenDegree.Name = "numValveOpenDegree";
            this.numValveOpenDegree.Size = new System.Drawing.Size(137, 21);
            this.numValveOpenDegree.TabIndex = 20;
            // 
            // lblValveOpenDegree
            // 
            this.lblValveOpenDegree.AutoSize = true;
            this.lblValveOpenDegree.Location = new System.Drawing.Point(3, 5);
            this.lblValveOpenDegree.Name = "lblValveOpenDegree";
            this.lblValveOpenDegree.Size = new System.Drawing.Size(65, 12);
            this.lblValveOpenDegree.TabIndex = 19;
            this.lblValveOpenDegree.Text = "阀门开度：";
            // 
            // UCValveOpenDegree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValveOpenDegreeUnit);
            this.Controls.Add(this.numValveOpenDegree);
            this.Controls.Add(this.lblValveOpenDegree);
            this.Name = "UCValveOpenDegree";
            this.Size = new System.Drawing.Size(280, 34);
            this.Load += new System.EventHandler(this.UCValveOpenDegree_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numValveOpenDegree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValveOpenDegreeUnit;
        private System.Windows.Forms.NumericUpDown numValveOpenDegree;
        private System.Windows.Forms.Label lblValveOpenDegree;
    }
}
