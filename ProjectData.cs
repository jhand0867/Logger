using System;
using System.Data;
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
            scanToolStripMenuItem.Enabled = false;

            if (buildDataGridView1().Rows.Count != 0)
            {
                dataGridView1.DataSource = buildDataGridView1();
                scanToolStripMenuItem.Enabled = true;
            }
            else
            {
                return;
            }

            fixLogNames(dataGridView1);

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "File Name";
            dataGridView1.Columns[2].Width = 400;
            dataGridView1.Columns[3].HeaderText = "Uploaded on";
            dataGridView1.Columns[3].Width = 250;

            for (int i = 4; i < dataGridView1.Columns.Count; i++)
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
                string logLocation = dgv.Rows[i].Cells[2].Value.ToString();
                dgv.Rows[i].Cells[2].ToolTipText = logLocation;
                int logIndex = logLocation.LastIndexOf(@"\") + 1;
                dgv.Rows[i].Cells[2].Value = logLocation.Substring(logIndex, logLocation.Length - logIndex);
                dgv.Rows[i].Cells[2].Tag = 0;

            }
        }

        public DataTable buildDataGridView1()
        {
            DataTable dtLogs = new DataTable();

            string sql = @"SELECT * FROM logs WHERE prjKey ='" + App.Prj.Key + "'";

            DbCrud db = new DbCrud();
            dtLogs = db.GetTableFromDb(sql);
            return dtLogs;
        }

        private void detachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            string logID = dgvr.Cells[0].Value.ToString();

            App.Prj.detachLogByID(logID);

            dataGridView1.DataSource = buildDataGridView1();

            fixLogNames(dataGridView1);

        }

        private void transactionReplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 4%";

            //string recordType = "01";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(01);
        }

        private void transactionRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "ATM2HOST: 11%";

            //string recordType = "00";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(00);
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects obj = (Projects)Application.OpenForms["Projects"];

            this.Close();
            obj.Projects_Load(null, null);
        }

        private void screensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "11";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(02);


        }

        private void statesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "12";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(03);
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "13";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(04);
        }

        private void fITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "15";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(05);
        }

        private void configurationIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "16";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(06);
        }

        private void enhancedConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "1A";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(07);
        }

        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 3%";

            //string recordType = "1C";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(09);

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
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            for (int x = 1, i = 4; i < dgvr.Cells.Count; i++, x++)
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
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            logID = dgvr.Cells["id"].Value.ToString();

            LogView logView = new LogView();
            logView.Show();

        }

        private void iCCCurrencyDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // string regExStr = "HOST2ATM: 8%";
            // string recordType = "81";

            //string regExStr = App.Prj.RecordTypes[11, 0] + "%";
            //string recordType = App.Prj.RecordTypes[11, 3];

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(11);
        }

        private void iCCTransactionDOTToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 8%";

            //string recordType = "82";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(12);
        }

        private void iCCLanguageSupportTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 8%";

            //string recordType = "83";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(13);

        }

        private void iCCTerminalDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 8%";

            //string recordType = "84";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(14);
        }

        private void iCCTerminalAcceptableAIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string regExStr = "HOST2ATM: 8%";

            //string recordType = "85";

            //DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            //string logID = dgvr.Cells[0].Value.ToString();

            //App.Prj.getData(regExStr, recordType, logID);
            //dataGridView1.DataSource = buildDataGridView1();
            //fixLogNames(dataGridView1);

            optionSelected(15);

        }
        private void optionSelected(int option)
        {

            // option is the entry position in the RecordTypes array

            string regExStr = App.Prj.RecordTypes[option, 0] + "%";
            string recordType = App.Prj.RecordTypes[option, 3];

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            string logID = dgvr.Cells[0].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID);
            dataGridView1.DataSource = buildDataGridView1();
            fixLogNames(dataGridView1);
        }
    }
}
