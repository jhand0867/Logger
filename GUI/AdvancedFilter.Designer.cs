
namespace Logger
{
    partial class AdvancedFilter
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
            this.SuspendLayout();
            // 
            // cbLine1AndOr
            // 
            this.cbLine1AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine1AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine1AndOr.FormattingEnabled = true;
            this.cbLine1AndOr.Location = new System.Drawing.Point(767, 86);
            this.cbLine1AndOr.Name = "cbLine1AndOr";
            this.cbLine1AndOr.Size = new System.Drawing.Size(159, 33);
            this.cbLine1AndOr.TabIndex = 0;
            this.cbLine1AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine1AndOr.Click += new System.EventHandler(this.cbLine1AndOr_Click);
            // 
            // cbLine1Field
            // 
            this.cbLine1Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine1Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine1Field.FormattingEnabled = true;
            this.cbLine1Field.Items.AddRange(new object[] {
            "Timestamp",
            "Class",
            "Method",
            "Direction",
            "Data"});
            this.cbLine1Field.Location = new System.Drawing.Point(23, 84);
            this.cbLine1Field.Name = "cbLine1Field";
            this.cbLine1Field.Size = new System.Drawing.Size(219, 33);
            this.cbLine1Field.TabIndex = 1;
            this.cbLine1Field.SelectedIndexChanged += new System.EventHandler(this.cbLine1Field_SelectedIndexChanged_1);
            this.cbLine1Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine1Field.Click += new System.EventHandler(this.cbLine1Field_Click);
            // 
            // cbLine1Operator
            // 
            this.cbLine1Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine1Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine1Operator.FormattingEnabled = true;
            this.cbLine1Operator.Location = new System.Drawing.Point(256, 84);
            this.cbLine1Operator.Name = "cbLine1Operator";
            this.cbLine1Operator.Size = new System.Drawing.Size(169, 33);
            this.cbLine1Operator.TabIndex = 2;
            this.cbLine1Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineOperator_SelectionChangeCommitted);
            this.cbLine1Operator.Click += new System.EventHandler(this.cbLine1Operator_Click);
            // 
            // dtpTimestamp
            // 
            this.dtpTimestamp.CustomFormat = "";
            this.dtpTimestamp.Location = new System.Drawing.Point(624, 513);
            this.dtpTimestamp.Name = "dtpTimestamp";
            this.dtpTimestamp.Size = new System.Drawing.Size(286, 31);
            this.dtpTimestamp.TabIndex = 3;
            // 
            // cbLine2Operator
            // 
            this.cbLine2Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine2Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine2Operator.FormattingEnabled = true;
            this.cbLine2Operator.Location = new System.Drawing.Point(256, 135);
            this.cbLine2Operator.Name = "cbLine2Operator";
            this.cbLine2Operator.Size = new System.Drawing.Size(169, 33);
            this.cbLine2Operator.TabIndex = 6;
            this.cbLine2Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineOperator_SelectionChangeCommitted);
            this.cbLine2Operator.Click += new System.EventHandler(this.cbLine2Operator_Click);
            // 
            // cbLine2Field
            // 
            this.cbLine2Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine2Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine2Field.FormattingEnabled = true;
            this.cbLine2Field.Items.AddRange(new object[] {
            "Timestamp",
            "Class",
            "Method",
            "Direction",
            "Data"});
            this.cbLine2Field.Location = new System.Drawing.Point(23, 135);
            this.cbLine2Field.Name = "cbLine2Field";
            this.cbLine2Field.Size = new System.Drawing.Size(219, 33);
            this.cbLine2Field.TabIndex = 5;
            this.cbLine2Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine2Field.Click += new System.EventHandler(this.cbLine2Field_Click);
            // 
            // cbLine2AndOr
            // 
            this.cbLine2AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine2AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine2AndOr.FormattingEnabled = true;
            this.cbLine2AndOr.Location = new System.Drawing.Point(767, 135);
            this.cbLine2AndOr.Name = "cbLine2AndOr";
            this.cbLine2AndOr.Size = new System.Drawing.Size(159, 33);
            this.cbLine2AndOr.TabIndex = 4;
            this.cbLine2AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine2AndOr.Click += new System.EventHandler(this.cbLine2AndOr_Click);
            // 
            // cbLine4Operator
            // 
            this.cbLine4Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine4Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine4Operator.FormattingEnabled = true;
            this.cbLine4Operator.Location = new System.Drawing.Point(256, 242);
            this.cbLine4Operator.Name = "cbLine4Operator";
            this.cbLine4Operator.Size = new System.Drawing.Size(169, 33);
            this.cbLine4Operator.TabIndex = 14;
            this.cbLine4Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineOperator_SelectionChangeCommitted);
            this.cbLine4Operator.Click += new System.EventHandler(this.cbLine4Operator_Click);
            // 
            // cbLine4Field
            // 
            this.cbLine4Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine4Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine4Field.FormattingEnabled = true;
            this.cbLine4Field.Items.AddRange(new object[] {
            "Timestamp",
            "Class",
            "Method",
            "Direction",
            "Data"});
            this.cbLine4Field.Location = new System.Drawing.Point(23, 242);
            this.cbLine4Field.Name = "cbLine4Field";
            this.cbLine4Field.Size = new System.Drawing.Size(219, 33);
            this.cbLine4Field.TabIndex = 13;
            this.cbLine4Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine4Field.Click += new System.EventHandler(this.cbLine4Field_Click);
            // 
            // cbLine4AndOr
            // 
            this.cbLine4AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine4AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine4AndOr.FormattingEnabled = true;
            this.cbLine4AndOr.Location = new System.Drawing.Point(767, 242);
            this.cbLine4AndOr.Name = "cbLine4AndOr";
            this.cbLine4AndOr.Size = new System.Drawing.Size(159, 33);
            this.cbLine4AndOr.TabIndex = 12;
            this.cbLine4AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine4AndOr.Click += new System.EventHandler(this.cbLine4AndOr_Click);
            // 
            // cbLine3Operator
            // 
            this.cbLine3Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine3Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine3Operator.FormattingEnabled = true;
            this.cbLine3Operator.Location = new System.Drawing.Point(256, 191);
            this.cbLine3Operator.Name = "cbLine3Operator";
            this.cbLine3Operator.Size = new System.Drawing.Size(169, 33);
            this.cbLine3Operator.TabIndex = 11;
            this.cbLine3Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineOperator_SelectionChangeCommitted);
            this.cbLine3Operator.Click += new System.EventHandler(this.cbLine3Operator_Click);
            // 
            // cbLine3Field
            // 
            this.cbLine3Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine3Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine3Field.FormattingEnabled = true;
            this.cbLine3Field.Items.AddRange(new object[] {
            "Timestamp",
            "Class",
            "Method",
            "Direction",
            "Data"});
            this.cbLine3Field.Location = new System.Drawing.Point(23, 191);
            this.cbLine3Field.Name = "cbLine3Field";
            this.cbLine3Field.Size = new System.Drawing.Size(219, 33);
            this.cbLine3Field.TabIndex = 10;
            this.cbLine3Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine3Field.Click += new System.EventHandler(this.cbLine3Field_Click);
            // 
            // cbLine3AndOr
            // 
            this.cbLine3AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine3AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine3AndOr.FormattingEnabled = true;
            this.cbLine3AndOr.Location = new System.Drawing.Point(767, 193);
            this.cbLine3AndOr.Name = "cbLine3AndOr";
            this.cbLine3AndOr.Size = new System.Drawing.Size(159, 33);
            this.cbLine3AndOr.TabIndex = 9;
            this.cbLine3AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine3AndOr.Click += new System.EventHandler(this.cbLine3AndOr_Click);
            // 
            // cbLine6Operator
            // 
            this.cbLine6Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine6Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine6Operator.FormattingEnabled = true;
            this.cbLine6Operator.Location = new System.Drawing.Point(256, 348);
            this.cbLine6Operator.Name = "cbLine6Operator";
            this.cbLine6Operator.Size = new System.Drawing.Size(169, 33);
            this.cbLine6Operator.TabIndex = 22;
            this.cbLine6Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineOperator_SelectionChangeCommitted);
            this.cbLine6Operator.Click += new System.EventHandler(this.cbLine6Operator_Click);
            // 
            // cbLine6Field
            // 
            this.cbLine6Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine6Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine6Field.FormattingEnabled = true;
            this.cbLine6Field.Items.AddRange(new object[] {
            "Timestamp",
            "Class",
            "Method",
            "Direction",
            "Data"});
            this.cbLine6Field.Location = new System.Drawing.Point(23, 348);
            this.cbLine6Field.Name = "cbLine6Field";
            this.cbLine6Field.Size = new System.Drawing.Size(219, 33);
            this.cbLine6Field.TabIndex = 21;
            this.cbLine6Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine6Field.Click += new System.EventHandler(this.cbLine6Field_Click);
            // 
            // cbLine6AndOr
            // 
            this.cbLine6AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine6AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine6AndOr.FormattingEnabled = true;
            this.cbLine6AndOr.Location = new System.Drawing.Point(767, 348);
            this.cbLine6AndOr.Name = "cbLine6AndOr";
            this.cbLine6AndOr.Size = new System.Drawing.Size(159, 33);
            this.cbLine6AndOr.TabIndex = 20;
            this.cbLine6AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine6AndOr.Click += new System.EventHandler(this.cbLine6AndOr_Click);
            // 
            // cbLine5Operator
            // 
            this.cbLine5Operator.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine5Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine5Operator.FormattingEnabled = true;
            this.cbLine5Operator.Location = new System.Drawing.Point(256, 297);
            this.cbLine5Operator.Name = "cbLine5Operator";
            this.cbLine5Operator.Size = new System.Drawing.Size(169, 33);
            this.cbLine5Operator.TabIndex = 19;
            this.cbLine5Operator.SelectionChangeCommitted += new System.EventHandler(this.cbLineOperator_SelectionChangeCommitted);
            this.cbLine5Operator.Click += new System.EventHandler(this.cbLine5Operator_Click);
            // 
            // cbLine5Field
            // 
            this.cbLine5Field.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine5Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine5Field.FormattingEnabled = true;
            this.cbLine5Field.Items.AddRange(new object[] {
            "Timestamp",
            "Class",
            "Method",
            "Direction",
            "Data"});
            this.cbLine5Field.Location = new System.Drawing.Point(23, 297);
            this.cbLine5Field.Name = "cbLine5Field";
            this.cbLine5Field.Size = new System.Drawing.Size(219, 33);
            this.cbLine5Field.TabIndex = 18;
            this.cbLine5Field.SelectionChangeCommitted += new System.EventHandler(this.cbLineField_SelectionChangeCommitted);
            this.cbLine5Field.Click += new System.EventHandler(this.cbLine5Field_Click);
            // 
            // cbLine5AndOr
            // 
            this.cbLine5AndOr.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine5AndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLine5AndOr.FormattingEnabled = true;
            this.cbLine5AndOr.Location = new System.Drawing.Point(767, 299);
            this.cbLine5AndOr.Name = "cbLine5AndOr";
            this.cbLine5AndOr.Size = new System.Drawing.Size(159, 33);
            this.cbLine5AndOr.TabIndex = 17;
            this.cbLine5AndOr.SelectionChangeCommitted += new System.EventHandler(this.cbLineAndOr_SelectionChangeCommitted);
            this.cbLine5AndOr.Click += new System.EventHandler(this.cbLine5AndOr_Click);
            // 
            // btnOK
            // 
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(767, 402);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(118, 44);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "Apply";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(108, 402);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 44);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rtbSQLResult
            // 
            this.rtbSQLResult.Location = new System.Drawing.Point(12, 466);
            this.rtbSQLResult.Name = "rtbSQLResult";
            this.rtbSQLResult.Size = new System.Drawing.Size(980, 114);
            this.rtbSQLResult.TabIndex = 34;
            this.rtbSQLResult.Text = "";
            // 
            // cbLine1Value
            // 
            this.cbLine1Value.FormattingEnabled = true;
            this.cbLine1Value.Location = new System.Drawing.Point(431, 84);
            this.cbLine1Value.Name = "cbLine1Value";
            this.cbLine1Value.Size = new System.Drawing.Size(318, 33);
            this.cbLine1Value.TabIndex = 35;
            this.cbLine1Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine1Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine2Value
            // 
            this.cbLine2Value.FormattingEnabled = true;
            this.cbLine2Value.Location = new System.Drawing.Point(431, 135);
            this.cbLine2Value.Name = "cbLine2Value";
            this.cbLine2Value.Size = new System.Drawing.Size(318, 33);
            this.cbLine2Value.TabIndex = 36;
            this.cbLine2Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine2Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine3Value
            // 
            this.cbLine3Value.FormattingEnabled = true;
            this.cbLine3Value.Location = new System.Drawing.Point(431, 193);
            this.cbLine3Value.Name = "cbLine3Value";
            this.cbLine3Value.Size = new System.Drawing.Size(318, 33);
            this.cbLine3Value.TabIndex = 37;
            this.cbLine3Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine3Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine4Value
            // 
            this.cbLine4Value.FormattingEnabled = true;
            this.cbLine4Value.Location = new System.Drawing.Point(431, 242);
            this.cbLine4Value.Name = "cbLine4Value";
            this.cbLine4Value.Size = new System.Drawing.Size(318, 33);
            this.cbLine4Value.TabIndex = 38;
            this.cbLine4Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine4Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine5Value
            // 
            this.cbLine5Value.FormattingEnabled = true;
            this.cbLine5Value.Location = new System.Drawing.Point(431, 297);
            this.cbLine5Value.Name = "cbLine5Value";
            this.cbLine5Value.Size = new System.Drawing.Size(318, 33);
            this.cbLine5Value.TabIndex = 39;
            this.cbLine5Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine5Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // cbLine6Value
            // 
            this.cbLine6Value.FormattingEnabled = true;
            this.cbLine6Value.Location = new System.Drawing.Point(431, 348);
            this.cbLine6Value.Name = "cbLine6Value";
            this.cbLine6Value.Size = new System.Drawing.Size(318, 33);
            this.cbLine6Value.TabIndex = 40;
            this.cbLine6Value.Leave += new System.EventHandler(this.cbLineValue_Leave);
            this.cbLine6Value.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLineValue_MouseClick);
            // 
            // AdvancedFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1006, 634);
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
            this.Name = "AdvancedFilter";
            this.Text = "AdvancedFilter";
            this.ResumeLayout(false);

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
    }
}