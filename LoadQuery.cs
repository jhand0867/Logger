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
    public partial class LoadQuery : Form
    {
        internal SQLSearchCondition[] gridrows = new SQLSearchCondition[6];

        public LoadQuery()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();
            Control.ControlCollection cc = Application.OpenForms["AdvancedFilter"].Controls;

            dt = ssc.getSearchCondition(tbName.Text);

            if (dt.Rows.Count != 0)
            {

                for (int i = 0; i < 6; i++)
                {
                    gridrows[i] = new SQLSearchCondition("", "", "", "", "");
                    gridrows[i].SQLFieldName = dt.Rows[i][2].ToString(); 
                    gridrows[i].SQLCondition = dt.Rows[i][3].ToString();
                    gridrows[i].SQLFieldValue = dt.Rows[i][4].ToString();
                    gridrows[i].SQLAndOr = dt.Rows[i][5].ToString();
                    gridrows[i].SQLFieldOutput = dt.Rows[i][6].ToString();
                }
            }

            Application.OpenForms["AdvancedFilter"].Close();
            AdvancedFilter advanceFilter = new AdvancedFilter(gridrows);
            advanceFilter.Text = "AdvancedFilter" + "." + tbName.Text; 
            advanceFilter.ShowDialog();
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
