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

        public SaveQuery(object sQLSearchCondition)
        {
            InitializeComponent();
            gridrows = (SQLSearchCondition[])sQLSearchCondition;
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
            for (int i = 0; i < 6; i++)
            {
                if (gridrows[i].FieldName != "" && gridrows[i].Condition != "" && gridrows[i].FieldValue != "")
                {

                }

            }
        }
    }
}
