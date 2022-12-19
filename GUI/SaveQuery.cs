using System;
using System.Data;
using System.Windows.Forms;

namespace Logger
{
    public partial class SaveQuery : Form
    {
        public ToPassData delegateQryName;

        private SQLSearchCondition[] gridrows = new SQLSearchCondition[6];

        public SaveQuery(object sQLSearchCondition, string queryName)
        {
            InitializeComponent();
            gridrows = (SQLSearchCondition[])sQLSearchCondition;

            if (queryName != "")
            {
                tbName.Text = queryName;

                DataTable dt = new DataTable();
                SQLSearchCondition ssc = new SQLSearchCondition();
                dt = ssc.getQueryInfo(tbName.Text);

                if (dt != null &&
                    dt.Rows.Count > 0)
                    tbDescription.Text = dt.Rows[0][2].ToString();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            // getting the content of the query
            // each query line is the combination of 
            // - fieldName
            // - condition
            // - fieldValue
            // - andOr
            // - filterKey

            // check if query already exist

            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();
            

            if (tbName.Text == "" || tbName.Text == null)
            {
                errorProvider1.SetError(tbName, "Please enter Query Name");
                tbName.Focus();
                return;
            }

            dt = ssc.getQueryInfo(tbName.Text);
            int sqlID = 0;
            string filterKey = string.Empty;

            if (dt == null) return;

            if (dt.Rows.Count == 0)
            {
                filterKey = Guid.NewGuid().ToString();
                sqlID = ssc.setSearchConditionBuilder(tbName.Text, tbDescription.Text, filterKey);
            }
            else
            {
                sqlID = (int)dt.Rows[0]["id"];
                filterKey = (string)dt.Rows[0]["filterKey"];
                ssc.updateSearchConditionBuilder(tbName.Text, tbDescription.Text, filterKey);
            }

            if (gridrows[0] == null) return;

            for (int i = 0; i < 6; i++)
            {
                ssc.SQLFieldName = gridrows[i].SQLFieldName;
                ssc.SQLCondition = gridrows[i].SQLCondition;
                ssc.SQLFieldValue = gridrows[i].SQLFieldValue;
                ssc.SQLAndOr = gridrows[i].SQLAndOr;
                ssc.SQLFieldOutput = gridrows[i].SQLFieldOutput;
                ssc.FilterKey = filterKey;

                ssc.setSearchConditionDetail(ssc, sqlID);
            }
            delegateQryName(tbName.Text);
            this.Close();
        }
    }
}
