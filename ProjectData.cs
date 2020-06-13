using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Logger
{
    public partial class ProjectData : Form
    {
        public ProjectData()
        {
            InitializeComponent();

            this.FormClosing += ProjectData_FormClosing;

        }

        private void ProjectData_FormClosing(object sender, FormClosingEventArgs e)
        {
            Projects obj = (Projects)Application.OpenForms["Projects"];

            obj.Projects_Load(null, null);
        }

        private void ProjectData_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = buildDataGridView1();

            //dataGridView1.Rows[0].Cells[0].Value = "New Value";

            fixLogNames(dataGridView1);

            dataGridView1.Columns[0].HeaderText = "File Name";
            dataGridView1.Columns[0].Width = 600;
            dataGridView1.Columns[1].HeaderText = "Uploaded on";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;

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

        public DataTable buildDataGridView1()
        {
            DataTable dtLogs = new DataTable();


            string cnnString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;

            string sql = @"SELECT logFile, uploadDate, id, screens, states, configParametersLoad
                        ,fit,configID,enhancedParametersLoad,mac,dateandtime,dispenserCurrency,treq,treply 
                        FROM logs WHERE prjKey ='" + App.Prj.Key + "'";

            using (SqlConnection conn = new SqlConnection(cnnString))
            {
                using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    dtLogs.Load(reader);
                }
            }
            return dtLogs;
        }
        private void detachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();


            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                String sql = "";

                sql = @"DELETE from loginfo WHERE logID = " + logID +
                       " ;DELETE from logs WHERE ID = " + logID +
                       " ;UPDATE Project SET prjLogs = prjLogs - 1 " +
                       "WHERE prjKey ='" + App.Prj.Key + "'";

                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();

                dataGridView1.DataSource = buildDataGridView1();
                fixLogNames(dataGridView1);


            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());

            }
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

        }

        private void statesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "12";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);

        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "13";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
        }

        private void fITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "15";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);


        }

        private void configurationIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "16";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
        }

        private void enhancedConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "1A";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
        }

        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regExStr = "HOST2ATM: 3%";

            string recordType = "1C";

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[2].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show(e.RowIndex.ToString());
        }

        private void scanToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            for (int x = 1, i = 3; i < 14; i++, x++)
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
    }
}
