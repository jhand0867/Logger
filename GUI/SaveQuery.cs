using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    public partial class SaveQuery : Form
    {
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
                tbDescription.Text = dt.Rows[0][2].ToString();
             }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

            // check if query already exist

            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();

            dt = ssc.getQueryInfo(tbName.Text);
            int sqlID = 0;

            if (dt.Rows.Count == 0)
                sqlID = ssc.setSearchConditionBuilder(tbName.Text, tbDescription.Text);
            else
            {
                sqlID = (int)dt.Rows[0]["id"];
                ssc.updateSearchConditionBuilder(tbName.Text, tbDescription.Text, sqlID);
            }

                for (int i = 0; i < 6; i++)
                {
                    ssc.SQLFieldName = gridrows[i].SQLFieldName;
                    ssc.SQLCondition = gridrows[i].SQLCondition;
                    ssc.SQLFieldValue = gridrows[i].SQLFieldValue;
                    ssc.SQLAndOr = gridrows[i].SQLAndOr;
                    ssc.SQLFieldOutput = gridrows[i].SQLFieldOutput;

                    ssc.setSearchConditionDetail(ssc, sqlID);
                }
            Application.OpenForms["AdvancedFilter"].Text = "AdvancedFilter." + tbName.Text;
            this.Close();
        }
    }
}
