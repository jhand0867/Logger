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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serchOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTwoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logInContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dataWrappingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dataWrappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btExport = new System.Windows.Forms.Button();
            this.cbQueryName = new System.Windows.Forms.ComboBox();
            this.tabDetail = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbMahineAndSoftwareInfo = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblApplyFilter = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.dgvLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLog.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvLog.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLog.Location = new System.Drawing.Point(2, 2);
            this.dgvLog.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowHeadersWidth = 62;
            this.dgvLog.RowTemplate.Height = 20;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvLog.Size = new System.Drawing.Size(1163, 262);
            this.dgvLog.TabIndex = 0;
            this.dgvLog.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLog_CellMouseDown);
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.SteelBlue;
            this.mainMenu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.mainMenu.ShowItemToolTips = true;
            this.mainMenu.Size = new System.Drawing.Size(1294, 24);
            this.mainMenu.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.printToolStripMenuItem,
            this.printToolStripMenuItem2,
            this.toolStripSeparator4,
            this.closeToolStripMenuItem,
            this.exitApplicationToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.printToolStripMenuItem.Text = "Print Screen";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem2
            // 
            this.printToolStripMenuItem2.Name = "printToolStripMenuItem2";
            this.printToolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
            this.printToolStripMenuItem2.Text = "Print ";
            this.printToolStripMenuItem2.Click += new System.EventHandler(this.printToolStripMenuItem2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(163, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.exitApplicationToolStripMenuItem.Text = "Exit Application";
            this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serchOneToolStripMenuItem,
            this.searchTwoToolStripMenuItem});
            this.searchToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(49, 22);
            this.searchToolStripMenuItem.Text = "Filter";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // serchOneToolStripMenuItem
            // 
            this.serchOneToolStripMenuItem.Name = "serchOneToolStripMenuItem";
            this.serchOneToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.serchOneToolStripMenuItem.Text = "Advanced";
            this.serchOneToolStripMenuItem.Click += new System.EventHandler(this.serchOneToolStripMenuItem_Click);
            // 
            // searchTwoToolStripMenuItem
            // 
            this.searchTwoToolStripMenuItem.Name = "searchTwoToolStripMenuItem";
            this.searchTwoToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.searchTwoToolStripMenuItem.Text = "Clear";
            this.searchTwoToolStripMenuItem.Click += new System.EventHandler(this.searchTwoToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logInContextToolStripMenuItem,
            this.toolStripSeparator2,
            this.dataWrappingToolStripMenuItem1,
            this.copyToClipboardToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // logInContextToolStripMenuItem
            // 
            this.logInContextToolStripMenuItem.Name = "logInContextToolStripMenuItem";
            this.logInContextToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.logInContextToolStripMenuItem.Text = "Log in Context";
            this.logInContextToolStripMenuItem.Click += new System.EventHandler(this.logInContextToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // dataWrappingToolStripMenuItem1
            // 
            this.dataWrappingToolStripMenuItem1.Name = "dataWrappingToolStripMenuItem1";
            this.dataWrappingToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.dataWrappingToolStripMenuItem1.Text = "Data Wrapping";
            this.dataWrappingToolStripMenuItem1.Click += new System.EventHandler(this.dataWrappingToolStripMenuItem1_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInContextToolStripMenuItem,
            this.toolStripSeparator1,
            this.dataWrappingToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator3,
            this.printToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 104);
            // 
            // showInContextToolStripMenuItem
            // 
            this.showInContextToolStripMenuItem.Name = "showInContextToolStripMenuItem";
            this.showInContextToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showInContextToolStripMenuItem.Text = "Show in Context";
            this.showInContextToolStripMenuItem.Click += new System.EventHandler(this.showInContextToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // dataWrappingToolStripMenuItem
            // 
            this.dataWrappingToolStripMenuItem.Name = "dataWrappingToolStripMenuItem";
            this.dataWrappingToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dataWrappingToolStripMenuItem.Text = "Data Wrapping";
            this.dataWrappingToolStripMenuItem.Click += new System.EventHandler(this.dataWrappingToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // printToolStripMenuItem1
            // 
            this.printToolStripMenuItem1.Name = "printToolStripMenuItem1";
            this.printToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.printToolStripMenuItem1.Text = "Print";
            // 
            // btExport
            // 
            this.btExport.BackColor = System.Drawing.Color.Transparent;
            this.btExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btExport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btExport.FlatAppearance.BorderSize = 2;
            this.btExport.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btExport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExport.Location = new System.Drawing.Point(1048, 0);
            this.btExport.Margin = new System.Windows.Forms.Padding(2);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(66, 24);
            this.btExport.TabIndex = 2;
            this.btExport.Text = "Excel";
            this.btExport.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btExport.UseVisualStyleBackColor = false;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            this.btExport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btExport_MouseClick);
            // 
            // cbQueryName
            // 
            this.cbQueryName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbQueryName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQueryName.FormattingEnabled = true;
            this.cbQueryName.Location = new System.Drawing.Point(530, 0);
            this.cbQueryName.Margin = new System.Windows.Forms.Padding(2);
            this.cbQueryName.Name = "cbQueryName";
            this.cbQueryName.Size = new System.Drawing.Size(172, 24);
            this.cbQueryName.TabIndex = 3;
            this.cbQueryName.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbQueryName_DrawItem);
            this.cbQueryName.SelectedIndexChanged += new System.EventHandler(this.cbQueryName_SelectedIndexChanged);
            this.cbQueryName.Click += new System.EventHandler(this.cbQueryName_Click);
            this.cbQueryName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbQueryName_MouseClick);
            this.cbQueryName.MouseHover += new System.EventHandler(this.cbQueryName_MouseHover);
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this.tabPage1);
            this.tabDetail.Controls.Add(this.tabPage2);
            this.tabDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetail.Location = new System.Drawing.Point(0, 24);
            this.tabDetail.Margin = new System.Windows.Forms.Padding(2);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.SelectedIndex = 0;
            this.tabDetail.Size = new System.Drawing.Size(1294, 426);
            this.tabDetail.TabIndex = 4;
            this.tabDetail.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabDetail_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1286, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "LogData";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.rtbMahineAndSoftwareInfo);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1286, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Additional Information";
            // 
            // rtbMahineAndSoftwareInfo
            // 
            this.rtbMahineAndSoftwareInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbMahineAndSoftwareInfo.BackColor = System.Drawing.SystemColors.Control;
            this.rtbMahineAndSoftwareInfo.Location = new System.Drawing.Point(18, 61);
            this.rtbMahineAndSoftwareInfo.Margin = new System.Windows.Forms.Padding(2);
            this.rtbMahineAndSoftwareInfo.Name = "rtbMahineAndSoftwareInfo";
            this.rtbMahineAndSoftwareInfo.Size = new System.Drawing.Size(1126, 301);
            this.rtbMahineAndSoftwareInfo.TabIndex = 1;
            this.rtbMahineAndSoftwareInfo.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(16, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Machine and Software Information";
            // 
            // lblApplyFilter
            // 
            this.lblApplyFilter.AutoSize = true;
            this.lblApplyFilter.BackColor = System.Drawing.Color.SteelBlue;
            this.lblApplyFilter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplyFilter.ForeColor = System.Drawing.Color.White;
            this.lblApplyFilter.Location = new System.Drawing.Point(442, 3);
            this.lblApplyFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApplyFilter.Name = "lblApplyFilter";
            this.lblApplyFilter.Size = new System.Drawing.Size(74, 16);
            this.lblApplyFilter.TabIndex = 5;
            this.lblApplyFilter.Text = "Apply Filter";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.AllowPrintToFile = false;
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1294, 450);
            this.Controls.Add(this.lblApplyFilter);
            this.Controls.Add(this.tabDetail);
            this.Controls.Add(this.cbQueryName);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "LogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LogView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LogView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serchOneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchTwoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showInContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem dataWrappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logInContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem dataWrappingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.ComboBox cbQueryName;
        private System.Windows.Forms.TabControl tabDetail;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.Label lblApplyFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.RichTextBox rtbMahineAndSoftwareInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem2;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
    }
}