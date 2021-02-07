using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    public partial class AdvancedFilter : Form
    {

        private string[] fields = { "", "[group1]", "[group4]", "[group5]", "[group6]", "[group8]" };
        private string[] operators = { "", "Like", "=", "<>", ">", "<" };
        private string[] conditions = { "", "AND", "OR" };

        private SQLSearchCondition[] gridrows = new SQLSearchCondition[6];


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

            gridrows[i].FieldOutput = cb.SelectedItem.ToString();
            gridrows[i].FieldName = fields[cb.SelectedIndex];
            preview();

        }

        // Operators selection

        private void cbLineOperator_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].Condition = operators[cb.SelectedIndex];
            preview();
        }


        // Text selection  

        private void cbLineValue_Leave(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].FieldValue = cb.Text;
            preview();
        }

        //  condition selection 

        private void cbLineAndOr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].AndOr = conditions[cb.SelectedIndex];
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

            if (gridrows[i].FieldOutput == "") 
                return;

            cbOperators.Items.Add("Like");

            if (gridrows[i].FieldOutput != "Data")
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
            DataTable dt = new DataTable();
            string temp = "";

            for (int i = 0; i < 6; i++)
            {

                if (gridrows[i].FieldName != "" && gridrows[i].Condition != "" && gridrows[i].FieldValue != "")
                {
                    temp = gridrows[i].FieldValue;

                    if (gridrows[i].Condition == "Like")
                    {
                        if (gridrows[i].FieldValue.StartsWith("["))
                            temp = gridrows[i].FieldValue.Substring(1, gridrows[i].FieldValue.Length - 1);
                    
                        temp = "%" + temp + "%";
                    }
                    sqlLike += " " + gridrows[i].FieldName + gridrows[i].Condition +
                           " '" + temp + "' ";
                }

                if ( i < 5 &&
                    gridrows[i].AndOr != "" &&
                    gridrows[i+1].FieldName != "" && gridrows[i+1].Condition != "" && gridrows[i+1].FieldValue != "")
                {
                    sqlLike += gridrows[i].AndOr;
                }

            }

            if (sqlLike != "")
            {
                object ob = Application.OpenForms["LogView"].Controls[0];
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
                if (gridrows[i].FieldName != "" && gridrows[i].Condition != "" && gridrows[i].FieldValue != "")
                {
                    rtbSQLResult.Text = rtbSQLResult.Text + gridrows[i].FieldOutput + " " +
                    gridrows[i].Condition + " " + gridrows[i].FieldValue + " ";
                }

                if (i < 5 &&
                    gridrows[i].AndOr != "" &&
                    gridrows[i+1].FieldName != "" && gridrows[i+1].Condition != "" && gridrows[i+1].FieldValue != "")
                {
                    rtbSQLResult.Text = rtbSQLResult.Text + gridrows[i].AndOr + " "; 
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

            string groupName = "";

            if (gridrows[i].FieldOutput == "Class") groupName = "group4";
            else if (gridrows[i].FieldOutput == "Method") groupName = "group5";
            else if (gridrows[i].FieldOutput == "Direction") groupName = "group6";

            if (groupName != "")
            {
                DataTable dt = new DataTable();
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
    }
}
