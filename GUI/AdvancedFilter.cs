using Logger.GUI;
using System;
using System.Data;
using System.Windows.Forms;

namespace Logger
{
    public delegate void ToPassData(string QryNameText);
    public delegate DataGridView ToPassDataGridView();


    public partial class AdvancedFilterw : Form
    {
        public ToPassDataGridView PassDataGridView;

        // options for SQLSearchCondition properties (fields, conditions, andOr)
        public string[] fields = { "", "[group1]", "[group4]", "[group5]", "[group6]", "[group7]", "[group8]" };
        private string[] conditions = { "", "Like", "=", "<>", ">", "<" };
        private string[] andOr = { "", "AND", "OR" };

        private string uiName = "AdvancedFilter";
        internal SQLSearchCondition[] gridrows = new SQLSearchCondition[6];

        public AdvancedFilterw()
        {
            InitializeComponent();


            // intitialize sql fields

            SQLSearchCondition sc = LoggerFactory.Create_SQLSearchCondition();

            for (int x = 0; x < 6; x++)
                gridrows[x] = LoggerFactory.Create_SQLSearchCondition("", "", "", "", "");
        }

        internal void AdvancedFilterLoad(object sQLSearchConditions)
        {
            gridrows = (SQLSearchCondition[])sQLSearchConditions;

            // for each line in the AdvancedFilter (condition)
            // - build the name of the condition's field (Field, Operator, Value, AndOr)
            // - 
            if (gridrows[0] == null) return;

            for (int x = 0; x < 6; x++)
            {
                string fieldName = "cbLine" + (x + 1).ToString("0") + "Field";
                string fieldCondition = "cbLine" + (x + 1).ToString("0") + "Operator";
                string fieldValue = "cbLine" + (x + 1).ToString("0") + "Value";
                string fieldAndOr = "cbLine" + (x + 1).ToString("0") + "AndOr";

                // add the options for FieldName
                // show list with item selected index
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

        /// <summary>
        /// Add the options for condition's FieldName
        /// 
        /// </summary>
        /// <param name="cbField"></param>
        /// cbField is the Options ComboBox. 
        private void addFieldNames(ComboBox cbField)
        {
            // adding options to combo
            cbField.Items.Clear();
            cbField.Items.Add(String.Empty);
            cbField.Items.Add("Timestamp");
            cbField.Items.Add("Class");
            cbField.Items.Add("Method");
            cbField.Items.Add("Type");
            cbField.Items.Add("Log");
            cbField.Items.Add("Data");
        }


        /// <summary>
        /// Fields selection
        /// -- This is the only driver of the FieldName all lines combos --
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineField_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLFieldOutput = cb.SelectedItem.ToString();
            gridrows[i].SQLFieldName = fields[cb.SelectedIndex];
            preview();
        }

        /// <summary>
        /// Condition selection
        /// -- This is the only driver of the Condition all lines combos --
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineCondition_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLCondition = conditions[cb.SelectedIndex];
            preview();
        }

        /// <summary>
        /// Value Selection
        /// -- This is the only driver of the Value all lines combos --
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineValue_Leave(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLFieldValue = cb.Text;
            preview();
        }

        /// <summary>
        /// AndOr Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineAndOr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLAndOr = andOr[cb.SelectedIndex];
            preview();
        }
        /// <summary>
        /// Load values to the Conditions combobox
        /// </summary>
        /// <param name="cbOperators"></param>
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

        /// <summary>
        /// AndOr options loading
        /// </summary>
        /// <param name="cbAndOr"></param>
        private void addAndOr(ComboBox cbAndOr)
        {
            cbAndOr.Items.Clear();
            cbAndOr.Items.Add(String.Empty);
            cbAndOr.Items.Add("And");
            cbAndOr.Items.Add("Or");
        }

        private void cbLineField_Click(object sender, EventArgs e)
        {
            addFieldNames(sender as ComboBox);
        }

        private void cbLineOperator_Click(object sender, EventArgs e)
        {
            addOperators(sender as ComboBox);
        }

        private void cbLineAndOr_Click(object sender, EventArgs e)
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

                if (i < 5 &&
                    gridrows[i].SQLAndOr != "" &&
                    gridrows[i + 1].SQLFieldName != "" && gridrows[i + 1].SQLCondition != "" && gridrows[i + 1].SQLFieldValue != "")

                    sqlLike += gridrows[i].SQLAndOr;
            }

            if (sqlLike != "")
            {
                //object ob = Application.OpenForms["LogView"].Controls[1].Controls[0].Controls["dgvLog"];
                DataGridView dg = PassDataGridView();
                if (dg != null)
                {
                    dg.DataSource = App.Prj.getALogByIDWithCriteria(ProjectData.logID, "", sqlLike);
                    dg.Refresh();
                    
                }
            }
        }

        private void preview()
        {
            rtbSQLResult.Text = " ";

            for (int i = 0; i < 6; i++)
            {
                if (gridrows[i].SQLFieldName != "" && gridrows[i].SQLCondition != "" && gridrows[i].SQLFieldValue != "")

                    rtbSQLResult.Text = rtbSQLResult.Text + gridrows[i].SQLFieldOutput + " " +
                    gridrows[i].SQLCondition + " " + gridrows[i].SQLFieldValue + " ";

                if (i < 5 &&
                    gridrows[i].SQLAndOr != "" &&
                    gridrows[i + 1].SQLFieldName != "" && gridrows[i + 1].SQLCondition != "" && gridrows[i + 1].SQLFieldValue != "")

                    rtbSQLResult.Text = rtbSQLResult.Text + gridrows[i].SQLAndOr + " ";
            }
        }

        private void cbLineValue_MouseClick(object sender, MouseEventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            cb.Items.Clear();
            cb.Items.Add(string.Empty);

            if (gridrows[i].SQLFieldName == "") return;

            System.Data.DataTable dt = new System.Data.DataTable();

            dt = App.Prj.getGroupOptions(ProjectData.logID, gridrows[i].SQLFieldName);

            if (dt == null) return;

            foreach (DataRow theRow in dt.Rows)
            {
                string str = (theRow[0].ToString());
                cb.Items.Add(str);
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string queryName = "";

            if (this.Text != uiName)
            {
                int idx = this.Text.IndexOf(".") + 1;
                queryName = this.Text.Substring(idx);
            }
            SaveQuery saveQuery = LoggerFactory.Create_SaveQuery(gridrows, queryName);
            saveQuery.delegateQryName += new ToPassData(QueryNametoShow);

            saveQuery.ShowDialog();
        }

        private void QueryNametoShow(string QryNameText)
        {
            this.Text = uiName + "." + QryNameText;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadQuery loadQuery = LoggerFactory.Create_LoadQuery();
            loadQuery.Owner = this;
            loadQuery.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbLineValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string iStr = cb.Name.Substring(6, 1);
            int i = Convert.ToInt32(iStr) - 1;

            gridrows[i].SQLFieldValue = cb.Text;
            preview();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteQuery dq = LoggerFactory.Create_DeleteQuery();
            dq.ShowDialog();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text != uiName)
                this.Text = uiName;

            for (int x = 0; x < 6; x++)
                gridrows[x] = LoggerFactory.Create_SQLSearchCondition("", "", "", "", "");

            AdvancedFilterLoad(gridrows);
        }
    }
}
