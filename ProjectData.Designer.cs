﻿namespace Logger
{
    partial class ProjectData
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
            this.lblPrjName = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.projectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enhancedConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateAndTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dispenseCurrencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionReplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project";
            // 
            // lblPrjName
            // 
            this.lblPrjName.AutoSize = true;
            this.lblPrjName.Location = new System.Drawing.Point(85, 38);
            this.lblPrjName.Name = "lblPrjName";
            this.lblPrjName.Size = new System.Drawing.Size(104, 20);
            this.lblPrjName.TabIndex = 1;
            this.lblPrjName.Text = "Project Name";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logsToolStripMenuItem,
            this.scanToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1393, 36);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attachToolStripMenuItem,
            this.detachToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.projectsToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(66, 30);
            this.logsToolStripMenuItem.Text = "Logs";
            // 
            // attachToolStripMenuItem
            // 
            this.attachToolStripMenuItem.Name = "attachToolStripMenuItem";
            this.attachToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.attachToolStripMenuItem.Text = "Attach";
            this.attachToolStripMenuItem.Click += new System.EventHandler(this.attachToolStripMenuItem_Click);
            // 
            // detachToolStripMenuItem
            // 
            this.detachToolStripMenuItem.Name = "detachToolStripMenuItem";
            this.detachToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.detachToolStripMenuItem.Text = "Detach";
            this.detachToolStripMenuItem.Click += new System.EventHandler(this.detachToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(173, 6);
            // 
            // projectsToolStripMenuItem
            // 
            this.projectsToolStripMenuItem.Name = "projectsToolStripMenuItem";
            this.projectsToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.projectsToolStripMenuItem.Text = "Projects";
            this.projectsToolStripMenuItem.Click += new System.EventHandler(this.projectsToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.screensToolStripMenuItem,
            this.statesToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.fITToolStripMenuItem,
            this.configurationIDToolStripMenuItem,
            this.enhancedConfigurationToolStripMenuItem,
            this.mACToolStripMenuItem,
            this.dateAndTimeToolStripMenuItem,
            this.dispenseCurrencyToolStripMenuItem,
            this.transactionRequestToolStripMenuItem,
            this.transactionReplyToolStripMenuItem});
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(65, 30);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            this.scanToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scanToolStripMenuItem_MouseDown);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.allToolStripMenuItem.Text = "All";
            // 
            // screensToolStripMenuItem
            // 
            this.screensToolStripMenuItem.Name = "screensToolStripMenuItem";
            this.screensToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.screensToolStripMenuItem.Text = "Screens";
            this.screensToolStripMenuItem.Click += new System.EventHandler(this.screensToolStripMenuItem_Click);
            // 
            // statesToolStripMenuItem
            // 
            this.statesToolStripMenuItem.Name = "statesToolStripMenuItem";
            this.statesToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.statesToolStripMenuItem.Text = "States";
            this.statesToolStripMenuItem.Click += new System.EventHandler(this.statesToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.configurationToolStripMenuItem.Text = "Configuration Parameters";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // fITToolStripMenuItem
            // 
            this.fITToolStripMenuItem.Name = "fITToolStripMenuItem";
            this.fITToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.fITToolStripMenuItem.Text = "FIT";
            this.fITToolStripMenuItem.Click += new System.EventHandler(this.fITToolStripMenuItem_Click);
            // 
            // configurationIDToolStripMenuItem
            // 
            this.configurationIDToolStripMenuItem.Name = "configurationIDToolStripMenuItem";
            this.configurationIDToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.configurationIDToolStripMenuItem.Text = "Configuration ID";
            this.configurationIDToolStripMenuItem.Click += new System.EventHandler(this.configurationIDToolStripMenuItem_Click);
            // 
            // enhancedConfigurationToolStripMenuItem
            // 
            this.enhancedConfigurationToolStripMenuItem.Name = "enhancedConfigurationToolStripMenuItem";
            this.enhancedConfigurationToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.enhancedConfigurationToolStripMenuItem.Text = "Enhanced Configuration";
            this.enhancedConfigurationToolStripMenuItem.Click += new System.EventHandler(this.enhancedConfigurationToolStripMenuItem_Click);
            // 
            // mACToolStripMenuItem
            // 
            this.mACToolStripMenuItem.Name = "mACToolStripMenuItem";
            this.mACToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.mACToolStripMenuItem.Text = "MAC";
            // 
            // dateAndTimeToolStripMenuItem
            // 
            this.dateAndTimeToolStripMenuItem.Name = "dateAndTimeToolStripMenuItem";
            this.dateAndTimeToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.dateAndTimeToolStripMenuItem.Text = "Date and Time";
            this.dateAndTimeToolStripMenuItem.Click += new System.EventHandler(this.dateAndTimeToolStripMenuItem_Click);
            // 
            // dispenseCurrencyToolStripMenuItem
            // 
            this.dispenseCurrencyToolStripMenuItem.Name = "dispenseCurrencyToolStripMenuItem";
            this.dispenseCurrencyToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.dispenseCurrencyToolStripMenuItem.Text = "Dispense Currency";
            // 
            // transactionRequestToolStripMenuItem
            // 
            this.transactionRequestToolStripMenuItem.Name = "transactionRequestToolStripMenuItem";
            this.transactionRequestToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.transactionRequestToolStripMenuItem.Text = "Transaction Request";
            this.transactionRequestToolStripMenuItem.Click += new System.EventHandler(this.transactionRequestToolStripMenuItem_Click);
            // 
            // transactionReplyToolStripMenuItem
            // 
            this.transactionReplyToolStripMenuItem.Name = "transactionReplyToolStripMenuItem";
            this.transactionReplyToolStripMenuItem.Size = new System.Drawing.Size(315, 34);
            this.transactionReplyToolStripMenuItem.Text = "Transaction Reply";
            this.transactionReplyToolStripMenuItem.Click += new System.EventHandler(this.transactionReplyToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 36);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1393, 731);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // ProjectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 767);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblPrjName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProjectData";
            this.Load += new System.EventHandler(this.ProjectData_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrjName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enhancedConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mACToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateAndTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionRequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionReplyToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripSeparator deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dispenseCurrencyToolStripMenuItem;
    }
}