namespace Xdgk.GR.Common
{
    partial class UCTimeControlLine2
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
            this.ucTimeControlLine1 = new Xdgk.GR.Common.UCTimeControlLine();
            this.label5 = new System.Windows.Forms.Label();
            this.numGTBase2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numGTBase2)).BeginInit();
            this.SuspendLayout();
            // 
            // ucTimeControlLine1
            // 
            this.ucTimeControlLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucTimeControlLine1.GTBase2 = 0;
            this.ucTimeControlLine1.Location = new System.Drawing.Point(0, 30);
            this.ucTimeControlLine1.Name = "ucTimeControlLine1";
            this.ucTimeControlLine1.Size = new System.Drawing.Size(806, 349);
            this.ucTimeControlLine1.TabIndex = 0;
            this.ucTimeControlLine1.TimeControlLine = null;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "（0 ~ 99 ℃）";
            // 
            // numGTBase2
            // 
            this.numGTBase2.Location = new System.Drawing.Point(76, 3);
            this.numGTBase2.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numGTBase2.Name = "numGTBase2";
            this.numGTBase2.Size = new System.Drawing.Size(137, 21);
            this.numGTBase2.TabIndex = 17;
            this.numGTBase2.ValueChanged += new System.EventHandler(this.numGTBase2_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "供温基准：";
            // 
            // UCTimeControlLine2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numGTBase2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucTimeControlLine1);
            this.Name = "UCTimeControlLine2";
            this.Size = new System.Drawing.Size(808, 384);
            ((System.ComponentModel.ISupportInitialize)(this.numGTBase2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCTimeControlLine ucTimeControlLine1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numGTBase2;
        private System.Windows.Forms.Label label3;
    }
}
