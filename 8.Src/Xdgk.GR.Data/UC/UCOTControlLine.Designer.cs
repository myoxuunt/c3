namespace Xdgk.GR.Common
{
    partial class UCOTControlLine
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
            this.components = new System.ComponentModel.Container();
            this.zedTimeControlLine = new ZedGraph.ZedGraphControl();
            this.dgvTimeControlLine = new System.Windows.Forms.DataGridView();
            this.btnModify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeControlLine)).BeginInit();
            this.SuspendLayout();
            // 
            // zedTimeControlLine
            // 
            this.zedTimeControlLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedTimeControlLine.Location = new System.Drawing.Point(213, 3);
            this.zedTimeControlLine.Name = "zedTimeControlLine";
            this.zedTimeControlLine.ScrollGrace = 0;
            this.zedTimeControlLine.ScrollMaxX = 0;
            this.zedTimeControlLine.ScrollMaxY = 0;
            this.zedTimeControlLine.ScrollMaxY2 = 0;
            this.zedTimeControlLine.ScrollMinX = 0;
            this.zedTimeControlLine.ScrollMinY = 0;
            this.zedTimeControlLine.ScrollMinY2 = 0;
            this.zedTimeControlLine.Size = new System.Drawing.Size(387, 372);
            this.zedTimeControlLine.TabIndex = 3;
            // 
            // dgvTimeControlLine
            // 
            this.dgvTimeControlLine.AllowUserToResizeColumns = false;
            this.dgvTimeControlLine.AllowUserToResizeRows = false;
            this.dgvTimeControlLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvTimeControlLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeControlLine.Location = new System.Drawing.Point(3, 3);
            this.dgvTimeControlLine.MultiSelect = false;
            this.dgvTimeControlLine.Name = "dgvTimeControlLine";
            this.dgvTimeControlLine.RowHeadersVisible = false;
            this.dgvTimeControlLine.RowTemplate.Height = 23;
            this.dgvTimeControlLine.Size = new System.Drawing.Size(204, 343);
            this.dgvTimeControlLine.TabIndex = 4;
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModify.Location = new System.Drawing.Point(132, 352);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 5;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // UCOTControlLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTimeControlLine);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.zedTimeControlLine);
            this.Name = "UCOTControlLine";
            this.Size = new System.Drawing.Size(603, 378);
            this.Load += new System.EventHandler(this.UCOTControlLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeControlLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedTimeControlLine;
        private System.Windows.Forms.DataGridView dgvTimeControlLine;
        private System.Windows.Forms.Button btnModify;

    }
}
