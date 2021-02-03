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
        private string[] fieldSelected = { "", "", "", "", "", "" };
        private string[] fieldOutput = { "", "", "", "", "", "" };
        private string[] operatorSelected = { "", "", "", "", "", "" };
        private string[] textSelected = { "", "", "", "", "", "" };
        private string[] andOrSelected = { "", "", "", "", "", "" };

        public AdvancedFilter()
        {
            InitializeComponent();


            dtpTimestamp.Format = DateTimePickerFormat.Custom;
            dtpTimestamp.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            dtpTimestamp.ShowUpDown = true;

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

            fieldSelected[i] = fields[cb.SelectedIndex];
            fieldOutput[i] = cb.SelectedItem.ToString();

            
        }

        // Operators selection

        private void cbLineOperator_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            operatorSelected[i] = operators[cb.SelectedIndex];
        }


        // Text selection  

        //private void tbLineValue_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox tb = sender as TextBox;
        //    string iStr = tb.Name.Substring(6, 1);
        //    int i = Convert.ToInt32(iStr) - 1;

        //    textSelected[i] = tb.Text;
        //    preview();
        //}

        private void cbLineValue_Leave(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            textSelected[i] = cb.Text;
            preview();
        }

        //  condition selection 

        private void cbLineAndOr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            andOrSelected[i] = conditions[cb.SelectedIndex];
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

            if (fieldOutput[i] == "") 
                return;

            if ( (fieldOutput[i] == "Timestamp") ||
                 (fieldOutput[i] == "Direction") ||
                 (fieldOutput[i] == "Data") )

            {
                cbOperators.Items.Add("Like");
                return;
            }

            cbOperators.Items.Add("Like");
            cbOperators.Items.Add("Equal");
            cbOperators.Items.Add("Not Equal");
            cbOperators.Items.Add("Greater Than");
            cbOperators.Items.Add("Less Than");
            return;

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

            //System.Windows.Forms.ComboBox[] comboBoxes = new System.Windows.Forms.ComboBox()[];


            // comboBoxes[0] = new ComboBox();
            ////addAndOr(comboBoxes[0]);
            //comboBoxes[0].Name = "line1";
            //comboBoxes[0].Size = Size(100, 100);
            //comboBoxes[0].Show();

            string sqlLike = " AND";

            for (int i = 0; i < 6; i++)
            {
                sqlLike += " " + fieldSelected[i] + operatorSelected[i] +
                           " '" + textSelected[i] + "' " + andOrSelected[i];
                if (andOrSelected[i] == "" || andOrSelected[i] == String.Empty)
                    break;


            }

            //  this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group4", sqlLike);

        }

        private void preview()
        {
            rtbSQLResult.Text = " ";

            for (int i = 0; i < 6; i++)
            {
                if (fieldOutput[i] != "" && operatorSelected[i] != "" && textSelected[i] != "")
                {
                    rtbSQLResult.Text = rtbSQLResult.Text + fieldOutput[i] + " " +
                    operatorSelected[i] + " " + textSelected[i] + " " + andOrSelected[i] + " ";
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

            if (fieldOutput[i] == "Class") groupName = "group4";
            else if (fieldOutput[i] == "Method") groupName = "group5";
            else if (fieldOutput[i] == "Direction") groupName = "group6";

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

        private void cbLine1Field_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbLine1Value_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
