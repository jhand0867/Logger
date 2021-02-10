using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Forms.Application;

namespace Logger
{
    public partial class AdvancedFilter : Form
    {

        private string[] fields = { "", "[group1]", "[group4]", "[group5]", "[group6]", "[group8]" };
        private string[] conditions = { "", "Like", "=", "<>", ">", "<" };
        private string[] andOr = { "", "AND", "OR" };

        internal SQLSearchCondition[] gridrows = new SQLSearchCondition[6];


        public AdvancedFilter()
        {
            InitializeComponent();

            dtpTimestamp.Format = DateTimePickerFormat.Custom;
            dtpTimestamp.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            dtpTimestamp.ShowUpDown = true;

            // intitialize sql fields

            SQLSearchCondition sc = new SQLSearchCondition();
            
            for (int x =0; x<6; x++)
            {
                gridrows[x] = new SQLSearchCondition("", "", "", "","");
            }

        }

        public AdvancedFilter(object sQLSearchConditions)
        {
            InitializeComponent();

            dtpTimestamp.Format = DateTimePickerFormat.Custom;
            dtpTimestamp.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            dtpTimestamp.ShowUpDown = true;

            gridrows = (SQLSearchCondition[])sQLSearchConditions;

            for (int x = 0; x < 6; x++)
            {
                string fieldName = "cbLine" + (x+1).ToString("0") + "Field";
                string fieldCondition = "cbLine" + (x+1).ToString("0") + "Operator";
                string fieldValue = "cbLine" + (x + 1).ToString("0") + "Value";
                string fieldAndOr = "cbLine" + (x + 1).ToString("0") + "AndOr";

            object ob = this.Controls[fieldName];
            ComboBox cb = (ComboBox)ob;
            addFieldNames(cb);
            int idx = Array.IndexOf(fields, gridrows[x].SQLFieldName);
            cb.SelectedIndex = idx;

            ob = this.Controls[fieldCondition];
            cb = (ComboBox)ob;
            addOperators(cb);
            idx = Array.IndexOf(conditions, gridrows[x].SQLCondition);
            cb.SelectedIndex = idx;

            ob = this.Controls[fieldValue];
            cb = (ComboBox)ob;
            cb.Text = gridrows[x].SQLFieldValue;

                ob = this.Controls[fieldAndOr];
                cb = (ComboBox)ob;
                addAndOr(cb);
                idx = Array.IndexOf(andOr, gridrows[x].SQLAndOr);
                cb.SelectedIndex = idx;
            }
        }

        private void cbLine1Field_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void addFieldNames(ComboBox cbField)
        {
            // adding options to combo
            cbField.Items.Clear();
            cbField.Items.Add(String.Empty);
            cbField.Items.Add("Timestamp");
            cbField.Items.Add("Class");
            cbField.Items.Add("Method");
            cbField.Items.Add("Direction");
            cbField.Items.Add("Data");
        }

        // Fields selection  

        
        private void cbLineField_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLFieldOutput = cb.SelectedItem.ToString();
            gridrows[i].SQLFieldName = fields[cb.SelectedIndex];
            preview();

        }

        // Operators selection

        private void cbLineOperator_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLCondition = conditions[cb.SelectedIndex];
            preview();
        }


        // Text selection  

        private void cbLineValue_Leave(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLFieldValue = cb.Text;
            preview();
        }

        //  condition selection 

        private void cbLineAndOr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLAndOr = andOr[cb.SelectedIndex];
            preview();
        }

        private void cbLine1Field_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbLine1Operator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void addOperators(ComboBox cbOperators)
        {
            string iStr = cbOperators.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            cbOperators.Items.Clear();
            cbOperators.Items.Add(String.Empty);

            if (gridrows[i].SQLFieldOutput == "") 
                return;

            cbOperators.Items.Add("Like");

            if (gridrows[i].SQLFieldOutput != "Data")
            {
                cbOperators.Items.Add("Equal");
                cbOperators.Items.Add("Not Equal");
                cbOperators.Items.Add("Greater Than");
                cbOperators.Items.Add("Less Than");
            }

        }

        private void cbLine1AndOr_Click(object sender, EventArgs e)
        {
            addAndOr(sender as ComboBox);
        }

        private void addAndOr(ComboBox cbAndOr)
        {
            cbAndOr.Items.Clear();
            cbAndOr.Items.Add(String.Empty);
            cbAndOr.Items.Add("And");
            cbAndOr.Items.Add("Or");
        }

        private void cbLine2Field_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbLine2Field_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void cbLine3Field_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void cbLine4Field_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void cbLine5Field_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void cbLine6Field_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void cbLine2Operator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void cbLine3Operator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void cbLine4Operator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void cbLine5Operator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void cbLine6Operator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void cbLine2AndOr_Click(object sender, EventArgs e)
        {
            addAndOr(sender as ComboBox);
        }

        private void cbLine3AndOr_Click(object sender, EventArgs e)
        {
            addAndOr(sender as ComboBox);
        }

        private void cbLine4AndOr_Click(object sender, EventArgs e)
        {
            addAndOr(sender as ComboBox);
        }

        private void cbLine5AndOr_Click(object sender, EventArgs e)
        {
            addAndOr(sender as ComboBox);
        }

        private void cbLine6AndOr_Click(object sender, EventArgs e)
        {
            addAndOr(sender as ComboBox);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            /*             
              SELECT [id],[logkey],[group1] as 'Timestamp',
                     [group2] as 'Log Level',[group3] as 'File Name',
                     [group4] as 'Class',[group5] as 'Method',
                     [group6] as 'Type',[group7],
                     [group8] as 'Log Data',[group9],
                     [prjKey],[logID] FROM [loginfo] 
              WHERE logID =9 and 
             
             */

            string sqlLike = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string temp = "";

            for (int i = 0; i < 6; i++)
            {

                if (gridrows[i].SQLFieldName != "" && gridrows[i].SQLCondition != "" && gridrows[i].SQLFieldValue != "")
                {
                    temp = gridrows[i].SQLFieldValue;

                    if (gridrows[i].SQLCondition == "Like")
                    {
                        if (gridrows[i].SQLFieldValue.StartsWith("["))
                            temp = gridrows[i].SQLFieldValue.Substring(1, gridrows[i].SQLFieldValue.Length - 1);
                    
                        temp = "%" + temp + "%";
                    }
                    sqlLike += " " + gridrows[i].SQLFieldName + gridrows[i].SQLCondition +
                           " '" + temp + "' ";
                }

                if ( i < 5 &&
                    gridrows[i].SQLAndOr != "" &&
                    gridrows[i+1].SQLFieldName != "" && gridrows[i+1].SQLCondition != "" && gridrows[i+1].SQLFieldValue != "")
                {
                    sqlLike += gridrows[i].SQLAndOr;
                }

            }

            if (sqlLike != "")
            {
                object ob = Application.OpenForms["LogView"].Controls["dgvLog"];
                DataGridView dg = (DataGridView)ob;
                dg.DataSource = App.Prj.getALogByIDWithCriteria(ProjectData.logID, "", sqlLike);
                dg.Refresh();
            }
        }

        private void preview()
        {
            rtbSQLResult.Text = " ";

            for (int i = 0; i < 6; i++)
            {
                if (gridrows[i].SQLFieldName != "" && gridrows[i].SQLCondition != "" && gridrows[i].SQLFieldValue != "")
                {
                    rtbSQLResult.Text = rtbSQLResult.Text + gridrows[i].SQLFieldOutput + " " +
                    gridrows[i].SQLCondition + " " + gridrows[i].SQLFieldValue + " ";
                }

                if (i < 5 &&
                    gridrows[i].SQLAndOr != "" &&
                    gridrows[i+1].SQLFieldName != "" && gridrows[i+1].SQLCondition != "" && gridrows[i+1].SQLFieldValue != "")
                {
                    rtbSQLResult.Text = rtbSQLResult.Text + gridrows[i].SQLAndOr + " "; 
                }
            }


        }

        private void cbLineValue_MouseClick(object sender, MouseEventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            cb.Items.Clear();
            cb.Items.Add(string.Empty);

            string groupName = gridrows[i].SQLFieldName;

            if (groupName != "")
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = App.Prj.getGroupOptions(ProjectData.logID, groupName);

                foreach (DataRow theRow in dt.Rows)
                {
                    string str = (theRow[0].ToString());
                    cb.Items.Add(str);

                }

            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveQuery saveQuery = new SaveQuery(gridrows);
            saveQuery.ShowDialog();
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadQuery loadQuery = new LoadQuery();
            loadQuery.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
