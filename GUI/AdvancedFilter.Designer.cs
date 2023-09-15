
namespace Logger
{
    partial class AdvancedFilterw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedFilterw));
            this.cbLine1AndOr = new System.Windows.Forms.ComboBox();
            this.cbLine1Field = new System.Windows.Forms.ComboBox();
            this.cbLine1Operator = new System.Windows.Forms.ComboBox();
            this.dtpTimestamp = new System.Windows.Forms.DateTimePicker();
            this.cbLine2Operator = new System.Windows.Forms.ComboBox();
            this.cbLine2Field = new System.Windows.Forms.ComboBox();
            this.cbLine2AndOr = new System.Windows.Forms.ComboBox();
            this.cbLine4Operator = new System.Windows.Forms.ComboBox();
            this.cbLine4Field = new System.Windows.Forms.ComboBox();
            this.cbLine4AndOr = new System.Windows.Forms.ComboBox();
            this.cbLine3Operator = new System.Windows.Forms.ComboBox();
            this.cbLine3Field = new System.Windows.Forms.ComboBox();
            this.cbLine3AndOr = new System.Windows.Forms.ComboBox();
            this.cbLine6Operator = new System.Windows.Forms.ComboBox();
            this.cbLine6Field = new System.Windows.Forms.ComboBox();
            this.cbLine6AndOr = new System.Windows.Forms.ComboBox();
            this.cbLine5Operator = new System.Windows.Forms.ComboBox();
            this.cbLine5Field = new System.Windows.Forms.ComboBox();
            this.cbLine5AndOr = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rtbSQLResult = new System.Windows.Forms.RichTextBox();
            this.cbLine1Value = new System.Windows.Forms.ComboBox();
            this.cbLine2Value = new System.Windows.Forms.ComboBox();
            this.cbLine3Value = new System.Windows.Forms.ComboBox();
            this.cbLine4Value = new System.Windows.Forms.ComboBox();
            this.cbLine5Value = new System.Windows.Forms.ComboBox();
            this.cbLine6Value = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLine1AndOr
            // 
            this.cbLine1AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine1AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine1AndOr.FormattingEnabled = true;
            this.cbLine1AndOr.Location = new System.Drawing.Point(403, 45);
            this.cbLine1AndOr.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine1AndOr.Name = "cbLine1AndOr";
            this.cbLine1AndOr.Size = new System.Drawing.Size(82, 21);
            this.cbLine1AndOr.TabIndex = 0;
            this.cbLine1AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine1AndOr.Click += new System.EventHandler(this.cbLineAndOr_Click);
            // 
            // cbLine1Field
            // 
            this.cbLine1Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine1Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine1Field.FormattingEnabled = true;
            this.cbLine1Field.Location = new System.Drawing.Point(31, 44);
            this.cbLine1Field.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine1Field.Name = "cbLine1Field";
            this.cbLine1Field.Size = new System.Drawing.Size(112, 21);
            this.cbLine1Field.TabIndex = 1;
            this.cbLine1Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine1Field.Click += new System.EventHandler(this.cbLineField_Click);
            // 
            // cbLine1Operator
            // 
            this.cbLine1Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine1Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine1Operator.FormattingEnabled = true;
            this.cbLine1Operator.Location = new System.Drawing.Point(148, 44);
            this.cbLine1Operator.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine1Operator.Name = "cbLine1Operator";
            this.cbLine1Operator.Size = new System.Drawing.Size(86, 21);
            this.cbLine1Operator.TabIndex = 2;
            this.cbLine1Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineCondition_SelectionChangeCommitted);
            this.cbLine1Operator.Click += new System.EventHandler(this.cbLineOperator_Click);
            // 
            // dtpTimestamp
            // 
            this.dtpTimestamp.CustomFormat = "";
            this.dtpTimestamp.Location = new System.Drawing.Point(312, 267);
            this.dtpTimestamp.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTimestamp.Name = "dtpTimestamp";
            this.dtpTimestamp.Size = new System.Drawing.Size(145, 20);
            this.dtpTimestamp.TabIndex = 3;
            // 
            // cbLine2Operator
            // 
            this.cbLine2Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine2Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine2Operator.FormattingEnabled = true;
            this.cbLine2Operator.Location = new System.Drawing.Point(148, 70);
            this.cbLine2Operator.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine2Operator.Name = "cbLine2Operator";
            this.cbLine2Operator.Size = new System.Drawing.Size(86, 21);
            this.cbLine2Operator.TabIndex = 6;
            this.cbLine2Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineCondition_SelectionChangeCommitted);
            this.cbLine2Operator.Click += new System.EventHandler(this.cbLineOperator_Click);
            // 
            // cbLine2Field
            // 
            this.cbLine2Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine2Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine2Field.FormattingEnabled = true;
            this.cbLine2Field.Location = new System.Drawing.Point(31, 70);
            this.cbLine2Field.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine2Field.Name = "cbLine2Field";
            this.cbLine2Field.Size = new System.Drawing.Size(112, 21);
            this.cbLine2Field.TabIndex = 5;
            this.cbLine2Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine2Field.Click += new System.EventHandler(this.cbLineField_Click);
            // 
            // cbLine2AndOr
            // 
            this.cbLine2AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine2AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine2AndOr.FormattingEnabled = true;
            this.cbLine2AndOr.Location = new System.Drawing.Point(403, 70);
            this.cbLine2AndOr.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine2AndOr.Name = "cbLine2AndOr";
            this.cbLine2AndOr.Size = new System.Drawing.Size(82, 21);
            this.cbLine2AndOr.TabIndex = 4;
            this.cbLine2AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine2AndOr.Click += new System.EventHandler(this.cbLineAndOr_Click);
            // 
            // cbLine4Operator
            // 
            this.cbLine4Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine4Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine4Operator.FormattingEnabled = true;
            this.cbLine4Operator.Location = new System.Drawing.Point(148, 126);
            this.cbLine4Operator.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine4Operator.Name = "cbLine4Operator";
            this.cbLine4Operator.Size = new System.Drawing.Size(86, 21);
            this.cbLine4Operator.TabIndex = 14;
            this.cbLine4Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineCondition_SelectionChangeCommitted);
            this.cbLine4Operator.Click += new System.EventHandler(this.cbLineOperator_Click);
            // 
            // cbLine4Field
            // 
            this.cbLine4Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine4Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine4Field.FormattingEnabled = true;
            this.cbLine4Field.Location = new System.Drawing.Point(31, 126);
            this.cbLine4Field.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine4Field.Name = "cbLine4Field";
            this.cbLine4Field.Size = new System.Drawing.Size(112, 21);
            this.cbLine4Field.TabIndex = 13;
            this.cbLine4Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine4Field.Click += new System.EventHandler(this.cbLineField_Click);
            // 
            // cbLine4AndOr
            // 
            this.cbLine4AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine4AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine4AndOr.FormattingEnabled = true;
            this.cbLine4AndOr.Location = new System.Drawing.Point(403, 126);
            this.cbLine4AndOr.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine4AndOr.Name = "cbLine4AndOr";
            this.cbLine4AndOr.Size = new System.Drawing.Size(82, 21);
            this.cbLine4AndOr.TabIndex = 12;
            this.cbLine4AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine4AndOr.Click += new System.EventHandler(this.cbLineAndOr_Click);
            // 
            // cbLine3Operator
            // 
            this.cbLine3Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine3Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine3Operator.FormattingEnabled = true;
            this.cbLine3Operator.Location = new System.Drawing.Point(148, 99);
            this.cbLine3Operator.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine3Operator.Name = "cbLine3Operator";
            this.cbLine3Operator.Size = new System.Drawing.Size(86, 21);
            this.cbLine3Operator.TabIndex = 11;
            this.cbLine3Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineCondition_SelectionChangeCommitted);
            this.cbLine3Operator.Click += new System.EventHandler(this.cbLineOperator_Click);
            // 
            // cbLine3Field
            // 
            this.cbLine3Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine3Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine3Field.FormattingEnabled = true;
            this.cbLine3Field.Location = new System.Drawing.Point(31, 99);
            this.cbLine3Field.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine3Field.Name = "cbLine3Field";
            this.cbLine3Field.Size = new System.Drawing.Size(112, 21);
            this.cbLine3Field.TabIndex = 10;
            this.cbLine3Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine3Field.Click += new System.EventHandler(this.cbLineField_Click);
            // 
            // cbLine3AndOr
            // 
            this.cbLine3AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine3AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine3AndOr.FormattingEnabled = true;
            this.cbLine3AndOr.Location = new System.Drawing.Point(403, 100);
            this.cbLine3AndOr.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine3AndOr.Name = "cbLine3AndOr";
            this.cbLine3AndOr.Size = new System.Drawing.Size(82, 21);
            this.cbLine3AndOr.TabIndex = 9;
            this.cbLine3AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine3AndOr.Click += new System.EventHandler(this.cbLineAndOr_Click);
            // 
            // cbLine6Operator
            // 
            this.cbLine6Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine6Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine6Operator.FormattingEnabled = true;
            this.cbLine6Operator.Location = new System.Drawing.Point(148, 181);
            this.cbLine6Operator.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine6Operator.Name = "cbLine6Operator";
            this.cbLine6Operator.Size = new System.Drawing.Size(86, 21);
            this.cbLine6Operator.TabIndex = 22;
            this.cbLine6Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineCondition_SelectionChangeCommitted);
            this.cbLine6Operator.Click += new System.EventHandler(this.cbLineOperator_Click);
            // 
            // cbLine6Field
            // 
            this.cbLine6Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine6Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine6Field.FormattingEnabled = true;
            this.cbLine6Field.Location = new System.Drawing.Point(31, 181);
            this.cbLine6Field.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine6Field.Name = "cbLine6Field";
            this.cbLine6Field.Size = new System.Drawing.Size(112, 21);
            this.cbLine6Field.TabIndex = 21;
            this.cbLine6Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine6Field.Click += new System.EventHandler(this.cbLineField_Click);
            // 
            // cbLine6AndOr
            // 
            this.cbLine6AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine6AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine6AndOr.FormattingEnabled = true;
            this.cbLine6AndOr.Location = new System.Drawing.Point(403, 181);
            this.cbLine6AndOr.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine6AndOr.Name = "cbLine6AndOr";
            this.cbLine6AndOr.Size = new System.Drawing.Size(82, 21);
            this.cbLine6AndOr.TabIndex = 20;
            this.cbLine6AndOr.Visible = false;
            this.cbLine6AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine6AndOr.Click += new System.EventHandler(this.cbLineAndOr_Click);
            // 
            // cbLine5Operator
            // 
            this.cbLine5Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine5Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine5Operator.FormattingEnabled = true;
            this.cbLine5Operator.Location = new System.Drawing.Point(148, 154);
            this.cbLine5Operator.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine5Operator.Name = "cbLine5Operator";
            this.cbLine5Operator.Size = new System.Drawing.Size(86, 21);
            this.cbLine5Operator.TabIndex = 19;
            this.cbLine5Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineCondition_SelectionChangeCommitted);
            this.cbLine5Operator.Click += new System.EventHandler(this.cbLineOperator_Click);
            // 
            // cbLine5Field
            // 
            this.cbLine5Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine5Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine5Field.FormattingEnabled = true;
            this.cbLine5Field.Location = new System.Drawing.Point(31, 154);
            this.cbLine5Field.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine5Field.Name = "cbLine5Field";
            this.cbLine5Field.Size = new System.Drawing.Size(112, 21);
            this.cbLine5Field.TabIndex = 18;
            this.cbLine5Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine5Field.Click += new System.EventHandler(this.cbLineField_Click);
            // 
            // cbLine5AndOr
            // 
            this.cbLine5AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine5AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine5AndOr.FormattingEnabled = true;
            this.cbLine5AndOr.Location = new System.Drawing.Point(403, 155);
            this.cbLine5AndOr.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine5AndOr.Name = "cbLine5AndOr";
            this.cbLine5AndOr.Size = new System.Drawing.Size(82, 21);
            this.cbLine5AndOr.TabIndex = 17;
            this.cbLine5AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine5AndOr.Click += new System.EventHandler(this.cbLineAndOr_Click);
            // 
            // btnOK
            // 
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(384, 209);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 23);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "Apply";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(54, 209);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rtbSQLResult
            // 
            this.rtbSQLResult.BackColor = System.Drawing.SystemColors.Window;
            this.rtbSQLResult.Location = new System.Drawing.Point(6, 242);
            this.rtbSQLResult.Margin = new System.Windows.Forms.Padding(2);
            this.rtbSQLResult.Name = "rtbSQLResult";
            this.rtbSQLResult.ReadOnly = true;
            this.rtbSQLResult.Size = new System.Drawing.Size(492, 62);
            this.rtbSQLResult.TabIndex = 34;
            this.rtbSQLResult.Text = "";
            // 
            // cbLine1Value
            // 
            this.cbLine1Value.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbLine1Value.FormattingEnabled = true;
            this.cbLine1Value.Location = new System.Drawing.Point(235, 44);
            this.cbLine1Value.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine1Value.Name = "cbLine1Value";
            this.cbLine1Value.Size = new System.Drawing.Size(161, 21);
            this.cbLine1Value.TabIndex = 35;
            this.cbLine1Value.SelectedIndexChanged += new System.EventHandler(this.cbLineValue_SelectedIndexChanged);
            this.cbLine1Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine1Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine2Value
            // 
            this.cbLine2Value.FormattingEnabled = true;
            this.cbLine2Value.Location = new System.Drawing.Point(235, 70);
            this.cbLine2Value.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine2Value.Name = "cbLine2Value";
            this.cbLine2Value.Size = new System.Drawing.Size(161, 21);
            this.cbLine2Value.TabIndex = 36;
            this.cbLine2Value.SelectedIndexChanged += new System.EventHandler(this.cbLineValue_SelectedIndexChanged);
            this.cbLine2Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine2Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine3Value
            // 
            this.cbLine3Value.FormattingEnabled = true;
            this.cbLine3Value.Location = new System.Drawing.Point(235, 100);
            this.cbLine3Value.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine3Value.Name = "cbLine3Value";
            this.cbLine3Value.Size = new System.Drawing.Size(161, 21);
            this.cbLine3Value.TabIndex = 37;
            this.cbLine3Value.SelectedIndexChanged += new System.EventHandler(this.cbLineValue_SelectedIndexChanged);
            this.cbLine3Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine3Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine4Value
            // 
            this.cbLine4Value.FormattingEnabled = true;
            this.cbLine4Value.Location = new System.Drawing.Point(235, 126);
            this.cbLine4Value.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine4Value.Name = "cbLine4Value";
            this.cbLine4Value.Size = new System.Drawing.Size(161, 21);
            this.cbLine4Value.TabIndex = 38;
            this.cbLine4Value.SelectedIndexChanged += new System.EventHandler(this.cbLineValue_SelectedIndexChanged);
            this.cbLine4Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine4Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine5Value
            // 
            this.cbLine5Value.FormattingEnabled = true;
            this.cbLine5Value.Location = new System.Drawing.Point(235, 154);
            this.cbLine5Value.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine5Value.Name = "cbLine5Value";
            this.cbLine5Value.Size = new System.Drawing.Size(161, 21);
            this.cbLine5Value.TabIndex = 39;
            this.cbLine5Value.SelectedIndexChanged += new System.EventHandler(this.cbLineValue_SelectedIndexChanged);
            this.cbLine5Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine5Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine6Value
            // 
            this.cbLine6Value.FormattingEnabled = true;
            this.cbLine6Value.Location = new System.Drawing.Point(235, 181);
            this.cbLine6Value.Margin = new System.Windows.Forms.Padding(2);
            this.cbLine6Value.Name = "cbLine6Value";
            this.cbLine6Value.Size = new System.Drawing.Size(161, 21);
            this.cbLine6Value.TabIndex = 40;
            this.cbLine6Value.SelectedIndexChanged += new System.EventHandler(this.cbLineValue_SelectedIndexChanged);
            this.cbLine6Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine6Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.refreshDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(503, 24);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(45, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            this.clearToolStripMenuItem.MouseLeave += new System.EventHandler(this.clearToolStripMenuItem_MouseLeave);
            // 
            // refreshDataToolStripMenuItem
            // 
            this.refreshDataToolStripMenuItem.Name = "refreshDataToolStripMenuItem";
            this.refreshDataToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.refreshDataToolStripMenuItem.Text = "Refresh Data";
            this.refreshDataToolStripMenuItem.Click += new System.EventHandler(this.refreshDataToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "1.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 15);
            this.label2.TabIndex = 43;
            this.label2.Text = "2.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 44;
            this.label3.Text = "3.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "4.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(11, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 15);
            this.label5.TabIndex = 46;
            this.label5.Text = "5.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 181);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 15);
            this.label6.TabIndex = 47;
            this.label6.Text = "6.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(28, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "Field";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(145, 27);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 15);
            this.label8.TabIndex = 49;
            this.label8.Text = "Condition";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(235, 27);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 15);
            this.label9.TabIndex = 50;
            this.label9.Text = "Value";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(407, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 51;
            this.label10.Text = "And/Or";
            // 
            // AdvancedFilterw
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(503, 309);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLine6Value);
            this.Controls.Add(this.cbLine5Value);
            this.Controls.Add(this.cbLine4Value);
            this.Controls.Add(this.cbLine3Value);
            this.Controls.Add(this.cbLine2Value);
            this.Controls.Add(this.cbLine1Value);
            this.Controls.Add(this.rtbSQLResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbLine6Operator);
            this.Controls.Add(this.cbLine6Field);
            this.Controls.Add(this.cbLine6AndOr);
            this.Controls.Add(this.cbLine5Operator);
            this.Controls.Add(this.cbLine5Field);
            this.Controls.Add(this.cbLine5AndOr);
            this.Controls.Add(this.cbLine4Operator);
            this.Controls.Add(this.cbLine4Field);
            this.Controls.Add(this.cbLine4AndOr);
            this.Controls.Add(this.cbLine3Operator);
            this.Controls.Add(this.cbLine3Field);
            this.Controls.Add(this.cbLine3AndOr);
            this.Controls.Add(this.cbLine2Operator);
            this.Controls.Add(this.cbLine2Field);
            this.Controls.Add(this.cbLine2AndOr);
            this.Controls.Add(this.dtpTimestamp);
            this.Controls.Add(this.cbLine1Operator);
            this.Controls.Add(this.cbLine1Field);
            this.Controls.Add(this.cbLine1AndOr);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedFilterw";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdvancedFilter";
            this.Load += new System.EventHandler(this.AdvancedFilterw_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLine1AndOr;
        private System.Windows.Forms.ComboBox cbLine1Field;
        private System.Windows.Forms.ComboBox cbLine1Operator;
        private System.Windows.Forms.DateTimePicker dtpTimestamp;
        private System.Windows.Forms.ComboBox cbLine2Operator;
        private System.Windows.Forms.ComboBox cbLine2Field;
        private System.Windows.Forms.ComboBox cbLine2AndOr;
        private System.Windows.Forms.ComboBox cbLine4Operator;
        private System.Windows.Forms.ComboBox cbLine4Field;
        private System.Windows.Forms.ComboBox cbLine4AndOr;
        private System.Windows.Forms.ComboBox cbLine3Operator;
        private System.Windows.Forms.ComboBox cbLine3Field;
        private System.Windows.Forms.ComboBox cbLine3AndOr;
        private System.Windows.Forms.ComboBox cbLine6Operator;
        private System.Windows.Forms.ComboBox cbLine6Field;
        private System.Windows.Forms.ComboBox cbLine6AndOr;
        private System.Windows.Forms.ComboBox cbLine5Operator;
        private System.Windows.Forms.ComboBox cbLine5Field;
        private System.Windows.Forms.ComboBox cbLine5AndOr;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox rtbSQLResult;
        private System.Windows.Forms.ComboBox cbLine1Value;
        private System.Windows.Forms.ComboBox cbLine2Value;
        private System.Windows.Forms.ComboBox cbLine3Value;
        private System.Windows.Forms.ComboBox cbLine4Value;
        private System.Windows.Forms.ComboBox cbLine5Value;
        private System.Windows.Forms.ComboBox cbLine6Value;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDataToolStripMenuItem;
    }
}