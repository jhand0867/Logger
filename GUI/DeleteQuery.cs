﻿using System;
using System.Data;
using System.Windows.Forms;

namespace Logger.GUI
{
    public partial class DeleteQuery : Form
    {
        public DeleteQuery()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            // delete all records of the selected query

            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();

            dt = ssc.getQueryInfo(cbQueryName.Text);
            string filterKey = string.Empty;

            if (dt == null) return;

            if (cbQueryName.Text == null || cbQueryName.Text == "")
            {
                MessageBox.Show("Select filter Name");
                return;
            }

            if (dt.Rows.Count != 0)
            {
                filterKey = (string)dt.Rows[0]["filterKey"];
                if (ssc.deleteSearchConditionBuilder(filterKey) == false)
                    MessageBox.Show("Filter could not be deleted");
            }
            this.Close();
        }

        private void cbQueryName_Click(object sender, EventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            cb.Items.Clear();

            SQLSearchCondition ssc = new SQLSearchCondition();
            DataTable dt = ssc.getAllQueries("U");

            if (dt == null) return;

            foreach (DataRow theRow in dt.Rows)
            {
                string str = (theRow["name"].ToString());
                cb.Items.Add(str);
            }
        }
    }
}
