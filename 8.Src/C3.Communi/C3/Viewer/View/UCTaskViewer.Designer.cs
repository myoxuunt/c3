namespace C3
{
    partial class UCTaskViewer
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.chOperaName = new System.Windows.Forms.ColumnHeader();
            this.chLastExecute = new System.Windows.Forms.ColumnHeader();
            this.chStrategy = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chOperaName,
            this.chLastExecute,
            this.chStrategy,
            this.chStatus});
            this.listView1.Location = new System.Drawing.Point(61, 29);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(397, 126);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // chOperaName
            // 
            this.chOperaName.Text = "OperaName";
            this.chOperaName.Width = 78;
            // 
            // chLastExecute
            // 
            this.chLastExecute.Text = "LastExecute";
            this.chLastExecute.Width = 96;
            // 
            // chStrategy
            // 
            this.chStrategy.Text = "Strategy";
            this.chStrategy.Width = 100;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // UCTaskViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "UCTaskViewer";
            this.Size = new System.Drawing.Size(512, 251);
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chOperaName;
        private System.Windows.Forms.ColumnHeader chLastExecute;
        private System.Windows.Forms.ColumnHeader chStrategy;
        private System.Windows.Forms.ColumnHeader chStatus;
    }
}