namespace Logger
{
    partial class LogView
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
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serchOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTwoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.option2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.option3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Location = new System.Drawing.Point(0, 69);
            this.dgvLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowHeadersWidth = 62;
            this.dgvLog.RowTemplate.Height = 28;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(2348, 542);
            this.dgvLog.TabIndex = 0;
            this.dgvLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellContentClick);
            this.dgvLog.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLog_CellMouseDown);
            this.dgvLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLog_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.option2ToolStripMenuItem,
            this.option3ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(2348, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseClick);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serchOneToolStripMenuItem,
            this.searchTwoToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(88, 36);
            this.searchToolStripMenuItem.Text = "Filter";
            // 
            // serchOneToolStripMenuItem
            // 
            this.serchOneToolStripMenuItem.Name = "serchOneToolStripMenuItem";
            this.serchOneToolStripMenuItem.Size = new System.Drawing.Size(254, 44);
            this.serchOneToolStripMenuItem.Text = "Advanced";
            // 
            // searchTwoToolStripMenuItem
            // 
            this.searchTwoToolStripMenuItem.Name = "searchTwoToolStripMenuItem";
            this.searchTwoToolStripMenuItem.Size = new System.Drawing.Size(254, 44);
            this.searchTwoToolStripMenuItem.Text = "Clear";
            this.searchTwoToolStripMenuItem.Click += new System.EventHandler(this.searchTwoToolStripMenuItem_Click);
            // 
            // option2ToolStripMenuItem
            // 
            this.option2ToolStripMenuItem.Name = "option2ToolStripMenuItem";
            this.option2ToolStripMenuItem.Size = new System.Drawing.Size(129, 36);
            this.option2ToolStripMenuItem.Text = "Option 2";
            // 
            // option3ToolStripMenuItem
            // 
            this.option3ToolStripMenuItem.Name = "option3ToolStripMenuItem";
            this.option3ToolStripMenuItem.Size = new System.Drawing.Size(129, 36);
            this.option3ToolStripMenuItem.Text = "Option 3";
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2348, 606);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimizeBox = false;
            this.Name = "LogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LogView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LogView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem option2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem option3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serchOneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchTwoToolStripMenuItem;
    }
}