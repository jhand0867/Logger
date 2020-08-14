using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Logger
{
    public partial class ProjectData : Form
    {
        internal static string logID = "";
        public ProjectData()
        {
            InitializeComponent();

            this.FormClosing += ProjectData_FormClosing;

        }

        private void ProjectData_FormClosing(object sender, FormClosingEventArgs e)
        {
            Projects obj = (Projects)Application.OpenForms["Projects"];
            if (obj != null)
                obj.Projects_Load(sender, e);
            else
                Application.Exit();
        }

        private void ProjectData_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = buildDataGridView1();

            fixLogNames(dataGridView1);

            dataGridView1.Columns[0].HeaderText = "File Name";
            dataGridView1.Columns[0].Width = 400;
            dataGridView1.Columns[1].HeaderText = "Uploaded on";
            dataGridView1.Columns[1].Width = 250;

            for (int i = 2; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
            dataGridView1.Refresh();
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Upload Log";
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.AppStarting;
                    App.Prj.uploadLog(ofd.FileName);
                    this.Cursor = Cursors.Default;
                    dataGridView1.DataSource = buildDataGridView1();
                    fixLogNames(dataGridView1);
                }

            }
            catch (AccessViolationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fixLogNames(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {

                string logLocation = dgv.Rows[i].Cells[0].Value.ToString();
                dgv.Rows[i].Cells[0].ToolTipText = logLocation;
                int logIndex = logLocation.LastIndexOf(@"\") + 1;
                dgv.Rows[i].Cells[0].Value = logLocation.Substring(logIndex, logLocation.Length - logIndex);
                dgv.Rows[i].Cells[0].Tag = 0;


            }
        }

        //public DataTable buildDataGridView1()
        //{
        //    // here mlh

        //    DataTable dtLogs = new DataTable();

        //    string cnnString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;

        //    string sql = @"SELECT logFile, uploadDate, id, screens, states, configParametersLoad
        //                ,fit,configID,enhancedParametersLoad,mac,dateandtime,dispenserCurrency,treq,treply,iccCurrencyDOT, 
        //                iccTransactionDOT, iccLanguageSupportT, iccTerminalDOT, iccApplicationIDT FROM logs WHERE prjKey ='" + App.Prj.Key + "'";

        //    using (SqlConnection conn = new SqlConnection(cnnString))
        //    {
        //        using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
        //        {
        //            conn.Open();

        //            SqlDataReader reader = sqlCmd.ExecuteReader();

        //            dtLogs.Load(reader);
        //        }
        //    }
        //    return dtLogs;
        //}

        public DataTable buildDataGridView1()
        {
            // here mlh

            DataTable dtLogs = new DataTable();

            string sql = @"SELECT logFile, uploadDate, id, screens, states, configParametersLoad
                        ,fit,configID,enhancedParametersLoad,mac,dateandtime,dispenserCurrency,treq,treply,iccCurrencyDOT, 
                        iccTransactionDOT, iccLanguageSupportT, iccTerminalDOT, iccApplicationIDT FROM logs WHERE prjKey ='" + App.Prj.Key + "'";

            DbCrud db = new DbCrud();
            dtLogs = db.GetTableFromDb(sql);
            return dtLogs;
        }

        private void detachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

                String sql = "";

                sql = @"DELETE from loginfo WHERE logID = " + logID +
                       " ;DELETE from logs WHERE ID = " + logID +
                       " ;UPDATE Project SET prjLogs = prjLogs - 1 " +
                       "WHERE prjKey ='" + App.Prj.Key + "'";


                DbCrud db = new DbCrud();
                if (db.addToDb(sql) == false) { };

                dataGridView1.DataSource = buildDataGridView1();
                fixLogNames(dataGridView1);

        }

        private void transactionReplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 4%";

            string recordType = "01";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
        }

        private void transactionRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "ATM2HOST: 11%";

            string recordType = "00";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects obj = (Projects)Application.OpenForms["Projects"];

            this.Close();
            obj.Projects_Load(null, null);
        }

        private void screensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "11";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);


        }

        private void statesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "12";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "13";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void fITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "15";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void configurationIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "16";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void enhancedConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "1A";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "1C";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show(e.RowIndex.ToString());
        }

        private void scanToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            //DataTable dt = buildDataGridView1();
            //dataGridView1.DataSource = dt;


            if (dataGridView1.Rows.Count == 0)
                return;
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            for (int x = 1, i = 3; i < dgvr.Cells.Count; i++, x++)
            {
                if (dgvr.Cells[i].Value.ToString() == "True" || dgvr.Cells[i].Value.ToString() == "true")
                {
                    scanToolStripMenuItem.DropDownItems[x].Enabled = false;
                }
                else
                {
                    scanToolStripMenuItem.DropDownItems[x].Enabled = true;
                }
            }

        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.RowIndex.ToString());
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            logID = dgvr.Cells["id"].Value.ToString();

            LogView logView = new LogView();
            //logView.MdiParent = "";
            logView.Show();



        }

        private void iCCCurrencyDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 8%";

            string recordType = "81";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }


        private void iCCTransactionDOTToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 8%";

            string recordType = "82";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }

        private void iCCLanguageSupportTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 8%";

            string recordType = "83";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);

        }

        private void iCCTerminalDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 8%";

            string recordType = "84";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);

        }

        private void iCCTerminalAcceptableAIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 8%";

            string recordType = "85";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);

        }
    }
}
