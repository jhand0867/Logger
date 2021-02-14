using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger.GUI;

namespace Logger.GUI
{
    public partial class DeleteQuery : Form
    {
        public DeleteQuery()
        {
            InitializeComponent();
        }

        private void cbQueryName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            // delete all records of the selected query

            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();

            dt = ssc.getQueryInfo(cbQueryName.Text);
            int sqlID = 0;

            if (dt.Rows.Count == 0)
                MessageBox.Show("Filter not found");
            else
            {
                sqlID = (int)dt.Rows[0]["id"];
                if (ssc.deleteSearchConditionBuilder(sqlID) == false)
                    MessageBox.Show("Filter could not be deleted");
            }
            this.Close();
        }

        private void cbQueryName_Click(object sender, EventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            cb.Items.Clear();

            SQLSearchCondition ssc = new SQLSearchCondition();
            DataTable dt = ssc.getAllQueries();

            foreach (DataRow theRow in dt.Rows)
            {
                string str = (theRow[1].ToString());
                cb.Items.Add(str);
            }
        }
    }
}
