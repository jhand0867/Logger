using System;
using System.Data;
using System.Windows.Forms;

namespace Logger
{
    public partial class LoadQuery : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal SQLSearchCondition[] gridrows = new SQLSearchCondition[6];

        public LoadQuery()
        {
            InitializeComponent();

            log.Info("LoadQuery");
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            log.Info($"Loading Query: " + cbQueryName.Text);
            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();

            dt = ssc.getSearchCondition(cbQueryName.Text);

            if (dt == null) return;

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    gridrows[i] = new SQLSearchCondition("", "", "", "", "", 0);
                    gridrows[i].SQLFieldName = dt.Rows[i]["fieldName"].ToString();
                    gridrows[i].SQLCondition = dt.Rows[i]["condition"].ToString();
                    gridrows[i].SQLFieldValue = dt.Rows[i]["fieldValue"].ToString();
                    gridrows[i].SQLAndOr = dt.Rows[i]["andOr"].ToString();
                    gridrows[i].SQLFieldOutput = dt.Rows[i]["fieldOutput"].ToString();

                    log.Debug($"SQLSearchCondition Name: {gridrows[i].SQLFieldName + " "}" +
                        $"                         Condition: {gridrows[i].SQLCondition + " "}" +
                        $"                         FieldValue: {gridrows[i].SQLFieldValue + " "}" +
                        $"                         And/Or: {gridrows[i].SQLAndOr + " "}" +
                        $"                         FieldOutput: {gridrows[i].SQLFieldOutput + " "}");
                }
            }
            this.Owner.Text = "AdvancedFilter" + "." + cbQueryName.Text;
            AdvancedFilterw advancedFilter = (AdvancedFilterw)this.Owner;
            advancedFilter.AdvancedFilterLoad(gridrows);
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
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

        private void cbQueryName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
